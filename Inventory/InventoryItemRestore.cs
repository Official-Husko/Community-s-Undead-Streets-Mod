using System;
using CWDM.Enums;
using CWDM.Interfaces;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryItemRestore : InventoryItem, IRestore
    {
        public InventoryItemRestore(string name, string description, int amount, int maxAmount, LootType[] lootTypes,
            RestoreType restoreType, float restore)
            : base(name, description, amount, maxAmount, lootTypes)
        {
            RestoreType = restoreType;
            Restore = restore;
        }

        public RestoreType RestoreType { get; set; }

        public float Restore { get; set; }
    }
}