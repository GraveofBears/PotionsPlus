// JotunnModStub
// a Valheim mod skeleton using Jötunn
// 
// File:    JotunnModStub.cs
// Project: JotunnModStub

using BepInEx;
using UnityEngine;
using BepInEx.Configuration;
using Jotunn.Utils;
using System.Reflection;
using Jotunn.Entities;
using Jotunn.Configs;
using Jotunn.Managers;

namespace PotionsPlus
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    internal class PotionsPlus : BaseUnityPlugin
    {
        public const string PluginGUID = "com.odinplus.potionsplus";
        public const string PluginName = "PotionsPlus";
        public const string PluginVersion = "2.0.1";

        private AssetBundle potions;

        private void Awake()
        {
            LoadAssets();
            FlaskFortification();
            FlaskoftheGods();
            Magelight();
            SecondWind();
            Grandtide();
            GrandSpiritual();
            GrandStam();
            GrandStealth();
            Mediumtide();
            Mediumspiritual();
            Mediumstam();
            Lessertide();
            Lesserspiritual();
            Lesserstam();
            opalchemy();

        } 

        private void LoadAssets()
        {
         potions = AssetUtils.LoadAssetBundleFromResources("potions", Assembly.GetExecutingAssembly());
        }

        //custom item addition example
        private void FlaskFortification()
        {
            var fortification_prefab = potions.LoadAsset<GameObject>("Flask_of_Fortification");
            /*
            var fortification = new CustomItem(fortification_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Fortification",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Obsidian", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Flint", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Stone", Amount = 8, AmountPerLevel = 10},
                        new RequirementConfig { Item = "MeadTasty", Amount = 1, AmountPerLevel = 10}
                    }
                });
            */
            PrefabManager.Instance.AddPrefab(fortification_prefab);
        }

        private void FlaskoftheGods()
        {
            var flaskofthegods_prefab = potions.LoadAsset<GameObject>("Flask_of_the_Gods");
            /*
            var flaskofthegods = new CustomItem(flaskofthegods_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of The Gods",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Carrot", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Thistle", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Flax", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "MeadTasty", Amount = 1, AmountPerLevel = 10}
                    }
                });
            */
            PrefabManager.Instance.AddPrefab(flaskofthegods_prefab);
        }

        private void Magelight()
        {
            var magelight_prefab = potions.LoadAsset<GameObject>("Flask_of_Magelight");
            /*
            var magelight = new CustomItem(magelight_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Magelight",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "GreydwarfEye", Amount = 8, AmountPerLevel = 10},
                        new RequirementConfig { Item = "FreezeGland", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "BoneFragments", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "MeadTasty", Amount = 1, AmountPerLevel = 10}
                    }
                });
            */
            PrefabManager.Instance.AddPrefab(magelight_prefab);
        }
    

        private void SecondWind()
        {
            var secondwind_prefab = potions.LoadAsset<GameObject>("Flask_of_Second_Wind");
            /*
            var secondwind = new CustomItem(secondwind_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Second Wind",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "SecondWind_PotionBase", Amount = 1, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Feathers", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Ooze", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "FreezeGland", Amount = 2, AmountPerLevel = 10}
                    }
                });
            */
            PrefabManager.Instance.AddPrefab(secondwind_prefab);
        }

        private void Grandtide()
        {
            var grandtide_prefab = potions.LoadAsset<GameObject>("Grand_Healing_Tide_Potion");
            var grandtide = new CustomItem(grandtide_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Grand Healing Tide Potion",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Cloudberry", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Needle", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Barley", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Ooze", Amount = 2, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(grandtide);
        }

        private void GrandSpiritual()
        {
            var grandspiritual_prefab = potions.LoadAsset<GameObject>("Grand_Spiritual_Healing_Potion");
            var grandspiritual = new CustomItem(grandspiritual_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Grand Spiritual Healing Potion",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Cloudberry", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Flax", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "WolfFang", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Ooze", Amount = 4, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(grandspiritual);
        }

        private void GrandStam()
        {
            var grandstam_prefab = potions.LoadAsset<GameObject>("Grand_Stamina_Elixir");
            var grandstam = new CustomItem(grandstam_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Grand Stamina Elixir",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Cloudberry", Amount = 8, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Carrot", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Turnip", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "LoxMeat", Amount = 2, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(grandstam);
        }

        private void GrandStealth()
        {
            var grandstealth_prefab = potions.LoadAsset<GameObject>("Grand_Stealth_Elixir");
            var grandstealth = new CustomItem(grandstealth_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Grand Stealth Elixir",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "FreezeGland", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Flax", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Feathers", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Carrot", Amount = 2, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(grandstealth);
        }

        private void Mediumtide()
        {
            var mediumtide_prefab = potions.LoadAsset<GameObject>("Medium_Healing_Tide_Flask");
            var mediumtide = new CustomItem(mediumtide_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Medium Healing Tide Flask",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Resin", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Bloodbag", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Blueberries", Amount = 4, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(mediumtide);
        }

        private void Mediumspiritual()
        {
            var mediumspiritual_prefab = potions.LoadAsset<GameObject>("Medium_Spiritual_Healing_Flask");
            var mediumspiritual = new CustomItem(mediumspiritual_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Medium Spiritual Healing Flask",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Bloodbag", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "BoneFragments", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Ooze", Amount = 2, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(mediumspiritual);
        }

        private void Mediumstam()
        {
            var mediumstam_prefab = potions.LoadAsset<GameObject>("Medium_Stamina_Flask");
            var mediumstam = new CustomItem(mediumstam_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Medium Stamina Flask",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Resin", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Bloodbag", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Blueberries", Amount = 4, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(mediumstam);
        }

        private void Lessertide()
        {
            var lessertide_prefab = potions.LoadAsset<GameObject>("Lesser_Healing_Tide_Vial");
            var lessertide = new CustomItem(lessertide_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Lesser Healing Tide Vial",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Raspberry", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Honey", Amount = 2, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(lessertide);
        }

        private void Lesserspiritual()
        {
            var lesserspiritual_prefab = potions.LoadAsset<GameObject>("Lesser_Spiritual_Healing_Vial");
            var lesserspiritual = new CustomItem(lesserspiritual_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Lesser Spiritual Healing Vial",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Raspberry", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Dandelion", Amount = 2, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(lesserspiritual);
        }

        private void Lesserstam()
        {
            var lesserstam_prefab = potions.LoadAsset<GameObject>("Lesser_Stamina_Vial");
            var lesserstam = new CustomItem(lesserstam_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Lesser Stamina Vial",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Mushroom", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Honey", Amount = 2, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(lesserstam);
        }
        
       private void opalchemy()
        {
            var opalchemy_prefab = potions.LoadAsset<GameObject>("opalchemy");
            var opalchemy = new CustomItem(lesserstam_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Alchemy Table",
                    Amount = 1,
                    CraftingStation = "_HammerPieceTable",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 8, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Stone", Amount = 12, AmountPerLevel = 10}
                    }
                });
            ItemPieceManager.Instance.AddItem(opalchemy);
        }

    }
}
