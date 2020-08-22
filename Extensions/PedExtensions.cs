using GTA;
using GTA.Native;
using System.Linq;
using CWDM.Enums;

namespace CWDM.Extensions
{
    public static class PedExtensions
    {
        public static bool IsCurrentWeaponSileced(this Ped ped)
        {
            return Function.Call<bool>(Hash.IS_PED_CURRENT_WEAPON_SILENCED, ped.Handle);
        }

        public static void SetMovementAnim(this Ped ped, string anim)
        {
            if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, anim))
            {
                Function.Call(Hash.REQUEST_ANIM_SET, anim);
            }
            Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, ped.Handle, anim, 1048576000);
        }

        public static void SetSeeingRange(this Ped ped, float value)
        {
            Function.Call(Hash.SET_PED_SEEING_RANGE, ped.Handle, value);
        }

        public static void SetHearingRange(this Ped ped, float value)
        {
            Function.Call(Hash.SET_PED_HEARING_RANGE, ped.Handle, value);
        }

        public static void Recruit(this Ped ped, Ped leader)
        {
            if (!(leader == null))
            {
                int groupLimit = Character.MaxPlayerGroupSize;
                if (leader == Game.Player.Character && leader.CurrentPedGroup.Count() < groupLimit)
                {
                    PedGroup group = leader.CurrentPedGroup;
                    ped.LeaveGroup();
                    Function.Call(Hash.SET_PED_RAGDOLL_ON_COLLISION, ped.Handle, false);
                    ped.Task.ClearAll();
                    group.SeparationRange = 2.14748365E+09f;
                    if (!group.Contains(leader))
                    {
                        group.Add(leader, true);
                    }
                    if (!group.Contains(ped))
                    {
                        group.Add(ped, false);
                    }
                    ped.IsPersistent = true;
                    ped.RelationshipGroup = leader.RelationshipGroup;
                    ped.NeverLeavesGroup = true;
                    Blip currentBlip = ped.CurrentBlip;
                    if (currentBlip.Type != 0)
                    {
                        currentBlip.Remove();
                    }
                    Blip blip = ped.AddBlip();
                    blip.Color = BlipColor.Green;
                    blip.Scale = 0.65f;
                    blip.Name = "Group";
                    ped.SetTask(PedTask.Follow);
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("~p~Group ~s~member recruited!");
                }
                else
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify($"You can only have up to {groupLimit} ~p~Group ~s~members!");
                }
            }
        }

        public static void SetTask(this Ped ped, PedTask task)
        {
            if (Population.SurvivorPeds.Exists(match: a => a.pedEntity == ped))
            {
                ped.Task.ClearAll();
                if (task == PedTask.Guard)
                {
                    ped.Task.GuardCurrentPosition();
                }
                else if (task == PedTask.Wander)
                {
                    ped.Task.WanderAround(ped.Position, 100f);
                }
                else if (task == PedTask.None)
                {
                    ped.Task.StandStill(-1);
                }
                else if (task == PedTask.Leave)
                {
                    ped.LeaveGroup();
                    Blip currentBlip = ped.CurrentBlip;
                    if (currentBlip.Handle != 0)
                    {
                        currentBlip.Remove();
                    }
                    ped.RelationshipGroup = Relationships.FriendlyGroup;
                    ped.Task.ClearAll();
                    Blip blip = ped.AddBlip();
                    blip.Color = BlipColor.Blue;
                    blip.Scale = 0.65f;
                    blip.Name = "Friendly";
                    task = PedTask.Wander;
                    ped.Task.WanderAround(ped.Position, 100f);
                }
                Population.SurvivorPeds.Find(match: a => a.pedEntity == ped).task = task;
            }
        }

        public static void GiveWeaponHashName(this Ped ped, string weapon, int ammo, bool equipNow, bool isAmmoLoaded)
        {
            WeaponHash weaponHash = (WeaponHash)Game.GenerateHash(weapon);
            ped.Weapons.Give(weaponHash, ammo, equipNow, isAmmoLoaded);
        }

        public static void GiveWeaponHashName(this Ped ped, string weapon, bool equipNow, bool isAmmoLoaded)
        {
            WeaponHash weaponHash = (WeaponHash)Game.GenerateHash(weapon);
            int ammo = Function.Call<int>(Hash.GET_WEAPON_CLIP_SIZE, weaponHash.GetHashCode());
            if (ammo < 0)
            {
                ammo = 0;
            }
            ped.Weapons.Give(weaponHash, ammo, equipNow, isAmmoLoaded);
        }
    }
}
