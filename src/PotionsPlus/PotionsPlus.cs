using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using PotionsPlus.Names;
using System;
using System.Reflection;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace PotionsPlus
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency(Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.ClientMustHaveMod, VersionStrictness.Minor)]
    [UsedImplicitly]
    public partial class PotionsPlus : BaseUnityPlugin
    {
        private const string PluginGuid = "com.odinplus.potionsplus";
        public const string PluginName = "PotionsPlus";
        public const string PluginVersion = "3.2.0";
        [UsedImplicitly] public static ConfigEntry<int> NexusId;
        private AssetBundle _assetBundle;
        public static PotionsPlus Instance;
        private Harmony _harmony;

        public PotionsPlus()
        {
            Instance = this;
            NexusId = Config.Bind("General", "NexusID", 1561, new ConfigDescription("Nexus mod ID for updates", null, new ConfigurationManagerAttributes { IsAdminOnly = false, Browsable = false, ReadOnly = true }));
        }

        [UsedImplicitly]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "UsedImplicitly")]
        private void Awake()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                _assetBundle = AssetUtils.LoadAssetBundleFromResources("potions", typeof(PotionsPlus).Assembly);

#if DEBUG
                foreach (var assetName in _assetBundle.GetAllAssetNames())
                {
                    Jotunn.Logger.LogInfo(assetName);
                }
