using GTA;

namespace CWDM.Wrappers
{
    public class AnimalPed
    {
        public Ped pedEntity;

        public AnimalPed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            this.pedEntity = pedEntity;
        }
    }
}
