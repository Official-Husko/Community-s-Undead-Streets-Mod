using System;
using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Native;
using GTA.Math;

namespace CWDM
{
    public enum GroupType
    {
        Friendly,
        Neutral,
        Hostile,
        Random
    }

    public enum PedTasks
    {
        None,
        Wander,
        Guard,
        Follow,
        Leave
    }

    public class Population
    {
        public static List<ZombiePed> zombieList;
        public static List<SurvivorPed> survivorList;
        public static List<Ped> animalList;
        public static Population instance;
        public int zombieCount = 0;
        public int vehicleCount = 0;
        public int animalCount = 0;
        public bool zomUpdateRanThisFrame = false;
        public bool zomPopRanThisFrame = false;
        public int zomPopTicksBetweenUpdates = 100;
        public int zomPopTicksSinceLastUpdate = 0;
        public bool vehPopRanThisFrame = false;
        public int vehPopTicksBetweenUpdates = 100;
        public int vehPopTicksSinceLastUpdate = 0;
        public bool aniUpdateRanThisFrame = false;
        public bool aniPopRanThisFrame = false;
        public int aniPopTicksBetweenUpdates = 100;
        public int aniPopTicksSinceLastUpdate = 0;
        public bool clearRanThisFrame = false;
        public int clearTicksBetweenUpdates = 100;
        public int clearTicksSinceLastUpdate = 0;
        public static DateTime suvLastSpawnTime = new DateTime();
        public Vector3 startingLoc;
        public static bool spawnVehicles = true;
        public static bool spawnSurvivors = true;
        public static bool spawnAnimals = true;
        public static bool zombieRunners = false;
        public static int maxZombies = 30;
        public static int maxVehicles = 10;
        public static int maxAnimals = 5;
        public static bool doubleCityPopulation = true;
        public static int survivorTime = 5;
        public static int zombieHealth = 750;
        public static int survivorHealth = 100;
        public static int minSpawnDistance = 50;
        public static int maxSpawnDistance = 100;
        public static bool customZombies = false;
        public static List<string> ZombieModels = new List<string>
        {
            "u_f_y_corpse_01",
            "u_m_y_corpse_01",
            "u_m_y_zombie_01"
        };
        public static bool customSurvivors = false;
        public static List<string> SurvivorModels = new List<string>
        {
            "mp_m_waremech_01",
            "mp_m_weapexp_01",
            "mp_m_weapwork_01",
            "s_m_y_xmech_02"
        };
        public static bool customCountryAnimals = false;
        public static List<string> CountryAnimalModels = new List<string>
        {
            "a_c_cat_01",
            "a_c_boar",
            "a_c_chickenhawk",
            "a_c_coyote",
            "a_c_deer",
            "a_c_hen",
            "a_c_husky",
            "a_c_mtlion",
            "a_c_poodle",
            "a_c_pug",
            "a_c_rabbit_01",
            "a_c_rottweiler",
            "a_c_shepherd",
            "a_c_westy"
        };
        public static bool customCityAnimals = false;
        public static List<string> CityAnimalModels = new List<string>
        {
            "a_c_cat_01",
            "a_c_husky",
            "a_c_poodle",
            "a_c_pug",
            "a_c_rottweiler",
            "a_c_shepherd",
            "a_c_westy"
        };
        public static bool customCountryVehicles = false;
        public static List<string> CountryVehicleModels = new List<string>
        {
            "ambulance",
            "firetruk",
            "sheriff",
            "sheriff2",
            "biff",
            "mule",
            "mule2",
            "mule3",
            "mule4",
            "pounder",
            "pounder2",
            "stockade",
            "rhapsody",
            "mixer",
            "mixer2",
            "rubble",
            "tiptruck",
            "tiptruck2",
            "blade",
            "buccaneer",
            "chino",
            "clique",
            "coquette3",
            "deviant",
            "dukes",
            "faction",
            "faction2",
            "faction3",
            "ellie",
            "impaler",
            "imperator",
            "picador",
            "ratloader",
            "tampa",
            "tulip",
            "vamos",
            "vigero",
            "virgo",
            "virgo2",
            "virgo3",
            "voodoo",
            "voodoo2",
            "yosemite",
            "bfinjection",
            "bodhi2",
            "dloader",
            "everon",
            "mesa3",
            "rancherxl",
            "rebel",
            "rebel2",
            "sandking",
            "sandking2",
            "dubsta2",
            "granger",
            "mesa",
            "patriot",
            "emperor",
            "emperor2",
            "glendale",
            "ingot",
            "regina",
            "warrener",
            "taxi"
        };
        public static bool customCityVehicles = false;
        public static List<string> CityVehicleModels = new List<string>
        {
            "ambulance",
            "fbi",
            "fbi2",
            "firetruk",
            "police1",
            "police2",
            "police3",
            "police4",
            "policet",
            "riot",
            "riot2",
            "biff",
            "mule",
            "mule2",
            "mule3",
            "mule4",
            "pounder",
            "pounder2",
            "stockade",
            "asbo",
            "blista",
            "dilettante",
            "kanjo",
            "panto",
            "prairie",
            "cogcabrio",
            "exemplar",
            "f620",
            "felon",
            "jackal",
            "oracle",
            "oracle2",
            "sentinel",
            "sentinel2",
            "windsor",
            "windsor2",
            "zion",
            "guardian",
            "deviant",
            "dominator3",
            "moonbeam",
            "nightshade",
            "dubsta3",
            "freecrawler",
            "hellion",
            "mesa3",
            "baller",
            "baller2",
            "baller3",
            "baller4",
            "baller5",
            "baller6",
            "bjxl",
            "cavalcade",
            "cavalcade2",
            "contender",
            "dubsta",
            "dubsta2",
            "granger",
            "patriot",
            "rebla",
            "toros",
            "asea",
            "asterope",
            "cog55",
            "cog552",
            "cognoscenti",
            "cognoscenti2",
            "emperor",
            "fugitive",
            "taxi"
        };
        public static bool enableSafeZones = false;
        public static List<string> SafeZones = new List<string>
        {
            "Los Santos International Airport",
            "Fort Zancudo",
            "Bolingbroke Penitentiary",
            "Davis Quartz",
            "Palmer-Taylor Power Station",
            "RON Alternates Wind Farm",
            "Terminal",
            "Humane Labs and Research"
        };
        public static List<string> CityZones = new List<string>
        {
            "Los Santos International Airport",
            "Elysian Island",
            "Terminal",
            "El Burro Heights",
            "Murrieta Heights",
            "Cypress Flats",
            "Banning",
            "Port of South Los Santos",
            "Maze Bank Arena",
            "La Puerta",
            "Vespucci Beach",
            "Vespucci",
            "Vespucci Canals",
            "Little Seoul",
            "Strawberry",
            "Chamberlain Hills",
            "Davis",
            "Rancho",
            "La Mesa",
            "Mission Row",
            "Legion Square",
            "Pillbox Hill",
            "Del Perro Beach",
            "Del Perro",
            "Richards Majestic",
            "Downtown",
            "Downtown Vinewood",
            "Vinewood",
            "Mirror Park",
            "East Vinewood",
            "Alta",
            "Hawick",
            "Burton",
            "Rockford Hills",
            "Morningwood",
            "Pacific Bluffs",
            "Richman",
            "GWC and Golfing Society",
            "West Vinewood",
            "Vinewood Racetrack",
        };

