using System;
using CWDM.Enums;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryItemCraftable : InventoryItem
    {
        private RequiredMaterial[] _requiredMaterials;

        public InventoryItemCraftable(string name, string description, int amount, int maxAmount, LootType[] lootTypes,
            CraftType craftType, RequiredMaterial[] requiredMaterials)
            : base(name, description, amount, maxAmount, lootTypes)
        {
            CraftType = craftType;
            SetRequiredMaterials(requiredMaterials);
        }

        public CraftType CraftType { get; set; }

        public RequiredMaterial[] GetRequiredMaterials()
        {
            return _requiredMaterials;
        }

        public void SetRequiredMaterials(RequiredMaterial[] value)
        {
            _requiredMaterials = value;
        }
    }
}