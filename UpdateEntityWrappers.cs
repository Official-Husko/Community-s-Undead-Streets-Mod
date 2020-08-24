using System;
using GTA;

namespace CWDM
{
    public class UpdateEntityWrappers : Script
    {
        public int SurvivorIndex;
        public int VehicleIndex;
        public int ZombieIndex;

        public UpdateEntityWrappers()
        {
            Interval = 10;
            Tick += OnTick;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (Main.ModActive)
            {
                if (Population.ZombiePeds.Count > 0) UpdateZombies();
                if (Population.SurvivorPeds.Count > 0) UpdateSurvivors();
                if (Population.Vehicles.Count > 0) UpdateVehicles();
            }
        }

        public void UpdateZombies()
        {
            for (var i = ZombieIndex; i < ZombieIndex + 5 && i < Population.ZombiePeds.Count; i++)
            {
                if (Population.ZombiePeds[i] == null || Population.ZombiePeds[i].PedEntity == null)
                {
                    Population.ZombiePeds.RemoveAt(i);
                    continue;
                }

                Population.ZombiePeds[i].Update();
            }

            ZombieIndex += 5;
            if (ZombieIndex >= Population.ZombiePeds.Count) ZombieIndex = 0;
        }

        public void UpdateSurvivors()
        {
            for (var i = SurvivorIndex; i < SurvivorIndex + 5 && i < Population.SurvivorPeds.Count; i++)
            {
                if (Population.SurvivorPeds[i] == null || Population.SurvivorPeds[i].PedEntity == null)
                {
                    Population.SurvivorPeds.RemoveAt(i);
                    continue;
                }

                Population.SurvivorPeds[i].Update();
            }

            SurvivorIndex += 5;
            if (SurvivorIndex >= Population.SurvivorPeds.Count) SurvivorIndex = 0;
        }

        public void UpdateVehicles()
        {
            for (var i = VehicleIndex; i < VehicleIndex + 5 && i < Population.Vehicles.Count; i++)
            {
                if (Population.Vehicles[i] == null || Population.Vehicles[i].Vehicle == null)
                {
                    Population.Vehicles.RemoveAt(i);
                    continue;
                }

                Population.Vehicles[i].Update();
            }

            VehicleIndex += 5;
            if (VehicleIndex >= Population.Vehicles.Count) VehicleIndex = 0;
        }
    }
}