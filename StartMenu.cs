using GTA;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CWDM
{
    public class StartMenu : Script
    {
        public static bool Runners = false;
        public static bool Electricity = false;
        public static bool CharacterStats = true;
        public static UIMenu StartupMenu;

        public static void AddMenuRunners(UIMenu menu)
        {
            UIMenuCheckboxItem newitem = new UIMenuCheckboxItem("Fast Zombies", Runners, "Enable/Disable fast zombies");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += (sender, item, checked_) =>
            {
                if (item == newitem)
                {
                    Runners = checked_;
                    Population.FastZombies = Runners;
                }
            };
        }

        public static void AddMenuElectricity(UIMenu menu)
        {
            UIMenuCheckboxItem newitem = new UIMenuCheckboxItem("Electricity", Electricity, "Enable/Disable electricity");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += (sender, item, checked_) =>
            {
                if (item == newitem)
                {
                    Electricity = checked_;
                    Map.Electricity = Electricity;
                }
            };
        }

        public static void AddMenuStats(UIMenu menu)
        {
            UIMenuCheckboxItem newitem = new UIMenuCheckboxItem("Stats", CharacterStats, "Enable/Disable survival stats (hunger, thirst, etc...)");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += (sender, item, checked_) =>
            {
                if (item == newitem)
                {
                    CharacterStats = checked_;
                    Stats.EnableStats = CharacterStats;
                }
            };
        }

        public static void AddMenuGender(UIMenu menu)
        {
            List<dynamic> gender = new List<dynamic>
            {
                "Male",
                "Female"
             };
            UIMenuListItem newitem = new UIMenuListItem("Gender", gender, 0, "Select gender for character");
            menu.AddItem(newitem);
            menu.OnListChange += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    string genderString = item.Items[index].ToString();
                    Character.PlayerGender = genderString == "Male" ? Gender.Male : Gender.Female;
                }
            };
        }

        public static void AddMenuStart(UIMenu menu)
        {
            UIMenuItem newitem = new UIMenuItem("Start", "Begin playing Undead Streets");
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

        public StartMenu()
        {
            StartupMenu = new UIMenu("Undead Streets", "Starting Settings");
            AddMenuRunners(StartupMenu);
            AddMenuElectricity(StartupMenu);
            AddMenuStats(StartupMenu);
            AddMenuGender(StartupMenu);
            AddMenuStart(StartupMenu);
            UIResRectangle banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.DarkRed)
            };
            StartupMenu.SetBannerType(banner);
            Main.MasterMenuPool.Add(StartupMenu);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Game.Player.Character.IsDead)
            {
                StartupMenu.Visible = false;
            }
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Main.MenuKey && !Main.ModActive && !Main.MasterMenuPool.IsAnyMenuOpen())
            {
                StartupMenu.Visible = !StartupMenu.Visible;
            }
        }
    }
}
