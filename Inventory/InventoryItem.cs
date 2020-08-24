using System;
using CWDM.Enums;

namespace CWDM.Inventory
{
    [Serializable]
    public class InventoryItem
    {
        private LootType[] _lootTypes;

        public InventoryItem(string name, string description, int amount, int maxAmount, LootType[] lootTypes)
        {
            Name = name;
            Description = description;
            Amount = amount;
            MaxAmount = maxAmount;
            SetLootTypes(lootTypes);
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public int MaxAmount { get; set; }

        public LootType[] GetLootTypes()
        {
            return _lootTypes;
        }

        public void SetLootTypes(LootType[] value)
        {
            _lootTypes = value;
        }
    }
}