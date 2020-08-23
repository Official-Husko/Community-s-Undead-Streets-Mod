using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Drawing;

namespace CWDM
{
    public class Stats : Script
    {
        public static bool EnableStats = true;
        public static int StatsTimeSecs = 1;
        public static DateTime StatsLastUpdateTime;
        public static TimerBarPool hudPool;
        public static BarTimerBar hungerBar;
        public static BarTimerBar thirstBar;
        public static BarTimerBar energyBar;
        public static BarTimerBar infectionBar;
        public static BarTimerBar fuelBar;
        public static BarTimerBar batteryBar;

        public Stats()
        {
            Tick += OnTick;
            hudPool = new TimerBarPool();
            hungerBar = new BarTimerBar("~y~HUNGER");
            thirstBar = new BarTimerBar("~b~THIRST");
            energyBar = new BarTimerBar("~g~ENERGY");
            infectionBar = new BarTimerBar("~r~INFECTION");
            fuelBar = new BarTimerBar("~o~FUEL");
            batteryBar = new BarTimerBar("~p~BATTERY");
            hungerBar.BackgroundColor = Color.Gray;
            hungerBar.ForegroundColor = Color.Yellow;
            thirstBar.BackgroundColor = Color.Gray;
            thirstBar.ForegroundColor = Color.Blue;
            energyBar.BackgroundColor = Color.Gray;
            energyBar.ForegroundColor = Color.Green;
            infectionBar.BackgroundColor = Color.Gray;
            infectionBar.ForegroundColor = Color.Red;
            fuelBar.BackgroundColor = Color.Gray;
            fuelBar.ForegroundColor = Color.Orange;
            batteryBar.BackgroundColor = Color.Gray;
            batteryBar.ForegroundColor = Color.Purple;
            hudPool.Add(energyBar);
            hudPool.Add(thirstBar);
            hudPool.Add(hungerBar);
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
            hungerBar.Percentage = hunger;
            thirstBar.Percentage = thirst;
            energyBar.Percentage = energy;
            infectionBar.Percentage = infection;
            fuelBar.Percentage = fuel;
            batteryBar.Percentage = battery;
            hudPool.Draw();
        }

        public static void UpdateStats()
        {
            if (DateTime.UtcNow.Subtract(StatsLastUpdateTime) >= TimeSpan.FromSeconds(StatsTimeSecs))
            {
                Character.CurrentHungerLevel -= Character.HungerDecrease;
                if (Character.CurrentHungerLevel <= 0.10f && Character.CurrentHungerLevel > 0.0f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("~r~WARNING:~s~ Hunger levels are getting low! You need to eat something to keep up your strength and avoid loss of health.");
                }
                else if (Character.CurrentHungerLevel < 0.01f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("~r~WARNING:~s~ Hunger levels are dangerously low! You need to eat something to regain your strength and raise your health.");
                }
                if (Character.CurrentHungerLevel < 0)
                {
                    Character.CurrentHungerLevel = 0;
                }
                Character.CurrentThirstLevel -= Character.ThirstDecrease;
                if (Character.CurrentThirstLevel <= 0.10f && Character.CurrentThirstLevel > 0.0f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("~r~WARNING:~s~ Thirst levels are getting low! You need to drink something to keep up your strength and avoid loss of health.");
                }
                else if (Character.CurrentThirstLevel < 0.01f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("~r~WARNING:~s~ Thirst levels are dangerously low! You need to drink something to regain your strength and raise your health.");
                }
                if (Character.CurrentThirstLevel < 0)
                {
                    Character.CurrentThirstLevel = 0;
                }
                Character.CurrentEnergyLevel -= Character.EnergyDecrease;
                if (Character.CurrentEnergyLevel <= 0.10f && Character.CurrentEnergyLevel > 0.0f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("~r~WARNING:~s~ Energy levels are getting low! You need to find somewhere safe to keep up your strength and avoid loss of health.");
                }
                else if (Character.CurrentEnergyLevel < 0.01f)
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("~r~WARNING:~s~ Energy levels are dangerously low! You need to find somewhere safe to sleep to regain your strength and raise your health.");
                }
                if (Character.CurrentEnergyLevel < 0)
                {
                    Character.CurrentEnergyLevel = 0;
                }
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