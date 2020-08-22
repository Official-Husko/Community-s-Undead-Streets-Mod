using GTA;
using GTA.Native;
using NativeUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CWDM.Enums;
using CWDM.Inventory;

namespace CWDM
{
	[Serializable]
	public class PlayerInventory : Script
	{
		public static List<InventoryItem> PlayerInventoryItems = new List<InventoryItem>();
		public static InventoryItemRestoreCraftable BandageItem = new InventoryItemRestoreCraftable("Bandage", "A bandage made of fabric", 0, 15, new LootType[] { LootType.Ped, LootType.Store, LootType.Bin, LootType.Vehicle }, RestoreType.Health, 25f, CraftType.Player, new RequiredMaterial[] { new RequiredMaterial(FabricMaterial, 2) });
		public static InventoryItemRestoreCraftable MetalPlateItem = new InventoryItemRestoreCraftable("Metal Plate", "A protective plate of metal", 0, 5, new LootType[] { LootType.None }, RestoreType.Armor, 50f, CraftType.Player, new RequiredMaterial[] { new RequiredMaterial(MetalMaterial, 5) });
		public static InventoryItemRestore WaterItem = new InventoryItemRestore("Water Bottle", "A bottle of water", 0, 10, new LootType[] { LootType.Ped, LootType.Store, LootType.Bin, LootType.Skip, LootType.Vehicle }, RestoreType.Thirst, 0.2f);
		public static InventoryItemRestore SodaItem = new InventoryItemRestore("Soda Bottle", "A bottle of soda", 0, 10, new LootType[] { LootType.Ped, LootType.Store, LootType.Bin, LootType.Vehicle }, RestoreType.Thirst, 0.15f);
		public static InventoryItemRestore ChocolateItem = new InventoryItemRestore("Chocolate Bar", "A bar of chocolate", 0, 20, new LootType[] { LootType.Ped, LootType.Store, LootType.Bin, LootType.Vehicle }, RestoreType.Hunger, 0.1f);
		public static InventoryItemRestore CanItem = new InventoryItemRestore("Canned Food", "A can containing uncooked food", 0, 10, new LootType[] { LootType.Ped, LootType.Store, LootType.Bin, LootType.Skip, LootType.Vehicle }, RestoreType.Hunger, 0.15f);
		public static InventoryItemRestoreCraftable CookedMeatItem = new InventoryItemRestoreCraftable("Cooked Meat", "Meat that has been cooked", 0, 10, new LootType[] { LootType.None }, RestoreType.Hunger, 0.25f, CraftType.Campfire, new RequiredMaterial[] { new RequiredMaterial(RawMeatMaterial, 1) });
		public static List<InventoryMaterial> PlayerInventoryMaterials = new List<InventoryMaterial>();
		public static InventoryMaterial FabricMaterial = new InventoryMaterial("Fabric", "A scrap of fabric", 0, 50, new LootType[] { LootType.Ped, LootType.Bin, LootType.Skip, LootType.Vehicle });
		public static InventoryMaterial MetalMaterial = new InventoryMaterial("Metal", "A scrap of metal", 0, 50, new LootType[] { LootType.Bin, LootType.Skip });
		public static InventoryMaterial WoodMaterial = new InventoryMaterial("Wood", "A scrap of metal", 0, 50, new LootType[] { LootType.Bin, LootType.Skip });
		public static InventoryMaterial PlasticMaterial = new InventoryMaterial("Plastic", "A scrap of metal", 0, 50, new LootType[] { LootType.Ped, LootType.Bin, LootType.Skip, LootType.Vehicle });
		public static InventoryMaterial GlassMaterial = new InventoryMaterial("Glass", "A scrap of glass", 0, 50, new LootType[] { LootType.Bin, LootType.Skip });
		public static InventoryMaterial RawMeatMaterial = new InventoryMaterial("Raw Meat", "Uncooked animal meat", 0, 20, new LootType[] { LootType.Animal });

