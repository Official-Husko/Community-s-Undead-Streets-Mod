using CWDM.Enums;
using GTA;

namespace CWDM.Wrappers
{
    public class SurvivorPed
    {
        public Ped pedEntity;
        public PedTask task;

        public SurvivorPed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            this.pedEntity = pedEntity;
        }

        public void Update()
        {
            Blip blip = pedEntity.CurrentBlip;
            if (!pedEntity.IsAlive)
            {
                if (blip.Handle != 0)
                {
                    blip.Remove();
                }
                else
                {
                    if (pedEntity.IsInVehicle())
                    {
                        if (blip.Handle != 0)
                        {
                            blip.Alpha = 0;
                        }
                    }
                    else
                    {
                        if (blip.Handle != 0)
                        {
                            blip.Alpha = 255;
                        }
                    }
                }
            }
        }
    }
}