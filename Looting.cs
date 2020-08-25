using System;
using System.Collections.Generic;
using System.Linq;
using CWDM.Enums;
using CWDM.Extensions;
using GTA;
using GTA.Math;
using GTA.Native;

namespace CWDM
{
    public class Looting : Script
    {
        public static List<Entity> LootedEntities = new List<Entity>();

        public static Model[] StoreModels =
        {
            new Model("v_ret_247shelves01"),
            new Model("v_ret_247shelves02"),
            new Model("v_ret_247shelves03"),
            new Model("v_ret_247shelves04"),
            new Model("v_ret_247shelves05")
        };

        public static Model[] BinModels =
        {
            new Model("phei_heist_kit_bin_01"),
            new Model("prop_bin_01a"),
            new Model("prop_bin_02a"),
            new Model("prop_bin_03a"),
            new Model("prop_bin_04a"),
            new Model("prop_bin_05a"),
            new Model("prop_bin_06a"),
            new Model("prop_bin_07a"),
            new Model("prop_bin_07b"),
            new Model("prop_bin_07c"),
            new Model("prop_bin_07d"),
            new Model("prop_bin_08a"),
            new Model("prop_bin_08open"),
            new Model("prop_bin_09a"),
            new Model("prop_bin_10a"),
            new Model("prop_bin_10b"),
            new Model("prop_bin_11a"),
            new Model("prop_bin_11b"),
            new Model("prop_bin_12a"),
            new Model("prop_bin_13a"),
            new Model("prop_bin_14a"),
            new Model("prop_bin_14b"),
            new Model("prop_bin_beach_01a"),
            new Model("prop_bin_beach_01d"),
            new Model("prop_bin_delpiero"),
            new Model("prop_bin_delpiero_b"),
            new Model("prop_cs_bin_01"),
            new Model("prop_cs_bin_01_skinned"),
            new Model("prop_cs_bin_02"),
            new Model("prop_cs_bin_03"),
            new Model("prop_food_bin_01"),
            new Model("prop_food_bin_02"),
            new Model("prop_recyclebin_01a"),
            new Model("prop_recyclebin_02_c"),
            new Model("prop_recyclebin_02_d"),
            new Model("prop_recyclebin_02a"),
            new Model("prop_recyclebin_02b"),
            new Model("prop_recyclebin_03_a"),
            new Model("prop_recyclebin_04_a"),
            new Model("prop_recyclebin_04_b"),
            new Model("prop_snow_bin_01"),
            new Model("prop_snow_bin_02"),
            new Model("zprop_bin_01a_old"),
            new Model("p_dumpster_t"),
            new Model("prop_cs_dumpster_01a"),
            new Model("prop_dumpster_01a"),
            new Model("prop_dumpster_02a"),
            new Model("prop_dumpster_02b"),
            new Model("prop_dumpster_3a"),
            new Model("prop_dumpster_4a"),
            new Model("prop_dumpster_4b")
        };

        public static Model[] SkipModels =
        {
            new Model("prop_skip_01a"),
            new Model("prop_skip_02a"),
            new Model("prop_skip_03"),
            new Model("prop_skip_04"),
            new Model("prop_skip_05a"),
            new Model("prop_skip_05b"),
            new Model("prop_skip_06a"),
            new Model("prop_skip_08a"),
            new Model("prop_skip_08b"),
            new Model("prop_skip_10a")
        };

        public static Vector3[] StoreLocations =
        {
            new Vector3(-3041.777f, 588.7258f, 7.908933f),
            new Vector3(-3243.759f, 1005.157f, 12.83071f),
            new Vector3(1732.932f, 6414.323f, 35.03724f),
            new Vector3(1963.272f, 3743.574f, 32.34375f),
            new Vector3(2678.908f, 3284.251f, 55.24114f),
            new Vector3(544.7951f, 2669.228f, 42.1565f),
            new Vector3(2557.156f, 384.4772f, 108.623f),
            new Vector3(377.7599f, 326.8445f, 103.5664f),
            new Vector3(29.1841f, -1346.031f, 29.49703f)
        };

        public Looting()
        {
            Tick += OnTick;
        }

