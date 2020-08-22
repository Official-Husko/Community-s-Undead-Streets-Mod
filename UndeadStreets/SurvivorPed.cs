using GTA;

namespace CWDM
{
    public class SurvivorPed : UpdaterClass
    {
        public Ped pedEntity;
        public PedTasks tasks = PedTasks.None;

        public SurvivorPed(Ped pedEntity)
        {
            AttachData(pedEntity);
        }

        public void AttachData(Ped pedEntity)
        {
            this.pedEntity = pedEntity;
        }

        public override void Update()
        {
        }
    }
}