using System;
using GTA;
using GTA.Native;
using GTA.Math;

public enum Gender
{
    Male,
    Female
}

namespace CWDM
{
    public class Character
    {
        public static Vehicle playerVehicle;
        public static Gender playerGender;
        public Model playerOldModel;
        public int playerOldMaxWantedLevel;
        public int playerOldMoney;
        public Vector3 playerOldPosition;
        public float playerOldHeading;
        public WeaponCollection playerOldWeapons;
        public static Prop campFire;
        public static Prop tent;
        public static Prop barrier;
        public static Prop dumpster;
        public static float currentHungerLevel = 1.0f;
        public static float maxHungerLevel = 1.0f;
        public static float hungerDecrease = 0.0004f;
        public static int hungerTicksBetweenUpdates = 200;
        public static int hungerTicksSinceLastUpdate;
        public static int lowHungerTicksBetweenUpdates = 100;
        public static int lowHungerTicksSinceLastUpdate;
        public static float currentThirstLevel = 1.0f;
        public static float maxThirstLevel = 1.0f;
        public static float thirstDecrease = 0.0008f;
        public static int thirstTicksBetweenUpdates = 200;
        public static int thirstTicksSinceLastUpdate;
        public static int lowThirstTicksBetweenUpdates = 100;
        public static int lowThirstTicksSinceLastUpdate;
        public static float currentEnergyLevel = 1.0f;
        public static float maxEnergyLevel = 1.0f;
        public static float energyDecrease = 0.0002f;
        public static int energyTicksBetweenUpdates = 200;
        public static int energyTicksSinceLastUpdate;
        public static int lowEnergyTicksBetweenUpdates = 100;
        public static int lowEnergyTicksSinceLastUpdate;

        public static void Update()
        {
            Game.Player.WantedLevel = 0;
            StatsUpdate();
        }

        public static void ResetCharacter()
        {
            Game.Player.Money = 0;
            Game.Player.Character.Weapons.RemoveAll();
            Game.Player.Character.Weapons.Give(WeaponHash.Pistol, 120, false, true);
            Game.Player.Character.Weapons.Give(WeaponHash.SMG, 300, false, true);
            Game.Player.Character.Weapons.Give(WeaponHash.Knife, 0, false, true);
            Game.Player.Character.Weapons.Give(WeaponHash.Unarmed, 0, true, true);
            Game.Player.Character.Health = Game.Player.Character.MaxHealth;
            Game.Player.Character.Armor = 0;
            foreach (InventoryItem item in Inventory.playerItemInventory)
            {
                item.Amount = 0;
                Inventory.itemsSubMenu.MenuItems.Find(menuItem => menuItem.Text == item.Name).SetRightLabel("(" + item.Amount + "/" + item.MaxAmount + ")");
            }
            foreach (InventoryMaterial material in Inventory.playerMaterialInventory)
            {
                material.Amount = 0;
                Inventory.materialsSubMenu.MenuItems.Find(menuItem => menuItem.Text == material.Name).SetRightLabel("(" + material.Amount + "/" + material.MaxAmount + ")");
            }
            hungerTicksSinceLastUpdate = 0;
            thirstTicksSinceLastUpdate = 0;
            maxHungerLevel = 1.0f;
            currentHungerLevel = maxHungerLevel;
            maxThirstLevel = 1.0f;
            currentThirstLevel = maxThirstLevel;
            Ped[] all_ped = World.GetAllPeds();
            if (all_ped.Length > 0)
            {
                foreach (Ped ped in all_ped)
                {
                    ped.Delete();
                }
            }
            Vehicle[] all_vecs = World.GetAllVehicles();
            if (all_vecs.Length > 0)
            {
                foreach (Vehicle vehicle in all_vecs)
                {
                    vehicle.Delete();
                }
            }
            Population.suvLastSpawnTime = DateTime.UtcNow;
            playerVehicle = null;
        }

