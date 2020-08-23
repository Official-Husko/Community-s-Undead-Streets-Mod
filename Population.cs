using CWDM.Enums;
using CWDM.Extensions;
using CWDM.Wrappers;
using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;

namespace CWDM
{
    public class Population : Script
    {
        public static List<ZombiePed> ZombiePeds = new List<ZombiePed>();
        public static List<SurvivorPed> SurvivorPeds = new List<SurvivorPed>();
        public static List<AnimalPed> AnimalPeds = new List<AnimalPed>();
        public static List<VehicleEntity> Vehicles = new List<VehicleEntity>();
        public static List<PropEntity> Props = new List<PropEntity>();
        public static int MaxZombies = 60;
        public static int MaxVehicles = 20;
        public static int MaxAnimals = 10;
        public static bool EnableVehicles = true;
        public static bool EnableAnimals = true;
        public static bool EnableSurvivors = true;
        public static bool EnableCityPopulationDifference = true;
        public static bool EnableSurvivorVehicles = true;
        public static bool FastZombies = false;
        public static int SurvivorTimeMins = 3;
        public static int ZombieHealth = 750;
        public static int ZombieDamage = 25;
        public static bool ZombieInfection = false;
        public static bool ZombieInstakill = false;
        public static int SurvivorHealth = 100;
        public static int SurvivorArmor = 50;
        public static int MinFriendlyGroup = 2;
        public static int MaxFriendlyGroup = 4;
        public static int MinNeutralGroup = 2;
        public static int MaxNeutralGroup = 4;
        public static int MinHostileGroup = 2;
        public static int MaxHostileGroup = 4;
        public static float MinSpawnDistance = 50f;
        public static float MaxSpawnDistance = 75f;
        public static DateTime SurvivorLastSpawnTime = new DateTime();
        public static bool CustomZombies = false;

        public static List<string> ZombieModels = new List<string>
        {
            "u_f_y_corpse_01",
            "u_m_y_corpse_01",
            "u_m_y_zombie_01"
        };

        public static bool CustomSurvivors = false;

        public static List<string> SurvivorModels = new List<string>
        {
            "mp_m_waremech_01",
            "mp_m_weapexp_01",
            "mp_m_weapwork_01",
            "s_m_y_xmech_02"
        };

        public static bool CustomCountryAnimals = false;

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

        public static bool CustomCityAnimals = false;

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

        public static bool CustomCountryVehicles = false;

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

        public static bool CustomCityVehicles = false;

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

        public static bool CustomFriendlyVehicles = false;

        public static List<string> FriendlyVehicleModels = new List<string>
        {
            "insurgent2",
            "mesa3",
            "contender",
            "baller5",
            "dubsta2",
            "xls2",
            "guardian"
        };

        public static bool CustomNeutralVehicles = false;

        public static List<string> NeutralVehicleModels = new List<string>
        {
            "insurgent2",
            "mesa3",
            "contender",
            "baller5",
            "dubsta2",
            "xls2",
            "guardian"
        };

        public static bool CustomHostileVehicles = false;

        public static List<string> HostileVehicleModels = new List<string>
        {
            "insurgent2",
            "mesa3",
            "contender",
            "baller5",
            "dubsta2",
            "xls2",
            "guardian"
        };

        public static bool EnableSafeZones = false;

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
            Interval = 100;
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (!Main.ModActive || !Map.MapPrepared || !Character.CharacterReset) return;
            PopulateZombies();
            PopulateVehicles();
            PopulateAnimals();
            PopulateSurvivors();
        }

        public static bool IsSafeZone(Vector3 position)
        {
            if (!EnableSafeZones)
            {
                return false;
            }
            return SafeZones.Exists(a => a.Equals(World.GetZoneName(position)));
        }

        public static bool IsCityZone(Vector3 position)
        {
            return CityZones.Exists(a => a.Equals(World.GetZoneName(position)));
        }

