using CWDM.Inventory;

namespace CWDM.Interfaces
{
    public interface ICraftable
    {
        RequiredMaterial[] RequiredMaterials
        {
            get;
            set;
        }
    }
}
