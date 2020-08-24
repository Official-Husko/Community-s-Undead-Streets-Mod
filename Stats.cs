using System;
using System.Drawing;
using GTA;
using GTA.Native;
using NativeUI;

namespace CWDM
{
    public class Stats : Script
    {
        public static bool EnableStats = true;
        public static int StatsTimeSecs = 1;
        public static DateTime StatsLastUpdateTime;
        public static TimerBarPool HudPool;
        public static BarTimerBar HungerBar;
        public static BarTimerBar ThirstBar;
        public static BarTimerBar EnergyBar;
        public static BarTimerBar InfectionBar;
        public static BarTimerBar FuelBar;
        public static BarTimerBar BatteryBar;

        public Stats()
        {
            Tick += OnTick;
            HudPool = new TimerBarPool();
            HungerBar = new BarTimerBar("~y~HUNGER");
            ThirstBar = new BarTimerBar("~b~THIRST");
            EnergyBar = new BarTimerBar("~g~ENERGY");
            InfectionBar = new BarTimerBar("~r~INFECTION");
            FuelBar = new BarTimerBar("~o~FUEL");
            BatteryBar = new BarTimerBar("~p~BATTERY");
            HungerBar.BackgroundColor = Color.Gray;
            HungerBar.ForegroundColor = Color.Yellow;
            ThirstBar.BackgroundColor = Color.Gray;
            ThirstBar.ForegroundColor = Color.Blue;
            EnergyBar.BackgroundColor = Color.Gray;
            EnergyBar.ForegroundColor = Color.Green;
            InfectionBar.BackgroundColor = Color.Gray;
            InfectionBar.ForegroundColor = Color.Red;
            FuelBar.BackgroundColor = Color.Gray;
            FuelBar.ForegroundColor = Color.Orange;
            BatteryBar.BackgroundColor = Color.Gray;
            BatteryBar.ForegroundColor = Color.Purple;
            HudPool.Add(EnergyBar);
            HudPool.Add(ThirstBar);
            HudPool.Add(HungerBar);
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive && EnableStats)
            {
                UpdateStats();
                ExtraStats();
                Draw(Character.CurrentHungerLevel, Character.CurrentThirstLevel, Character.CurrentEnergyLevel, 0, 0, 0);
            }
        }

        public static void Draw(float hunger, float thirst, float energy, float infection, float fuel, float battery)
        {
            HungerBar.Percentage = hunger;
            ThirstBar.Percentage = thirst;
            EnergyBar.Percentage = energy;
            InfectionBar.Percentage = infection;
            FuelBar.Percentage = fuel;
            BatteryBar.Percentage = battery;
            HudPool.Draw();
        }

        public static void UpdateStats()
        {
            if (DateTime.UtcNow.Subtract(StatsLastUpdateTime) >= TimeSpan.FromSeconds(StatsTimeSecs))
            {
                Character.CurrentHungerLevel -= Character.HungerDecrease;
                if (Character.CurrentHungerLevel <= 0.10f && Character.CurrentHungerLevel > 0.0f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify(
                        "~r~WARNING:~s~ Hunger levels are getting low! You need to eat something to keep up your strength and avoid loss of health.");
                }
                else if (Character.CurrentHungerLevel < 0.01f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify(
                        "~r~WARNING:~s~ Hunger levels are dangerously low! You need to eat something to regain your strength and raise your health.");
                }

                if (Character.CurrentHungerLevel < 0) Character.CurrentHungerLevel = 0;
                Character.CurrentThirstLevel -= Character.ThirstDecrease;
                if (Character.CurrentThirstLevel <= 0.10f && Character.CurrentThirstLevel > 0.0f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify(
                        "~r~WARNING:~s~ Thirst levels are getting low! You need to drink something to keep up your strength and avoid loss of health.");
                }
                else if (Character.CurrentThirstLevel < 0.01f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify(
                        "~r~WARNING:~s~ Thirst levels are dangerously low! You need to drink something to regain your strength and raise your health.");
                }

                if (Character.CurrentThirstLevel < 0) Character.CurrentThirstLevel = 0;
                Character.CurrentEnergyLevel -= Character.EnergyDecrease;
                if (Character.CurrentEnergyLevel <= 0.10f && Character.CurrentEnergyLevel > 0.0f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify(
                        "~r~WARNING:~s~ Energy levels are getting low! You need to find somewhere safe to keep up your strength and avoid loss of health.");
                }
                else if (Character.CurrentEnergyLevel < 0.01f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify(
                        "~r~WARNING:~s~ Energy levels are dangerously low! You need to find somewhere safe to sleep to regain your strength and raise your health.");
                }

                if (Character.CurrentEnergyLevel < 0) Character.CurrentEnergyLevel = 0;
                StatsLastUpdateTime = DateTime.UtcNow;
            }
        }

        public static void ExtraStats()
        {
            if (Population.ZombieInfection)
            {
                // Placeholder for Zombie Infection mode code
            }

            if (Fuel.EnableFuel)
            {
                // Placeholder for Vehicle Fuel mode code
            }

            if (Battery.EnableBattery)
            {
                // Placeholder for Torch Battery mode code
            }
        }
    }
}