using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using CWDM.Extensions;

namespace CWDM
{
    public static class Character
    {
        public static bool CharacterReset = false;
        public static List<Vehicle> PlayerVehicles = new List<Vehicle>();
        public static int MaxPlayerVehicles = 10;
        public static int MaxPlayerGroupSize = 30;
        public static Vector3 StartingLocation = new Vector3(-125.38f, -1288.61f, 47.89f);
        public static float StartingHeading = 40.0f;
        public static Prop CampFire;
        public static Prop Tent;
        public static Gender PlayerGender;
        public static Model PlayerModel;
        public static float CurrentHungerLevel = 1.0f;
        public static float MaxHungerLevel = 1.0f;
        public static float HungerDecrease = 0.00041f;
        public static float CurrentThirstLevel = 1.0f;
        public static float MaxThirstLevel = 1.0f;
        public static float ThirstDecrease = 0.00081f;
        public static float CurrentEnergyLevel = 1.0f;
        public static float MaxEnergyLevel = 1.0f;
        public static float EnergyDecrease = 0.00021f;
        public static float CurrentInfectionLevel = 1.0f;
        public static float MaxInfectionLevel = 1.0f;
        public static float InfectionDecrease = 0f;
        public static float CurrentBatteryLevel = 1.0f;
        public static float MaxBatteryLevel = 1.0f;
        public static float BatteryDecrease = 0f;
        public static bool CustomWeaponLoadout = false;
        public static List<string> WeaponLoadout = new List<string>
        {
            "weapon_pistol",
            "weapon_machinepistol",
            "weapon_wrench"
        };