		[NonSerialized]
		public static UIMenu InventoryMenu;
		[NonSerialized]
		public static UIMenu ItemsSubMenu;
		[NonSerialized]
		public static UIMenu MaterialsSubMenu;
		[NonSerialized]
		public static UIMenu BasicCraftingSubMenu;

		public PlayerInventory()
		{
			PlayerInventoryItems.Add(BandageItem);
			PlayerInventoryItems.Add(MetalPlateItem);
			PlayerInventoryItems.Add(WaterItem);
			PlayerInventoryItems.Add(SodaItem);
			PlayerInventoryItems.Add(ChocolateItem);
			PlayerInventoryItems.Add(CanItem);
			PlayerInventoryItems.Add(CookedMeatItem);
			PlayerInventoryMaterials.Add(FabricMaterial);
			PlayerInventoryMaterials.Add(MetalMaterial);
			PlayerInventoryMaterials.Add(WoodMaterial);
			PlayerInventoryMaterials.Add(PlasticMaterial);
			PlayerInventoryMaterials.Add(GlassMaterial);
			PlayerInventoryMaterials.Add(RawMeatMaterial);
			InventoryMenu = new UIMenu("Inventory", "See what you are carrying and what you can craft");
			Main.MasterMenuPool.Add(InventoryMenu);
			AddItemsSubMenu(InventoryMenu);
			AddMaterialsSubMenu(InventoryMenu);
			//AddBasicCraftingSubMenu(InventoryMenu);
			UIResRectangle banner = new UIResRectangle
			{
				Color = Color.FromArgb(255, Color.Gold)
			};
			InventoryMenu.SetBannerType(banner);
			Main.MasterMenuPool.RefreshIndex();
			Tick += OnTick;
			KeyDown += OnKeyDown;
		}

		public static void AddItemsSubMenu(UIMenu menu)
		{
			ItemsSubMenu = Main.MasterMenuPool.AddSubMenu(menu, "Items", "See what useful items you are carrying");
			UIResRectangle banner = new UIResRectangle
			{
				Color = Color.FromArgb(255, Color.Gold)
			};
			ItemsSubMenu.SetBannerType(banner);
			for (int i = 0; i < PlayerInventoryItems.Count; i++)
			{
				ItemsSubMenu.AddItem(new UIMenuItem(PlayerInventoryItems[i].Name, PlayerInventoryItems[i].Description));
				ItemsSubMenu.MenuItems[ItemsSubMenu.MenuItems.Count - 1].SetRightLabel("(" + PlayerInventoryItems[i].Amount + "/" + PlayerInventoryItems[i].MaxAmount + ")");
			}
			ItemsSubMenu.OnItemSelect += (sender, item, index) =>
			{
				if (PlayerInventoryItems[index].Amount > 0)
				{
					if (PlayerInventoryItems[index].GetType() == typeof(InventoryItemRestore) || PlayerInventoryItems[index].GetType() == typeof(InventoryItemRestoreCraftable))
					{
						InventoryItemRestore itemRestore = (InventoryItemRestore)PlayerInventoryItems[index];
						UseRestoreItem(itemRestore);
					}
				}
				else
				{
					Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
					UI.Notify("Cannot use ~b~" + item.Text + "~s~ as you are not carrying any.");
				}
			};
		}

		public static void AddMaterialsSubMenu(UIMenu menu)
		{
			MaterialsSubMenu = Main.MasterMenuPool.AddSubMenu(menu, "Materials", "See what materials that can be used for crafting you are carrying");
			UIResRectangle banner = new UIResRectangle
			{
				Color = Color.FromArgb(255, Color.Gold)
			};
			MaterialsSubMenu.SetBannerType(banner);
			for (int i = 0; i < PlayerInventoryMaterials.Count; i++)
			{
				MaterialsSubMenu.AddItem(new UIMenuItem(PlayerInventoryMaterials[i].Name, PlayerInventoryMaterials[i].Description));
				MaterialsSubMenu.MenuItems[MaterialsSubMenu.MenuItems.Count - 1].SetRightLabel("(" + PlayerInventoryMaterials[i].Amount + "/" + PlayerInventoryMaterials[i].MaxAmount + ")");
			}
		}

