using System.Threading.Tasks;
using GTA;
using GTA.Native;
using GTA.Math;

namespace CWDM
{
    public class ZombiePed : UpdaterClass
    {
        public Ped pedEntity;
        public Ped target;
        public bool isRunner;
        public bool newSearch = true;

        public ZombiePed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            this.pedEntity = pedEntity;
        }

        public override void Update()
        {
            if (pedEntity.IsOnFire)
            {
                pedEntity.Kill();
            }
            if (!pedEntity.IsAlive || pedEntity.DistanceBetween(Game.Player.Character) > 100)
            {
                if (pedEntity.CurrentBlip.Exists())
                {
                    pedEntity.CurrentBlip.Remove();
                }
            }
            if (Population.zombieRunners && pedEntity.IsRunning)
            {
                Function.Call(Hash.STOP_PED_SPEAKING, pedEntity.Handle, 0);
                Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, pedEntity.Handle, false);
                Function.Call(Hash.PLAY_PAIN, pedEntity.Handle, 8, 0, 0);
                Function.Call(Hash.PLAY_FACIAL_ANIM, pedEntity.Handle, "burning_1", "facials@gen_male@base");
            }
            else
            {
                Function.Call(Hash.STOP_PED_SPEAKING, pedEntity.Handle, 1);
                Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, pedEntity.Handle, true);
            }
            if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, pedEntity.Handle))
            {
                Function.Call(Hash.STOP_CURRENT_PLAYING_AMBIENT_SPEECH, pedEntity.Handle);
            }
            if (isRunner)
            {
                if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@injured"))
                {
                    Function.Call(Hash.REQUEST_ANIM_SET, "move_m@injured");
                }
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, pedEntity.Handle, "move_m@injured", 0x3E800000);
            }
            else
            {
                if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@drunk@verydrunk"))
                {
                    Function.Call(Hash.REQUEST_ANIM_SET, "move_m@drunk@verydrunk");
                }
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, pedEntity.Handle, "move_m@drunk@verydrunk", 0x3E800000);
            }
            if (target == null)
            {
                target = FindTarget();
                if (target == null && newSearch)
                {
                    pedEntity.Task.WanderAround();
                    newSearch = false;
                    return;
                }
            }
            else
            {
                if (!target.IsAlive || pedEntity.DistanceBetween(target) >= 80 || target.RelationshipGroup == Relationships.ZombieGroup)
                {
                    target = null;
                    newSearch = true;
                    target = FindTarget();
                    if (target == null && newSearch)
                    {
                        pedEntity.Task.WanderAround();
                        newSearch = false;
                        return;
                    }
                }
                if (target != null && (pedEntity.DistanceBetween(target) <= 1.2) && !target.IsInVehicle())
                {
                    if (target.IsDead)
                    {
                        if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, pedEntity, "amb@world_human_bum_wash@male@high@idle_a", "idle_b", 3))
                        {
                            pedEntity.Task.PlayAnimation("amb@world_human_bum_wash@male@high@idle_a", "idle_b", 8f, -1, GTA.AnimationFlags.Loop);
                            Task.Delay(8);
                        }
                    }
                    if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, pedEntity, "rcmbarry", "bar_1_teleport_aln", 3))
                    {
                        Vector3 rotationTarget = pedEntity.Rotation - Game.Player.Character.Rotation;
                        pedEntity.Rotation = rotationTarget;
                        pedEntity.Task.PlayAnimation("rcmbarry", "bar_1_teleport_aln", 8f, 1000, (AnimationFlags)16);
                        Task.Delay(8);
                        if (target == Game.Player.Character)
                        {
                            target.ApplyDamage(50);
                            target.Kill();
                        }
                        else
                        {
                            target.ApplyDamage(50);
                            int rnd = RandoMath.CachedRandom.Next(0, 2);
                            if (rnd == 0)
                            {
                                Function.Call(Hash.SET_PED_TO_RAGDOLL, target.Handle, 3000, 0, 0, false, false, false);
                                target.Weapons.Drop();
                                if (target.CurrentBlip.Exists())
                                {
                                    target.CurrentBlip.Remove();
                                }
                                Population.Infect(target);
                                target.LeaveGroup();
                                target.Weapons.Drop();
                                target = null;
                                newSearch = true;
                                target = FindTarget();
                                if (target == null && newSearch)
                                {
                                    pedEntity.Task.WanderAround();
                                    newSearch = false;
                                    return;
                                }
                            }
                            else
                            {
                                target.Weapons.Drop();
                                target.Kill();
                                if (target.CurrentBlip.Exists())
                                {
                                    target.CurrentBlip.Remove();
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (isRunner)
                    {
                        if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@injured"))
                        {
                            Function.Call(Hash.REQUEST_ANIM_SET, "move_m@injured");
                        }
                        Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, pedEntity.Handle, "move_m@injured", 0x3E800000);
                        pedEntity.Task.Climb();
                        Function.Call(Hash.TASK_GO_TO_ENTITY, pedEntity.Handle, target.Handle, -1, 0f, 5f, 1073741824, 0);
                    }
                    else
                    {
                        pedEntity.Task.Climb();
                        pedEntity.Task.GoTo(target.Position);
                    }
                }
            }
        }

        private static bool CanHearPed(Ped hearer, Ped target)
        {
            float distance = target.Position.DistanceTo(hearer.Position);
            return !IsWeaponWellSilenced(target, distance) || IsBehindZombie(distance) || IsRunningNoticed(target, distance);
        }

        private static bool IsWeaponWellSilenced(Ped ped, float distance)
        {
            if (ped.IsShooting)
            {
                return ped.IsCurrentWeaponSileced() && distance > 15f;
            }
            return true;
        }

        private static bool IsBehindZombie(float distance)
        {
            return distance < 1f;
        }

        private static bool IsRunningNoticed(Ped ped, float distance)
        {
            return ped.IsSprinting && distance < 25f;
        }

        private Ped FindTarget()
        {
            Ped[] targets = World.GetNearbyPeds(pedEntity.Position, 50f);
            foreach (Ped ped in targets)
            {
                if (ped != null && ped.RelationshipGroup != Relationships.ZombieGroup && ped.IsAlive && ped.IsHuman && (pedEntity.HasClearLineOfSight(ped, 35f) || CanHearPed(pedEntity, ped)))
                {
                    return ped;
                }
            }
            return null;
        }
    }
}
