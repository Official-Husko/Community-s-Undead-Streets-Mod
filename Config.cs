using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CWDM.Collections;
using CWDM.Inventory;

namespace CWDM
{
    public static class Config
    {
        public static ScriptSettings Settings;

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
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
            if (!Directory.Exists("./scripts/CWDM/Settings/Custom/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/Settings/Custom/");
            }
            if (!Directory.Exists("./scripts/CWDM/Logs/"))
            {
                Directory.CreateDirectory("./scripts/CWDM/Logs/");
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/CountryVehicles.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/CountryVehicles.lst", Population.CountryVehicleModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/CityVehicles.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/CityVehicles.lst", Population.CityVehicleModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/Zombies.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/Zombies.lst", Population.ZombieModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/Survivors.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/Survivors.lst", Population.SurvivorModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/CityAnimals.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/CityAnimals.lst", Population.CityAnimalModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/CountryAnimals.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/CountryAnimals.lst", Population.CountryAnimalModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/FriendlyVehicles.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/FriendlyVehicles.lst", Population.FriendlyVehicleModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/NeutralVehicles.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/NeutralVehicles.lst", Population.NeutralVehicleModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/HostileVehicles.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/HostileVehicles.lst", Population.HostileVehicleModels);
            }
            if (!File.Exists("./scripts/CWDM/Settings/Custom/WeaponLoadout.lst"))
            {
                File.WriteAllLines("./scripts/CWDM/Settings/Custom/WeaponLoadout.lst", Character.WeaponLoadout);
            }
            if (File.Exists("./scripts/CWDM/Settings/Custom/Settings.ini"))
            {
                Settings = ScriptSettings.Load("./scripts/CWDM/Settings/Settings.ini");
                Main.Version = Settings.GetValue("mod", "version", 0.2);
                Main.MenuKey = Settings.GetValue("hotkeys", "menu_key", Keys.F10);
                Main.InventoryKey = Settings.GetValue("hotkeys", "inventory_key", Keys.I);
                Population.ZombieHealth = Settings.GetValue("world", "zombie_health", 750);
                Population.ZombieDamage = Settings.GetValue("world", "zombie_damage", 25);
                Population.ZombieInstakill = Settings.GetValue("world", "enable_zombie_instakill", false);
                Population.EnableCityPopulationDifference = Settings.GetValue("world", "enable_city_population_difference", true);
                Population.MinSpawnDistance = Settings.GetValue("world", "min_spawn_distance", 50f);
                Population.MaxSpawnDistance = Settings.GetValue("world", "max_spawn_distance", 75f);
                Population.MaxZombies = Settings.GetValue("world", "max_zombies", 60);
                Population.EnableVehicles = Settings.GetValue("world", "enable_abandoned_vehicles", true);
                Population.MaxVehicles = Settings.GetValue("world", "max_vehicles", 20);
                Character.MaxPlayerVehicles = Settings.GetValue("world", "max_player_vehicles", 10);
                Population.EnableAnimals = Settings.GetValue("world", "enable_animals", true);
                Population.MaxAnimals = Settings.GetValue("world", "max_animals", 10);
                Population.SurvivorTimeMins = Settings.GetValue("world", "survivor_spawn_time_mins", 5);
                Population.EnableSurvivors = Settings.GetValue("world", "enable_survivors", true);
                Population.EnableSurvivorVehicles = Settings.GetValue("world", "enable_survivor_vehicles", true);
                Population.SurvivorHealth = Settings.GetValue("world", "survivor_health", 100);
                Population.SurvivorArmor = Settings.GetValue("world", "survivor_armor", 50);
                Population.MinFriendlyGroup = Settings.GetValue("world", "min_friendly_group", 2);
                Population.MaxFriendlyGroup = Settings.GetValue("world", "max_friendly_group", 4);
                Population.MinNeutralGroup = Settings.GetValue("world", "min_neutral_group", 2);
                Population.MaxNeutralGroup = Settings.GetValue("world", "max_neutral_group", 4);
                Population.MinHostileGroup = Settings.GetValue("world", "min_hostile_group", 2);
                Population.MaxHostileGroup = Settings.GetValue("world", "max_hostile_group", 4);
                Character.MaxPlayerGroupSize = Settings.GetValue("world", "max_player_group_size", 30);
                Population.CustomCityVehicles = Settings.GetValue("world", "enable_custom_city_vehicles", false);
                Population.CustomCountryVehicles = Settings.GetValue("world", "enable_custom_country_vehicles", false);
                Population.CustomZombies = Settings.GetValue("world", "enable_custom_zombies", false);
                Population.CustomSurvivors = Settings.GetValue("world", "enable_custom_survivors", false);
                Population.CustomCityAnimals = Settings.GetValue("world", "enable_custom_city_animals", false);
                Population.CustomCountryAnimals = Settings.GetValue("world", "enable_custom_country_animals", false);
                Population.CustomFriendlyVehicles = Settings.GetValue("world", "enable_custom_friendly_vehicles", false);
                Population.CustomNeutralVehicles = Settings.GetValue("world", "enable_custom_neutral_vehicles", false);
                Population.CustomHostileVehicles = Settings.GetValue("world", "enable_custom_hostile_vehicles", false);
                Stats.StatsTimeSecs = Settings.GetValue("player", "stats_refresh_time_secs", 1);
                Character.HungerDecrease = Settings.GetValue("player", "hunger_rate", 0.0004f);
                Character.ThirstDecrease = Settings.GetValue("player", "thirst_rate", 0.0008f);
                Character.EnergyDecrease = Settings.GetValue("player", "energy_rate", 0.0002f);
                Character.CustomWeaponLoadout = Settings.GetValue("player", "enabled_custom_weapon_loadout", false);
            }
            else
            {
                Settings = ScriptSettings.Load("./scripts/CWDM/Settings/Settings.ini");
                Settings.SetValue("mod", "version", 0.2);
                Settings.SetValue("hotkeys", "menu_key", Keys.F10);
                Settings.SetValue("hotkeys", "inventory_key", Keys.I);
                Settings.SetValue("world", "zombie_health", 750);
                Settings.SetValue("world", "zombie_damage", 25);
                Settings.SetValue("world", "enable_zombie_instakill", false);
                Settings.SetValue("world", "enable_city_population_difference", true);
                Settings.SetValue("world", "min_spawn_distance", 50f);
                Settings.SetValue("world", "max_spawn_distance", 75f);
                Settings.SetValue("world", "max_zombies", 30);
                Settings.SetValue("world", "enable_abandoned_vehicles", true);
                Settings.SetValue("world", "max_vehicles", 10);
                Settings.SetValue("world", "max_player_vehicles", 10);
                Settings.SetValue("world", "enable_animals", true);
                Settings.SetValue("world", "max_animals", 5);
                Settings.SetValue("world", "survivor_spawn_time_mins", 5);
                Settings.SetValue("world", "enable_survivors", true);
                Settings.SetValue("world", "enable_survivor_vehicles", true);
                Settings.SetValue("world", "survivor_health", 100);
                Settings.SetValue("world", "survivor_armor", 50);
                Settings.SetValue("world", "min_friendly_group", 2);
                Settings.SetValue("world", "max_friendly_group", 4);
                Settings.SetValue("world", "min_neutral_group", 2);
                Settings.SetValue("world", "max_neutral_group", 4);
                Settings.SetValue("world", "min_hostile_group", 2);
                Settings.SetValue("world", "max_hostile_group", 4);
                Settings.SetValue("world", "max_player_group_size", 30);
                Settings.SetValue("world", "enable_custom_city_vehicles", false);
                Settings.SetValue("world", "enable_custom_country_vehicles", false);
                Settings.SetValue("world", "enable_custom_zombies", false);
                Settings.SetValue("world", "enable_custom_survivors", false);
                Settings.SetValue("world", "enable_custom_city_animals", false);
                Settings.SetValue("world", "enable_custom_country_animals", false);
                Settings.SetValue("world", "enable_custom_friendly_vehicles", false);
                Settings.SetValue("world", "enable_custom_neutral_vehicles", false);
                Settings.SetValue("world", "enable_custom_hostile_vehicles", false);
                Settings.SetValue("player", "stats_refresh_time_secs", 1);
                Settings.SetValue("player", "hunger_rate", 0.0004f);
                Settings.SetValue("player", "thirst_rate", 0.0008f);
                Settings.SetValue("player", "energy_rate", 0.0002f);
                Settings.SetValue("player", "enabled_custom_weapon_loadout", false);
                Main.Version = Settings.GetValue("mod", "version", 0.2);
                Main.MenuKey = Settings.GetValue("hotkeys", "menu_key", Keys.F10);
                Main.InventoryKey = Settings.GetValue("hotkeys", "inventory_key", Keys.I);
                Population.ZombieHealth = Settings.GetValue("world", "zombie_health", 750);
                Population.ZombieDamage = Settings.GetValue("world", "zombie_damage", 25);
                Population.ZombieInstakill = Settings.GetValue("world", "enable_zombie_instakill", false);
                Population.EnableCityPopulationDifference = Settings.GetValue("world", "enable_city_population_difference", true);
                Population.MinSpawnDistance = Settings.GetValue("world", "min_spawn_distance", 50f);
                Population.MaxSpawnDistance = Settings.GetValue("world", "max_spawn_distance", 75f);
                Population.MaxZombies = Settings.GetValue("world", "max_zombies", 60);
                Population.EnableVehicles = Settings.GetValue("world", "enable_abandoned_vehicles", true);
                Population.MaxVehicles = Settings.GetValue("world", "max_vehicles", 20);
                Character.MaxPlayerVehicles = Settings.GetValue("world", "max_player_vehicles", 10);
                Population.EnableAnimals = Settings.GetValue("world", "enable_animals", true);
                Population.MaxAnimals = Settings.GetValue("world", "max_animals", 10);
                Population.SurvivorTimeMins = Settings.GetValue("world", "survivor_spawn_time_mins", 5);
                Population.EnableSurvivors = Settings.GetValue("world", "enable_survivors", true);
                Population.EnableSurvivorVehicles = Settings.GetValue("world", "enable_survivor_vehicles", true);
                Population.SurvivorHealth = Settings.GetValue("world", "survivor_health", 100);
                Population.SurvivorArmor = Settings.GetValue("world", "survivor_armor", 50);
                Population.MinFriendlyGroup = Settings.GetValue("world", "min_friendly_group", 2);
                Population.MaxFriendlyGroup = Settings.GetValue("world", "max_friendly_group", 4);
                Population.MinNeutralGroup = Settings.GetValue("world", "min_neutral_group", 2);
                Population.MaxNeutralGroup = Settings.GetValue("world", "max_neutral_group", 4);
                Population.MinHostileGroup = Settings.GetValue("world", "min_hostile_group", 2);
                Population.MaxHostileGroup = Settings.GetValue("world", "max_hostile_group", 4);
                Character.MaxPlayerGroupSize = Settings.GetValue("world", "max_player_group_size", 30);
                Population.CustomCityVehicles = Settings.GetValue("world", "enable_custom_city_vehicles", false);
                Population.CustomCountryVehicles = Settings.GetValue("world", "enable_custom_country_vehicles", false);
                Population.CustomZombies = Settings.GetValue("world", "enable_custom_zombies", false);
                Population.CustomSurvivors = Settings.GetValue("world", "enable_custom_survivors", false);
                Population.CustomCityAnimals = Settings.GetValue("world", "enable_custom_city_animals", false);
                Population.CustomCountryAnimals = Settings.GetValue("world", "enable_custom_country_animals", false);
                Population.CustomFriendlyVehicles = Settings.GetValue("world", "enable_custom_friendly_vehicles", false);
                Population.CustomNeutralVehicles = Settings.GetValue("world", "enable_custom_neutral_vehicles", false);
                Population.CustomHostileVehicles = Settings.GetValue("world", "enable_custom_hostile_vehicles", false);
                Stats.StatsTimeSecs = Settings.GetValue("player", "stats_refresh_time_secs", 1);
                Character.HungerDecrease = Settings.GetValue("player", "hunger_rate", 0.0004f);
                Character.ThirstDecrease = Settings.GetValue("player", "thirst_rate", 0.0008f);
                Character.EnergyDecrease = Settings.GetValue("player", "energy_rate", 0.0002f);
                Character.CustomWeaponLoadout = Settings.GetValue("player", "enabled_custom_weapon_loadout", false);
                Settings.Save();
            }
            if (Population.CustomCityVehicles)
            {
                try
                {
                    Population.CityVehicleModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/CityVehicles.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomCountryVehicles)
            {
                try
                {
                    Population.CountryVehicleModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/CountryVehicles.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomZombies)
            {
                try
                {
                    Population.ZombieModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/Zombies.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomSurvivors)
            {
                try
                {
                    Population.SurvivorModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/Survivors.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomCountryAnimals)
            {
                try
                {
                    Population.CountryAnimalModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/CountryAnimals.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomCityAnimals)
            {
                try
                {
                    Population.CityAnimalModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/CityAnimals.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomFriendlyVehicles)
            {
                try
                {
                    Population.FriendlyVehicleModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/FriendlyVehicles.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomNeutralVehicles)
            {
                try
                {
                    Population.NeutralVehicleModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/NeutralVehicles.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Population.CustomHostileVehicles)
            {
                try
                {
                    Population.HostileVehicleModels = File.ReadAllLines("./scripts/CWDM/Settings/Custom/HostileVehicles.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
            if (Character.CustomWeaponLoadout)
            {
                try
                {
                    Character.WeaponLoadout = File.ReadAllLines("./scripts/CWDM/Settings/Custom/WeaponLoadout.lst").ToList();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void SaveInventory()
        {
            try
            {
                WriteToBinaryFile("./scripts/CWDM/SaveGame/Items.sav", PlayerInventory.PlayerInventoryItems);
                WriteToBinaryFile("./scripts/CWDM/SaveGame/Materials.sav", PlayerInventory.PlayerInventoryMaterials);
            }
            catch (Exception x)
            {
                Log.Write(x.ToString());
            }
        }

        public static void LoadInventory()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Items.sav") && File.Exists("./scripts/CWDM/SaveGame/Materials.sav"))
            {
                try
                {
                    PlayerInventory.PlayerInventoryItems = ReadFromBinaryFile<List<InventoryItem>>("./scripts/CWDM/SaveGame/Items.sav");
                    for (int i = 0; i < PlayerInventory.PlayerInventoryItems.Count; i++)
                    {
                        PlayerInventory.UpdateItemsMenus(PlayerInventory.PlayerInventoryItems[i]);
                    }
                    PlayerInventory.PlayerInventoryMaterials = ReadFromBinaryFile<List<InventoryMaterial>>("./scripts/CWDM/SaveGame/Materials.sav");
                    for (int i = 0; i < PlayerInventory.PlayerInventoryMaterials.Count; i++)
                    {
                        PlayerInventory.UpdateMaterialsMenus(PlayerInventory.PlayerInventoryMaterials[i]);
                    }
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void SaveGroup()
        {
            if (Game.Player.Character.CurrentPedGroup.MemberCount > 1)
            {
                PlayerGroup.PlayerPedCollection = new PedCollection();
                foreach (Ped ped in Game.Player.Character.CurrentPedGroup)
                {
                    PlayerGroup.AddPedData(ped);
                }
                try
                {
                    WriteToBinaryFile("./scripts/CWDM/SaveGame/Group.sav", PlayerGroup.PlayerPedCollection);
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void LoadGroup()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Group.sav"))
            {
                try
                {
                    PedCollection pedCollection = new PedCollection();
                    pedCollection = ReadFromBinaryFile<PedCollection>("./scripts/CWDM/SaveGame/Group.sav");
                    List<PedData> pedDatas = pedCollection.ToList();
                    foreach (PedData pedData in pedDatas)
                    {
                        PlayerGroup.LoadPedFromPedData(pedData);
                    }
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void SaveVehicles()
        {
            if (Character.PlayerVehicles.Count > 0)
            {
                PlayerGroup.PlayerVehicleCollection = new VehicleCollection();
                foreach (Vehicle vehicle in Character.PlayerVehicles)
                {
                    PlayerGroup.AddVehicleData(vehicle);
                }
                try
                {
                    WriteToBinaryFile("./scripts/CWDM/SaveGame/Vehicles.sav", PlayerGroup.PlayerVehicleCollection);
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void LoadVehicles()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Vehicles.sav"))
            {
                try
                {
                    VehicleCollection vehicleCollection = new VehicleCollection();
                    vehicleCollection = ReadFromBinaryFile<VehicleCollection>("./scripts/CWDM/SaveGame/Vehicles.sav");
                    List<VehicleData> vehicleDatas = vehicleCollection.ToList();
                    foreach (VehicleData vehicleData in vehicleDatas)
                    {
                        PlayerGroup.LoadVehicleFromVehicleData(vehicleData);
                    }
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void SaveWeapons()
        {
            PlayerGroup.PlayerWeapons = new List<WeaponData>();
            PlayerGroup.SavePlayerWeapons();
            try
            {
                WriteToBinaryFile("./scripts/CWDM/SaveGame/Weapons.sav", PlayerGroup.PlayerWeapons);
            }
            catch (Exception x)
            {
                Log.Write(x.ToString());
            }
        }

        public static void LoadWeapons()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Weapons.sav"))
            {
                try
                {
                    PlayerGroup.PlayerWeapons = ReadFromBinaryFile<List<WeaponData>>("./scripts/CWDM/SaveGame/Weapons.sav");
                    PlayerGroup.LoadPlayerWeapons();
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void SaveCharacter()
        {
            PlayerData playerData = new PlayerData(Character.PlayerGender, Game.Player.Character.Position, Game.Player.Character.Rotation, Game.Player.Character.Heading, Game.Player.Character.Health, Game.Player.Character.Armor, Character.CurrentHungerLevel, Character.CurrentThirstLevel, Character.CurrentEnergyLevel, Character.CurrentInfectionLevel, Character.CurrentBatteryLevel, World.CurrentDate, World.CurrentDayTime, World.Weather);
            try
            {
                WriteToBinaryFile("./scripts/CWDM/SaveGame/Character.sav", playerData);
            }
            catch (Exception x)
            {
                Log.Write(x.ToString());
            }
        }

        public static void LoadCharacter()
        {
            if (File.Exists("./scripts/CWDM/SaveGame/Character.sav"))
            {
                try
                {
                    Character.CharacterReset = false;
                    Character.SetCharacterModel();
                    PlayerData playerData = ReadFromBinaryFile<PlayerData>("./scripts/CWDM/SaveGame/Character.sav");
                    Character.PlayerGender = playerData.Gender;
                    Game.Player.Character.Position = playerData.Position;
                    Game.Player.Character.Rotation = playerData.Rotation;
                    Game.Player.Character.Heading = playerData.Heading;
                    Game.Player.Character.Health = playerData.Health;
                    Game.Player.Character.Armor = playerData.Armor;
                    Character.CurrentHungerLevel = playerData.Hunger;
                    Character.CurrentThirstLevel = playerData.Thirst;
                    Character.CurrentEnergyLevel = playerData.Energy;
                    Character.CurrentInfectionLevel = playerData.Infection;
                    Character.CurrentBatteryLevel = playerData.Battery;
                    World.CurrentDate = playerData.Date;
                    World.CurrentDayTime = playerData.Time;
                    World.Weather = playerData.Weather;
                    Stats.StatsLastUpdateTime = DateTime.UtcNow;
                    Population.SurvivorLastSpawnTime = DateTime.UtcNow;
                    if (Character.PlayerVehicles.Count > 0)
                    {
                        for (int i = 0; i < Character.PlayerVehicles.Count; i++)
                        {
                            Character.PlayerVehicles.RemoveAt(i);
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
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void SavePlayerAll()
        {
            try
            {
                SaveCharacter();
                SaveGroup();
                SaveInventory();
                SaveVehicles();
                SaveWeapons();
                UI.Notify("Game saved!");
            }
            catch (Exception x)
            {
                Log.Write(x.ToString());
                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                UI.Notify("~r~WARNING: ~s~Game not saved - see error log!");
            }
        }

        public static void LoadPlayerAll()
        {
            try
            {
                LoadCharacter();
                LoadGroup();
                LoadInventory();
                LoadVehicles();
                LoadWeapons();
            }
            catch (Exception x)
            {
                Log.Write(x.ToString());
                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                UI.Notify("~r~WARNING: ~s~Save Game cannot load - see error log!");
            }
        }
    }
}