        public static void Loot(Entity entity, LootType type)
        {
            if (type == LootType.Animal)
            {
                if (Game.Player.Character.Weapons.HasWeapon(WeaponHash.Knife))
                {
                    Game.Player.Character.Weapons.Select(WeaponHash.Knife, true);
                    Game.Player.Character.Task.PlayAnimation("pickup_object", "pickup_low", 8f, 3000,
                        AnimationFlags.None);
                    var materialsToFind = PlayerInventory.PlayerInventoryMaterials;
                    for (var i = 0; i < materialsToFind.Count; i++)
                    {
                        if (!materialsToFind[i].GetLootTypes().Contains(LootType.Animal))
                        {
                            materialsToFind.RemoveAt(i);
                        }
                    }

                    var materialFound = materialsToFind.GetRandomElementFromList();
                    var invCheckAmount = PlayerInventory.PlayerInventoryMaterials
                        .Find(material => material.Name == materialFound.Name).Amount;
                    var invCheckMaxAmount = PlayerInventory.PlayerInventoryMaterials
                        .Find(material => material.Name == materialFound.Name).MaxAmount;
                    if (invCheckAmount == invCheckMaxAmount)
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                        UI.Notify("You cannot carry any more ~b~" + materialFound.Name + "~s~!", true);
                    }
                    else
                    {
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                        LootedEntities.Add(entity);
                        PlayerInventory.UpdateItemsInventory(materialFound, 1, InventoryUpdate.Increase);
                        UI.Notify(
                            "You have harvested ~b~" + materialFound.Name + "~g~ (" +
                            PlayerInventory.PlayerInventoryItems.Find(item => item.Name == materialFound.Name).Amount +
                            "/" + PlayerInventory.PlayerInventoryItems.Find(item => item.Name == materialFound.Name)
                                .MaxAmount + ")", true);
                    }
                }
                else
                {
                    Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                    UI.Notify(
                        "You need a knife to harvest ~b~" + PlayerInventory.RawMeatMaterial.Name +
                        "~s~ from dead animals!", true);
                }
            }
            else
            {
                if (type == LootType.Ped || type == LootType.Skip)
                {
                    Game.Player.Character.Task.PlayAnimation("pickup_object", "pickup_low");
                    Wait(100);
                }
                else if (type == LootType.Vehicle)
                {
                    Game.Player.Character.Task.PlayAnimation("veh@handler@base", "hotwire");
                    Wait(200);
                }
                else
                {
                    var val = entity.Position - Game.Player.Character.Position;
                    Game.Player.Character.Heading = val.ToHeading();
                    Game.Player.Character.Task.PlayAnimation("oddjobs@shop_robbery@rob_till", "loop");
                    Wait(300);
                }

                var random = new Random();
                var chance = random.Next(0, 3);
                switch (chance)
                {
                    case 0:
                    {
                        var itemsToFind = PlayerInventory.PlayerInventoryItems;
                        for (var i = 0; i < itemsToFind.Count; i++)
                        {
                            if (!itemsToFind[i].GetLootTypes().Contains(type))
                            {
                                itemsToFind.RemoveAt(i);
                            }
                        }

                        var itemFound = itemsToFind.GetRandomElementFromList();
                        var invCheckAmount = PlayerInventory.PlayerInventoryItems.Find(item => item.Name == itemFound.Name)
                            .Amount;
                        var invCheckMaxAmount = PlayerInventory.PlayerInventoryItems
                            .Find(item => item.Name == itemFound.Name).MaxAmount;
                        if (invCheckAmount == invCheckMaxAmount)
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            UI.Notify("Found ~b~" + itemFound.Name + "~s~ but inventory is full!", true);
                        }
                        else
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            LootedEntities.Add(entity);
                            PlayerInventory.UpdateItemsInventory(itemFound, 1, InventoryUpdate.Increase);
                            UI.Notify(
                                "Found ~b~" + itemFound.Name + "~g~ (" +
                                PlayerInventory.PlayerInventoryItems.Find(item => item.Name == itemFound.Name).Amount +
                                "/" + PlayerInventory.PlayerInventoryItems.Find(item => item.Name == itemFound.Name)
                                    .MaxAmount + ")", true);
                        }

                        break;
                    }
                    case 1:
                    {
                        var materialsToFind = PlayerInventory.PlayerInventoryMaterials;
                        for (var i = 0; i < materialsToFind.Count; i++)
                        {
                            if (!materialsToFind[i].GetLootTypes().Contains(type))
                            {
                                materialsToFind.RemoveAt(i);
                            }
                        }

                        var materialFound = materialsToFind.GetRandomElementFromList();
                        var invCheckAmount = PlayerInventory.PlayerInventoryMaterials
                            .Find(material => material.Name == materialFound.Name).Amount;
                        var invCheckMaxAmount = PlayerInventory.PlayerInventoryMaterials
                            .Find(material => material.Name == materialFound.Name).MaxAmount;
                        if (invCheckAmount == invCheckMaxAmount)
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            UI.Notify("Found ~b~" + materialFound.Name + "~s~ but inventory is full!", true);
                        }
                        else
                        {
                            Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "CONTINUE", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                            LootedEntities.Add(entity);
                            PlayerInventory.UpdateMaterialsInventory(materialFound, 1, InventoryUpdate.Increase);
                            UI.Notify(
                                "Found ~b~" + materialFound.Name + "~g~ (" +
                                PlayerInventory.PlayerInventoryMaterials
                                    .Find(material => material.Name == materialFound.Name).Amount + "/" + PlayerInventory
                                    .PlayerInventoryMaterials.Find(material => material.Name == materialFound.Name)
                                    .MaxAmount + ")", true);
                        }

