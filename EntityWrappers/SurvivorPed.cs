using CWDM.Enums;
using GTA;

namespace CWDM.EntityWrappers
{
    public class SurvivorPed
    {
        public Ped PedEntity;
        public PedTask Task;

        public SurvivorPed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            PedEntity = pedEntity;
        }

        public void Update()
        {
            var blip = PedEntity.CurrentBlip;
            if (!PedEntity.IsAlive)
            {
                if (blip.Handle != 0)
                {
                    blip.Remove();
                }
                else
                {
                    if (PedEntity.IsInVehicle())
                    {
                        if (blip.Handle != 0) blip.Alpha = 0;
                    }
                    else
                    {
                        if (blip.Handle != 0) blip.Alpha = 255;
                    }
                }
            }
        }
    }
}