        public static void PopulateZombies()
        {
            Vector3 spawnPosition;
            int zoneMaxZombies = MaxZombies;
            if (EnableCityPopulationDifference && !IsCityZone(Game.Player.Character.Position))
            {
                zoneMaxZombies = MaxZombies / 2;
            }
            Ped[] peds = World.GetAllPeds();
            List<Ped> pedsList = new List<Ped>(peds);
            for (int i = 0; i < pedsList.Count; i++)
            {
                if (pedsList[i].RelationshipGroup != Relationships.ZombieGroup)
                {
                    pedsList.RemoveAt(i);
                }
            }
            for (int i = 0; i < zoneMaxZombies; i++)
            {
                if (pedsList.Count >= zoneMaxZombies)
                {
                    continue;
                }
                Random random = new Random();
                int chance = random.Next(0, 1);
                if (chance == 0)
                {
                    spawnPosition = World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(MaxSpawnDistance), true);
                }
                else
                {
                    spawnPosition = World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(MaxSpawnDistance));
                }
                Vector3 checkPosition = spawnPosition.Around(5);
                if (spawnPosition == Vector3.Zero || checkPosition.IsOnScreen() || (float)Game.Player.Character.DistanceTo(spawnPosition) < MinSpawnDistance || IsSafeZone(checkPosition))
                {
                    continue;
                }
                try
                {
                    ZombiePeds.Add(SpawnZombie(spawnPosition));
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void PopulateVehicles()
        {
            if (!EnableVehicles) return;
            Vector3 spawnPosition;
            int zoneMaxVehicles = MaxVehicles;
            if (EnableCityPopulationDifference && !IsCityZone(Game.Player.Character.Position))
            {
                zoneMaxVehicles = MaxVehicles / 2;
            }
            Vehicle[] vehicles = World.GetAllVehicles();
            List<Vehicle> vehiclesList = new List<Vehicle>(vehicles);
            for (int i = 0; i < zoneMaxVehicles; i++)
            {
                if (vehiclesList.Count >= zoneMaxVehicles)
                {
                    continue;
                }
                spawnPosition = World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(MaxSpawnDistance), true);
                Vector3 checkPosition = spawnPosition.Around(5);
                if (spawnPosition == Vector3.Zero || checkPosition.IsOnScreen() || (float)Game.Player.Character.DistanceTo(spawnPosition) < MinSpawnDistance || IsSafeZone(checkPosition))
                {
                    continue;
                }
                try
                {
                    Vehicles.Add(SpawnVehicle(spawnPosition));
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void PopulateAnimals()
        {
            if (!EnableAnimals) return;
            Vector3 spawnPosition;
            int zoneMaxAnimals = MaxAnimals;
            if (EnableCityPopulationDifference)
            {
                if (!IsCityZone(Game.Player.Character.Position))
                {
                    zoneMaxAnimals = MaxAnimals / 2;
                }
            }
            Ped[] peds = World.GetAllPeds();
            List<Ped> pedsList = new List<Ped>(peds);
            for (int i = 0; i < pedsList.Count; i++)
            {
                if (pedsList[i].RelationshipGroup != Relationships.AnimalGroup)
                {
                    pedsList.RemoveAt(i);
                }
            }
            for (int i = 0; i < zoneMaxAnimals; i++)
            {
                if (pedsList.Count >= zoneMaxAnimals)
                {
                    continue;
                }
                Random random = new Random();
                int chance = random.Next(0, 1);
                spawnPosition = chance == 0 ? World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(MaxSpawnDistance), true) : World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(MaxSpawnDistance));
                Vector3 checkPosition = spawnPosition.Around(5);
                if (spawnPosition == Vector3.Zero || checkPosition.IsOnScreen() || (float)Game.Player.Character.DistanceTo(spawnPosition) < MinSpawnDistance || IsSafeZone(checkPosition))
                {
                    continue;
                }
                try
                {
                    AnimalPeds.Add(SpawnAnimal(spawnPosition));
                }
                catch (Exception x)
                {
                    Log.Write(x.ToString());
                }
            }
        }

        public static void PopulateSurvivors()
        {
            if (!EnableSurvivors) return;
            TimeSpan timeSpan = new TimeSpan(0, SurvivorTimeMins, 0);
            DateTime checkTime = SurvivorLastSpawnTime.Add(timeSpan);
            if (checkTime >= DateTime.UtcNow) return;
            SurvivorLastSpawnTime = DateTime.UtcNow;
            Vector3 spawnPosition;
            bool inVehicle = false;
            if (EnableSurvivorVehicles)
            {
                Random random1 = new Random();
                int chance1 = random1.Next(0, 10);
                if (chance1 > 6)
                {
                    inVehicle = true;
                }
            }
            GroupType groupType = GroupType.Friendly;
            Random random2 = new Random();
            switch (random2.Next(0, 3))
            {
                case 0:
                    groupType = GroupType.Friendly;
                    break;

                case 1:
                    groupType = GroupType.Neutral;
                    break;

                case 2:
                    groupType = GroupType.Hostile;
                    break;
            }
            int groupSize = 1;
            Random random3 = new Random();
            switch (groupType)
            {
                case GroupType.Friendly:
                    groupSize = random3.Next(MinFriendlyGroup, MaxFriendlyGroup);
                    break;

                case GroupType.Neutral:
                    groupSize = random3.Next(MinNeutralGroup, MaxNeutralGroup);
                    break;

                case GroupType.Hostile:
                    groupSize = random3.Next(MinHostileGroup, MaxHostileGroup);
                    break;
            }
            Random random4 = new Random();
            int chance4 = random4.Next(0, 2);
            spawnPosition = chance4 == 0 ? World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(MaxSpawnDistance * 2), true) : World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(MaxSpawnDistance * 2));
            Vector3 checkPosition = spawnPosition.Around(5);
            if (spawnPosition == Vector3.Zero || checkPosition.IsOnScreen() || (float)Game.Player.Character.DistanceTo(spawnPosition) < MinSpawnDistance || IsSafeZone(checkPosition))
            {
                return;
            }
            try
            {
                SpawnSurvivorGroup(groupType, groupSize, spawnPosition, inVehicle);
            }
            catch (Exception x)
            {
                Log.Write(x.ToString());
            }
        }

        public static void SpawnSurvivorGroup(GroupType groupType, int groupSize, Vector3 position, bool inVehicle = false)
        {
            List<Ped> peds = new List<Ped>();
            PedGroup pedGroup = new PedGroup();
            for (int i = 0; i < groupSize; i++)
            {
                SurvivorPeds.Add(SpawnSurvivor(position.Around(5f)));
                peds.Add(SurvivorPeds[SurvivorPeds.Count - 1].pedEntity);
            }
            for (int i = 0; i < peds.Count; i++)
            {
                switch (groupType)
                {
                    case GroupType.Friendly:
                        {
                            peds[i].RelationshipGroup = Relationships.FriendlyGroup;
                            Blip blip = peds[i].AddBlip();
                            blip.Color = BlipColor.Blue;
                            blip.Scale = 0.65f;
                            blip.Name = "Friendly";
                            break;
                        }
                    case GroupType.Neutral:
                        {
                            peds[i].RelationshipGroup = Relationships.NeutralGroup;
                            Blip blip = peds[i].AddBlip();
                            blip.Color = BlipColor.Yellow;
                            blip.Scale = 0.65f;
                            blip.Name = "Neutral";
                            break;
                        }
                    case GroupType.Hostile:
                        {
                            peds[i].RelationshipGroup = Relationships.HostileGroup;
                            Blip blip = peds[i].AddBlip();
                            blip.Color = BlipColor.Red;
                            blip.Scale = 0.65f;
                            blip.Name = "Hostile";
                            break;
                        }
                }

                if (i == 0)
                {
                    pedGroup.Add(peds[i], true);
                    peds[i].SetTask(PedTask.Wander);
                }
                else
                {
                    pedGroup.Add(peds[i], false);
                    peds[i].SetTask(PedTask.Follow);
                }
            }

            if (!inVehicle) return;
            {
                Model model = GetRandomVehicleModel();
                switch (groupType)
                {
                    case GroupType.Friendly:
                        model = GetRandomFriendlyVehicleModel();
                        break;

                    case GroupType.Neutral:
                        model = GetRandomNeutralVehicleModel();
                        break;

                    case GroupType.Hostile:
                        model = GetRandomHostileVehicleModel();
                        break;
                }

                Vehicles.Add(new VehicleEntity(CreatePersistentVehicle(model, World.GetNextPositionOnStreet(position), MathExtensions.RandomHeading())));
                Vehicle vehicle = Vehicles[Vehicles.Count - 1].vehicle;
                for (int i = 0; i < peds.Count; i++)
                {
                    if (i == 0)
                    {
                        if (vehicle.IsSeatFree(VehicleSeat.Driver))
                        {
                            peds[i].SetIntoVehicle(vehicle, VehicleSeat.Driver);
                        }
                    }
                    else
                    {
                        if (vehicle.IsSeatFree(VehicleSeat.Any))
                        {
                            peds[i].SetIntoVehicle(vehicle, VehicleSeat.Any);
                        }
                    }
                }
                peds[0].Task.CruiseWithVehicle(vehicle, 15f);
            }
        }

        public static SurvivorPed SpawnSurvivor(Vector3 position)
        {
            Ped ped;
            ped = CustomSurvivors ? World.CreatePed(GetRandomSurvivorModel(), position) : World.CreateRandomPed(position);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped.Handle, 0, 0);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_CLIMBOVERS, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_CAN_USE_LADDERS, ped.Handle, true);
            Function.Call(Hash.SET_PED_CAN_EVASIVE_DIVE, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_PREFER_TO_AVOID_WATER, ped.Handle, true);
            Function.Call(Hash.SET_PED_PATH_CAN_DROP_FROM_HEIGHT, ped.Handle, true);
            ped.Accuracy = 75;
            ped.DiesInstantlyInWater = false;
            Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, ped.Handle, 1, true);
            Random random = new Random();
            int rndWeapon = random.Next(0, 10);
            ped.MaxHealth = SurvivorHealth;
            ped.Health = ped.MaxHealth;
            ped.Armor = SurvivorArmor;
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
            ped.Weapons.Select(ped.Weapons.BestWeapon.Hash, true);
            ped.Money = 0;
            ped.NeverLeavesGroup = true;
            return new SurvivorPed(ped);
        }

        public static Model GetRandomFriendlyVehicleModel()
        {
            return new Model(FriendlyVehicleModels.GetRandomElementFromList());
        }

        public static Model GetRandomNeutralVehicleModel()
        {
            return new Model(NeutralVehicleModels.GetRandomElementFromList());
        }

        public static Model GetRandomHostileVehicleModel()
        {
            return new Model(HostileVehicleModels.GetRandomElementFromList());
        }

        public static Model GetRandomZombieModel()
        {
            return new Model(ZombieModels.GetRandomElementFromList());
        }

        public static Model GetRandomSurvivorModel()
        {
            Model model = new Model(SurvivorModels.GetRandomElementFromList());
            return model.Request(1500) ? model : null;
        }

        public static Model GetRandomAnimalModel()
        {
            Model model;
            model = IsCityZone(Game.Player.Character.Position) ? new Model(CityAnimalModels.GetRandomElementFromList()) : new Model(CountryAnimalModels.GetRandomElementFromList());
            return model.Request(1500) ? model : null;
        }

        public static Model GetRandomVehicleModel()
        {
            Model model;
            model = IsCityZone(Game.Player.Character.Position) ? new Model(CityVehicleModels.GetRandomElementFromList()) : new Model(CountryVehicleModels.GetRandomElementFromList());
            return model.Request(1500) ? model : null;
        }

        public static Vehicle CreatePersistentVehicle(Model model, Vector3 position, float heading = 0.0f)
        {
            if (!model.IsVehicle || !model.Request(1000))
            {
                return null;
            }
            Vehicle vehicle = Function.Call<Vehicle>(Hash.CREATE_VEHICLE, model.Hash, position.X, position.Y, position.Z, heading, false, false);
            Function.Call(Hash.SET_VEHICLE_ON_GROUND_PROPERLY, vehicle);
            Function.Call(Hash.SET_ENTITY_AS_MISSION_ENTITY, vehicle, true, false);
            return vehicle;
        }

        public static AnimalPed SpawnAnimal(Vector3 position)
        {
            Model model = GetRandomAnimalModel();
            Ped animal = World.CreatePed(model, position);
            animal.RelationshipGroup = Relationships.AnimalGroup;
            AnimalPed animalPed = new AnimalPed(animal);
            animal.Task.WanderAround();
            return animalPed;
        }

        public static VehicleEntity SpawnVehicle(Vector3 position)
        {
            Model model = GetRandomVehicleModel();
            float heading = MathExtensions.RandomHeading();
            Vehicle vehicle = CreatePersistentVehicle(model, position, heading);
            VehicleEntity vehicleEntity = new VehicleEntity(vehicle);
            vehicle.SmashWindow(VehicleWindow.BackLeftWindow);
            vehicle.SmashWindow(VehicleWindow.BackRightWindow);
            vehicle.SmashWindow(VehicleWindow.FrontLeftWindow);
            vehicle.SmashWindow(VehicleWindow.FrontRightWindow);
            vehicle.OpenDoor(vehicle.GetRandomDoor(), false, true);
            vehicle.OpenDoor(vehicle.GetRandomDoor(), false, true);
            vehicle.OpenDoor(vehicle.GetRandomDoor(), false, true);
            vehicle.OpenDoor(vehicle.GetRandomDoor(), false, true);
            Random random = new Random();
            int chance = random.Next(0, 100);
            if (chance > 15)
            {
                vehicle.EngineHealth = 0f;
            }
            return vehicleEntity;
        }

        public static ZombiePed SpawnZombie(Vector3 position)
        {
            Ped ped;
            if (CustomZombies)
            {
                Model model = GetRandomZombieModel();
                ped = World.CreatePed(model, position);
            }
            else
            {
                ped = World.CreateRandomPed(position);
            }
            Infect(ped);
            return new ZombiePed(ped);
        }

        public static void Infect(Ped ped)
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
            int runnerRnd;
            TimeSpan time = World.CurrentDayTime;
            if (time.Hours >= 20 || time.Hours <= 3)
            {
                Random random = new Random();
                runnerRnd = random.Next(0, 100);
            }
            else
            {
                Random random = new Random();
                runnerRnd = random.Next(0, 50);
            }
            bool isRunner;
            if (!FastZombies)
            {
                runnerRnd = 100;
            }
            if (runnerRnd <= 10)
            {
                isRunner = true;
                ped.SetMovementAnim("move_m@injured");
            }
            else
            {
                isRunner = false;
                ped.SetMovementAnim("move_m@drunk@verydrunk");
            }
            ped.CanWrithe = false;
            ped.MaxHealth = ZombieHealth;
            ped.Health = ZombieHealth;
            ped.Armor = 0;
            ped.Money = 0;
            ped.SetSeeingRange(35f);
            ped.SetHearingRange(60f);
            ped.AlwaysDiesOnLowHealth = false;
            ped.AlwaysKeepTask = true;
            ZombiePed newZombie = new ZombiePed(ped);
            if (isRunner)
            {
                newZombie.isRunner = true;
            }
            if (!ZombiePeds.Exists(match: a => a.pedEntity == ped))
            {
                ZombiePeds.Add(newZombie);
            }
        }
    }
}