		public static void AddBasicCraftingSubMenu(UIMenu menu)
		{
			BasicCraftingSubMenu = Main.MasterMenuPool.AddSubMenu(menu, "Basic Crafting", "Craft basic items on the go from scrap materials");
			UIResRectangle banner = new UIResRectangle
			{
				Color = Color.FromArgb(255, Color.Gold)
			};
			BasicCraftingSubMenu.SetBannerType(banner);
			Crafting.AddCraftingItemsToMenu(BasicCraftingSubMenu, CraftType.Player);
			BasicCraftingSubMenu.OnItemSelect += (sender, item, index) =>
			{
				InventoryItem craftingItem;
				if (PlayerInventoryItems.Exists(match: a => a.Name == item.Text))
				{
					craftingItem = PlayerInventoryItems.Find(match: a => a.Name == item.Text);
				}
				else
				{
					craftingItem = PlayerInventoryMaterials.Find(match: a => a.Name == item.Text);
				}
				Crafting.Craft(craftingItem);
			};
		}

		public static void UseRestoreItem(InventoryItemRestore item)
		{
			switch (item.RestoreType)
			{
				case RestoreType.Health:
					{
						if (Game.Player.Character.Health >= Game.Player.Character.MaxHealth)
						{
							Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
							UI.Notify("Cannot use ~b~" + item.Name + "~s~ as your Health levels are full.");
							break;
						}
						int amount = (int)Math.Round(item.Restore);
						Game.Player.Character.Health += amount;
						if (Game.Player.Character.Health >= Game.Player.Character.MaxHealth)
						{
							Game.Player.Character.Health = Game.Player.Character.MaxHealth;
						}
						Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
						UI.Notify("Used ~b~" + item.Name + "~s~ to replenish Health levels.");
						UpdateItemsInventory(item, 1, InventoryUpdate.Decrease);
						break;
					}
				case RestoreType.Armor:
					{
						if (Game.Player.Character.Armor >= Game.Player.MaxArmor)
						{
							Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
							UI.Notify("Cannot use ~b~" + item.Name + "~s~ as your Armor levels are full.");
							break;
						}
						int amount = (int)Math.Round(item.Restore);
						Game.Player.Character.Armor += amount;
						if (Game.Player.Character.Armor >= Game.Player.MaxArmor)
						{
							Game.Player.Character.Armor = Game.Player.MaxArmor;
						}
						Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
						UI.Notify("Used ~b~" + item.Name + "~s~ to replenish Armor levels.");
						UpdateItemsInventory(item, 1, InventoryUpdate.Decrease);
						break;
					}
				case RestoreType.Hunger:
					{
						if (Character.CurrentHungerLevel >= Character.MaxHungerLevel)
						{
							Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
							UI.Notify("Cannot use ~b~" + item.Name + "~s~ as your Hunger levels are full.");
							break;
						}
						Character.CurrentHungerLevel += item.Restore;
						if (Character.CurrentHungerLevel >= Character.MaxHungerLevel)
						{
							Character.CurrentHungerLevel = Character.MaxHungerLevel;
						}
						Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
						UI.Notify("Used ~b~" + item.Name + "~s~ to replenish Hunger levels.");
						UpdateItemsInventory(item, 1, InventoryUpdate.Decrease);
						break;
					}
				case RestoreType.Thirst:
					{
						if (Character.CurrentThirstLevel >= Character.MaxThirstLevel)
						{
							Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
							UI.Notify("Cannot use ~b~" + item.Name + "~s~ as your Thirst levels are full.");
							break;
						}
						Character.CurrentThirstLevel += item.Restore;
						if (Character.CurrentThirstLevel >= Character.MaxThirstLevel)
						{
							Character.CurrentThirstLevel = Character.MaxThirstLevel;
						}
						Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
						UI.Notify("Used ~b~" + item.Name + "~s~ to replenish Thirst levels.");
						UpdateItemsInventory(item, 1, InventoryUpdate.Decrease);
						break;
					}
				case RestoreType.Energy:
					{
						if (Character.CurrentEnergyLevel >= Character.MaxEnergyLevel)
						{
							Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
							UI.Notify("Cannot use ~b~" + item.Name + "~s~ as your Energy levels are full.");
							break;
						}
						Character.CurrentEnergyLevel += item.Restore;
						if (Character.CurrentEnergyLevel >= Character.MaxEnergyLevel)
						{
							Character.CurrentEnergyLevel = Character.MaxEnergyLevel;
						}
						Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
						UI.Notify("Used ~b~" + item.Name + "~s~ to replenish Energy levels.");
						UpdateItemsInventory(item, 1, InventoryUpdate.Decrease);
						break;
					}
				case RestoreType.Infection:
					{
						// Placeholder for Zombie Infection mode code 
						break;
					}
				case RestoreType.Battery:
					{
						// Placeholder for Torch Battery mode code
						break;
					}
			}
		}