                        break;
                    }
                    default:
                        Function.Call(Hash.PLAY_SOUND_FRONTEND, -1, "ERROR", "HUD_FRONTEND_DEFAULT_SOUNDSET", 1);
                        UI.Notify("Found nothing", true);
                        LootedEntities.Add(entity);
                        break;
                }
            }
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (!Main.ModActive) return;
            var ped = World.GetClosest(Game.Player.Character.Position,
                World.GetNearbyPeds(Game.Player.Character, 1.5f));
            switch (ped?.IsDead)
            {
                case true when ped.IsHuman && !LootedEntities.Contains(ped):
                {
                    "Press ~INPUT_CONTEXT~ to search corpse".DisplayHelpTextThisFrame();
                    Game.DisableControlThisFrame(2, Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, Control.Context))
                    {
                        Loot(ped, LootType.Ped);
                    }
                    break;
                }
                case true when !ped.IsHuman && !LootedEntities.Contains(ped):
                {
                    "Press ~INPUT_CONTEXT~ to harvest meat from animal corpse".DisplayHelpTextThisFrame();
                    Game.DisableControlThisFrame(2, Control.Context);
                    if (Game.IsDisabledControlJustPressed(2, Control.Context))
                    {
                        Loot(ped, LootType.Animal);
                    }

                    break;
                }
            }

            var prop = World.GetClosest(Game.Player.Character.Position,
                World.GetNearbyProps(Game.Player.Character.Position, 1.5f));
            if (prop != null && StoreModels.Contains(prop.Model) && !LootedEntities.Contains(prop))
            {
                "Press ~INPUT_CONTEXT~ to search shelves".DisplayHelpTextThisFrame();
                Game.DisableControlThisFrame(2, Control.Context);
                if (Game.IsDisabledControlJustPressed(2, Control.Context))
                {
                    Loot(prop, LootType.Store);
                }
            }
            else if (prop != null && BinModels.Contains(prop.Model) && !LootedEntities.Contains(prop))
            {
                "Press ~INPUT_CONTEXT~ to search bin".DisplayHelpTextThisFrame();
                Game.DisableControlThisFrame(2, Control.Context);
                if (Game.IsDisabledControlJustPressed(2, Control.Context))
                {
                    Loot(prop, LootType.Bin);
                }
            }

            var prop2 = World.GetClosest(Game.Player.Character.Position,
                World.GetNearbyProps(Game.Player.Character.Position, 2.5f));
            if (prop2 != null && SkipModels.Contains(prop2.Model) && !LootedEntities.Contains(prop2))
            {
                "Press ~INPUT_CONTEXT~ to search skip".DisplayHelpTextThisFrame();
                Game.DisableControlThisFrame(2, Control.Context);
                if (Game.IsDisabledControlJustPressed(2, Control.Context))
                {
                    Loot(prop, LootType.Skip);
                }
            }

            if (!Game.Player.Character.IsInVehicle()) return;
            if (Game.Player.Character.CurrentVehicle.Driver != Game.Player.Character ||
                LootedEntities.Contains(Game.Player.Character.CurrentVehicle)) return;
            "Press ~INPUT_CONTEXT~ to search glovebox".DisplayHelpTextThisFrame();
            Game.DisableControlThisFrame(2, Control.Context);
            if (Game.IsDisabledControlJustPressed(2, Control.Context))
            {
                Loot(Game.Player.Character.CurrentVehicle, LootType.Vehicle);
            }
        }
    }
}