using GTA;

namespace CWDM.EntityWrappers
{
    public class AnimalPed
    {
        public Ped PedEntity;

        public AnimalPed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            PedEntity = pedEntity;
        }
    }
}