        public Population()
        {
            instance = this;
            startingLoc = new Vector3(478.8616f, -921.53f, 38.77953f);
            zombieList = new List<ZombiePed>();
            survivorList = new List<SurvivorPed>();
            animalList = new List<Ped>();
        }

        public void Populate()
        {
            ClearUnlistedPeds();
            PopulateZombies();
            if (spawnVehicles)
            {
                PopulateVehicles();
            }
            if (spawnSurvivors)
            {
                PopulateSurvivors();
            }
            if (spawnAnimals)
            {
                PopulateAnimals();
            }
        }

        public static bool IsSafeZone(Vector3 position)
        {
            return SafeZones.Exists(a => a.Equals(World.GetZoneName(position)));
        }

        public static bool IsCityZone(Vector3 position)
        {
            return CityZones.Exists(a => a.Equals(World.GetZoneName(position)));
        }

        public void ClearUnlistedPeds()
        {
            clearRanThisFrame = false;
            clearTicksSinceLastUpdate++;
            if (!clearRanThisFrame)
            {
                if (clearTicksSinceLastUpdate >= clearTicksBetweenUpdates)
                {
                    Ped[] all_ped = World.GetAllPeds();
                    if (all_ped.Length > 0)
                    {
                        foreach (Ped ped in all_ped)
                        {
                            bool Found = false;
                            for (int i = 0; i < zombieList.Count; i++)
                            {
                                if (zombieList[i].pedEntity?.IsDead != false)
                                {
                                    zombieList.RemoveAt(i);
                                    continue;
                                }
                                if (zombieList[i].pedEntity == ped)
                                {
                                    Found = true;
                                    break;
                                }
                            }
                            if (!Found)
                            {
                                for (int i = 0; i < survivorList.Count; i++)
                                {
                                    if (survivorList[i].pedEntity?.IsDead != false)
                                    {
                                        survivorList.RemoveAt(i);
                                        continue;
                                    }
                                    if (survivorList[i].pedEntity == ped)
                                    {
                                        Found = true;
                                        break;
                                    }
                                }
                            }
                            if (!Found)
                            {
                                for (int i = 0; i < animalList.Count; i++)
                                {
                                    if (animalList[i]?.IsDead != false)
                                    {
                                        animalList.RemoveAt(i);
                                        continue;
                                    }
                                    if (animalList[i] == ped)
                                    {
                                        Found = true;
                                        break;
                                    }
                                }
                            }
                            if (!Found && !ped.IsPlayer && ped.IsAlive)
                            {
                                ped.Delete();
                            }
                        }
                    }
                    clearTicksSinceLastUpdate = 0;
                    clearRanThisFrame = true;
                }
            }
        }

