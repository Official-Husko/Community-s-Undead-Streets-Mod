using System;

namespace CWDM.Inventory
{
    [Serializable]
    public class RequiredMaterial
    {
        public InventoryMaterial Material
        {
            get;
            set;
        }

        public int RequiredAmount
        {
            get;
            set;
        }

        public RequiredMaterial(InventoryMaterial material, int requiredAmount)
        {
            Material = material;
            RequiredAmount = requiredAmount;
        }
    }
}
