using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTA.Math;
using NativeUI;

namespace CWDM
{
    public class Main : Script
    {
        public static bool ModActive = false;
        public static Map Map;
        public static Character Character;
        public static Population Population;
        public static Stats Stats;
        public static Keys MenuKey = Keys.F10;
        public static Keys InventoryKey = Keys.I;
        public static MenuPool MasterMenuPool = new MenuPool();

        public Main()
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Map = new Map();
            Character = new Character();
            Population = new Population();
            Config.LoadSettings();
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (ModActive)
            {
                Character.Update();
                Map.Update();
                Population.Populate();
                Relationships.Update();
                Population.Update();
                Custom_Respawn();
            }
            MasterMenuPool.ProcessMenus();
        }

        public static void StartMod()
        {
            Game.FadeScreenOut(1000);
            Wait(2000);
            ModActive = true;
            Map.Setup();
            Character.Setup();
            Wait(3000);
            Game.FadeScreenIn(1000);
            BigMessageThread.MessageInstance.ShowOldMessage("~r~Undead Streets", 5000);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyCode == Keys.F9)
            {
                UI.Notify("Location saved to log");
                var playerPosition = Game.Player.Character.Position;
                Debug.Log($"Player co-ordinates: {playerPosition.X}, {playerPosition.Y}, {playerPosition.Z}");
                var playerHeading = Game.Player.Character.Heading;
                Debug.Log($"Player heading: {playerHeading}");
            }

            if (e.KeyCode == Keys.F7)
            {
                Vector3 spawnPos = World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(5.0f), true);
                Model model = new Model("contender");
                Character.playerVehicle = Extensions.SpawnVehicle(model, spawnPos);
                Character.playerVehicle.PrimaryColor = VehicleColor.MetallicBlack;
                Character.playerVehicle.SecondaryColor = VehicleColor.MetallicBlack;
                Character.playerVehicle.NumberPlateType = NumberPlateType.YellowOnBlack;
                Character.playerVehicle.NumberPlate = "SPACEDAD";
                Character.playerVehicle.AddBlip();
                Character.playerVehicle.CurrentBlip.Sprite = BlipSprite.GetawayCar;
                Character.playerVehicle.CurrentBlip.Color = BlipColor.Green;
                Character.playerVehicle.CurrentBlip.Name = "Personal Vehicle";
            }

            if (e.KeyCode == Keys.F8)
            {
                Population.SurvivorGroupSpawn(World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(5.0f), true), GroupType.Friendly);
            }*/
        }

        public static void Custom_Respawn()
        {
            Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "respawn_controller");
            Function.Call(Hash.IGNORE_NEXT_RESTART, true);
            Function.Call(Hash._DISABLE_AUTOMATIC_RESPAWN, true);
            if (Game.Player.Character.IsDead)
            {
                Vector3 respawnLoc = new Vector3(478.8616f, -921.53f, 38.77953f);
                while (!Game.IsScreenFadedOut)
                {
                    Wait(0);
                }
                Function.Call(Hash.TERMINATE_ALL_SCRIPTS_WITH_THIS_NAME, "respawn_controller");
                Game.TimeScale = 1f;
                Function.Call(Hash._STOP_ALL_SCREEN_EFFECTS);
                Function.Call(Hash.NETWORK_REQUEST_CONTROL_OF_ENTITY, Game.Player.Character.Handle);
                const float heading = 266;
                Function.Call(Hash.NETWORK_RESURRECT_LOCAL_PLAYER, respawnLoc.X, respawnLoc.Y, respawnLoc.Z, heading, false, false);
                Wait(2000);
                Character.ResetCharacter();
                Game.FadeScreenIn(3500);
                Function.Call(Hash._RESET_LOCALPLAYER_STATE);
                Function.Call(Hash.RESET_PLAYER_ARREST_STATE, Game.Player.Character.Handle);
                Function.Call(Hash.DISPLAY_HUD, true);
                Game.Player.Character.FreezePosition = false;
            }
        }
    }
}
