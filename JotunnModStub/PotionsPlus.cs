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
        public const string PluginVersion = "0.0.1";

        private AssetBundle potions;
        public CustomStatusEffect PotionEffect;

        private void Awake()
        {
            LoadAssets();
            FlaskFortification();
            FlaskoftheGods();
            Magelight();
            Grandtide();
            GrandSpiritual();
            GrandSpiritual();
            GrandSpiritual();
            GrandSpiritual();
            GrandSpiritual();
            GrandSpiritual();
            GrandSpiritual();
            MeadToPotion();
           // FermenterTweak1();
           // CustomStatusEffect();
           // ExamplePotionnewSE();
        }

        private void LoadAssets()
        {
         potions = AssetUtils.LoadAssetBundleFromResources("potions", Assembly.GetExecutingAssembly());
        }

        //custom item addition example
        private void FlaskFortification()
        {
             var fortification_prefab = potions.LoadAsset<GameObject>("Flask_of_Fortification");
            var fortification = new CustomItem(fortification_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Fortification",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(fortification);
        }

        private void FlaskoftheGods()
        {
            var flaskofthegods_prefab = potions.LoadAsset<GameObject>("Flask_of_the_Gods");
            var flaskofthegods = new CustomItem(flaskofthegods_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Fortification",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(flaskofthegods);
        }

        private void Magelight()
        {
            var magelight_prefab = potions.LoadAsset<GameObject>("Flask_of_Magelight");
            var magelight = new CustomItem(magelight_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Magelight",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(magelight);
        }

        private void SecondWind()
        {
            var secondwind_prefab = potions.LoadAsset<GameObject>("Flask_of_Second_Wind");
            var secondwind = new CustomItem(secondwind_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Second Wind",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(secondwind);
        }

        private void Grandtide()
        {
            var grandtide_prefab = potions.LoadAsset<GameObject>("Grand_Healing_Tide_Potion");
            var grandtide = new CustomItem(grandtide_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Grand Healing Tide Potion",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
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
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(grandspiritual);
        }

        private void foopotion1()
        {
            var magelight_prefab = potions.LoadAsset<GameObject>("Flask_of_Magelight");
            var magelight = new CustomItem(magelight_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Magelight",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(magelight);
        }

        private void foopotion2()
        {
            var magelight_prefab = potions.LoadAsset<GameObject>("Flask_of_Magelight");
            var magelight = new CustomItem(magelight_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Magelight",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(magelight);
        }

        private void foopotion3()
        {
            var magelight_prefab = potions.LoadAsset<GameObject>("Flask_of_Magelight");
            var magelight = new CustomItem(magelight_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Magelight",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(magelight);
        }

        //custom item conversion example

        private void MeadToPotion()
        {
            var meadbasefab = potions.LoadAsset<GameObject>("potionbase");
            var potionbase = new CustomItem(meadbasefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Potion Base",
                    Amount = 4,
                    CraftingStation = "piece_cauldron",
                    Requirements = new []
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });


            var FermenterTweak = new CustomItemConversion(new FermenterConversionConfig
            {
                Station = "Fermenter",
                FromItem = "potionbase",
                ToItem = "Flask_of_Magelight"
            });
            ItemManager.Instance.AddItem(potionbase);
            ItemManager.Instance.AddItemConversion(FermenterTweak);
        }

/*
        private void FermenterTweak1()
        {
            var Tweakmyfermenter = new CustomItemConversion(new FermenterConversionConfig
            {
                Station = default,
                FromItem = "thinghereforbaseneedstobeprefabname",
                ToItem = "prefabnamefermenterspitsout"
            });
            ItemManager.Instance.AddItemConversion(Tweakmyfermenter);
        }
*/

 /*       //example with extra brokedown itemdrop via code to assign TTL via code 
        private void ExamplePotionnewSE()
        {
            var secondwind_prefab = potions.LoadAsset<GameObject>("Flask_of_Second_Wind");
            var secondwind = new CustomItem(secondwind_prefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Second Wind",
                    Amount = 1,
                    CraftingStation = "piece_cauldron",
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = "Wood", Amount = 10, AmountPerLevel = 10}
                    }
                });
            var itemDrop = secondwind.ItemDrop;
            itemDrop.m_itemData.m_shared.m_consumeStatusEffect = PotionEffect.StatusEffect;
            ItemManager.Instance.AddItem(secondwind);
        }
 */
 /*

        //this is a custom status effect example to allow any input via code to the status effect
        private void CustomStatusEffect()
        {
            StatusEffect effect = ScriptableObject.CreateInstance<StatusEffect>();
            effect.name = "Test SE";
            effect.m_name = "StonedAF";
            effect.m_startMessageType = MessageHud.MessageType.TopLeft;
            effect.m_startMessage = "Feeling nice mon";
            effect.m_stopMessageType = MessageHud.MessageType.TopLeft;
            effect.m_stopMessage = "coming down mon";
            effect.m_icon = AssetUtils.LoadSpriteFromFile("JotunnModStub/Assets/odinspot.png");
            effect.m_ttl = 100000; //eventually you replace this with a variable call to the cfg file so that it will just read the cfg file for the #
            PotionEffect = new CustomStatusEffect(effect, fixReference: false);

            ItemManager.Instance.AddStatusEffect(PotionEffect);
        }*/
    }
}