        public static void StatsUpdate()
        {
            hungerTicksSinceLastUpdate++;
            if (hungerTicksSinceLastUpdate >= hungerTicksBetweenUpdates)
            {
                currentHungerLevel -= hungerDecrease;
                if (currentHungerLevel <= 0.10f && currentHungerLevel > 0.0f)
                {
                    UI.Notify("~r~WARNING:~w~ Hunger levels are getting low! You need to eat something to keep up your strength and avoid loss of health.");
                }
                else if (currentHungerLevel < 0.01f)
                {
                    UI.Notify("~r~WARNING:~w~ Hunger levels are dangerously low! You need to eat something to regain your strength and raise your health.");
                }
                if (currentHungerLevel < 0)
                {
                    currentHungerLevel = 0;
                }
                hungerTicksSinceLastUpdate = 0;
            }
            thirstTicksSinceLastUpdate++;
            if (thirstTicksSinceLastUpdate >= thirstTicksBetweenUpdates)
            {
                currentThirstLevel -= thirstDecrease;
                if (currentThirstLevel <= 0.10f && currentThirstLevel > 0.0f)
                {
                    UI.Notify("~r~WARNING:~w~ Thirst levels are getting low! You need to drink something to keep up your strength and avoid loss of health.");
                }
                else if (currentThirstLevel < 0.01f)
                {
                    UI.Notify("~r~WARNING:~w~ Thirst levels are dangerously low! You need to drink something to regain your strength and raise your health.");
                }
                if (currentThirstLevel < 0)
                {
                    currentThirstLevel = 0;
                }
                thirstTicksSinceLastUpdate = 0;
            }
            if (currentHungerLevel < 0.01f)
            {
                lowHungerTicksSinceLastUpdate++;
                if (lowHungerTicksSinceLastUpdate >= lowHungerTicksBetweenUpdates)
                {
                    Game.Player.Character.Health--;
                    lowHungerTicksSinceLastUpdate = 0;
                }
            }
            if (currentThirstLevel < 0.01f)
            {
                lowThirstTicksSinceLastUpdate++;
                if (lowThirstTicksSinceLastUpdate >= lowThirstTicksBetweenUpdates)
                {
                    Game.Player.Character.Health--;
                    lowThirstTicksSinceLastUpdate = 0;
                }
            }
            energyTicksSinceLastUpdate++;
            if (energyTicksSinceLastUpdate >= energyTicksBetweenUpdates)
            {
                currentEnergyLevel -= energyDecrease;
                if (currentEnergyLevel <= 0.10f && currentEnergyLevel > 0.0f)
                {
                    UI.Notify("~r~WARNING:~w~ Energy levels are getting low! You need to find somewhere safe to keep up your strength and avoid loss of health.");
                }
                else if (currentEnergyLevel < 0.01f)
                {
                    UI.Notify("~r~WARNING:~w~ Energy levels are dangerously low! You need to find somewhere safe to sleep to regain your strength and raise your health.");
                }
                if (currentEnergyLevel < 0)
                {
                    currentEnergyLevel = 0;
                }
                energyTicksSinceLastUpdate = 0;
                energyTicksSinceLastUpdate = 0;
            }
            if (currentEnergyLevel < 0.01f)
            {
                lowEnergyTicksSinceLastUpdate++;
                if (lowEnergyTicksSinceLastUpdate >= lowEnergyTicksBetweenUpdates)
                {
                    Game.Player.Character.Health--;
                    lowEnergyTicksSinceLastUpdate = 0;
                }
            }
        }

        public static void RestoreHunger(float amount)
        {
            currentHungerLevel += amount;
            if (currentHungerLevel > maxHungerLevel)
            {
                currentHungerLevel = maxHungerLevel;
            }
            if (currentHungerLevel > 0.10f)
            {
                lowHungerTicksSinceLastUpdate = 0;
            }
        }

        public static void RestoreThirst(float amount)
        {
            currentThirstLevel += amount;
            if (currentThirstLevel > maxThirstLevel)
            {
                currentThirstLevel = maxThirstLevel;
            }
            if (currentThirstLevel > 0.10f)
            {
                lowThirstTicksSinceLastUpdate = 0;
            }
        }

        public static void RestoreEnergy(float amount)
        {
            currentEnergyLevel += amount;
            if (currentEnergyLevel > maxEnergyLevel)
            {
                currentEnergyLevel = maxEnergyLevel;
            }
            if (currentEnergyLevel > 0.10f)
            {
                lowEnergyTicksSinceLastUpdate = 0;
            }
        }

