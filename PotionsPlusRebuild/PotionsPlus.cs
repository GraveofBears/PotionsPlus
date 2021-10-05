using BepInEx;
using HarmonyLib;
using JetBrains.Annotations;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public const string PluginVersion = "2.1.4";

        private AssetBundle _assetBundle;
        private const string PotionsPlusCraftingStation = "opalchemy";
        public static PotionsPlus Instance;
        private Harmony _harmony;
        public Skills.SkillType PotionsPlusAlchemySkill;

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
                PhilosopherStoneGreen();
                PhilosopherStoneRed();
                PhilosopherStoneBlue();
                PhilosopherStonePurple();
                PhilosopherStoneBlack();

                AddToSkills();

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
        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }

        #region Alchemy Skill

        /// <summary>
        /// Adds the Alchemy skill to the game.
        /// </summary>
        private void AddToSkills()
        {

            if (AlchemySkillEnable.Value)
            {
                PotionsPlusAlchemySkill = SkillManager.Instance.AddSkill(new SkillConfig
                {
                    Identifier = $"{PluginGuid}.skill.druidry"
                  ,
                    Name = "$pp_potion_skill_name"
                  ,
                    Description = "$pp_potion_skill_description"
                  ,
                    Icon = _assetBundle.LoadAsset<Sprite>("AlcSkill")
                  ,
                    IncreaseStep = 1f,
                });
            }
        }

        /// <summary>
        /// Raises Alchemy skills
        /// </summary>
        private void RaiseAlchemySkill()
        {
            PrintAlchemySkillInfo();
            Player.m_localPlayer.RaiseSkill(PotionsPlusAlchemySkill);
            LogDebug($"Alchemy Skill Raised");
            PrintAlchemySkillInfo();
        }

        /// <summary>
        /// Print to the log details about the current alchemy crafting skill level if a DEBUG Build
        /// </summary>
        [System.Diagnostics.Conditional("DEBUG")]
        private void PrintAlchemySkillInfo()
        {
            LogDebug($"[Skill Level Info] Current Level: {Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == PotionsPlusAlchemySkill).Value?.m_level ?? 0} ({(Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == PotionsPlusAlchemySkill).Value?.GetLevelPercentage() ?? 0) * 100}%), " +
                     $"Next Level: {Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == PotionsPlusAlchemySkill).Value?.m_accumulator ?? 0}/{Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == PotionsPlusAlchemySkill).Value?.GetNextLevelRequirement() ?? 0}");
        }

        /// <summary>
        /// Check if the current crafting station is one used for alchemy.
        /// </summary>
        /// <param name="currentCraftingStationName">Name of the crafting station</param>
        /// <returns>true if the current crafting station is one used for alchemy else false</returns>
        private bool IsValidAlchemyCraftingStation(string currentCraftingStationName)
        {
            LogDebug($"currentCraftingStationName : {currentCraftingStationName}");
            switch (currentCraftingStationName)
            {
                case "opalchemy(Clone)":
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check if the item is being added via crafting.
        /// </summary>
        /// <param name="crafterID">Id of the player who is crafting</param>
        /// <param name="crafterName">Name of the player who is crafting</param>
        /// <returns></returns>
        private bool IsFromCrafting(long crafterID, string crafterName)
        {
            return !string.IsNullOrEmpty(crafterName) && crafterID >= 1;
        }

        /// <summary>
        /// Check if an item is a Consumable Type
        /// </summary>
        /// <param name="prefabName">Name of the item</param>
        /// <returns>true if the item is a Consumable else false.</returns>
        private bool IsConsumable(string prefabName)
        {
            var itemPrefab = ObjectDB.instance.GetItemPrefab(prefabName);
            if (itemPrefab == null) return false;
            var itemDrop = itemPrefab.GetComponent<ItemDrop>();
            if (itemDrop == null) return false;
            return itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable;
        }

        /// <summary>
        /// Patch for AddItem method.
        /// </summary>
        /// <param name="itemName">Name of the item</param>
        /// <param name="stack">Stack size</param>
        /// <param name="quality">Quality level</param>
        /// <param name="variant">Variant to use</param>
        /// <param name="crafterID">Id of the player who is crafting</param>
        /// <param name="crafterName">Name of the player who is crafting</param>
        public void OnInventoryAddItemPostFix(string itemName, int stack, int quality, int variant, long crafterID, string crafterName)
        {
            if (_isAddingExtraItem) return; // Recursive loop detection. 
            LogDebug($"itemName: {itemName}, crafterID: {crafterID}, crafterName: {crafterName}");

            LogDebug($"AlchemySkillEnable.Value : {AlchemySkillEnable?.Value}");
            if (!AlchemySkillEnable?.Value ?? false) return;
            LogDebug($"IsFromCrafting(crafterID, crafterName) : {IsFromCrafting(crafterID, crafterName)}");
            if (!IsFromCrafting(crafterID, crafterName)) return; // Item is being bought from trader.
            LogDebug($"IsConsumable(itemName) : {IsConsumable(itemName)}");
            if (!IsConsumable(itemName)) return;
            LogDebug($"IsValidAlchemyCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name) : {IsValidAlchemyCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name)}");
            if (!IsValidAlchemyCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name)) return;

            LogDebug($"AlchemySkillBonusWhenCraftingEnabled.Value : {AlchemySkillBonusWhenCraftingEnabled?.Value}");
            if (AlchemySkillBonusWhenCraftingEnabled?.Value ?? false)
            {
                var skillLevel = Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == PotionsPlusAlchemySkill).Value?.m_level ?? 0;
                LogDebug($"skillLevel : {skillLevel}");
                // 1-100% chance to craft an extra item. 1% per level of skill.
                if (IsCrafterLucky(skillLevel))
                {
                    LogDebug($"[1][Start] -------------- ");
                    AddExtraItem(itemName);
                    LogDebug($"[1][End] ---------------- ");
                }

                // Max 25% chance to craft a 2nd extra after getting to skill level 25.
                if (skillLevel > 25f && IsCrafterLucky(skillLevel / 4))
                {
                    LogDebug($"[2][Start] -------------- ");
                    AddExtraItem(itemName);
                    LogDebug($"[2][End] ---------------- ");
                }
            }

            if (!AlchemySkillEnable.Value) return;
            RaiseAlchemySkill();
        }

        /// <summary>
        /// Adds an extra item to the player inventory.
        /// Checks that the player has room in their
        /// inventory before trying to add the item.
        /// </summary>
        /// <param name="itemName"></param>
        private void AddExtraItem(string itemName)
        {
            var itemPrefab = ObjectDB.instance.GetItemPrefab(itemName);
            if (!Player.m_localPlayer.GetInventory().CanAddItem(itemPrefab, 1)) return;
            LogDebug($"Trying to add extra item: {itemName}");
            AddItem(itemName);
            LogDebug($"Added extra item: {itemName}");
        }

        /// <summary>
        /// AddItem Recursive loop flag
        /// </summary>
        private static bool _isAddingExtraItem;

        /// <summary>
        /// Adds an item to the players inventory.
        /// All checks for the player having space for a new
        /// item must be done before calling this method.
        /// 
        /// This is a recursive loop because the AddItem
        /// method is being patched. To break it, we are
        /// setting a flag to track this.
        /// </summary>
        /// <param name="itemName">Name of item to add.</param>
        private void AddItem(string itemName)
        {
            _isAddingExtraItem = true; // Recursive loop flag.
            Player.m_localPlayer.GetInventory().AddItem(itemName, 1, 1, 0, Player.m_localPlayer.GetPlayerID(), Player.m_localPlayer.GetPlayerName());
            _isAddingExtraItem = false; // Reset flag.
        }

        /// <summary>
        /// Calculate crafter's luck.
        /// </summary>
        /// <param name="skillLevel">Current skill level</param>
        /// <returns>true if crafter is lucky else false</returns>
        private bool IsCrafterLucky(float skillLevel)
        {
            if (skillLevel < 1) return false;
            var rand = Random.Range(1, 100);
            LogDebug($"Skill Level: {skillLevel} - Rand: {rand}");
            LogDebug($"rand < skillLevel : {rand < skillLevel}");
            return rand < skillLevel;
        }

        /// <summary>
        /// Writes Debug messages if a DEBUG Build
        /// </summary>
        /// <param name="msg">Message to print to the log.</param>
        [System.Diagnostics.Conditional("DEBUG")]
        public static void LogDebug(string msg)
        {
            Jotunn.Logger.LogDebug(msg);
        }

        #endregion

        #region Flasks

        private void FlaskElements()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");

                var prefab = _assetBundle.LoadAsset<GameObject>("Flask_of_Elements");
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "FreezeGland"
              , Amount = 2
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "ElderBark"
              , Amount = 4
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Entrails"
              , Amount = 8
              , AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "Potion_Meadbase"
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
              Item = "Potion_Meadbase"
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
              Item = "Potion_Meadbase", Amount = 1, AmountPerLevel = 10
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
              Item = "Potion_Meadbase", Amount = 1, AmountPerLevel = 10
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "FreezeGland", Amount = 2, AmountPerLevel = 10
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
              Item = "Potion_Meadbase", Amount = 1, AmountPerLevel = 10
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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

        private void GrandSpiritualHealingElixir()
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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

        private void MediumSpiritualHealingPotion()
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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

        private void LesserSpiritualHealingVial()
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
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
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "Mushroom", Amount = 4, AmountPerLevel = 10
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

        #endregion

        #region Potion Extras

        private void PotionMeadbase()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>("Potion_Meadbase");
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "YmirRemains", Amount = 4, AmountPerLevel = 10
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
        private void PhilosopherStoneBlue()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>("PhilosopherStoneBlue");
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "YmirRemains", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "PotionMeadbase", Amount = 2, AmountPerLevel = 10
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
                private void PhilosopherStoneGreen()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>("PhilosopherStoneGreen");
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "YmirRemains", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "PotionMeadbase", Amount = 2, AmountPerLevel = 10
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
                private void PhilosopherStoneRed()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>("PhilosopherStoneRed");
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "YmirRemains", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "PotionMeadbase", Amount = 2, AmountPerLevel = 10
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
                private void PhilosopherStoneBlack()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>("PhilosopherStoneBlack");
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "YmirRemains", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "PotionMeadbase", Amount = 2, AmountPerLevel = 10
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
                private void PhilosopherStonePurple()
        {
            try
            {
                Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
                var prefab = _assetBundle.LoadAsset<GameObject>("PhilosopherStonePruple");
                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                ItemManager.Instance.AddItem(new CustomItem(prefab, false, new ItemConfig
                {
                    CraftingStation = PotionsPlusCraftingStation
                  ,
                    Requirements = new[]
                  {
            new RequirementConfig
            {
              Item = "YmirRemains", Amount = 4, AmountPerLevel = 10
            }
            , new RequirementConfig
            {
              Item = "PotionMeadbase", Amount = 2, AmountPerLevel = 10
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

        #region Crafting Stations

        private void OdinPotionsAlchemyCraftingStation()
        {
            try
            {
                var prefab = _assetBundle.LoadAsset<GameObject>("opalchemy");

                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var customPiece = new CustomPiece(prefab,
                  false,
                  new PieceConfig
                  {
                      Enabled = true
                    ,
                      PieceTable = "Hammer"
                    ,
                      CraftingStation = "piece_workbench"
                    ,
                      Requirements = new[]
                    {
              new RequirementConfig
              {
                Amount = 8
                , Item = "Stone"
                , AmountPerLevel = 8
              }
                    }
                  });

                PieceManager.Instance.AddPiece(customPiece);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Issue Loading OP Alchemy Table");
                Jotunn.Logger.LogError(ex);
            }
        }

        private void OdinPotionsCauldron()
        {
            try
            {
                var prefab = _assetBundle.LoadAsset<GameObject>("opcauldron");

                if (prefab == null)
                {
                    throw new NullReferenceException(nameof(prefab));
                }

                var customPiece = new CustomPiece(prefab,
                  false,
                  new PieceConfig
                  {
                      Enabled = true
                    ,
                      PieceTable = "Hammer"
                    ,
                      CraftingStation = "piece_workbench"
                    ,
                      Requirements = new[]
                    {
              new RequirementConfig
              {
                Amount = 4
                , Item = "Iron"
                , AmountPerLevel = 4
              }
                    }
                  });

                PieceManager.Instance.AddPiece(customPiece);
            }
            catch (Exception ex)
            {
                Jotunn.Logger.LogError($"Issue Loading OP Alchemy Table");
                Jotunn.Logger.LogError(ex);
            }
        }

        #endregion
    }
}
