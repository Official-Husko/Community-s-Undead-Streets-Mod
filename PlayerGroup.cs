using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CWDM.Collections;
using CWDM.EntityWrappers;
using CWDM.Enums;
using CWDM.Extensions;
using GTA;
using GTA.Native;
using NativeUI;

namespace CWDM
{
    public class PlayerGroup : Script
    {
        public static PedCollection PlayerPedCollection = new PedCollection();
        public static VehicleCollection PlayerVehicleCollection = new VehicleCollection();
        public static List<WeaponData> PlayerWeapons = new List<WeaponData>();
        public static UIMenu GroupMenu;
        private Ped _currentGroupPed;
        private PedTask _taskApply;

        public PlayerGroup()
        {
            GroupMenu = new UIMenu("Manage Group", "");
            Main.MasterMenuPool.Add(GroupMenu);
            AddMenuTasks(GroupMenu);
            AddMenuApplyToPed(GroupMenu);
            AddMenuApplyToAll(GroupMenu);
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.Purple)
            };
            GroupMenu.SetBannerType(banner);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
        }

        public static void LoadVehicleFromVehicleData(VehicleData vehicleData)
        {
            var vehicle =
                Population.CreatePersistentVehicle(vehicleData.Hash, vehicleData.Position, vehicleData.Heading);
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
            var vehicleData = new VehicleData(vehicle.Handle, vehicle.Model.Hash, vehicle.Rotation, vehicle.Position,
                vehicle.PrimaryColor, vehicle.SecondaryColor, vehicle.Health, vehicle.EngineHealth, vehicle.Heading,
                vehicle.FuelLevel);
            PlayerVehicleCollection.Add(vehicleData);
        }

        public static void LoadPedFromPedData(PedData pedData)
        {
            var ped = World.CreatePed(pedData.Hash, pedData.Position);
            Population.SurvivorPeds.Add(new SurvivorPed(ped));
            if (ped != null)
            {
                ped.Rotation = pedData.Rotation;
                pedData.Weapons.ForEach(w => ped.Weapons.Give(w.Hash, w.Ammo, true, true));
                pedData.Handle = ped.Handle;
                ped.Recruit(Game.Player.Character);
                ped.SetTask(pedData.Task);
            }
        }

        public static void SavePlayerWeapons()
        {
            var hashes = from hash in (WeaponHash[]) Enum.GetValues(typeof(WeaponHash))
                where Game.Player.Character.Weapons.HasWeapon(hash)
                select hash;
            var componentHashes = (WeaponComponent[]) Enum.GetValues(typeof(WeaponComponent));
            var weapons = hashes.ToList().ConvertAll(hash =>
            {
                var weapon = Game.Player.Character.Weapons[hash];
                var components = (from h in componentHashes
                    where weapon.IsComponentActive(h)
                    select h).ToArray();
                return new WeaponData(weapon.Ammo, weapon.Hash, components);
            }).ToList();
            PlayerWeapons = weapons;
        }

        public static void LoadPlayerWeapons()
        {
            PlayerWeapons.ForEach(w => Game.Player.Character.Weapons.Give(w.Hash, w.Ammo, true, true));
        }

        public static void AddPedData(Ped ped)
        {
            var survivorPed = Population.SurvivorPeds.Find(a => a.PedEntity == ped);
            var pedTasks = survivorPed.Task;
            var hashes = from hash in (WeaponHash[]) Enum.GetValues(typeof(WeaponHash))
                where ped.Weapons.HasWeapon(hash)
                select hash;
            var componentHashes = (WeaponComponent[]) Enum.GetValues(typeof(WeaponComponent));
            var weapons = hashes.ToList().ConvertAll(hash =>
            {
                var weapon = ped.Weapons[hash];
                var components = (from h in componentHashes
                    where weapon.IsComponentActive(h)
                    select h).ToArray();
                return new WeaponData(weapon.Ammo, weapon.Hash, components);
            }).ToList();
            var pedData = new PedData(ped.Handle, ped.Model.Hash, ped.Rotation, ped.Position, pedTasks, weapons);
            PlayerPedCollection.Add(pedData);
        }

        public void AddMenuTasks(UIMenu menu)
        {
            var tasks = new List<dynamic>
            {
                "None",
                "Wander",
                "Guard",
                "Follow",
                "Leave"
            };
            var newitem = new UIMenuListItem("Task", tasks, 0, "Select a task for your Group member(s)");
            menu.AddItem(newitem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    var taskString = item.Items[index].ToString();
                    if (taskString == "None")
                        _taskApply = PedTask.None;
                    else if (taskString == "Wander")
                        _taskApply = PedTask.Wander;
                    else if (taskString == "Guard")
                        _taskApply = PedTask.Guard;
                    else if (taskString == "Follow")
                        _taskApply = PedTask.Follow;
                    else if (taskString == "Leave") _taskApply = PedTask.Leave;
                }
            };
        }

        public void AddMenuApplyToPed(UIMenu menu)
        {
            var newitem = new UIMenuItem("Set Task", "Set task for selected Group member");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    if (_currentGroupPed == null)
                    {
                        UI.Notify("You do not have a ~p~Group ~s~member seleted");
                    }
                    else
                    {
                        _currentGroupPed.SetTask(_taskApply);
                        UI.Notify("You have given seleted task to selected ~p~Group ~s~member");
                    }

                    GroupMenu.Visible = !GroupMenu.Visible;
                }
            };
        }

        public void AddMenuApplyToAll(UIMenu menu)
        {
            var newitem = new UIMenuItem("Set Task (All)", "Set task for all Group members");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    var group = Game.Player.Character.CurrentPedGroup.ToList(false);
                    foreach (var ped in group) ped.SetTask(_taskApply);
                    UI.Notify("You have given seleted task to all ~p~Group ~s~members");
                }

                GroupMenu.Visible = !GroupMenu.Visible;
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (!Main.ModActive) return;
            var ped = World.GetClosest(Game.Player.Character.Position,
                World.GetNearbyPeds(Game.Player.Character, 1.5f));
            switch (ped?.IsDead)
            {
                case false when ped.IsHuman && ped.RelationshipGroup == Relationships.FriendlyGroup:
                {
                    "Press ~INPUT_CONTEXT~ to recruit".DisplayHelpTextThisFrame();
                    Game.DisableControlThisFrame(0, Control.Context);
                    if (!Game.IsDisabledControlJustPressed(2, Control.Context)) return;
                    try
                    {
                        ped.Recruit(Game.Player.Character);
                    }
                    catch (Exception x)
                    {
                        Log.Write(x.ToString());
                    }

                    break;
                }
                case false when ped.IsHuman && ped.RelationshipGroup == Game.Player.Character.RelationshipGroup:
                {
                    "Press ~INPUT_CONTEXT~ to manage Group".DisplayHelpTextThisFrame();
                    Game.DisableControlThisFrame(0, Control.Context);
                    if (Game.IsDisabledControlJustPressed(0, Control.Context) && !Main.MasterMenuPool.IsAnyMenuOpen())
                    {
                        _currentGroupPed = ped;
                        GroupMenu.Visible = !GroupMenu.Visible;
                    }

                    break;
                }
            }
        }
    }
}