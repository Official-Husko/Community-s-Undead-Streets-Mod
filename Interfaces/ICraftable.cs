using CWDM.Inventory;

namespace CWDM.Interfaces
{
    public interface ICraftable
    {
        RequiredMaterial[] GetRequiredMaterials();
        void SetRequiredMaterials(RequiredMaterial[] value);
    }
}
