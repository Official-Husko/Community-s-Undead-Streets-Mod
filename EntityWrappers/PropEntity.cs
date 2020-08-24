using GTA;

namespace CWDM.EntityWrappers
{
    public class PropEntity
    {
        public Prop Prop;

        public PropEntity(Prop prop)
        {
            AttachData(prop);
        }

        public void AttachData(Prop prop)
        {
            Prop = prop;
        }
    }
}