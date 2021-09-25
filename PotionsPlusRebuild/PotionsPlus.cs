using System;
using System.Reflection;
using BepInEx;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace PotionsPlus
{
  [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
  [BepInDependency(Main.ModGuid)]
  [UsedImplicitly]
  public partial class PotionsPlus : BaseUnityPlugin
  {
    private const string PluginGuid = "com.odinplus.potionsplus";
    public const string PluginName = "PotionsPlus";
    public const string PluginVersion = "2.0.1";

    private AssetBundle _assetBundle;
    private const string PotionsPlusCraftingStation = "opalchemy";

    private void Awake()
    {
      try
      {
        Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
        _assetBundle = AssetUtils.LoadAssetBundleFromResources("potions", typeof(PotionsPlus).Assembly);
        ConfigEntries();
        OdinPotionsAlchemyCraftingStation();

        FlaskFortification();
        FlaskOfTheGods();
        FlaskOfMagelight();
        FlaskOfSecondWind();

        GrandHealingTideElixir();
        GrandSpiritualTideElixir();
        GrandStaminaElixir();
        GrandStealthElixir();

        MediumHealingTidePotion();
        MediumSpiritualTidePotion();
        MediumStaminaPotion();

        LesserHealingTideVial();
        LesserSpiritualTideVial();
        LesserStaminaVial();


        // FlaskOfFortificationSe();
        // MagelightSe();
        // SecondWindSe(); //*
        // GodsSe();
        // GrandTideSe();
        // GrandSpiritualSe();
        // GrandStaminaSe();
        // GrandStealthSe();
        // MediumTideSe();
        // MedSpiritualSe();
        // MedStaminaSe();
        // LesserTideSe();
        // LesserSpiritualSe();
        // LesserStaminaSe();

        _assetBundle.Unload(false);
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError($"[{MethodBase.GetCurrentMethod().DeclaringType?.Name}] {e.Message}");
        Jotunn.Logger.LogError(e);
      }
    }

    #region Flasks

    private void FlaskFortification()
    {
      try
      {
        Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");

        var prefab = _assetBundle.LoadAsset<GameObject>("Flask_of_Fortification");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.FlaskOfFortification
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Obsidian"
              , Amount = 2
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Flint"
              , Amount = 4
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Stone"
              , Amount = 8
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "MeadTasty"
              , Amount = 1
              , AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Flask_of_the_Gods");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.FlaskOfTheGods
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Carrot", Amount = 2, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Thistle", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Flax", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "MeadTasty", Amount = 1, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Flask_of_Magelight");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.FlaskOfMagelight
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "GreydwarfEye", Amount = 8, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "FreezeGland", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "BoneFragments", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "MeadTasty", Amount = 1, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Flask_of_Second_Wind");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.FlaskOfSecondWind
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "MeadTasty", Amount = 1, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Feathers", Amount = 6, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Ooze", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "FreezeGland", Amount = 2, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Grand_Healing_Tide_Potion");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.GrandHealingTideElixir
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Cloudberry", Amount = 6, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Needle", Amount = 2, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Barley", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Ooze", Amount = 2, AmountPerLevel = 10
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

    private void GrandSpiritualTideElixir()
    {
      try
      {
        Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
        var prefab = _assetBundle.LoadAsset<GameObject>("Grand_Spiritual_Healing_Potion");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.GrandSpiritualTideElixir
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Cloudberry", Amount = 6, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Flax", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "WolfFang", Amount = 2, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Ooze", Amount = 4, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Grand_Stamina_Elixir");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.GrandStaminaElixir
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Cloudberry", Amount = 8, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Carrot", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Turnip", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "LoxMeat", Amount = 2, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Grand_Stealth_Elixir");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.GrandStealthElixir
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "FreezeGland", Amount = 2, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Flax", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Feathers", Amount = 2, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Carrot", Amount = 2, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Medium_Healing_Tide_Flask");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.MediumHealingTidePotion
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Resin"
              , Amount = 6
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Bloodbag"
              , Amount = 2
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Blueberries"
              , Amount = 4
              , AmountPerLevel = 10
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

    private void MediumSpiritualTidePotion()
    {
      try
      {
        Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
        var prefab = _assetBundle.LoadAsset<GameObject>("Medium_Spiritual_Healing_Flask");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.MediumSpiritualTidePotion
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Bloodbag", Amount = 2, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "BoneFragments", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Ooze", Amount = 2, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Medium_Stamina_Flask");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.MediumStaminaPotion
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Resin", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Bloodbag", Amount = 2, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Blueberries", Amount = 4, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Lesser_Healing_Tide_Vial");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.LesserHealingTideVial
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Raspberry", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Honey", Amount = 2, AmountPerLevel = 10
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

    private void LesserSpiritualTideVial()
    {
      try
      {
        Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
        var prefab = _assetBundle.LoadAsset<GameObject>("Lesser_Spiritual_Healing_Vial");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.LesserSpiritualTideVial
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Raspberry", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Dandelion", Amount = 2, AmountPerLevel = 10
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
        var prefab = _assetBundle.LoadAsset<GameObject>("Lesser_Stamina_Vial");
        if (prefab == null)
        {
          throw new NullReferenceException(nameof(prefab));
        }

        ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
        {
          Name = PotionNames.LesserStaminaVial
          , Amount = 1
          , CraftingStation = PotionsPlusCraftingStation
          , Requirements = new[]
          {
            new RequirementConfig
            {
              Item = "Mushroom", Amount = 4, AmountPerLevel = 10
            }, new RequirementConfig
            {
              Item = "Honey", Amount = 2, AmountPerLevel = 10
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

    private void OdinPotionsAlchemyCraftingStation()
    {
      try
      {
        var opalchemyPiecePrefab = _assetBundle.LoadAsset<GameObject>("opalchemy");

        var opalchemyPiece = new CustomPiece(opalchemyPiecePrefab,
          false,
          new PieceConfig
          {
            Enabled = true
            , PieceTable = "Hammer"
            // , Name = "Alchemy Table"
            , CraftingStation = "piece_workbench"
            // , Description = "A special brewing stand to make your potions...."
            , Requirements = new[]
            {
              new RequirementConfig
              {
                Amount = 1
                , Item = "Wood"
                , AmountPerLevel = 1
              }
            }
          });

        PieceManager.Instance.AddPiece(opalchemyPiece);
      }
      catch (Exception ex)
      {
        Jotunn.Logger.LogError($"Issue Loading OP Alchemy Table");
        Jotunn.Logger.LogError(ex);
      }
    }

    # region SE Configuration items

    private EffectList _buildsounds;

    private CustomStatusEffect _sePotion1;
    private SE_Stats _potion1 = ScriptableObject.CreateInstance<SE_Stats>();


    private CustomStatusEffect _seGrandSpiritual;
    private SE_Stats _grandSpiritualSe = ScriptableObject.CreateInstance<SE_Stats>();
    private CustomItem _grandspiritual;


    private CustomItem _grandstam;
    private SE_Stats _grandstamSE = ScriptableObject.CreateInstance<SE_Stats>();
    private CustomStatusEffect _seGrandStam;

    private CustomItem _grandstealth;
    private SE_Stats _grandstealthSE = ScriptableObject.CreateInstance<SE_Stats>();
    private CustomStatusEffect _seGrandStealth;

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
