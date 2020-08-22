using System;
using System.Drawing;
using GTA;
using NativeUI;

namespace CWDM
{
    public class Stats : Script
    {
        private readonly TimerBarPool hudPool;
        private readonly BarTimerBar hungerBar;
        private readonly BarTimerBar thirstBar;
        private readonly BarTimerBar energyBar;

        public Stats()
        {
            Tick += OnTick;
            hudPool = new TimerBarPool();
            hungerBar = new BarTimerBar("~y~HUNGER");
            thirstBar = new BarTimerBar("~b~THIRST");
            energyBar = new BarTimerBar("~g~ENERGY");
            hungerBar.BackgroundColor = Color.Gray;
            hungerBar.ForegroundColor = Color.Yellow;
            thirstBar.BackgroundColor = Color.Gray;
            thirstBar.ForegroundColor = Color.Blue;
            energyBar.BackgroundColor = Color.Gray;
            energyBar.ForegroundColor = Color.Green;
            hudPool.Add(energyBar);
            hudPool.Add(thirstBar);
            hudPool.Add(hungerBar);
        }
        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                Draw(Character.currentHungerLevel, Character.currentThirstLevel, Character.currentEnergyLevel);
            }
        }

        public void Draw(float hunger, float thirst, float energy)
        {
            hungerBar.Percentage = hunger;
            thirstBar.Percentage = thirst;
            energyBar.Percentage = energy;
            hudPool.Draw();
        }
    }
}
