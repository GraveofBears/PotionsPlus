using BepInEx;
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
    public const string PluginVersion = "2.2.0";

    private AssetBundle _assetBundle;
    public static PotionsPlus Instance;
    private Harmony _harmony;
    private Skills.SkillType _potionsPlusAlchemySkill;

    public PotionsPlus()
    {
      Instance = this;
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.FreezeGland
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.ElderBark
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Entrails
              , Amount = 8
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.PotionMeadbase
              , Amount = 1
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Obsidian
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Flint
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Stone
              , Amount = 8
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.PotionMeadbase
              , Amount = 1
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Carrot
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Thistle
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Flax
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.PotionMeadbase
              , Amount = 1
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.GreydwarfEye
              , Amount = 8
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.FreezeGland
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.BoneFragments
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.PotionMeadbase
              , Amount = 1
            }
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
        statusEffect.m_runStaminaDrainModifier = _secondWindrunDrain.Value;
        statusEffect.m_jumpStaminaUseModifier = _secondWindjumpDrain.Value;
        statusEffect.m_staminaRegenMultiplier = _secondWindRegen.Value;

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.FreezeGland
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Feathers
              , Amount = 6
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Ooze
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.PotionMeadbase
              , Amount = 1
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Cloudberry
              , Amount = 6
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Needle
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Barley
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Ooze
              , Amount = 2
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Cloudberry
              , Amount = 6
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Flax
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.WolfFang
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Ooze
              , Amount = 4
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Cloudberry
              , Amount = 8
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Carrot
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Turnip
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.LoxMeat
              , Amount = 2
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.FreezeGland
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Flax
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Feathers
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Carrot
              , Amount = 2
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Resin
              , Amount = 6
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Bloodbag
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Blueberries
              , Amount = 4
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Bloodbag
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.BoneFragments
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Ooze
              , Amount = 2
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Resin
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Bloodbag
              , Amount = 2
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Blueberries
              , Amount = 4
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Raspberry
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Honey
              , Amount = 2
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Raspberry
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Dandelion
              , Amount = 2
            }
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

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.Mushroom
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Honey
              , Amount = 2
            }
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
          CraftingStation = CraftingStationNames.AlchemyTable
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = ItemDropNames.YmirRemains
              , Amount = 4
            }
            , new RequirementConfig
            {
              Item = ItemDropNames.Honey
              , Amount = 2
            }
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

        var customPiece = new CustomPiece(prefab,
          false,
          new PieceConfig
          {
            Enabled = true
            , PieceTable = ItemDropNames.Hammer
            , CraftingStation = CraftingStationNames.Workbench
            , Requirements = new[]
            {
              new RequirementConfig
              {
                Amount = 8
                , Item = ItemDropNames.Stone
                , AmountPerLevel = 8
              }
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
            Enabled = true
            , PieceTable = ItemDropNames.Hammer
            , CraftingStation = CraftingStationNames.Workbench
            , Requirements = new[]
            {
              new RequirementConfig
              {
                Amount = 4
                , Item = ItemDropNames.Iron
                , AmountPerLevel = 4
              }
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
