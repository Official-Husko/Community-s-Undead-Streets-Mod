using System;

namespace CWDM.Inventory
{
    [Serializable]
    public class RequiredMaterial
    {
        public RequiredMaterial(InventoryMaterial material, int requiredAmount)
        {
            Material = material;
            RequiredAmount = requiredAmount;
        }

        public InventoryMaterial Material { get; set; }

        public int RequiredAmount { get; set; }
    }
}