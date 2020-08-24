using System;
using GTA;

namespace CWDM
{
    public class Fuel : Script
    {
        public static bool EnableFuel;

        public Fuel()
        {
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive && EnableFuel)
            {
                // Placeholder for Vehicle Fuel mode code
            }
        }
    }
}