        private static bool IsEnemy(Ped ped)
        {
            return (ped.IsHuman && !ped.IsDead && (int)ped.GetRelationshipWithPed(Game.Player.Character) >= 4) || ped.IsInCombatAgainst(Game.Player.Character);
        }

        public static void Sleep(Vector3 position)
        {
            Ped[] peds = World.GetNearbyPeds(position, 50f).Where(IsEnemy).ToArray();
            if (peds.Length == 0)
            {
                TimeSpan time = World.CurrentDayTime + new TimeSpan(0, 8, 0, 0);
                Game.Player.Character.IsVisible = false;
                Game.Player.CanControlCharacter = false;
                Game.Player.Character.FreezePosition = true;
                Game.FadeScreenOut(2000);
                Script.Wait(2000);
                World.CurrentDayTime = time;
                Game.Player.Character.IsVisible = true;
                Game.Player.CanControlCharacter = true;
                Game.Player.Character.FreezePosition = false;
                Game.Player.Character.ClearBloodDamage();
                Weather randWeather = RandoMath.GetRandomElementFromArray(Map.weathers);
                World.Weather = randWeather;
                Character.currentEnergyLevel = 1f;
                Character.currentHungerLevel -= 0.15f;
                Character.currentThirstLevel -= 0.25f;
                Script.Wait(2000);
                Game.FadeScreenIn(2000);
            }
            else
            {
                UI.Notify("You cannot sleep here as there are ~r~hostiles~w~ nearby!");
            }
        }

