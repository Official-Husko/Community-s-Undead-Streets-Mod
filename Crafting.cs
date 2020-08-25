using System.Collections.Generic;
using CWDM.Enums;
using CWDM.Inventory;
using GTA;
using GTA.Native;
using NativeUI;

namespace CWDM
{
    public static class Crafting
    {
        public static void AddCraftingItemsToMenu(UIMenu menu, CraftType craftType)
        {
            var craftables = new List<InventoryItem>();
            for (var i = 0; i < PlayerInventory.PlayerInventoryItems.Count; i++)
            {
                if (PlayerInventory.PlayerInventoryItems[i].GetType() == typeof(InventoryItemCraftable))
                {
                    var item = (InventoryItemCraftable) PlayerInventory.PlayerInventoryItems[i];
                    if (item.CraftType == craftType) craftables.Add(PlayerInventory.PlayerInventoryItems[i]);
                }
                else if (PlayerInventory.PlayerInventoryItems[i].GetType() == typeof(InventoryItemRestoreCraftable))
                {
                    var item = (InventoryItemRestoreCraftable) PlayerInventory.PlayerInventoryItems[i];
                    if (item.CraftType == craftType) craftables.Add(PlayerInventory.PlayerInventoryItems[i]);
                }
            }

            for (var i = 0; i < PlayerInventory.PlayerInventoryMaterials.Count; i++)
            {
                if (PlayerInventory.PlayerInventoryMaterials[i].GetType() == typeof(InventoryMaterialCraftable))
                {
                    var item = (InventoryMaterialCraftable) PlayerInventory.PlayerInventoryMaterials[i];
                    if (item.CraftType == craftType) craftables.Add(PlayerInventory.PlayerInventoryMaterials[i]);
                }
            }

            for (var i = 0; i < craftables.Count; i++)
            {
                var description = "Requirements: ";
                if (craftables[i].GetType() == typeof(InventoryItemCraftable))
                {
                    var item = (InventoryItemCraftable) craftables[i];
                    foreach (var requiredMaterial in item.GetRequiredMaterials())
                    {
                        var name = requiredMaterial.Material.Name;
                        var amount = requiredMaterial.RequiredAmount;
                        description += "" + name + " (" + amount + "); ";
                    }
                }
                else if (craftables[i].GetType() == typeof(InventoryItemRestoreCraftable))
                {
                    var item = (InventoryItemRestoreCraftable) craftables[i];
                    foreach (var requiredMaterial in item.GetRequiredMaterials())
                    {
                        var name = requiredMaterial.Material.Name;
                        var amount = requiredMaterial.RequiredAmount;
                        description += "" + name + " (" + amount + "); ";
                    }
                }
                else if (craftables[i].GetType() == typeof(InventoryMaterialCraftable))
                {
                    var item = (InventoryMaterialCraftable) craftables[i];
                    foreach (var requiredMaterial in item.GetRequiredMaterials())
                    {
                        var name = requiredMaterial.Material.Name;
                        var amount = requiredMaterial.RequiredAmount;
                        description += "" + name + " (" + amount + "); ";
                    }
                }

                menu.AddItem(new UIMenuItem(craftables[i].Name, description));
            }
        }

        public static bool CanBeCrafted(InventoryItem item)
        {
            if (item.GetType() == typeof(InventoryItemCraftable))
            {
                var check = (InventoryItemCraftable) item;
                foreach (var requiredMaterial in check.GetRequiredMaterials())
                {
                    if (requiredMaterial.RequiredAmount > PlayerInventory.PlayerInventoryItems
                        .Find(a => a.Name == requiredMaterial.Material.Name).Amount)
                    {
                        return false;
                    }
                }
            }
            else if (item.GetType() == typeof(InventoryItemRestoreCraftable))
            {
                var check = (InventoryItemRestoreCraftable) item;
                foreach (var requiredMaterial in check.GetRequiredMaterials())
                {
                    if (requiredMaterial.RequiredAmount > PlayerInventory.PlayerInventoryItems
                        .Find(a => a.Name == requiredMaterial.Material.Name).Amount)
                    {
                        return false;
                    }
                }
            }
            else if (item.GetType() == typeof(InventoryMaterialCraftable))
            {
                var check = (InventoryMaterialCraftable) item;
                foreach (var requiredMaterial in check.GetRequiredMaterials())
                {
                    if (requiredMaterial.RequiredAmount > PlayerInventory.PlayerInventoryMaterials
                        .Find(a => a.Name == requiredMaterial.Material.Name).Amount)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool HaveEnoughRoomInInventory(InventoryItem item)
        {
            int amount;
            int maxAmount;
            if (PlayerInventory.PlayerInventoryItems.Exists(a => a.Name == item.Name))
            {
                amount = PlayerInventory.PlayerInventoryItems.Find(a => a.Name == item.Name).Amount;
                maxAmount = PlayerInventory.PlayerInventoryItems.Find(a => a.Name == item.Name).MaxAmount;
            }
            else
            {
                amount = PlayerInventory.PlayerInventoryMaterials.Find(a => a.Name == item.Name).Amount;
                maxAmount = PlayerInventory.PlayerInventoryMaterials.Find(a => a.Name == item.Name).MaxAmount;
            }

            return maxAmount < amount;
        }

        public static void Craft(InventoryItem item)
        {
            if (CanBeCrafted(item))
            {
                if (HaveEnoughRoomInInventory(item))
                {
                    if (item.GetType() == typeof(InventoryItemCraftable))
                    {
                        var check = (InventoryItemCraftable) item;
                        foreach (var requiredMaterial in check.GetRequiredMaterials())
                        {
                            PlayerInventory.UpdateMaterialsInventory(requiredMaterial.Material,
                                requiredMaterial.RequiredAmount, InventoryUpdate.Decrease);
                        }
                    }
                    else if (item.GetType() == typeof(InventoryItemRestoreCraftable))
                    {
                        var check = (InventoryItemRestoreCraftable) item;
                        foreach (var requiredMaterial in check.GetRequiredMaterials())
                        {
                            PlayerInventory.UpdateMaterialsInventory(requiredMaterial.Material,
                                requiredMaterial.RequiredAmount, InventoryUpdate.Decrease);
                        }
                    }
                    else if (item.GetType() == typeof(InventoryMaterialCraftable))
                    {
                        var check = (InventoryMaterialCraftable) item;
                        foreach (var requiredMaterial in check.GetRequiredMaterials())
                        {
                            PlayerInventory.UpdateMaterialsInventory(requiredMaterial.Material,
                                requiredMaterial.RequiredAmount, InventoryUpdate.Decrease);
                        }
                    }

                    if (PlayerInventory.PlayerInventoryItems.Exists(a => a.Name == item.Name))
                    {
                        PlayerInventory.UpdateItemsInventory(item, 1, InventoryUpdate.Increase);
                    }
                    else
                    {
                        var material = (InventoryMaterial) item;
                        PlayerInventory.UpdateMaterialsInventory(material, 1, InventoryUpdate.Increase);
                    }

                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("Crafted ~b~" + item.Name + "~s~!");
                }
                else
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify("Not enough space in inventory!", true);
                }
            }
            else
            {
                Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                UI.Notify("Crafting requirements not met!", true);
            }
        }
    }
}