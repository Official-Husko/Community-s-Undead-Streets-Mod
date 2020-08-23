using CWDM.Extensions;
using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Threading.Tasks;

namespace CWDM.Wrappers
{
    public class ZombiePed
    {
        public Ped pedEntity;
        public Ped target;
        public bool isRunner = false;
        public bool newSearch = true;
        public bool movingToTarget = false;

        public ZombiePed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            this.pedEntity = pedEntity;
        }

        public void Update()
        {
            if (pedEntity == null)
            {
                return;
            }
            if (pedEntity.IsOnFire)
            {
                pedEntity.Kill();
            }
            if (!pedEntity.IsAlive || pedEntity.DistanceBetween(Game.Player.Character) > 100)
            {
                Blip blip = pedEntity.CurrentBlip;
                if (blip.Handle != 0)
                {
                    blip.Remove();
                }
                return;
            }
            if (Population.FastZombies && pedEntity.IsRunning)
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
                pedEntity.SetMovementAnim("move_m@injured");
            }
            else
            {
                pedEntity.SetMovementAnim("move_m@drunk@verydrunk");
            }
            if (target == null)
            {
                movingToTarget = false;
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
                    movingToTarget = false;
                    target = FindTarget();
                    if (target == null && newSearch)
                    {
                        pedEntity.Task.WanderAround();
                        newSearch = false;
                        return;
                    }
                }
                if (pedEntity.DistanceBetween(target) <= 1.2 && !target.IsInVehicle())
                {
                    movingToTarget = false;
                    Vector3 val = target.Position - pedEntity.Position;
                    pedEntity.Heading = val.ToHeading();
                    Task.Delay(100);
                    if (target.IsDead)
                    {
                        if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, pedEntity, "amb@world_human_bum_wash@male@high@idle_a", "idle_b", 3))
                        {
                            pedEntity.Task.PlayAnimation("amb@world_human_bum_wash@male@high@idle_a", "idle_b", 8f, -1, AnimationFlags.Loop);
                            Task.Delay(5000);
                        }
                    }
                    else if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, pedEntity, "rcmbarry", "bar_1_teleport_aln", 3))
                    {
                        pedEntity.Task.PlayAnimation("rcmbarry", "bar_1_teleport_aln", 8f, 1000, (AnimationFlags)16);
                        Task.Delay(1000);
                        target.ApplyDamage(Population.ZombieDamage);
                        if (target == Game.Player.Character)
                        {
                            // Placeholder for Zombie Infection mode code
                        }
                        else
                        {
                            Random random = new Random();
                            int rnd = random.Next(0, 2);
                            Blip targetBlip = target.CurrentBlip;
                            if (targetBlip.Handle != 0)
                            {
                                targetBlip.Remove();
                            }
                            if (rnd == 0)
                            {
                                Function.Call(Hash.SET_PED_TO_RAGDOLL, target.Handle, 3000, 0, 0, false, false, false);
                                target.Weapons.Drop();
                                Task.Delay(7000);
                                InfectTarget(target);
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
                            }
                        }
                    }
                    if (target != null)
                    {
                        if (isRunner)
                        {
                            pedEntity.SetMovementAnim("move_m@injured");
                            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, pedEntity.Handle, target.Handle, 1f, 1f, 0f, 5f, -1, 1f, true);
                            movingToTarget = true;
                        }
                        else
                        {
                            pedEntity.SetMovementAnim("move_m@drunk@verydrunk");
                            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, pedEntity.Handle, target.Handle, 1f, 1f, 0f, 1f, -1, 1f, true);
                            movingToTarget = true;
                        }
                    }
                }
                else
                {
                    if (!movingToTarget)
                    {
                        if (isRunner)
                        {
                            pedEntity.SetMovementAnim("move_m@injured");
                            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, pedEntity.Handle, target.Handle, 1f, 1f, 0f, 5f, -1, 1f, true);
                            movingToTarget = true;
                        }
                        else
                        {
                            pedEntity.SetMovementAnim("move_m@drunk@verydrunk");
                            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, pedEntity.Handle, target.Handle, 1f, 1f, 0f, 1f, -1, 1f, true);
                            movingToTarget = true;
                        }
                    }
                }
            }
        }

        public static void InfectTarget(Ped ped)
        {
            Population.Infect(ped);
            Population.ZombiePeds.Add(new ZombiePed(ped));
            if (Population.SurvivorPeds.Exists(match: a => a.pedEntity == ped))
            {
                Population.SurvivorPeds.RemoveAt(Population.SurvivorPeds.FindIndex(match: a => a.pedEntity == ped));
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