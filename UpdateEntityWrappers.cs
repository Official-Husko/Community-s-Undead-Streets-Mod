using GTA;
using System;

namespace CWDM
{
    public class UpdateEntityWrappers : Script
    {
        public int zombieIndex;
        public int survivorIndex;
        public int vehicleIndex;

        public UpdateEntityWrappers()
        {
            Interval = 10;
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                if (Population.ZombiePeds.Count > 0)
                {
                    UpdateZombies();
                }
                if (Population.SurvivorPeds.Count > 0)
                {
                    UpdateSurvivors();
                }
                if (Population.Vehicles.Count > 0)
                {
                    UpdateVehicles();
                }
            }
        }

        public void UpdateZombies()
        {
            for (int i = zombieIndex; i < zombieIndex + 5 && i < Population.ZombiePeds.Count; i++)
            {
                if (Population.ZombiePeds[i] == null || Population.ZombiePeds[i].pedEntity == null)
                {
                    Population.ZombiePeds.RemoveAt(i);
                    continue;
                }
                Population.ZombiePeds[i].Update();
            }
            zombieIndex += 5;
            if (zombieIndex >= Population.ZombiePeds.Count)
            {
                zombieIndex = 0;
            }
        }

        public void UpdateSurvivors()
        {
            for (int i = survivorIndex; i < survivorIndex + 5 && i < Population.SurvivorPeds.Count; i++)
            {
                if (Population.SurvivorPeds[i] == null || Population.SurvivorPeds[i].pedEntity == null)
                {
                    Population.SurvivorPeds.RemoveAt(i);
                    continue;
                }
                Population.SurvivorPeds[i].Update();
            }
            survivorIndex += 5;
            if (survivorIndex >= Population.SurvivorPeds.Count)
            {
                survivorIndex = 0;
            }
        }

        public void UpdateVehicles()
        {
            for (int i = vehicleIndex; i < vehicleIndex + 5 && i < Population.Vehicles.Count; i++)
            {
                if (Population.Vehicles[i] == null || Population.Vehicles[i].vehicle == null)
                {
                    Population.Vehicles.RemoveAt(i);
                    continue;
                }
                Population.Vehicles[i].Update();
            }
            vehicleIndex += 5;
            if (vehicleIndex >= Population.Vehicles.Count)
            {
                vehicleIndex = 0;
            }
        }
    }
}