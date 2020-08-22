using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using GTA;

namespace CWDM
{
    public static class Config
    {
        public static ScriptSettings Settings;

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        public static void SaveInventory()
        {
            try
            {
                WriteToBinaryFile("./scripts/CWDM/SaveGame/Items.sav", Inventory.playerItemInventory);
                WriteToBinaryFile("./scripts/CWDM/SaveGame/Materials.sav", Inventory.playerMaterialInventory);
                UI.Notify("~y~Inventory ~w~saved!");
            }
            catch (Exception x)
            {
                Debug.Log(x.ToString());
            }
        }

        public static void LoadInventory()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Items.sav") && File.Exists("./scripts/CWDM/SaveGame/Materials.sav"))
            {
                try
                {
                    Inventory.playerItemInventory = ReadFromBinaryFile<List<InventoryItem>>("./scripts/CWDM/SaveGame/Items.sav");
                    for (int i = 0; i < Inventory.playerItemInventory.Count; i++)
                    {
                        Inventory.itemsSubMenu.MenuItems[i].SetRightLabel("(" + Inventory.playerItemInventory[i].Amount + "/" + Inventory.playerItemInventory[i].MaxAmount + ")");
                    }
                    Inventory.playerMaterialInventory = ReadFromBinaryFile<List<InventoryMaterial>>("./scripts/CWDM/SaveGame/Materials.sav");
                    for (int i = 0; i < Inventory.playerMaterialInventory.Count; i++)
                    {
                        Inventory.materialsSubMenu.MenuItems[i].SetRightLabel("(" + Inventory.playerMaterialInventory[i].Amount + "/" + Inventory.playerMaterialInventory[i].MaxAmount + ")");
                    }
                    UI.Notify("~y~Inventory ~w~loaded!");
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            else
            {
                UI.Notify("No ~y~Inventory ~w~available to load!");
            }
        }

        public static void LoadSettings()
        {
            if (!Directory.Exists("./scripts/CWDM/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/");
            }
            if (!Directory.Exists("./scripts/CWDM/SaveGame/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/SaveGame/");
            }
            if (!Directory.Exists("./scripts/CWDM/Settings/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/Settings/");
            }
            if (!Directory.Exists("./scripts/CWDM/Logs/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/Logs/");
            }
            if (!File.Exists("./scripts/CWDM/Settings/CountryVehicles.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/CountryVehicles.lst", Population.CountryVehicleModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/CityVehicles.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/CityVehicles.lst", Population.CityVehicleModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Zombies.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Zombies.lst", Population.ZombieModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Survivors.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Survivors.lst", Population.SurvivorModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/CityAnimals.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/CityAnimals.lst", Population.CityAnimalModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/CountryAnimals.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/CountryAnimals.lst", Population.CountryAnimalModels);
            }
            if (File.Exists("./scripts/CWDM/Settings/Settings.ini"))
            {
                Settings = ScriptSettings.Load("./scripts/CWDM/Settings/Settings.ini");
                Main.MenuKey = Settings.GetValue("hotkeys", "menu_key", Keys.F10);
                Main.InventoryKey = Settings.GetValue("hotkeys", "inventory_key", Keys.I);
                Population.spawnVehicles = Settings.GetValue("world", "enable_abandoned_vehicles", true);
                Population.spawnSurvivors = Settings.GetValue("world", "enable_survivors", true);
                Population.spawnAnimals = Settings.GetValue("world", "enable_animals", true);
                Population.zombieHealth = Settings.GetValue("world", "zombie_health", 750);
                Population.survivorHealth = Settings.GetValue("world", "survivor_health", 750);
                Population.maxZombies = Settings.GetValue("world", "max_zombies", 30);
                Population.maxVehicles = Settings.GetValue("world", "max_vehicles", 10);
                Population.maxAnimals = Settings.GetValue("world", "max_animals", 5);
                Population.doubleCityPopulation = Settings.GetValue("world", "enable_double_city_population", true);
                Population.survivorTime = Settings.GetValue("world", "survivor_spawn_time", 5);
                Population.minSpawnDistance = Settings.GetValue("world", "min_spawn_distance", 50);
                Population.maxSpawnDistance = Settings.GetValue("world", "max_spawn_distance", 100);
                Population.customCityVehicles = Settings.GetValue("world", "enable_custom_city_vehicles", false);
                Population.customCityVehicles = Settings.GetValue("world", "enable_custom_country_vehicles", false);
                Population.customZombies = Settings.GetValue("world", "enable_custom_zombies", false);
                Population.customSurvivors = Settings.GetValue("world", "enable_custom_survivors", false);
                Population.customCityAnimals = Settings.GetValue("world", "enable_custom_city_animals", false);
                Population.customCountryAnimals = Settings.GetValue("world", "enable_custom_country_animals", false);
                Character.hungerDecrease = Settings.GetValue("player", "hunger_rate", 0.0004f);
                Character.thirstDecrease = Settings.GetValue("player", "thirst_rate", 0.0008f);
                Character.energyDecrease = Settings.GetValue("player", "energy_rate", 0.0002f);
            }
            else
            {
                Settings = ScriptSettings.Load("./scripts/CWDM/Settings/Settings.ini");
                Settings.SetValue("hotkeys", "menu_key", Keys.F10);
                Settings.SetValue("hotkeys", "inventory_key", Keys.I);
                Settings.SetValue("world", "enable_abandoned_vehicles", true);
                Settings.SetValue("world", "enable_survivors", true);
                Settings.SetValue("world", "enable_animals", true);
                Settings.SetValue("world", "zombie_health", 750);
                Settings.SetValue("world", "survivor_health", 100);
                Settings.SetValue("world", "max_zombies", 30);
                Settings.SetValue("world", "max_vehicles", 10);
                Settings.SetValue("world", "max_animals", 5);
                Settings.SetValue("world", "enable_double_city_population", true);
                Settings.SetValue("world", "survivor_spawn_time", 5);
                Settings.SetValue("world", "min_spawn_distance", 50);
                Settings.SetValue("world", "max_spawn_distance", 100);
                Settings.SetValue("world", "enable_custom_city_vehicles", false);
                Settings.SetValue("world", "enable_custom_country_vehicles", false);
                Settings.SetValue("world", "enable_custom_zombies", false);
                Settings.SetValue("world", "enable_custom_survivors", false);
                Settings.SetValue("world", "enable_custom_city_animals", false);
                Settings.SetValue("world", "enable_custom_country_animals", false);
                Settings.SetValue("player", "hunger_rate", 0.0004f);
                Settings.SetValue("player", "thirst_rate", 0.0008f);
                Settings.SetValue("player", "energy_rate", 0.0002f);
                Main.MenuKey = Settings.GetValue("hotkeys", "menu_key", Keys.F10);
                Main.InventoryKey = Settings.GetValue("hotkeys", "inventory_key", Keys.I);
                Population.spawnVehicles = Settings.GetValue("world", "enable_abandoned_vehicles", true);
                Population.spawnSurvivors = Settings.GetValue("world", "enable_survivors", true);
                Population.spawnAnimals = Settings.GetValue("world", "enable_animals", true);
                Population.zombieHealth = Settings.GetValue("world", "zombie_health", 750);
                Population.zombieHealth = Settings.GetValue("world", "zombie_health", 750);
                Population.survivorHealth = Settings.GetValue("world", "survivor_health", 750);
                Population.maxZombies = Settings.GetValue("world", "max_zombies", 30);
                Population.maxVehicles = Settings.GetValue("world", "max_vehicles", 10);
                Population.maxAnimals = Settings.GetValue("world", "max_animals", 5);
                Population.doubleCityPopulation = Settings.GetValue("world", "enable_double_city_population", true);
                Population.survivorTime = Settings.GetValue("world", "survivor_spawn_time", 5);
                Population.minSpawnDistance = Settings.GetValue("world", "min_spawn_distance", 50);
                Population.maxSpawnDistance = Settings.GetValue("world", "max_spawn_distance", 100);
                Population.customCityVehicles = Settings.GetValue("world", "enable_custom_city_vehicles", false);
                Population.customCityVehicles = Settings.GetValue("world", "enable_custom_country_vehicles", false);
                Population.customZombies = Settings.GetValue("world", "enable_custom_zombies", false);
                Population.customSurvivors = Settings.GetValue("world", "enable_custom_survivors", false);
                Population.customCityAnimals = Settings.GetValue("world", "enable_custom_city_animals", false);
                Population.customCountryAnimals = Settings.GetValue("world", "enable_custom_country_animals", false);
                Character.hungerDecrease = Settings.GetValue("player", "hunger_rate", 0.0004f);
                Character.thirstDecrease = Settings.GetValue("player", "thirst_rate", 0.0008f);
                Character.energyDecrease = Settings.GetValue("player", "energy_rate", 0.0002f);
                Settings.Save();
            }
            if (Population.customCityVehicles)
            {
                try
                {
                    Population.CityVehicleModels = File.ReadAllLines("./scripts/CWDM/Settings/CityVehicles.lst").ToList();
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            if (Population.customCountryVehicles)
            {
                try
                {
                    Population.CountryVehicleModels = File.ReadAllLines("./scripts/CWDM/Settings/CountryVehicles.lst").ToList();
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            if (Population.customZombies)
            {
                try
                {
                    Population.ZombieModels = File.ReadAllLines("./scripts/CWDM/Settings/Zombies.lst").ToList();
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            if (Population.customSurvivors)
            {
                try
                {
                    Population.SurvivorModels = File.ReadAllLines("./scripts/CWDM/Settings/Survivors.lst").ToList();
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            if (Population.customCountryAnimals)
            {
                try
                {
                    Population.CountryAnimalModels = File.ReadAllLines("./scripts/CWDM/Settings/CountryAnimals.lst").ToList();
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            if (Population.customCityAnimals)
            {
                try
                {
                    Population.CityAnimalModels = File.ReadAllLines("./scripts/CWDM/Settings/CityAnimals.lst").ToList();
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
        }

        public static void SaveWeapons()
        {
            for (int i = 0; i < PlayerGroup.PlayerWeapons.Count; i++)
            {
                PlayerGroup.PlayerWeapons.RemoveAt(i);
            }
            PlayerGroup.SavePlayerWeapons();
            try
            {
                WriteToBinaryFile("./scripts/CWDM/SaveGame/Weapons.sav", PlayerGroup.PlayerWeapons);
                UI.Notify("~r~Weapons~w~ saved!");
            }
            catch (Exception x)
            {
                Debug.Log(x.ToString());
            }
        }

        public static void LoadWeapons()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Weapons.sav"))
            {
                PlayerGroup.PlayerWeapons = ReadFromBinaryFile<List<Weapon>>("./scripts/CWDM/SaveGame/Weapons.sav");
                if (PlayerGroup.PlayerWeapons.Count > 0)
                {
                    Game.Player.Character.Weapons.RemoveAll();
                    PlayerGroup.LoadPlayerWeapons();
                    UI.Notify("~r~Weapons ~w~loaded!");
                }
                else
                {
                    UI.Notify("~r~Weapons ~w~load failed!");
                }
            }
            else
            {
                UI.Notify("No ~r~Weapons ~w~available to load!");
            }
        }

        public static void RegisterVehicle()
        {
            if (Game.Player.Character.IsInVehicle())
            {
                Vehicle vehicle = Game.Player.Character.CurrentVehicle;
                if (Character.playerVehicle != null)
                {
                    if (Character.playerVehicle.CurrentBlip.Type != 0)
                    {
                        Character.playerVehicle.CurrentBlip.Remove();
                    }
                }
                Character.playerVehicle = vehicle;
                if (Character.playerVehicle.CurrentBlip.Type != 0)
                {
                    Character.playerVehicle.CurrentBlip.Remove();
                }
                Blip blip = Character.playerVehicle.AddBlip();
                blip.Sprite = BlipSprite.GetawayCar;
                blip.Color = BlipColor.Green;
                blip.Name = "Personal Vehicle";
                UI.Notify("Current vehicle now registered as ~o~Personal Vehicle");
            }
            else
            {
                UI.Notify("You are not in a vehicle!");
            }
        }

        public static void SaveVehicle()
        {
            if (Character.playerVehicle != null)
            {
                for (int i = 0; i < PlayerVehicle.PlayerVehicleCollection.Count; i++)
                {
                    PlayerVehicle.PlayerVehicleCollection.RemoveAt(i);
                }
                PlayerVehicle.AddVehicleData(Character.playerVehicle);
                try
                {
                    WriteToBinaryFile("./scripts/CWDM/SaveGame/Vehicle.sav", PlayerVehicle.PlayerVehicleCollection);
                    UI.Notify("~o~Personal Vehicle~w~ saved!");
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            else
            {
                UI.Notify("You don't have a ~o~Personal Vehicle~w~ to save!");
            }
        }

        public static void LoadVehicle()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Vehicle.sav"))
            {
                PlayerVehicle.PlayerVehicleCollection = ReadFromBinaryFile<VehicleCollection>("./scripts/CWDM/SaveGame/Vehicle.sav");
                if (PlayerVehicle.PlayerVehicleCollection.Count > 0)
                {
                    List<VehicleData> vehicleDatas = PlayerVehicle.PlayerVehicleCollection.ToList();
                    if (Character.playerVehicle != null)
                    {
                        Blip blip = Character.playerVehicle.CurrentBlip;
                        blip.Remove();
                        Character.playerVehicle.MarkAsNoLongerNeeded();
                        Character.playerVehicle.Delete();
                    }
                    foreach (VehicleData vehicleData in vehicleDatas)
                    {
                        PlayerVehicle.LoadVehicleFromVehicleData(vehicleData);
                    }
                    Blip blip2 = Character.playerVehicle.AddBlip();
                    blip2.Sprite = BlipSprite.GetawayCar;
                    blip2.Color = BlipColor.Green;
                    blip2.Name = "Personal Vehicle";
                    UI.Notify("~o~Personal Vehicle ~w~loaded!");
                }
                else
                {
                    UI.Notify("~o~Personal Vehicle ~w~load failed!");
                }
            }
            else
            {
                UI.Notify("No ~o~Personal Vehicle ~w~available to load!");
            }
        }

        public static void SaveGroup()
        {
            List<Ped> group = Game.Player.Character.CurrentPedGroup.ToList(false);
            if (group.Count > 0)
            {
                for (int i = 0; i < PlayerGroup.PlayerPedCollection.Count; i++)
                {
                    PlayerGroup.PlayerPedCollection.RemoveAt(i);
                }
                foreach (Ped ped in group)
                {
                    PlayerGroup.AddPedData(ped);
                }
                try
                {
                    WriteToBinaryFile("./scripts/CWDM/SaveGame/Group.sav", PlayerGroup.PlayerPedCollection);
                    UI.Notify("~p~Group ~w~saved!");
                }
                catch (Exception x)
                {
                    Debug.Log(x.ToString());
                }
            }
            else
            {
                UI.Notify("You have no one in your group!");
            }
        }

        public static void LoadGroup()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Group.sav"))
            {
                PlayerGroup.PlayerPedCollection = ReadFromBinaryFile<PedCollection>("./scripts/CWDM/SaveGame/Group.sav");
                if (PlayerGroup.PlayerPedCollection.Count > 0)
                {
                    List<PedData> pedDatas = PlayerGroup.PlayerPedCollection.ToList();
                    List<Ped> oldGroup = Game.Player.Character.CurrentPedGroup.ToList(false);
                    foreach (Ped ped in oldGroup)
                    {
                        Blip currentBlip = ped.CurrentBlip;
                        if (currentBlip.Type != 0)
                        {
                            currentBlip.Remove();
                        }
                        ped.LeaveGroup();
                        int i = Population.survivorList.FindIndex(match: a => a.pedEntity == ped);
                        Population.survivorList.RemoveAt(i);
                        ped.MarkAsNoLongerNeeded();
                        ped.Delete();
                    }
                    foreach (PedData pedData in pedDatas)
                    {
                        PlayerGroup.LoadPedFromPedData(pedData);
                    }
                    UI.Notify("~p~Group ~w~loaded!");
                }
                else
                {
                    UI.Notify("~p~Group ~w~load failed!");
                }
            }
            else
            {
                UI.Notify("No ~p~Group ~w~available to load!");
            }
        }
    }
}