        public static void SetCharacterModel()
        {
            PedHash[] characterMaleModels = { PedHash.FreemodeMale01 };
            PedHash[] characterFemaleModels = { PedHash.FreemodeFemale01 };
            PedHash[] characterModels;
            if (PlayerGender == Gender.Male)
            {
                characterModels = characterMaleModels;
            }
            else
            {
                characterModels = characterFemaleModels;
            }
            PlayerModel = new Model(characterModels.GetRandomElementFromArray());
            PlayerModel.Request(500);
            if (PlayerModel.IsInCdImage && PlayerModel.IsValid)
            {
                while (!PlayerModel.IsLoaded)
                {
                    Script.Wait(100);
                }

                Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, PlayerModel.Hash);
                if (PlayerGender == Gender.Male)
                {
                    Function.Call(Hash.SET_PED_HEAD_BLEND_DATA, Game.Player.Character.Handle, 44, 44, 0, 44, 44, 0, 1.0f, 1.0f, 0.0f, true);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 3, 12, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 4, 1, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 8, 0, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 11, 233, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 6, 24, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 2, 12, 0, 0);
                    Function.Call(Hash._SET_PED_HAIR_COLOR, Game.Player.Character.Handle, 57, 58);
                    Function.Call(Hash._SET_PED_EYE_COLOR, Game.Player.Character.Handle, 3);
                    Function.Call(Hash.SET_PED_HEAD_OVERLAY, Game.Player.Character.Handle, 1, 0, 1.0f);
                    Function.Call(Hash.SET_PED_HEAD_OVERLAY, Game.Player.Character.Handle, 2, 1, 1.0f);
                    Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, Game.Player.Character.Handle, 1, 1, 57, 58);
                    Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, Game.Player.Character.Handle, 2, 1, 57, 58);
                }
                else
                {
                    Function.Call(Hash.SET_PED_HEAD_BLEND_DATA, Game.Player.Character.Handle, 33, 33, 0, 33, 33, 0, 1.0f, 1.0f, 0.0f, true);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 2, 4, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 11, 243, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 3, 7, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 8, 0, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 6, 24, 0, 0);
                    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Game.Player.Character.Handle, 4, 84, 0, 0);
                    Function.Call(Hash._SET_PED_HAIR_COLOR, Game.Player.Character.Handle, 11, 11);
                    Function.Call(Hash._SET_PED_EYE_COLOR, Game.Player.Character.Handle, 3);
                    Function.Call(Hash.SET_PED_HEAD_OVERLAY, Game.Player.Character.Handle, 2, 1, 1.0f);
                    Function.Call(Hash._SET_PED_HEAD_OVERLAY_COLOR, Game.Player.Character.Handle, 2, 1, 11, 11);
                }
            }
            PlayerModel.MarkAsNoLongerNeeded();
        }

        public static void Setup()
        {
            ResetCharacter();
            Model model = new Model("prop_beach_fire");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid)
            {
                while (!model.IsLoaded)
                {
                    Script.Wait(50);
                }

                Vector3 CFpos = new Vector3(-133.85f, -1288.70f, 46.4f);
                CampFire = World.CreateProp(model, CFpos, true, false);
                CampFire.Heading = 269.99f;
            }
            model.MarkAsNoLongerNeeded();
            CampFire.AddBlip();
            CampFire.CurrentBlip.Sprite = (BlipSprite)436;
            CampFire.CurrentBlip.Color = BlipColor.Yellow;
            CampFire.CurrentBlip.Name = "Campfire";
            CampFire.CurrentBlip.IsShortRange = true;
            Model model2 = new Model("prop_skid_tent_01");
            model2.Request(250);
            if (model2.IsInCdImage && model2.IsValid)
            {
                while (!model2.IsLoaded)
                {
                    Script.Wait(50);
                }

                Vector3 Tpos = new Vector3(-129.34f, -1284.49f, 45.90f);
                Tent = World.CreateProp(model2, Tpos, true, false);
                Tent.Heading = 217f;
            }
            model2.MarkAsNoLongerNeeded();
            Tent.AddBlip();
            Tent.CurrentBlip.Sprite = BlipSprite.CaptureHouse;
            Tent.CurrentBlip.Color = BlipColor.Blue;
            Tent.CurrentBlip.Name = "Tent";
            Tent.CurrentBlip.IsShortRange = true;
            Game.Player.Character.Position = StartingLocation;
            Game.Player.Character.Heading = StartingHeading;
        }

        public static void LoadWeapons()
        {
            Game.Player.Character.Weapons.RemoveAll();
            foreach (string weapon in WeaponLoadout)
            {
                Game.Player.Character.GiveWeaponHashName(weapon, false, true);
            }
            Game.Player.Character.Weapons.Give(WeaponHash.Unarmed, 0, true, true);
        }

        public static void ResetCharacter()
        {
            SetCharacterModel();
            Game.Player.Money = 0;
            LoadWeapons();
            Game.Player.Character.Health = Game.Player.Character.MaxHealth;
            Game.Player.Character.Armor = 0;
            CurrentHungerLevel = MaxHungerLevel;
            CurrentThirstLevel = MaxThirstLevel;
            CurrentEnergyLevel = MaxEnergyLevel;
            Stats.StatsLastUpdateTime = DateTime.UtcNow;
            Population.SurvivorLastSpawnTime = DateTime.UtcNow;
            if (PlayerVehicles.Count > 0)
            {
                for (int i = 0; i < PlayerVehicles.Count; i++)
                {
                    PlayerVehicles.RemoveAt(i);
                }
            }
            if (Population.Vehicles.Count > 0)
            {
                for (int i = 0; i < Population.Props.Count; i++)
                {
                    Population.Vehicles[i].vehicle?.Delete();
                    Population.Vehicles.RemoveAt(i);
                }
            }
            if (Population.Props.Count > 0)
            {
                for (int i = 0; i < Population.Props.Count; i++)
                {
                    Population.Props[i].prop?.Delete();
                    Population.Props.RemoveAt(i);
                }
            }
            if (Population.AnimalPeds.Count > 0)
            {
                for (int i = 0; i < Population.AnimalPeds.Count; i++)
                {
                    Population.AnimalPeds[i].pedEntity?.Delete();
                    Population.AnimalPeds.RemoveAt(i);
                }
            }
            if (Population.ZombiePeds.Count > 0)
            {
                for (int i = 0; i < Population.ZombiePeds.Count; i++)
                {
                    Population.ZombiePeds[i].pedEntity?.Delete();
                    Population.ZombiePeds.RemoveAt(i);
                }
            }
            if (Population.SurvivorPeds.Count > 0)
            {
                for (int i = 0; i < Population.SurvivorPeds.Count; i++)
                {
                    Population.SurvivorPeds[i].pedEntity?.Delete();
                    Population.SurvivorPeds.RemoveAt(i);
                }
            }
            if (Looting.LootedEntities.Count > 0)
            {
                for (int i = 0; i < Looting.LootedEntities.Count; i++)
                {
                    Looting.LootedEntities?.RemoveAt(i);
                }
            }
            if (FixVehicles.FixedVehicles.Count > 0)
            {
                for (int i = 0; i < FixVehicles.FixedVehicles.Count; i++)
                {
                    FixVehicles.FixedVehicles?.RemoveAt(i);
                }
            }
            CharacterReset = true;
        }
    }
}
