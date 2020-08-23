using CWDM.Enums;
using CWDM.Extensions;
using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CWDM
{
    public class Main : Script
    {
        public static bool ModActive = false;
        public static Keys MenuKey = Keys.F10;
        public static Keys InventoryKey = Keys.I;
        public static MenuPool MasterMenuPool = new MenuPool();
        public static double Version = 0.2;

        public Main()
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Config.LoadSettings();
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (ModActive)
            {
                Map.Update();
                Relationships.Update();
                Game.Player.WantedLevel = 0;
                CustomRespawn();
            }
            MasterMenuPool.ProcessMenus();
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!ModActive) return;
            switch (e.KeyCode)
            {
                case Keys.F6:
                    {
                        Ped[] all_ped = World.GetAllPeds();
                        Vehicle[] all_veh = World.GetAllVehicles();
                        List<Ped> pedsList = new List<Ped>(all_ped);
                        for (int i = 0; i < pedsList.Count; i++)
                        {
                            if (pedsList[i].RelationshipGroup != Relationships.ZombieGroup)
                            {
                                pedsList.RemoveAt(i);
                            }
                        }
                        UI.Notify($"Zombies: {Population.ZombiePeds.Count}~n~Animals: {Population.AnimalPeds.Count}~n~Total Peds: {all_ped.Length}~n~Total Zombie Peds: {pedsList.Count}~n~Vehicles: {Population.Vehicles.Count}~n~Total Vehicles: {all_veh.Length}");
                        break;
                    }
                case Keys.F7:
                    {
                        string coords = $"Co-ordinates: {Game.Player.Character.Position}";
                        string heading = $"Heading: {Game.Player.Character.Heading}";
                        UI.Notify($"{coords}~n~{heading}~n~~n~Written to log!");
                        Log.Write(coords);
                        Log.Write(heading);
                        break;
                    }
            }
        }

        public static void StartMod()
        {
            Game.FadeScreenOut(1500);
            Wait(3000);
            ModActive = true;
            Map.Prepare();
            Character.Setup();
            Wait(10000);
            Game.FadeScreenIn(1500);
            BigMessageThread.MessageInstance.ShowOldMessage("~r~COMMUNITY'S WALKING DEAD MOD", 5000);
            Wait(5000);
            UI.Notify("Welcome to Community's Walking Dead Mod!~n~~n~Make sure you run the latest version!~n~Please make sure your GTA and all~n~Dependencies are updated!~n~~n~Current Version: Debug Build Aurora");
        }

        public static void CustomRespawn()
        {
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "respawn_controller");
            Function.Call(Hash.IGNORE_NEXT_RESTART, true);
            Function.Call(Hash._DISABLE_AUTOMATIC_RESPAWN, true);
            if (!Game.Player.Character.IsDead) return;
            Audio.PlaySoundFrontend("Bed", "WastedSounds");
            World.RenderingCamera.Shake(CameraShake.DeathFail, 1f);
            ScreenEffect.DeathFailMpDark.StartEffect();
            BigMessageThread.MessageInstance.ShowOldMessage("~r~WASTED", 5000);
            Game.TimeScale = 0.3f;
            Character.CharacterReset = false;
            while (!Game.IsScreenFadedOut)
            {
                Wait(0);
            }
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "respawn_controller");
            Game.TimeScale = 1f;
            Function.Call(Hash._STOP_ALL_SCREEN_EFFECTS);
            Function.Call(Hash.NETWORK_REQUEST_CONTROL_OF_ENTITY, Game.Player.Character.Handle);
            BigMessageThread.MessageInstance.Dispose();
            Function.Call(Hash.NETWORK_RESURRECT_LOCAL_PLAYER, Character.StartingLocation.X, Character.StartingLocation.Y, Character.StartingLocation.Z, Character.StartingHeading, false, false);
            Wait(8000);
            Character.ResetCharacter();
            Game.FadeScreenIn(5000);
            Function.Call(Hash._RESET_LOCALPLAYER_STATE);
            Function.Call(Hash.RESET_PLAYER_ARREST_STATE, Game.Player.Character.Handle);
            Function.Call(Hash.DISPLAY_HUD, true);
            Game.Player.Character.FreezePosition = false;
        }
    }
}