		public static void UpdateItemsInventory(InventoryItem item, int amount, InventoryUpdate type)
		{
			switch (type)
			{
				case InventoryUpdate.Increase:
					{
						PlayerInventoryItems.Find(inventoryItem => inventoryItem.Name == item.Name).Amount += amount;
						break;
					}

				case InventoryUpdate.Decrease:
					{
						PlayerInventoryItems.Find(inventoryItem => inventoryItem.Name == item.Name).Amount -= amount;
						break;
					}
			}
			UpdateItemsMenus(item);
		}

		public static void UpdateItemsMenus(InventoryItem item)
		{
			ItemsSubMenu.MenuItems.Find(menuItem => menuItem.Text == item.Name).SetRightLabel("(" + PlayerInventoryItems.Find(inventoryItem => inventoryItem.Name == item.Name).Amount + "/" + PlayerInventoryItems.Find(inventoryItem => inventoryItem.Name == item.Name).MaxAmount + ")");
		}

		public static void UpdateMaterialsMenus(InventoryMaterial material)
		{
			MaterialsSubMenu.MenuItems.Find(menuMaterial => menuMaterial.Text == material.Name).SetRightLabel("(" + PlayerInventoryMaterials.Find(inventoryMaterial => inventoryMaterial.Name == material.Name).Amount + "/" + PlayerInventoryMaterials.Find(inventoryMaterial => inventoryMaterial.Name == material.Name).MaxAmount + ")");
		}

		public static void UpdateMaterialsInventory(InventoryMaterial material, int amount, InventoryUpdate type)
		{
			switch (type)
			{
				case InventoryUpdate.Increase:
					{
						PlayerInventoryMaterials.Find(inventoryMaterial => inventoryMaterial.Name == material.Name).Amount += amount;
						break;
					}

				case InventoryUpdate.Decrease:
					{
						PlayerInventoryMaterials.Find(inventoryMaterial => inventoryMaterial.Name == material.Name).Amount -= amount;
						break;
					}
			}
			UpdateMaterialsMenus(material);
		}

		public void OnTick(object sender, EventArgs e)
		{
			if (Main.ModActive)
			{
				if (Game.Player.Character.IsDead)
				{
					InventoryMenu.Visible = false;
				}
			}
		}

		public void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (Main.ModActive && e.KeyCode == Main.InventoryKey && !Main.MasterMenuPool.IsAnyMenuOpen())
			{
				InventoryMenu.Visible = !InventoryMenu.Visible;
			}
		}
	}
}
