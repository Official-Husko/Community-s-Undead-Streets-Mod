using System;
using System.Drawing;
using System.Collections.Generic;
using GTA;
using NativeUI;

namespace CWDM
{
    public class StartMenu : Script
    {
        private bool runners = false;
        private UIMenu mainMenu;

        public void AddMenuRunners(UIMenu menu)
        {
            var newitem = new UIMenuCheckboxItem("Fast Zombies", runners, "Enable/Disable fast zombies");
            menu.AddItem(newitem);
            menu.OnCheckboxChange += (sender, item, checked_) =>
            {
                if (item == newitem)
                {
                    runners = checked_;
                    if (runners == true)
                    {
                        Population.zombieRunners = true;
                    }
                    else
                    {
                        Population.zombieRunners = false;
                    }
                }
            };
        }

        public void AddMenuGender(UIMenu menu)
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
                    string genderString = item.Items[index].ToString();
                    if (genderString == "Male")
                    {
                        Character.playerGender = Gender.Male;
                    }
                    else
                    {
                        Character.playerGender = Gender.Female;
                    }
                }

            };
        }

        public void AddMenuStart(UIMenu menu)
        {
            var newitem = new UIMenuItem("Start", "Begin playing Undead Streets");
            newitem.SetRightBadge(UIMenuItem.BadgeStyle.Tick);
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    mainMenu.Visible = !mainMenu.Visible;
                    Main.StartMod();
                }
            };
        }

        public StartMenu()
        {
            mainMenu = new UIMenu("Undead Streets", "Starting Settings");
            AddMenuRunners(mainMenu);
            AddMenuGender(mainMenu);
            AddMenuStart(mainMenu);
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.DarkRed)
            };
            mainMenu.SetBannerType(banner);
            Main.MasterMenuPool.Add(mainMenu);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
            KeyDown += (o, e) =>
            {
                if (e.KeyCode == Main.MenuKey && Main.ModActive == false && !Main.MasterMenuPool.IsAnyMenuOpen())
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
        }
    }
}