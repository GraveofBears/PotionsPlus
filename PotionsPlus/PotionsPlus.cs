// JotunnModStub
// a Valheim mod skeleton using Jötunn
// 

using System;
using System.Reflection;
using BepInEx;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

namespace PotionsPlus
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    internal partial class PotionsPlus : BaseUnityPlugin
    {
        private const string PluginGuid = "com.odinplus.potionsplus";
        private const string PluginName = "PotionsPlus";
        private const string PluginVersion = "2.0.1";


        private AssetBundle _assetBundle;
        private GameObject _fortificationPrGameObject;
        

        private void Awake()
        {
            LoadAssets();
            ConfigEntries();

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

            PrefabManager.OnPrefabsRegistered += Opalchemy;
            
            FlaskOfFortificationSe();
            MagelightSe();
            SecondWindSe();
            GodsSe();
            GrandTideSe();
            GrandSpiritualSe();
            GrandStaminaSe();
            GrandStealthSe();
            MediumTideSe();
            MedSpiritualSe();
            MedStaminaSe();
            LesserTideSe();
            LesserSpiritualSe();
            LesserStaminaSe();

        }

        private void LoadAssets()
        {
            _assetBundle = AssetUtils.LoadAssetBundleFromResources("potions", Assembly.GetExecutingAssembly());
        }

        //custom item addition example
        private void FlaskFortification()
        {
            _fortificationPrGameObject = _assetBundle.LoadAsset<GameObject>("Flask_of_Fortification");

           var prefab= new CustomItem(_fortificationPrGameObject, false,
                new ItemConfig
                {
                    Name = "Flask of Fortification",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Obsidian", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Flint", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Stone", Amount = 8, AmountPerLevel = 10},
                        new RequirementConfig {Item = "MeadTasty", Amount = 1, AmountPerLevel = 10}
                    }
                });
               prefab.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _fortificationSe;
               ItemManager.Instance.AddItem(prefab);
        }

        private void FlaskoftheGods()
        {
            _flaskofthegodsPrefab = _assetBundle.LoadAsset<GameObject>("Flask_of_the_Gods");
            var flaskofthegods = new CustomItem(_flaskofthegodsPrefab, fixReference: false,
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
            flaskofthegods.ItemDrop.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Consumable;
            flaskofthegods.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _godsSestat;
            ItemManager.Instance.AddItem(flaskofthegods);
        }

        private void Magelight()
        {
            _magelightPrefab = _assetBundle.LoadAsset<GameObject>("Flask_of_Magelight");
            
            var magelight = new CustomItem(_magelightPrefab, fixReference: false,
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
            magelight.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _magelightSestat;
            ItemManager.Instance.AddItem(magelight);
        }


        private void SecondWind()
        {
            _secondwindPrefab = _assetBundle.LoadAsset<GameObject>("Flask_of_Second_Wind");
            
            var secondwind = new CustomItem(_secondwindPrefab, fixReference: false,
                new ItemConfig
                {
                    Name = "Flask of Second Wind",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        //new RequirementConfig { Item = "SecondWind_PotionBase", Amount = 1, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Feathers", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig { Item = "Ooze", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig { Item = "FreezeGland", Amount = 2, AmountPerLevel = 10}
                    }
                });
            secondwind.ItemDrop.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Consumable;
            secondwind.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _secondWindSestat;
            ItemManager.Instance.AddItem(secondwind);
        }

        public void Grandtide()
        {
            var grandtide = new CustomItem(_assetBundle.LoadAsset<GameObject>("Grand_Healing_Tide_Potion"), false,
                new ItemConfig
                {
                    Name = "Grand Healing Tide Potion",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Cloudberry", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Needle", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Barley", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Ooze", Amount = 2, AmountPerLevel = 10}
                    }
                });
            grandtide.ItemDrop.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Consumable;
            grandtide.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _potion1;
            ItemManager.Instance.AddItem(grandtide);
        }

        private void GrandSpiritual()
        {
            _grandspiritual = new CustomItem(_assetBundle.LoadAsset<GameObject>("Grand_Spiritual_Healing_Potion"), false,
                new ItemConfig
                {
                    Name = "Grand Spiritual Healing Potion",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Cloudberry", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Flax", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "WolfFang", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Ooze", Amount = 4, AmountPerLevel = 10}
                    }
                });
            _grandspiritual.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _grandSpiritualSe;
            ItemManager.Instance.AddItem(_grandspiritual);
        }


        private void GrandStam()
        {
            _grandstam = new CustomItem(_assetBundle.LoadAsset<GameObject>("Grand_Stamina_Elixir"), false,
                new ItemConfig
                {
                    Name = "Grand Stamina Elixir",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Cloudberry", Amount = 8, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Carrot", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Turnip", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "LoxMeat", Amount = 2, AmountPerLevel = 10}
                    }
                });
            _grandstam.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _grandstamSE;
            ItemManager.Instance.AddItem(_grandstam);
        }

        private void GrandStealth()
        {
            _grandstealth = new CustomItem(_assetBundle.LoadAsset<GameObject>("Grand_Stealth_Elixir"), false,
                new ItemConfig
                {
                    Name = "Grand Stealth Elixir",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "FreezeGland", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Flax", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Feathers", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Carrot", Amount = 2, AmountPerLevel = 10}
                    }
                });
            _grandstealth.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _grandstealthSE;
            ItemManager.Instance.AddItem(_grandstealth);
        }

        private void Mediumtide()
        {
            _mediumtide = new CustomItem(_assetBundle.LoadAsset<GameObject>("Medium_Healing_Tide_Flask"), false,
                new ItemConfig
                {
                    Name = "Medium Healing Tide Flask",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Resin", Amount = 6, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Bloodbag", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Blueberries", Amount = 4, AmountPerLevel = 10}
                    }
                });
            ItemManager.Instance.AddItem(_mediumtide);
        }

        private void Mediumspiritual()
        {
            _mediumspiritual = new CustomItem(_assetBundle.LoadAsset<GameObject>("Medium_Spiritual_Healing_Flask"),
                false,
                new ItemConfig
                {
                    Name = "Medium Spiritual Healing Flask",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Bloodbag", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "BoneFragments", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Ooze", Amount = 2, AmountPerLevel = 10}
                    }
                });
            _mediumspiritual.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _mediumSpiritualSe;
            ItemManager.Instance.AddItem(_mediumspiritual);
        }

        private void Mediumstam()
        {
            _mediumstam = new CustomItem(_assetBundle.LoadAsset<GameObject>("Medium_Stamina_Flask"), false,
                new ItemConfig
                {
                    Name = "Medium Stamina Flask",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Resin", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Bloodbag", Amount = 2, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Blueberries", Amount = 4, AmountPerLevel = 10}
                    }
                });
            _mediumstam.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _mediumstamSe;
            ItemManager.Instance.AddItem(_mediumstam);
        }

        private void Lessertide()
        {
            _lessertide = new CustomItem(_assetBundle.LoadAsset<GameObject>("Lesser_Healing_Tide_Vial"), false,
                new ItemConfig
                {
                    Name = "Lesser Healing Tide Vial",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Raspberry", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Honey", Amount = 2, AmountPerLevel = 10}
                    }
                });
            _lessertide.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _lessertideSe;
            ItemManager.Instance.AddItem(_lessertide);
        }

        private void Lesserspiritual()
        {
            _lesserspiritual = new CustomItem(_assetBundle.LoadAsset<GameObject>("Lesser_Spiritual_Healing_Vial"),
                false,
                new ItemConfig
                {
                    Name = "Lesser Spiritual Healing Vial",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Raspberry", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Dandelion", Amount = 2, AmountPerLevel = 10}
                    }
                });
            _lesserspiritual.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _lesserSpiritualSe;
            ItemManager.Instance.AddItem(_lesserspiritual);
        }

        private void Lesserstam()
        {
            _lesserstam = new CustomItem(_assetBundle.LoadAsset<GameObject>("Lesser_Stamina_Vial"), false,
                new ItemConfig
                {
                    Name = "Lesser Stamina Vial",
                    Amount = 1,
                    CraftingStation = "opalchemy",
                    Requirements = new[]
                    {
                        new RequirementConfig {Item = "Mushroom", Amount = 4, AmountPerLevel = 10},
                        new RequirementConfig {Item = "Honey", Amount = 2, AmountPerLevel = 10}
                    }
                });
            _lesserstam.ItemDrop.m_itemData.m_shared.m_consumeStatusEffect = _lesserstamSe;
            ItemManager.Instance.AddItem(_lesserstam);
        }

        private void Opalchemy()
        {
            try
            {
                var sfxhammer = PrefabManager.Cache.GetPrefab<GameObject>("sfx_build_hammer_wood");
                var vfxPlaceWoodPole = PrefabManager.Cache.GetPrefab<GameObject>("vfx_Place_wood_pole");
                _buildsounds = new EffectList
                {
                    m_effectPrefabs = new[]
                    {
                        new EffectList.EffectData {m_prefab = sfxhammer},
                        new EffectList.EffectData {m_prefab = vfxPlaceWoodPole}
                    }
                };
                Jotunn.Logger.LogInfo("Loaded the SFX for the brewstand");
                var opalchemy = new CustomPiece(_assetBundle.LoadAsset<GameObject>("opalchemy"),
                    new PieceConfig
                    {
                        AllowedInDungeons = false,
                        Category = "Potions",
                        Enabled = true,
                        PieceTable = "Hammer",
                        Name = "Alchemy Table",
                        CraftingStation = "piece_workbench",
                        Description = "A special brewing stand to make your potions....",
                        Requirements = new[]
                        {
                            new RequirementConfig {Amount = 1, Item = "Wood", AmountPerLevel = 1, Recover = false}
                        }
                    });
                opalchemy.Piece.m_placeEffect = _buildsounds;
                PieceManager.Instance.AddPiece(opalchemy);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Issue Loading OP Alchemy Table{ex}");
            }
            finally
            {
                PrefabManager.OnPrefabsRegistered -= Opalchemy;
            }

        }

 

        # region SE Configuration items
        
        private EffectList _buildsounds;
        
        private CustomStatusEffect _sePotion1;
        private SE_Stats _potion1 = ScriptableObject.CreateInstance<SE_Stats>();
        
        private CustomStatusEffect _fortification;
        private SE_Stats _fortificationSe = ScriptableObject.CreateInstance<SE_Stats>();

        private CustomStatusEffect _seMagelight;
        private SE_Stats _magelightSestat = ScriptableObject.CreateInstance<SE_Stats>();
        private GameObject _magelightPrefab;

        private CustomStatusEffect _seSecondWind;
        private SE_Stats _secondWindSestat = ScriptableObject.CreateInstance<SE_Stats>();
        private GameObject _secondwindPrefab;

        private CustomStatusEffect _seGods;
        private SE_Stats _godsSestat = ScriptableObject.CreateInstance<SE_Stats>();
        private GameObject _flaskofthegodsPrefab;

        private CustomStatusEffect _seGrandSpiritual;
        private SE_Stats _grandSpiritualSe = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomItem _grandspiritual;
        
        
        private CustomItem _grandstam;
        private SE_Stats _grandstamSE = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seGrandStam;

        private CustomItem _grandstealth;
        private SE_Stats _grandstealthSE = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seGrandStealth;
        
        private CustomItem _mediumtide;
        private SE_Stats _mediumtideSE = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seMediumTide;
        
        
        private CustomItem _mediumspiritual;
        private SE_Stats _mediumSpiritualSe = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seMediumSpiritual;
        
        private CustomItem _mediumstam;
        private SE_Stats _mediumstamSe = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seMediumStam;
        
        private CustomItem _lessertide;
        private SE_Stats _lessertideSe = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seLesserTide;
        
        private CustomItem _lesserspiritual;
        private SE_Stats _lesserSpiritualSe = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seLesserSpiritual;
        
        private CustomItem _lesserstam;
        private SE_Stats _lesserstamSe = ScriptableObject.CreateInstance<SE_Stats>();
        private CustomStatusEffect _seLesserStam;
        

        #endregion
    }
}