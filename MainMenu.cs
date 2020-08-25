﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using NativeUI;

namespace CWDM
{
    public class MainMenu : Script
    {
        public static UIMenu MainModMenu;

        public MainMenu()
        {
            MainModMenu = new UIMenu("CWDM", "Main Menu");
            Main.MasterMenuPool.Add(MainModMenu);
            AddMenuGameLoad(MainModMenu);
            AddMenuVehicleRegister(MainModMenu);
            var banner = new UIResRectangle
            {
                Color = Color.FromArgb(255, Color.DarkRed)
            };
            MainModMenu.SetBannerType(banner);
            Main.MasterMenuPool.RefreshIndex();
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        public static void AddMenuVehicleRegister(UIMenu menu)
        {
            var newitem = new UIMenuItem("Claim/Unclaim Vehicle", "Claim/Unclaim current vehicle");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (item == newitem)
                {
                    if (!Game.Player.Character.IsInVehicle())
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                        UI.Notify("You are not in a vehicle!");
                    }
                    else
                    {
                        var vehicle = Game.Player.Character.CurrentVehicle;
                        if (Character.PlayerVehicles.Contains(vehicle))
                        {
                            Character.PlayerVehicles.Remove(vehicle);
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            UI.Notify("You have unclaimed this vehicle!");
                        }
                        else
                        {
                            if (Character.PlayerVehicles.Count == Character.MaxPlayerVehicles)
                            {
                                var maxVehicles = Character.MaxPlayerVehicles;
                                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET",
                                    1);
                                UI.Notify($"You cannot claim more than {maxVehicles} vehicles!");
                            }
                            else
                            {
                                Character.PlayerVehicles.Add(vehicle);
                                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET",
                                    1);
                                UI.Notify("Vehicle claimed!");
                            }
                        }
                    }
                }
            };
        }

        public static void AddMenuGameLoad(UIMenu menu)
        {
            var newitem = new UIMenuItem("Load Saved Game", "Load a previously saved game");
            menu.AddItem(newitem);
            menu.OnItemSelect += (sender, item, index) =>
            {
                if (!File.Exists("./scripts/CWDM/SaveGame/Character.sav"))
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("No saved games detected!");
                }
                else
                {
                    MainModMenu.Visible = false;
                    Game.FadeScreenOut(1500);
                    Wait(3000);
                    Config.LoadPlayerAll();
                    Wait(10000);
                    Character.CharacterReset = true;
                    Game.FadeScreenIn(1500);
                }
            };
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (!Main.ModActive) return;
            if (Game.Player.Character.IsDead)
                MainModMenu.Visible = false;
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Main.MenuKey && Main.ModActive && !Main.MasterMenuPool.IsAnyMenuOpen())
                MainModMenu.Visible = !MainModMenu.Visible;
        }
    }
}