#endif

                ConfigEntries();
                OdinPotionsAlchemyCraftingStation();
                OdinPotionsCauldron();

                FlaskElements();
                FlaskFortification();
                FlaskOfTheGods();
                FlaskOfMagelight();
                FlaskOfSecondWind();

                GrandHealingTideElixir();
                GrandSpiritualHealingElixir();
                GrandStaminaElixir();
                GrandStealthElixir();

                MediumHealingTidePotion();
                MediumSpiritualHealingPotion();
                MediumStaminaPotion();

                LesserHealingTideVial();
                LesserSpiritualHealingVial();
                LesserStaminaVial();

                PotionMeadbase();
                AddToSkills();
                LoadStatusEffects();
                PhilosopherStoneGreen();
                PhilosopherStoneRed();
                PhilosopherStoneBlue();
                PhilosopherStonePurple();
                PhilosopherStoneBlack();

                _assetBundle.Unload(false);

                _harmony = Harmony.CreateAndPatchAll(typeof(PotionsPlus).Assembly, PluginGuid);
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        [UsedImplicitly]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "UsedImplicitly")]
        private void OnDestroy()
        {
            try
            {
                _harmony?.UnpatchSelf();
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError(e);
            }
        }

        #region Flasks

        private void FlaskElements()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");

                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.FlaskOfElements);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }


                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _elementsTtl.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = ItemDropNames.FreezeGland,       Amount = 2  }, 
                        new RequirementConfig { Item = ItemDropNames.ElderBark,         Amount = 4  },
                        new RequirementConfig { Item = ItemDropNames.Entrails,          Amount = 8  },
                        new RequirementConfig { Item = ItemDropNames.PotionMeadbase,    Amount = 1  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void FlaskFortification()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");

                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.FlaskOfFortification);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _fortificationTtl.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig {  Item = ItemDropNames.Obsidian,        Amount = 2  },
                        new RequirementConfig {  Item = ItemDropNames.Flint,           Amount = 4  },
                        new RequirementConfig {  Item = ItemDropNames.Stone,           Amount = 8  },
                        new RequirementConfig {  Item = ItemDropNames.PotionMeadbase,  Amount = 1  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void FlaskOfTheGods()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.FlaskOfTheGods);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }


                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _godsTTl.Value;
                statusEffect.m_healthOverTime = _godsHealovertime.Value;
                statusEffect.m_healthRegenMultiplier = _godsregen.Value;
                statusEffect.m_healthOverTimeDuration = _hotDuration.Value;
                statusEffect.m_healthOverTimeInterval = _hotInterval.Value;
                statusEffect.m_healthOverTimeTicks = _hotTicks.Value;
                statusEffect.m_healthOverTimeTimer = _hoTtimer.Value;
                statusEffect.m_healthOverTimeTickHP = _hotTimeTickHp.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig {  Item = ItemDropNames.Carrot,          Amount = 2  },
                        new RequirementConfig {  Item = ItemDropNames.Thistle,         Amount = 4  },
                        new RequirementConfig {  Item = ItemDropNames.Flax,            Amount = 4  },
                        new RequirementConfig {  Item = ItemDropNames.PotionMeadbase,  Amount = 1  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void FlaskOfMagelight()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.FlaskOfMagelight);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _magelightTtl.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{  Item = ItemDropNames.GreydwarfEye,      Amount = 8  },
                        new RequirementConfig{  Item = ItemDropNames.FreezeGland,       Amount = 4  },
                        new RequirementConfig{  Item = ItemDropNames.BoneFragments,     Amount = 4  },
                        new RequirementConfig{  Item = ItemDropNames.PotionMeadbase,    Amount = 1  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void FlaskOfSecondWind()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.FlaskOfSecondWind);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_cooldown = _secondWindCooldown.Value;
                statusEffect.m_ttl = _secondWindTtl.Value;
                statusEffect.m_runStaminaDrainModifier = _secondWindRunDrain.Value;
                statusEffect.m_jumpStaminaUseModifier = _secondWindJumpDrain.Value;
                statusEffect.m_staminaRegenMultiplier = _secondWindRegen.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{  Item = ItemDropNames.FreezeGland,     Amount = 2  },
                        new RequirementConfig{  Item = ItemDropNames.Feathers,        Amount = 6  },
                        new RequirementConfig{  Item = ItemDropNames.Ooze,            Amount = 4  },
                        new RequirementConfig{  Item = ItemDropNames.PotionMeadbase,  Amount = 1  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        #endregion

        #region Elixirs

        private void GrandHealingTideElixir()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.GrandHealingTidePotion);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _grandTideTtl.Value;
                statusEffect.m_cooldown = _grandTideCooldownTimer.Value;
                statusEffect.m_healthOverTime = _grandHealthOvertime.Value;
                statusEffect.m_healthRegenMultiplier = _grandTideRegen.Value;
                statusEffect.m_healthOverTimeDuration = _grandHealthOvertimeDuration.Value;
                statusEffect.m_healthOverTimeInterval = _grandHealthOvertimeInterval.Value;
                statusEffect.m_healthOverTimeTicks = _grandHealthOvertimeTicks.Value;
                statusEffect.m_healthOverTimeTimer = _grandHealthOvertimeTimer.Value;
                statusEffect.m_healthOverTimeTickHP = _grandHealthOvertimeTickHp.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = ItemDropNames.Cloudberry,    Amount = 6 },
                        new RequirementConfig { Item = ItemDropNames.Needle,        Amount = 2 },
                        new RequirementConfig { Item = ItemDropNames.Barley,        Amount = 4 },
                        new RequirementConfig { Item = ItemDropNames.Ooze,          Amount = 2 }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void GrandSpiritualHealingElixir()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.GrandSpiritualHealingPotion);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _grandSiritualHealingTtl.Value;
                statusEffect.m_cooldown = _grandSpiritualHealingCooldownTimer.Value;
                statusEffect.m_healthOverTime = _grandSpiritualHealingOvertime.Value;
                statusEffect.m_healthOverTimeDuration = _grandSpiritualHealingOvertimeDuration.Value;
                statusEffect.m_healthOverTimeInterval = _grandSpiritualHealingOvertimeInterval.Value;
                statusEffect.m_healthOverTimeTicks = _grandSpiritualHealingOvertimeTicks.Value;
                statusEffect.m_healthOverTimeTimer = _grandSpiritualHealingOvertimeTimer.Value;
                statusEffect.m_healthOverTimeTickHP = _grandSpiritualHealingOvertimeTickHp.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = ItemDropNames.Cloudberry,    Amount = 6   },
                        new RequirementConfig { Item = ItemDropNames.Flax,          Amount = 4   },
                        new RequirementConfig { Item = ItemDropNames.WolfFang,      Amount = 2   },
                        new RequirementConfig { Item = ItemDropNames.Ooze,          Amount = 4   }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void GrandStaminaElixir()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.GrandStaminaElixir);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _grandStaminaTtl.Value;
                statusEffect.m_cooldown = _grandStaminaCooldown.Value;
                statusEffect.m_healthOverTime = _grandStaminaOvertime.Value;
                statusEffect.m_staminaDrainPerSec = _grandStaminaDrainPS.Value;
                statusEffect.m_jumpStaminaUseModifier = _grandStaminaJump.Value;
                statusEffect.m_runStaminaDrainModifier = _grandStaminaRun.Value;
                statusEffect.m_staminaRegenMultiplier = _grandStaminaRegen.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = ItemDropNames.Cloudberry,    Amount = 8  },
                        new RequirementConfig { Item = ItemDropNames.Carrot,        Amount = 4  },
                        new RequirementConfig { Item = ItemDropNames.Turnip,        Amount = 4  },
                        new RequirementConfig { Item = ItemDropNames.LoxMeat,       Amount = 2  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void GrandStealthElixir()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.GrandStealthElixir);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _grandStealthTtl.Value;
                statusEffect.m_cooldown = _grandStealthCooldown.Value;
                statusEffect.m_stealthModifier = _grandStealthStealthModifier.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = ItemDropNames.FreezeGland,   Amount = 2  },
                        new RequirementConfig { Item = ItemDropNames.Flax,          Amount = 4  },
                        new RequirementConfig { Item = ItemDropNames.Feathers,      Amount = 2  },
                        new RequirementConfig { Item = ItemDropNames.Carrot,        Amount = 2  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        #endregion

        #region Medium Potions

        private void MediumHealingTidePotion()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.MediumHealingTideFlask);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _medTideTtl.Value;
                statusEffect.m_cooldown = _medTideCooldownTimer.Value;
                statusEffect.m_healthRegenMultiplier = _medTideRegen.Value;
                statusEffect.m_healthOverTime = _medHealingOvertime.Value;
                statusEffect.m_healthOverTimeDuration = _medHotDuration.Value;
                statusEffect.m_healthOverTimeInterval = _medHotInterval.Value;
                statusEffect.m_healthOverTimeTicks = _medHotTicks.Value;
                statusEffect.m_healthOverTimeTimer = _medHoTtimer.Value;
                statusEffect.m_healthOverTimeTickHP = _medHotTimeTickHp.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig { Item = ItemDropNames.Resin,       Amount = 6  },
                        new RequirementConfig { Item = ItemDropNames.Bloodbag,    Amount = 2  },
                        new RequirementConfig { Item = ItemDropNames.Blueberries, Amount = 4  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void MediumSpiritualHealingPotion()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.MediumSpiritualHealingFlask);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _medSpHtl.Value;
                statusEffect.m_cooldown = _medSpCooldown.Value;
                statusEffect.m_healthOverTime = _medSpHotOvertime.Value;
                statusEffect.m_healthOverTimeDuration = _medSphotDuration.Value;
                statusEffect.m_healthOverTimeInterval = _medSpHotInterval.Value;
                statusEffect.m_healthOverTimeTicks = _medSphoTticks.Value;
                statusEffect.m_healthOverTimeTimer = _medSphoTtimer.Value;
                statusEffect.m_healthOverTimeTickHP = _medSphoTtimeTickHp.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{Item = ItemDropNames.Bloodbag, Amount = 2},
                        new RequirementConfig{Item = ItemDropNames.BoneFragments, Amount = 4},
                        new RequirementConfig{Item = ItemDropNames.Ooze, Amount = 2}
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void MediumStaminaPotion()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.MediumStaminaFlask);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _medStaminaTtl.Value;
                statusEffect.m_cooldown = _medStaminaCooldown.Value;
                statusEffect.m_healthOverTime = _medStaminaOvertime.Value;
                statusEffect.m_staminaDrainPerSec = _medStaminaDrainPS.Value;
                statusEffect.m_jumpStaminaUseModifier = _medStaminaJump.Value;
                statusEffect.m_runStaminaDrainModifier = _medStaminaRun.Value;
                statusEffect.m_staminaRegenMultiplier = _medStaminaRegen.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{Item = ItemDropNames.Resin, Amount = 4},
                        new RequirementConfig{Item = ItemDropNames.Bloodbag, Amount = 2},
                        new RequirementConfig{Item = ItemDropNames.Blueberries, Amount = 4}
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        #endregion

        #region Lesser Vials

        private void LesserHealingTideVial()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.LesserHealingTideVial);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _lesserTideTtl.Value;
                statusEffect.m_cooldown = _lesserTideCooldownTimer.Value;
                statusEffect.m_healthOverTime = _lesserHealthOvertime.Value;
                statusEffect.m_healthRegenMultiplier = _lesserTideRegen.Value;
                statusEffect.m_healthOverTimeDuration = _lesserHealthOvertimeDuration.Value;
                statusEffect.m_healthOverTimeInterval = _lesserHealthOvertimeInterval.Value;
                statusEffect.m_healthOverTimeTicks = _lesserHealthOvertimeTicks.Value;
                statusEffect.m_healthOverTimeTimer = _lesserHealthOverTimeTimer.Value;
                statusEffect.m_healthOverTimeTickHP = _lesserHealthOvertimeTickHp.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{Item = ItemDropNames.Raspberry,   Amount = 4  },
                        new RequirementConfig{Item = ItemDropNames.Honey,       Amount = 2  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void LesserSpiritualHealingVial()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.LesserSpiritualHealingVial);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _lesserSpHtl.Value;
                statusEffect.m_cooldown = _lesserSpCooldown.Value;
                statusEffect.m_healthOverTime = _lesserSpHot.Value;
                statusEffect.m_healthOverTimeDuration = _lesserSpHotDuration.Value;
                statusEffect.m_healthOverTimeInterval = _lesserSpHotInterval.Value;
                statusEffect.m_healthOverTimeTicks = _lesserSpHoTticks.Value;
                statusEffect.m_healthOverTimeTimer = _lesserSpHoTtimer.Value;
                statusEffect.m_healthOverTimeTickHP = _lesserSpHoTtimeTickHp.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{  Item = ItemDropNames.Raspberry, Amount = 4  },
                        new RequirementConfig{  Item = ItemDropNames.Dandelion, Amount = 2  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void LesserStaminaVial()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.LesserStaminaVial);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();

                if (itemDrop == null)
                {
                    throw new NullReferenceException(nameof(itemDrop));
                }

                var statusEffect = itemDrop.m_itemData.m_shared.m_consumeStatusEffect as SE_Stats;

                if (statusEffect == null)
                {
                    throw new NullReferenceException(nameof(statusEffect));
                }

                statusEffect.m_ttl = _lesserStaminaTtl.Value;
                statusEffect.m_cooldown = _lesserStaminaCooldown.Value;
                statusEffect.m_healthOverTime = _lesserStaminaOvertime.Value;
                statusEffect.m_staminaDrainPerSec = _lesserStaminaDrainPS.Value;
                statusEffect.m_jumpStaminaUseModifier = _lesserStaminaJump.Value;
                statusEffect.m_runStaminaDrainModifier = _lesserStaminaRun.Value;
                statusEffect.m_staminaRegenMultiplier = _lesserStaminaRegen.Value;

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{Item = ItemDropNames.Mushroom,    Amount = 4  },
                        new RequirementConfig{Item = ItemDropNames.Honey,       Amount = 2  }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        #endregion

        #region Potion Extras

        private void PotionMeadbase()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.PotionMeadbase);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = CraftingStationNames.AlchemyTable,
                    Requirements = new[]
                    {
                        new RequirementConfig{  Item = ItemDropNames.YmirRemains,   Amount = 4    },
                        new RequirementConfig{  Item = ItemDropNames.Honey,         Amount = 2    }
                    }
                }));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void PhilosopherStoneBlue()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.PhilosopherStoneBlue);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();
                var statusEffect = itemDrop.m_itemData.m_shared.m_equipStatusEffect as SE_Stats;
                AddRaiseSkillModifier(ref statusEffect);

                ItemManager.Instance.AddItem(new CustomItem(prefab, false));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void PhilosopherStoneGreen()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.PhilosopherStoneGreen);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();
                var statusEffect = itemDrop.m_itemData.m_shared.m_equipStatusEffect as SE_Stats;
                AddRaiseSkillModifier(ref statusEffect);

                ItemManager.Instance.AddItem(new CustomItem(prefab, false));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void PhilosopherStoneRed()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.PhilosopherStoneRed);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();
                var statusEffect = itemDrop.m_itemData.m_shared.m_equipStatusEffect as SE_Stats;
                AddRaiseSkillModifier(ref statusEffect);

                ItemManager.Instance.AddItem(new CustomItem(prefab, false));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void PhilosopherStoneBlack()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.PhilosopherStoneBlack);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();
                var statusEffect = itemDrop.m_itemData.m_shared.m_equipStatusEffect as SE_Stats;
                AddRaiseSkillModifier(ref statusEffect);

                ItemManager.Instance.AddItem(new CustomItem(prefab, false));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        private void PhilosopherStonePurple()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>(ItemDropNames.PhilosopherStonePurple);
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var itemDrop = prefab.GetComponent<ItemDrop>();
                var statusEffect = itemDrop.m_itemData.m_shared.m_equipStatusEffect as SE_Stats;
                AddRaiseSkillModifier(ref statusEffect);

                ItemManager.Instance.AddItem(new CustomItem(prefab, false));
            }
            catch (Exception e)
            {
                Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
                Jotunn.Logger.LogError(e);
            }
        }

        #endregion

        #region Crafting Stations

        private void OdinPotionsAlchemyCraftingStation()
        {
            try
            {
                var prefab = _assetBundle.LoadAsset<GameObject>(CraftingStationNames.AlchemyTable);

                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var customPiece = new CustomPiece(prefab, false,
                  new PieceConfig
                  {
                      Enabled = true,
                      PieceTable = ItemDropNames.Hammer,
                      CraftingStation = CraftingStationNames.Workbench,
                      Requirements = new[]
                      {
                        new RequirementConfig{Amount = 8, Item = ItemDropNames.Stone, AmountPerLevel = 8}
                      }
                  });

                PieceManager.Instance.AddPiece(customPiece);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Issue Loading {CraftingStationNames.AlchemyTable}");
                Jotunn.Logger.LogError(ex);
            }
        }

        private void OdinPotionsCauldron()
        {
            try
            {
                var prefab = _assetBundle.LoadAsset<GameObject>(CraftingStationNames.AlchemyCauldron);

                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var customPiece = new CustomPiece(prefab,
                  false,
                  new PieceConfig
                  {
                      Enabled = true,
                      PieceTable = ItemDropNames.Hammer,
                      CraftingStation = CraftingStationNames.Workbench,
                      Requirements = new[]
                      {
                        new RequirementConfig{Amount = 4, Item = ItemDropNames.Iron, AmountPerLevel = 4}
                      }
                  });

                PieceManager.Instance.AddPiece(customPiece);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Issue Loading {CraftingStationNames.AlchemyCauldron}");
                Jotunn.Logger.LogError(ex);
            }
        }

        #endregion
    }
}
