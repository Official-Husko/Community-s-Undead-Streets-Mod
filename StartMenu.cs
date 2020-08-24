using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GTA;
using NativeUI;

namespace CWDM
{
    public class StartMenu : Script
    {
        public static bool Runners;
        public static bool Electricity;
        public static bool CharacterStats = true;
        public static UIMenu StartupMenu;

        public StartMenu()
        {
            StartupMenu = new UIMenu("Undead Streets", "Starting Settings");
            AddMenuRunners(StartupMenu);
            AddMenuElectricity(StartupMenu);
            AddMenuStats(StartupMenu);
            AddMenuGender(StartupMenu);
            AddMenuStart(StartupMenu);
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.DarkRed)
            };
            StartupMenu.SetBannerType(banner);
            Main.MasterMenuPool.Add(StartupMenu);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        public static void AddMenuRunners(UIMenu menu)
        {
            var newitem = new UIMenuCheckboxItem("Fast Zombies", Runners, "Enable/Disable fast zombies");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += (sender, item, @checked) =>
            {
                if (item == newitem)
                {
                    Runners = @checked;
                    Population.FastZombies = Runners;
                }
            };
        }

        public static void AddMenuElectricity(UIMenu menu)
        {
            var newitem = new UIMenuCheckboxItem("Electricity", Electricity, "Enable/Disable electricity");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += (sender, item, @checked) =>
            {
                if (item == newitem)
                {
                    Electricity = @checked;
                    Map.Electricity = Electricity;
                }
            };
        }

        public static void AddMenuStats(UIMenu menu)
        {
            var newitem = new UIMenuCheckboxItem("Stats", CharacterStats,
                "Enable/Disable survival stats (hunger, thirst, etc...)");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += (sender, item, @checked) =>
            {
                if (item == newitem)
                {
                    CharacterStats = @checked;
                    Stats.EnableStats = CharacterStats;
                }
            };
        }

        public static void AddMenuGender(UIMenu menu)
        {
            var gender = new List<dynamic>
            {
                "Male",
                "Female"
            };
            var newitem = new UIMenuListItem("Gender", gender, 0, "Select gender for character");
            menu.AddItem(newitem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    var genderString = item.Items[index].ToString();
                    Character.PlayerGender = genderString == "Male" ? Gender.Male : Gender.Female;
                }
            };
        }

        public static void AddMenuStart(UIMenu menu)
        {
            var newitem = new UIMenuItem("Start", "Begin playing Undead Streets");
            newitem.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    StartupMenu.Visible = !StartupMenu.Visible;
                    Main.StartMod();
                }
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsDead) StartupMenu.Visible = false;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Main.MenuKey && !Main.ModActive && !Main.MasterMenuPool.IsAnyMenuOpen())
                StartupMenu.Visible = !StartupMenu.Visible;
        }
    }
}