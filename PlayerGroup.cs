using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CWDM.Collections;
using CWDM.Enums;
using CWDM.Extensions;
using CWDM.Wrappers;

namespace CWDM
{
    public class PlayerGroup : Script
    {
        public static PedCollection PlayerPedCollection = new PedCollection();
        public static VehicleCollection PlayerVehicleCollection = new VehicleCollection();
        public static List<WeaponData> PlayerWeapons = new List<WeaponData>();
        public static UIMenu GroupMenu;
        private Ped currentGroupPed;
        private PedTask taskApply;

        public PlayerGroup()
        {
            GroupMenu = new UIMenu("Manage Group", "");
            Main.MasterMenuPool.Add(GroupMenu);
            AddMenuTasks(GroupMenu);
            AddMenuApplyToPed(GroupMenu);
            AddMenuApplyToAll(GroupMenu);
            UIResRectangle banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.Purple)
            };
            GroupMenu.SetBannerType(banner);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
        }

        public static void LoadVehicleFromVehicleData(VehicleData vehicleData)
        {
            Vehicle vehicle = Population.CreatePersistentVehicle(vehicleData.Hash, vehicleData.Position, vehicleData.Heading);
            Population.Vehicles.Add(new VehicleEntity(vehicle));
            if (vehicle != null)
            {
                vehicle.Rotation = vehicleData.Rotation;
                vehicle.Health = vehicleData.Health;
                vehicle.EngineHealth = vehicleData.EngineHealth;
                vehicle.PrimaryColor = vehicleData.PrimaryColor;
                vehicle.SecondaryColor = vehicleData.SecondaryColor;
            }
        }

        public static void AddVehicleData(Vehicle vehicle)
        {
            VehicleData vehicleData = new VehicleData(vehicle.Handle, vehicle.Model.Hash, vehicle.Rotation, vehicle.Position, vehicle.PrimaryColor, vehicle.SecondaryColor, vehicle.Health, vehicle.EngineHealth, vehicle.Heading, vehicle.FuelLevel);
            PlayerVehicleCollection.Add(vehicleData);
        }

        public static void LoadPedFromPedData(PedData pedData)
        {
            Ped ped = World.CreatePed(pedData.Hash, pedData.Position);
            Population.SurvivorPeds.Add(new SurvivorPed(ped));
            if (ped != null)
            {
                ped.Rotation = pedData.Rotation;
                pedData.Weapons.ForEach((WeaponData w) => ped.Weapons.Give(w.Hash, w.Ammo, true, true));
                pedData.Handle = ped.Handle;
                ped.Recruit(Game.Player.Character);
                ped.SetTask(pedData.Task);
            }
        }

        public static void SavePlayerWeapons()
        {
            IEnumerable<WeaponHash> hashes = from hash in (WeaponHash[])Enum.GetValues(typeof(WeaponHash))
                                             where Game.Player.Character.Weapons.HasWeapon(hash)
                                             select hash;
            WeaponComponent[] componentHashes = (WeaponComponent[])Enum.GetValues(typeof(WeaponComponent));
            List<WeaponData> weapons = hashes.ToList().ConvertAll((WeaponHash hash) =>
            {
                Weapon weapon = Game.Player.Character.Weapons[hash];
                WeaponComponent[] components = (from h in componentHashes
                                                where weapon.IsComponentActive(h)
                                                select h).ToArray();
                return new WeaponData(weapon.Ammo, weapon.Hash, components);
            }).ToList();
            PlayerWeapons = weapons;
        }

        public static void LoadPlayerWeapons()
        {
            PlayerWeapons.ForEach((WeaponData w) => Game.Player.Character.Weapons.Give(w.Hash, w.Ammo, true, true));
        }

        public static void AddPedData(Ped ped)
        {
            SurvivorPed survivorPed = Population.SurvivorPeds.Find(match: a => a.pedEntity == ped);
            PedTask pedTasks = survivorPed.task;
            IEnumerable<WeaponHash> hashes = from hash in (WeaponHash[])Enum.GetValues(typeof(WeaponHash))
                                             where ped.Weapons.HasWeapon(hash)
                                             select hash;
            WeaponComponent[] componentHashes = (WeaponComponent[])Enum.GetValues(typeof(WeaponComponent));
            List<WeaponData> weapons = hashes.ToList().ConvertAll((WeaponHash hash) =>
            {
                Weapon weapon = ped.Weapons[hash];
                WeaponComponent[] components = (from h in componentHashes
                                                where weapon.IsComponentActive(h)
                                                select h).ToArray();
                return new WeaponData(weapon.Ammo, weapon.Hash, components);
            }).ToList();
            PedData pedData = new PedData(ped.Handle, ped.Model.Hash, ped.Rotation, ped.Position, pedTasks, weapons);
            PlayerPedCollection.Add(pedData);
        }

        public void AddMenuTasks(UIMenu menu)
        {
            List<dynamic> tasks = new List<dynamic>
            {
                "None",
                "Wander",
                "Guard",
                "Follow",
                "Leave"
             };
            UIMenuListItem newitem = new UIMenuListItem("Task", tasks, 0, "Select a task for your Group member(s)");
            menu.AddItem(newitem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    string taskString = item.Items[index].ToString();
                    if (taskString == "None")
                    {
                        taskApply = PedTask.None;
                    }
                    else if (taskString == "Wander")
                    {
                        taskApply = PedTask.Wander;
                    }
                    else if (taskString == "Guard")
                    {
                        taskApply = PedTask.Guard;
                    }
                    else if (taskString == "Follow")
                    {
                        taskApply = PedTask.Follow;
                    }
                    else if (taskString == "Leave")
                    {
                        taskApply = PedTask.Leave;
                    }
                }
            };
        }

        public void AddMenuApplyToPed(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Set Task", "Set task for selected Group member");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    if (currentGroupPed == null)
                    {
                        UI.Notify("You do not have a ~p~Group ~s~member seleted");
                    }
                    else
                    {
                        currentGroupPed.SetTask(taskApply);
                        UI.Notify("You have given seleted task to selected ~p~Group ~s~member");
                    }
                    GroupMenu.Visible = !GroupMenu.Visible;
                }
            };
        }

        public void AddMenuApplyToAll(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Set Task (All)", "Set task for all Group members");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    List<Ped> group = Game.Player.Character.CurrentPedGroup.ToList(false);
                    foreach (Ped ped in group)
                    {
                        ped.SetTask(taskApply);
                    }
                    UI.Notify("You have given seleted task to all ~p~Group ~s~members");
                }
                GroupMenu.Visible = !GroupMenu.Visible;
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                Ped ped = World.GetClosest(Game.Player.Character.Position, World.GetNearbyPeds(Game.Player.Character, 1.5f));
                if (ped?.IsDead == false && ped.IsHuman && ped.RelationshipGroup == Relationships.FriendlyGroup)
                {
                    UIExtensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to recruit");
                    Game.DisableControlThisFrame(0, GTA.Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, GTA.Control.Context))
                    {
                        try
                        {
                            ped.Recruit(Game.Player.Character);
                        }
                        catch (Exception x)
                        {
                            Log.Write(x.ToString());
                        }
                    }
                }
                else if (ped?.IsDead == false && ped.IsHuman && ped.RelationshipGroup == Game.Player.Character.RelationshipGroup)
                {
                    UIExtensions.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to manage Group");
                    Game.DisableControlThisFrame(0, GTA.Control.Context);
                    if (Game.IsDisabledControlJustPressed(0, GTA.Control.Context) && !Main.MasterMenuPool.IsAnyMenuOpen())
                    {
                        currentGroupPed = ped;
                        GroupMenu.Visible = !GroupMenu.Visible;
                    }
                }
            }
        }
    }
}
