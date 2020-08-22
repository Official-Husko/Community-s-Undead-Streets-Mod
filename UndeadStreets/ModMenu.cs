using System;
using System.Drawing;
using GTA;
using NativeUI;

namespace CWDM
{
    public class ModMenu : Script
    {
        private readonly UIMenu mainMenu;

        public static void AddMenuSaveInventory(UIMenu menu)
        {
            var newitem = new UIMenuItem("Save Inventory", "Save your current inventory");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.SaveInventory();
                }
            };
        }

        public static void AddMenuLoadInventory(UIMenu menu)
        {
            var newitem = new UIMenuItem("Load Inventory", "Load saved inventory (This will wipe your current inventory)");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.LoadInventory();
                }
            };
        }

        public static void AddMenuRegisterVehicle(UIMenu menu)
        {
            var newitem = new UIMenuItem("Register Vehicle", "Set current vehicle as Personal Vehicle");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.RegisterVehicle();
                }
            };
        }

        public static void AddMenuSaveWeapons(UIMenu menu)
        {
            var newitem = new UIMenuItem("Save Weapons", "Save your current Weapons and Ammo");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.SaveWeapons();
                }
            };
        }

        public static void AddMenuLoadWeapons(UIMenu menu)
        {
            var newitem = new UIMenuItem("Load Weapons", "Load saved Weapons and Ammo (This will wipe your current Weapons and Ammo)");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.LoadWeapons();
                }
            };
        }

        public static void AddMenuSaveVehicle(UIMenu menu)
        {
            var newitem = new UIMenuItem("Save Vehicle", "Save your current Personal Vehicle");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.SaveVehicle();
                }
            };
        }

        public static void AddMenuLoadVehicle(UIMenu menu)
        {
            var newitem = new UIMenuItem("Load Vehicle", "Load saved Personal Vehicle (This will wipe your current Personal Vehicle)");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.LoadVehicle();
                }
            };
        }

        public static void AddMenuSaveGroup(UIMenu menu)
        {
            var newitem = new UIMenuItem("Save Group", "Save your current group");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.SaveGroup();
                }
            };
        }

        public static void AddMenuLoadGroup(UIMenu menu)
        {
            var newitem = new UIMenuItem("Load Group", "Load saved group (This will wipe your current group)");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    Config.LoadGroup();
                }
            };
        }

        public ModMenu()
        {
            mainMenu = new UIMenu("Undead Streets", "");
            Main.MasterMenuPool.Add(mainMenu);
            AddMenuSaveInventory(mainMenu);
            AddMenuLoadInventory(mainMenu);
            AddMenuSaveWeapons(mainMenu);
            AddMenuLoadWeapons(mainMenu);
            AddMenuRegisterVehicle(mainMenu);
            AddMenuSaveVehicle(mainMenu);
            AddMenuLoadVehicle(mainMenu);
            AddMenuSaveGroup(mainMenu);
            AddMenuLoadGroup(mainMenu);
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.DarkRed)
            };
            mainMenu.SetBannerType(banner);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
            KeyDown += (o, e) =>
            {
                if (e.KeyCode == Main.MenuKey && Main.ModActive && !Main.MasterMenuPool.IsAnyMenuOpen())
                {
                    mainMenu.Visible = !mainMenu.Visible;
                }
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsDead)
            {
                mainMenu.Visible = false;
            }
            if (Main.ModActive)
            {
                Game.DisableControlThisFrame(2, GTA.Control.CharacterWheel);
                if (Game.IsDisabledControlJustPressed(2, GTA.Control.CharacterWheel))
                {
                    if (!Main.MasterMenuPool.IsAnyMenuOpen())
                    {
                        mainMenu.Visible = !mainMenu.Visible;
                    }
                }
            }
        }
    }
}