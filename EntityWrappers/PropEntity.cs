using GTA;

namespace CWDM.Wrappers
{
    public class PropEntity
    {
        public Prop prop;

        public PropEntity(Prop prop)
        {
            AttachData(prop);
        }

        public void AttachData(Prop prop)
        {
            this.prop = prop;
        }
    }
}