        public void Setup()
        {
            playerOldMaxWantedLevel = Game.MaxWantedLevel;
            playerOldMoney = Game.Player.Money;
            playerOldPosition = Game.Player.Character.Position;
            playerOldHeading = Game.Player.Character.Heading;
            playerOldModel = Game.Player.Character.Model;
            playerOldWeapons = Game.Player.Character.Weapons;
            Game.MaxWantedLevel = 0;
            Game.Player.WantedLevel = 0;
            Game.Player.Money = 0;
            Model characterModel;
            PedHash[] characterMaleModels = { PedHash.FreemodeMale01 };
            PedHash[] characterFemaleModels = { PedHash.FreemodeFemale01 };
            PedHash[] characterModels;
            if (playerGender == Gender.Male)
            {
                characterModels = characterMaleModels;
            }
            else
            {
                characterModels = characterFemaleModels;
            }
            characterModel = new Model(RandoMath.GetRandomElementFromArray(characterModels));
            characterModel.Request(500);
            if (characterModel.IsInCdImage && characterModel.IsValid)
            {
                while (!characterModel.IsLoaded) Script.Wait(100);
                Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, characterModel.Hash);
                if (playerGender == Gender.Male)
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
            characterModel.MarkAsNoLongerNeeded();
            Game.Player.Money = 0;
            Game.Player.Character.Position = new Vector3(478.8616f, -921.53f, 38.77953f);
            Game.Player.Character.Heading = 266;
            var model = new Model("prop_beach_fire");
            model.Request(250);
            if (model.IsInCdImage && model.IsValid)
            {
                while (!model.IsLoaded) Script.Wait(50);
                var CFpos = new Vector3(482.3683f, -921.3369f, 37.2f);
                campFire = World.CreateProp(model, CFpos, true, false);
            }
            model.MarkAsNoLongerNeeded();
            campFire.AddBlip();
            campFire.CurrentBlip.Sprite = (BlipSprite)436;
            campFire.CurrentBlip.Color = BlipColor.Yellow;
            campFire.CurrentBlip.Name = "Campfire";
            var model2 = new Model("prop_skid_tent_01");
            model2.Request(250);
            if (model2.IsInCdImage && model2.IsValid)
            {
                while (!model2.IsLoaded) Script.Wait(50);
                var Tpos = new Vector3(478.2682f, -925.3043f, 36.8f);
                tent = World.CreateProp(model2, Tpos, true, false);
                tent.Heading = 135;
            }
            model2.MarkAsNoLongerNeeded();
            tent.AddBlip();
            tent.CurrentBlip.Sprite = BlipSprite.CaptureHouse;
            tent.CurrentBlip.Color = BlipColor.Blue;
            tent.CurrentBlip.Name = "Tent";
            var model3 = new Model("prop_const_fence02a");
            model3.Request(250);
            if (model3.IsInCdImage && model3.IsValid)
            {
                while (!model3.IsLoaded) Script.Wait(50);
                var Bpos = new Vector3(418.9457f, -890.5727f, 28.4f);
                barrier = World.CreateProp(model3, Bpos, true, false);
                barrier.Heading = 270;
            }
            model3.MarkAsNoLongerNeeded();
            var model4 = new Model("prop_dumpster_02a");
            model4.Request(250);
            if (model4.IsInCdImage && model4.IsValid)
            {
                while (!model4.IsLoaded) Script.Wait(50);
                var Dpos = new Vector3(459.4905f, -933.745f, 31.2f);
                dumpster = World.CreateProp(model4, Dpos, true, false);
                dumpster.Heading = 270;
            }
            model4.MarkAsNoLongerNeeded();
            ResetCharacter();
        }

        public void Revert()
        {
            Game.MaxWantedLevel = playerOldMaxWantedLevel;
            Game.Player.Money = 0;
            var characterModel = new Model(playerOldModel.Hash);
            characterModel.Request(500);
            if (characterModel.IsInCdImage && characterModel.IsValid)
            {
                while (!characterModel.IsLoaded) Script.Wait(100);

                Function.Call(Hash.SET_PLAYER_MODEL, Game.Player, characterModel.Hash);
                Function.Call(Hash.SET_PED_DEFAULT_COMPONENT_VARIATION, Game.Player.Character.Handle);
            }
            characterModel.MarkAsNoLongerNeeded();
            Game.Player.Money = playerOldMoney;
            Game.Player.Character.Position = playerOldPosition;
            Game.Player.Character.Heading = playerOldHeading;
            campFire.Delete();
        }
    }
}