        public static Ped Infect(Ped ped)
        {
            ped.Task.ClearAllImmediately();
            ped.Weapons.Drop();
            ped.LeaveGroup();
            ped.RelationshipGroup = Relationships.ZombieGroup;
            ped.BlockPermanentEvents = true;
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped.Handle, 46, true);
            Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "BigHitByVehicle", 0, 1);
            Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "HOSPITAL_8", 0, 1);
            Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "HOSPITAL_9", 0, 1);
            Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "Explosion_Med", 0, 1);
            ped.CanPlayGestures = false;
            Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_ANIMS, ped.Handle, false);
            Function.Call(Hash.SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS, ped.Handle, false);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, ped.Handle, false);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, ped.Handle, true);
            Function.Call(Hash.SET_PED_CAN_EVASIVE_DIVE, ped.Handle, false);
            Function.Call(Hash.SET_PED_PATH_PREFER_TO_AVOID_WATER, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_AVOID_FIRE, ped.Handle, false);
            Function.Call(Hash.SET_PED_ALERTNESS, ped.Handle, 0);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped.Handle, 0, 0);
            ped.DrownsInWater = true;
            ped.DiesInstantlyInWater = true;
            if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped.Handle))
            {
                Function.Call(Hash.STOP_CURRENT_PLAYING_AMBIENT_SPEECH, ped.Handle);
            }
            Function.Call(Hash.STOP_PED_SPEAKING, ped.Handle, 1);
            Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, ped.Handle, true);
            TimeSpan time = World.CurrentDayTime;
            int runnerRnd = time.Hours >= 20 || time.Hours <= 3 ? RandoMath.CachedRandom.Next(0, 100) : RandoMath.CachedRandom.Next(0, 50);
            if (!zombieRunners)
            {
                runnerRnd = 100;
            }
            bool isRunner;
            if (runnerRnd <= 10)
            {
                isRunner = true;
                if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@injured"))
                {
                    Function.Call(Hash.REQUEST_ANIM_SET, "move_m@injured");
                }
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, ped.Handle, "move_m@injured", 1048576000);
            }
            else
            {
                isRunner = false;
                if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@drunk@verydrunk"))
                {
                    Function.Call(Hash.REQUEST_ANIM_SET, "move_m@drunk@verydrunk");
                }
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, ped.Handle, "move_m@drunk@verydrunk", 1048576000);
            }
            ped.CanWrithe = false;
            ped.MaxHealth = zombieHealth;
            ped.Health = ped.MaxHealth;
            ped.Armor = 0;
            ped.Money = 0;
            Function.Call(Hash.SET_PED_SEEING_RANGE, ped.Handle, 150f);
            Function.Call(Hash.SET_PED_HEARING_RANGE, ped.Handle, 300f);
            ped.AlwaysDiesOnLowHealth = false;
            ped.AlwaysKeepTask = true;
            ZombiePed newZombie = new ZombiePed(ped);
            bool couldEnlistWithoutAdding = false;
            for (int i = 0; i < zombieList.Count; i++)
            {
                if (zombieList[i].pedEntity == null)
                {
                    zombieList[i].AttachData(ped);
                    zombieList[i].isRunner = isRunner;
                    newZombie = zombieList[i];
                    couldEnlistWithoutAdding = true;
                    break;
                }
            }
            if (!couldEnlistWithoutAdding)
            {
                zombieList.Add(newZombie);
                zombieList[zombieList.Count - 1].isRunner = isRunner;
            }
            else
            {
                UI.Notify("Zombie not added to list");
            }
            return ped;
        }

        public static void PopulateSurvivors()
        {
            Vector3 spawnPosition = World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(500f), true);
            if (DateTime.UtcNow.Subtract(suvLastSpawnTime) >= TimeSpan.FromMinutes(survivorTime))
            {
                try
                {
                    SurvivorGroupSpawn(spawnPosition);
                    suvLastSpawnTime = DateTime.UtcNow;
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }
            }
        }

        public void PopulateAnimals()
        {
            Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
            animalCount = animalList.Count;
            aniPopRanThisFrame = false;
            aniPopTicksSinceLastUpdate++;
            if (!aniPopRanThisFrame)
            {
                if (aniPopTicksSinceLastUpdate >= aniPopTicksBetweenUpdates)
                {
                    int tempMaxAnimals = maxAnimals;
                    if (IsCityZone(Game.Player.Character.Position))
                    {
                        tempMaxAnimals = maxAnimals * 2;
                    }
                    for (int i = 0; i < tempMaxAnimals; i++)
                    {
                        int rndNum = RandoMath.CachedRandom.Next(1, 101);
                        spawnPosition = rndNum <= 40
                            ? World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(maxSpawnDistance), true)
                            : World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(maxSpawnDistance));
                        Vector3 checkPosition = spawnPosition.Around(5);
                        if (checkPosition.IsOnScreen() || Game.Player.Character.DistanceTo(spawnPosition) < minSpawnDistance || IsSafeZone(checkPosition))
                        {
                            continue;
                        }
                        try
                        {
                            if (animalCount < tempMaxAnimals)
                            {
                                Model model = IsCityZone(spawnPosition)
                                    ? new Model(RandoMath.GetRandomElementFromList(CityAnimalModels))
                                    : new Model(RandoMath.GetRandomElementFromList(CountryAnimalModels));
                                Ped ped = World.CreatePed(model, spawnPosition);
                                ped.RelationshipGroup = Relationships.AnimalGroup;
                                ped.Task.WanderAround();
                                animalList.Add(ped);
                                aniPopTicksSinceLastUpdate = 0 - RandoMath.CachedRandom.Next(aniPopTicksBetweenUpdates / 3);
                                aniPopRanThisFrame = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.ToString());
                        }
                    }
                }
            }
        }

        public void PopulateZombies()
        {
            Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
            zombieCount = zombieList.Count;
            zomPopRanThisFrame = false;
            zomPopTicksSinceLastUpdate++;
            if (!zomPopRanThisFrame)
            {
                if (zomPopTicksSinceLastUpdate >= zomPopTicksBetweenUpdates)
                {
                    int tempMaxZombies = maxZombies;
                    if (IsCityZone(Game.Player.Character.Position))
                    {
                        tempMaxZombies = maxZombies * 2;
                    }
                    for (int i = 0; i < tempMaxZombies; i++)
                    {
                        int rndNum = RandoMath.CachedRandom.Next(1, 101);
                        spawnPosition = rndNum <= 40
                            ? World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(maxSpawnDistance), true)
                            : World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(maxSpawnDistance));
                        Vector3 checkPosition = spawnPosition.Around(5);
                        if (checkPosition.IsOnScreen() || Game.Player.Character.DistanceTo(spawnPosition) < minSpawnDistance || IsSafeZone(checkPosition))
                        {
                            continue;
                        }
                        try
                        {
                            ZombieSpawn(spawnPosition);
                            zomPopTicksSinceLastUpdate = 0 - RandoMath.CachedRandom.Next(zomPopTicksBetweenUpdates / 3);
                            zomPopRanThisFrame = true;
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.ToString());
                        }
                    }
                }
            }
        }

        public void PopulateVehicles()
        {
            Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
            Vehicle[] all_vecs = World.GetAllVehicles();
            vehicleCount = all_vecs.Length;
            vehPopRanThisFrame = false;
            vehPopTicksSinceLastUpdate++;
            if (!vehPopRanThisFrame)
            {
                if (vehPopTicksSinceLastUpdate >= vehPopTicksBetweenUpdates)
                {
                    int tempMaxVehicles = maxVehicles;
                    if (IsCityZone(Game.Player.Character.Position))
                    {
                        tempMaxVehicles = maxVehicles * 2;
                    }
                    for (int i = 0; i < tempMaxVehicles; i++)
                    {
                        spawnPosition = World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(maxSpawnDistance), true);
                        Vector3 checkPosition = spawnPosition.Around(5);
                        if (checkPosition.IsOnScreen() || Game.Player.Character.DistanceTo(spawnPosition) < minSpawnDistance || IsSafeZone(checkPosition))
                        {
                            continue;
                        }
                        try
                        {
                            VehicleSpawn(spawnPosition, RandoMath.RandomHeading());
                            vehPopTicksSinceLastUpdate = 0 - RandoMath.CachedRandom.Next(vehPopTicksBetweenUpdates / 3);
                            vehPopRanThisFrame = true;
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e.ToString());
                        }
                    }
                }
            }
        }

        public static void Update()
        {
            for (int i = 0; i < zombieList.Count; i++)
            {
                if (!zombieList[i].pedEntity.IsAlive || (zombieList[i].pedEntity.DistanceBetween(Game.Player.Character) > maxSpawnDistance) || zombieList[i].pedEntity == null)
                {
                    zombieList.RemoveAt(i);
                    continue;
                }
                else
                {
                    zombieList[i].ticksSinceLastUpdate++;
                    if (zombieList[i].ticksSinceLastUpdate >= zombieList[i].ticksBetweenUpdates)
                    {
                        zombieList[i].Update();
                        zombieList[i].ticksSinceLastUpdate = 0;
                    }
                }
            }
        }

        public static Model GetRandomVehicleModel()
        {
            Model model = IsCityZone(Game.Player.Character.Position)
                ? new Model(RandoMath.GetRandomElementFromList(CityVehicleModels))
                : new Model(RandoMath.GetRandomElementFromList(CountryVehicleModels));
            return model.Request(1500) ? model : null;
        }

        public void VehicleSpawn(Vector3 position, float heading)
        {
            int tempMaxVehicles = maxVehicles;
            if (IsCityZone(Game.Player.Character.Position))
            {
                tempMaxVehicles = maxVehicles * 2;
            }
            if (vehicleCount >= tempMaxVehicles || position == Vector3.Zero || position.DistanceBetweenV3(startingLoc) < minSpawnDistance || position.DistanceBetweenV3(Game.Player.Character.Position) < minSpawnDistance)
            {
                return;
            }
            else
            {
                var model = GetRandomVehicleModel();
                var vehicle = model.SpawnVehicle(position, heading);
                int rnd = RandoMath.CachedRandom.Next(0, 100);
                vehicle.EngineHealth = rnd <= 10 ? 1000.0f : 0.0f;
                vehicle.DirtLevel = 14.0f;
                var vehicleDoors = vehicle.GetDoors();
                for (int i = 0; i < 5; i++)
                {
                    var door = RandoMath.GetRandomElementFromArray(vehicleDoors);
                    vehicle.OpenDoor(door, false, true);
                }
                for (int i = 0; i < 3; i++)
                {
                    List<int> windows = new List<int>();
                    if (Function.Call<bool>(Hash.IS_VEHICLE_WINDOW_INTACT, vehicle.Handle, i))
                    {
                        windows.Add(i);
                    }
                    if (windows.Count > 0)
                    {
                        int window = RandoMath.GetRandomElementFromList(windows);
                        Function.Call(Hash.SMASH_VEHICLE_WINDOW, vehicle.Handle, window);
                    }
                }
            }
        }

        public ZombiePed ZombieSpawn(Vector3 pos)
        {
            int tempMaxZombies = maxZombies;
            if (IsCityZone(Game.Player.Character.Position))
            {
                tempMaxZombies = maxZombies * 2;
            }
            if (zombieCount >= tempMaxZombies || pos == Vector3.Zero || pos.DistanceBetweenV3(startingLoc) < minSpawnDistance || pos.DistanceBetweenV3(Game.Player.Character.Position) < minSpawnDistance)
            {
                return null;
            }
            else
            {
                Ped ped;
                if (customZombies)
                {
                    Model model = new Model(RandoMath.GetRandomElementFromList(ZombieModels));
                    ped = World.CreatePed(model, pos);
                }
                else
                {
                    ped = World.CreateRandomPed(pos);
                }
                Infect(ped);
                ZombiePed newZombie = zombieList.Find(match: a => a.pedEntity == ped);
                return newZombie ?? null;
            }
        }

        public static void SurvivorGroupSpawn(Vector3 pos, GroupType groupType = GroupType.Random, int groupSize = -1, PedTasks pedTasks = PedTasks.Wander)
        {
            if (groupType == GroupType.Random)
            {
                int rndGroupType = RandoMath.CachedRandom.Next(0, 3);
                if (rndGroupType == 0)
                {
                    groupType = GroupType.Friendly;
                }
                if (rndGroupType == 1)
                {
                    groupType = GroupType.Neutral;
                }
                if (rndGroupType == 2)
                {
                    groupType = GroupType.Hostile;
                }
            }
            List<Ped> peds = new List<Ped>();
            PedGroup group = new PedGroup();
            if (groupSize == -1)
            {
                groupSize = RandoMath.CachedRandom.Next(3, 9);
            }
            for (int i = 0; i < groupSize; i++)
            {
                SurvivorPed sPed = SurvivorSpawn(pos);
                if (groupType == GroupType.Friendly)
                {
                    sPed.pedEntity.RelationshipGroup = Relationships.FriendlyGroup;
                    sPed.pedEntity.AddBlip();
                    sPed.pedEntity.CurrentBlip.Color = BlipColor.Blue;
                    sPed.pedEntity.CurrentBlip.Scale = 0.65f;
                    sPed.pedEntity.CurrentBlip.Name = "Friendly";
                }
                else if (groupType == GroupType.Neutral)
                {
                    sPed.pedEntity.RelationshipGroup = Relationships.NeutralGroup;
                    sPed.pedEntity.AddBlip();
                    sPed.pedEntity.CurrentBlip.Color = BlipColor.Yellow;
                    sPed.pedEntity.CurrentBlip.Scale = 0.65f;
                    sPed.pedEntity.CurrentBlip.Name = "Neutral";
                }
                else if (groupType == GroupType.Hostile)
                {
                    sPed.pedEntity.RelationshipGroup = Relationships.HostileGroup;
                    sPed.pedEntity.AddBlip();
                    sPed.pedEntity.CurrentBlip.Color = BlipColor.Red;
                    sPed.pedEntity.CurrentBlip.Scale = 0.65f;
                    sPed.pedEntity.CurrentBlip.Name = "Hostile";
                }
                peds.Add(sPed.pedEntity);
            }
            foreach (Ped ped in peds)
            {
                if (group.MemberCount < 1)
                {
                    group.Add(ped, true);
                }
                else
                {
                    group.Add(ped, false);
                }
            }
            group.FormationType = 0;
            List<Ped> groupPeds = group.ToList(true);
            foreach (Ped ped in groupPeds)
            {
                PlayerGroup.SetPedTasks(ped, pedTasks);
            }
        }

        public static SurvivorPed SurvivorSpawn(Vector3 pos)
        {
            Ped ped;
            if (customSurvivors)
            {
                Model model = new Model(RandoMath.GetRandomElementFromList(SurvivorModels));
                ped = World.CreatePed(model, pos.Around(5f));
            }
            else
            {
                ped = World.CreateRandomPed(pos.Around(5f));
            }
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped.Handle, 0, 0);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, ped.Handle, true);
            Function.Call(Hash.SET_PED_CAN_EVASIVE_DIVE, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_PREFER_TO_AVOID_WATER, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, ped.Handle, true);
            ped.Accuracy = 75;
            ped.DiesInstantlyInWater = false;
            Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, ped.Handle, 1, true);
            int rndWeapon = RandoMath.CachedRandom.Next(0, 11);
            ped.MaxHealth = 100;
            ped.Health = ped.MaxHealth;
            ped.Armor = 70;
            if (rndWeapon < 5)
            {
                ped.Weapons.Give(WeaponHash.SMG, 300, true, true);
                ped.Weapons.Give(WeaponHash.Pistol, 120, true, true);
                ped.Weapons.Give(WeaponHash.Knife, 0, false, true);
            }
            if (rndWeapon >= 5)
            {
                ped.Weapons.Give(WeaponHash.AssaultRifle, 300, true, true);
                ped.Weapons.Give(WeaponHash.CombatPistol, 120, true, true);
                ped.Weapons.Give(WeaponHash.BattleAxe, 0, false, true);
            }
            ped.Money = 0;
            ped.NeverLeavesGroup = true;
            SurvivorPed newSurvivor = new SurvivorPed(ped);
            bool couldEnlistWithoutAdding = false;
            for (int i = 0; i < survivorList.Count; i++)
            {
                if (survivorList[i].pedEntity == null)
                {
                    survivorList[i].AttachData(ped);
                    newSurvivor = survivorList[i];
                    couldEnlistWithoutAdding = true;
                    break;
                }
            }
            if (!couldEnlistWithoutAdding)
            {
                survivorList.Add(newSurvivor);
            }
            else
            {
                UI.Notify("Survivor not added to list");
            }
            return newSurvivor;
        }
    }
}
