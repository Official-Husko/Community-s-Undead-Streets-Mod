using System;
using System.Threading.Tasks;
using CWDM.Extensions;
using GTA;
using GTA.Native;

namespace CWDM.EntityWrappers
{
    public class ZombiePed
    {
        private bool _movingToTarget;
        private bool _newSearch = true;
        private Ped _target;
        public bool IsRunner = false;
        public Ped PedEntity;

        public ZombiePed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            PedEntity = pedEntity;
        }

        public void Update()
        {
            PedEntity.RelationshipGroup = Relationships.ZombieGroup;
            if (PedEntity == null) return;
            if (PedEntity.IsOnFire) PedEntity.Kill();
            if (!PedEntity.IsAlive || PedEntity.DistanceBetween(Game.Player.Character) > 100)
            {
                var blip = PedEntity.CurrentBlip;
                if (blip.Handle != 0) blip.Remove();
                return;
            }

            if (Population.FastZombies && PedEntity.IsRunning)
            {
                Function.Call(Hash.STOP_PED_SPEAKING, PedEntity.Handle, 0);
                Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, PedEntity.Handle, false);
                Function.Call(Hash.PLAY_PAIN, PedEntity.Handle, 8, 0, 0);
                Function.Call(Hash.PLAY_FACIAL_ANIM, PedEntity.Handle, "burning_1", "facials@gen_male@base");
            }
            else
            {
                Function.Call(Hash.STOP_PED_SPEAKING, PedEntity.Handle, 1);
                Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, PedEntity.Handle, true);
            }

            if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, PedEntity.Handle))
                Function.Call(Hash.STOP_CURRENT_PLAYING_AMBIENT_SPEECH, PedEntity.Handle);
            if (IsRunner)
                PedEntity.SetMovementAnim("move_m@injured");
            else
                PedEntity.SetMovementAnim("move_m@drunk@verydrunk");
            if (_target == null)
            {
                _movingToTarget = false;
                _target = FindTarget();
                Task.Delay(100);
                if (_target == null && _newSearch)
                {
                    PedEntity.Task.WanderAround();
                    _newSearch = false;
                }
            }
            else
            {
                if (!_target.IsAlive || PedEntity.DistanceBetween(_target) >= 80 ||
                    _target.RelationshipGroup == Relationships.ZombieGroup)
                {
                    _target = null;
                    _newSearch = true;
                    _movingToTarget = false;
                    _target = FindTarget();
                    Task.Delay(100);
                    if (_target == null && _newSearch)
                    {
                        PedEntity.Task.WanderAround();
                        _newSearch = false;
                        return;
                    }
                }

                if (_target != null && PedEntity.DistanceBetween(_target) <= 1.2 && !_target.IsInVehicle())
                {
                    _movingToTarget = false;
                    var val = _target.Position - PedEntity.Position;
                    PedEntity.Heading = val.ToHeading();
                    Task.Delay(100);
                    if (_target.IsDead)
                    {
                        if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, PedEntity,
                            "amb@world_human_bum_wash@male@high@idle_a", "idle_b", 3))
                        {
                            PedEntity.Task.PlayAnimation("amb@world_human_bum_wash@male@high@idle_a", "idle_b", 8f, -1,
                                AnimationFlags.Loop);
                            Task.Delay(5000);
                        }
                    }
                    else if (!Function.Call<bool>(Hash.IS_ENTITY_PLAYING_ANIM, PedEntity, "rcmbarry",
                        "bar_1_teleport_aln", 3))
                    {
                        PedEntity.Task.PlayAnimation("rcmbarry", "bar_1_teleport_aln", 8f, 1000, (AnimationFlags) 16);
                        Task.Delay(1000);
                        _target.ApplyDamage(Population.ZombieDamage);
                        if (_target == Game.Player.Character)
                        {
                            // Placeholder for Zombie Infection mode code
                        }
                        else
                        {
                            var random = new Random();
                            var rnd = random.Next(0, 2);
                            var targetBlip = _target.CurrentBlip;
                            if (targetBlip.Handle != 0) targetBlip.Remove();
                            if (rnd == 0)
                            {
                                Function.Call(Hash.SET_PED_TO_RAGDOLL, _target.Handle, 3000, 0, 0, false, false, false);
                                _target.Weapons.Drop();
                                Task.Delay(7000);
                                InfectTarget(_target);
                                _target.LeaveGroup();
                                _target.Weapons.Drop();
                                _target = null;
                                _newSearch = true;
                                _target = FindTarget();
                                if (_target == null && _newSearch)
                                {
                                    PedEntity.Task.WanderAround();
                                    _newSearch = false;
                                    return;
                                }
                            }
                            else
                            {
                                _target.Weapons.Drop();
                                _target.Kill();
                            }
                        }
                    }

                    if (_target != null)
                    {
                        if (IsRunner)
                        {
                            PedEntity.SetMovementAnim("move_m@injured");
                            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, PedEntity.Handle, _target.Handle, 1f,
                                1f,
                                0f, 5f, -1, 1f, true);
                            _movingToTarget = true;
                        }
                        else
                        {
                            PedEntity.SetMovementAnim("move_m@drunk@verydrunk");
                            Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, PedEntity.Handle, _target.Handle, 1f,
                                1f,
                                0f, 1f, -1, 1f, true);
                            _movingToTarget = true;
                        }
                    }
                }
                else
                {
                    if (!_movingToTarget)
                    {
                        if (IsRunner)
                        {
                            PedEntity.SetMovementAnim("move_m@injured");
                            if (_target != null)
                            {
                                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, PedEntity.Handle, _target.Handle,
                                    1f, 1f,
                                    0f, 5f, -1, 1f, true);
                            }

                            _movingToTarget = true;
                        }
                        else
                        {
                            PedEntity.SetMovementAnim("move_m@drunk@verydrunk");
                            if (_target != null)
                            {
                                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, PedEntity.Handle, _target.Handle,
                                    1f, 1f,
                                    0f, 1f, -1, 1f, true);
                            }

                            _movingToTarget = true;
                        }
                    }
                }
            }
        }

        public static void InfectTarget(Ped ped)
        {
            Population.Infect(ped);
            Population.ZombiePeds.Add(new ZombiePed(ped));
            if (Population.SurvivorPeds.Exists(a => a.PedEntity == ped))
                Population.SurvivorPeds.RemoveAt(Population.SurvivorPeds.FindIndex(a => a.PedEntity == ped));
        }

        private static bool CanHearPed(Ped hearer, Ped target)
        {
            var distance = target.Position.DistanceTo(hearer.Position);
            return !IsWeaponWellSilenced(target, distance) || IsBehindZombie(distance) ||
                   IsRunningNoticed(target, distance);
        }

        private static bool IsWeaponWellSilenced(Ped ped, float distance)
        {
            if (ped.IsShooting) return ped.IsCurrentWeaponSileced() && distance > 15f;
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
            var targets = World.GetNearbyPeds(PedEntity.Position, 50f);
            foreach (var ped in targets)
            {
                if (ped != null && ped.RelationshipGroup != Relationships.ZombieGroup && ped.IsAlive && ped.IsHuman &&
                    (PedEntity.HasClearLineOfSight(ped, 35f) || CanHearPed(PedEntity, ped)))
                {
                    return ped;
                }
            }

            return null;
        }
    }
}