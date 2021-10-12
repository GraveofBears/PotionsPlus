using Jotunn.Configs;
using Jotunn.Managers;
using PotionsPlus.Names;
using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PotionsPlus
{
  public partial class PotionsPlus
  {
    private Skills.SkillType _potionsPlusAlchemySkill;
    private SE_Stats _se_AlchSkillProc;
    private SE_Stats _se_CheatDeath;

    /// <summary>
    /// Adds the Alchemy skill to the game.
    /// </summary>
    private void AddToSkills()
    {
      try
      {
        if (!AlchemySkillEnable.Value) return;
        _potionsPlusAlchemySkill = SkillManager.Instance.AddSkill(new SkillConfig
        {
          Identifier = $"{PluginGuid}.skill.druidry"
          , Name = "$pp_potion_skill_name"
          , Description = "$pp_potion_skill_description"
          , Icon = _assetBundle.LoadAsset<Sprite>("AlcSkill")
          , IncreaseStep = 1f,
        });
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }

    /// <summary>
    /// Raises Alchemy skills
    /// </summary>
    private void RaiseAlchemySkill()
    {
      try
      {
        if (!AlchemySkillEnable.Value) return;
        PrintAlchemySkillInfo();
        Player.m_localPlayer.RaiseSkill(_potionsPlusAlchemySkill);
        Utils.LogDebug($"Alchemy Skill Raised");
        PrintAlchemySkillInfo();
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }

    /// <summary>
    /// Print to the log details about the current alchemy crafting skill level if a DEBUG Build
    /// </summary>
    [System.Diagnostics.Conditional("DEBUG")]
    private void PrintAlchemySkillInfo()
    {
      try
      {
        Utils.LogDebug($"[Skill Level Info] Current Level: {Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == _potionsPlusAlchemySkill).Value?.m_level ?? 0} ({(Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == _potionsPlusAlchemySkill).Value?.GetLevelPercentage() ?? 0) * 100}%), " +
                       $"Next Level: {Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == _potionsPlusAlchemySkill).Value?.m_accumulator ?? 0}/{Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == _potionsPlusAlchemySkill).Value?.GetNextLevelRequirement() ?? 0}");
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }

    /// <summary>
    /// Check if the current crafting station is one used for alchemy.
    /// </summary>
    /// <param name="currentCraftingStationName">Name of the crafting station</param>
    /// <returns>true if the current crafting station is one used for alchemy else false</returns>
    private bool IsValidAlchemyCraftingStation(string currentCraftingStationName)
    {
      try
      {
        Utils.LogDebug($"currentCraftingStationName : {currentCraftingStationName}");
        return currentCraftingStationName switch
        {
          CraftingStationNames.AlchemyTable + "(Clone)" => true
          , CraftingStationNames.AlchemyTable => true
          // , CraftingStationNames.AlchemyCauldron + "(Clone)" => true
          , _ => false
        };
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
        return false;
      }
    }

    /// <summary>
    /// Check if the item is being added via crafting.
    /// </summary>
    /// <param name="crafterId">Id of the player who is crafting</param>
    /// <param name="crafterName">Name of the player who is crafting</param>
    /// <returns></returns>
    private bool IsFromCrafting(long crafterId, string crafterName)
    {
      try
      {
        return !string.IsNullOrEmpty(crafterName) && crafterId >= 1;
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
        return false;
      }
    }

    /// <summary>
    /// Check if an item is a Consumable Type
    /// </summary>
    /// <param name="prefabName">Name of the item</param>
    /// <returns>true if the item is a Consumable else false.</returns>
    private bool IsConsumable(string prefabName)
    {
      try
      {
        var itemPrefab = ObjectDB.instance.GetItemPrefab(prefabName);
        if (itemPrefab == null) return false;
        var itemDrop = itemPrefab.GetComponent<ItemDrop>();
        if (itemDrop == null) return false;
        return itemDrop.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable;
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
        return false;
      }
    }

    /// <summary>
    /// Patch for AddItem method.
    /// </summary>
    /// <param name="itemName">Name of the item</param>
    /// <param name="stack">Stack size</param>
    /// <param name="quality">Quality level</param>
    /// <param name="variant">Variant to use</param>
    /// <param name="crafterId">Id of the player who is crafting</param>
    /// <param name="crafterName">Name of the player who is crafting</param>
    public void OnInventoryAddItemPostFix(string itemName, int stack, int quality, int variant, long crafterId, string crafterName)
    {
      try
      {
        if (_isAddingExtraItem) return; // Recursive loop detection. 
        Utils.LogDebug($"itemName: {itemName}, crafterID: {crafterId}, crafterName: {crafterName}");

        Utils.LogDebug($"AlchemySkillEnable.Value : {AlchemySkillEnable?.Value}");
        if (!AlchemySkillEnable?.Value ?? false) return;
        Utils.LogDebug($"IsFromCrafting(crafterID, crafterName) : {IsFromCrafting(crafterId, crafterName)}");
        if (!IsFromCrafting(crafterId, crafterName)) return; // Item is being bought from trader.
        Utils.LogDebug($"IsConsumable(itemName) : {IsConsumable(itemName)}");
        if (!IsConsumable(itemName)) return;
        Utils.LogDebug($"IsValidAlchemyCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name) : {IsValidAlchemyCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name)}");
        if (!IsValidAlchemyCraftingStation(Player.m_localPlayer.GetCurrentCraftingStation()?.name)) return;

        Utils.LogDebug($"AlchemySkillBonusWhenCraftingEnabled.Value : {AlchemySkillBonusWhenCraftingEnabled?.Value}");
        if (AlchemySkillBonusWhenCraftingEnabled?.Value ?? false)
        {
          var skillLevel = Player.m_localPlayer.GetSkills().m_skillData.FirstOrDefault(s => s.Key == _potionsPlusAlchemySkill).Value?.m_level ?? 0;
          Utils.LogDebug($"skillLevel : {skillLevel}");
          // 1-100% chance to craft an extra item. 1% per level of skill.
          if (IsCrafterLucky(skillLevel))
          {
            Utils.LogDebug($"[1][Start] -------------- ");
            AddExtraItem(itemName);
            Utils.LogDebug($"[1][End] ---------------- ");
          }

          // Max 25% chance to craft a 2nd extra after getting to skill level 25.
          if (skillLevel > 25f && IsCrafterLucky(skillLevel / 4))
          {
            Utils.LogDebug($"[2][Start] -------------- ");
            AddExtraItem(itemName);
            Utils.LogDebug($"[2][End] ---------------- ");
          }
        }

        if (!AlchemySkillEnable.Value) return;
        RaiseAlchemySkill();
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }

    /// <summary>
    /// Adds an extra item to the player inventory.
    /// Checks that the player has room in their
    /// inventory before trying to add the item.
    /// </summary>
    /// <param name="itemName"></param>
    private void AddExtraItem(string itemName)
    {
      try
      {
        var itemPrefab = ObjectDB.instance.GetItemPrefab(itemName);
        if (!Player.m_localPlayer.GetInventory().CanAddItem(itemPrefab, 1)) return;
        Utils.LogDebug($"Trying to add extra item: {itemName}");
        AddItem(itemName);
        Utils.LogDebug($"Added extra item: {itemName}");
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
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
      try
      {
        _isAddingExtraItem = true; // Recursive loop flag.
        Player.m_localPlayer.GetSEMan().AddStatusEffect(_se_AlchSkillProc);
        Player.m_localPlayer.GetInventory().AddItem(itemName, 1, 1, 0, Player.m_localPlayer.GetPlayerID(), Player.m_localPlayer.GetPlayerName());
        _isAddingExtraItem = false; // Reset flag.
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }

    /// <summary>
    /// Calculate crafter's luck.
    /// </summary>
    /// <param name="skillLevel">Current skill level</param>
    /// <returns>true if crafter is lucky else false</returns>
    private bool IsCrafterLucky(float skillLevel)
    {
      try
      {
        if (skillLevel < 1) return false;
        var rand = Random.Range(1, 100);
        Utils.LogDebug($"Skill Level: {skillLevel} - Rand: {rand}");
        Utils.LogDebug($"rand < skillLevel : {rand < skillLevel}");
        return rand < skillLevel;
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
        return false;
      }
    }

    private void AddRaiseSkillModifier(ref SE_Stats statusEffect)
    {
      statusEffect.m_raiseSkill = _potionsPlusAlchemySkill;
      statusEffect.m_raiseSkillModifier = PhilosopherStoneXpGain.Value;
    }

    private void LoadStatusEffects()
    {
      try
      {
        _se_CheatDeath = _assetBundle.LoadAsset<SE_Stats>("CheatDeath");
        _se_AlchSkillProc = _assetBundle.LoadAsset<SE_Stats>("AlcSkillProc");
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }

    public void OnPlayerAboutToDie(ref Character character, ref float health)
    {
      try
      {
        if (character != Player.m_localPlayer) return;
        var category = "pp_philstone";
        if (Player.m_localPlayer.GetSEMan().GetStatusEffects().All(se => se.m_category != category)) return;
        
        var equippedPhilosopherStone = ((Humanoid)character).GetInventory().GetAllItems().FirstOrDefault(i => i.m_equiped 
                                                                                                             && i.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Utility
                                                                                                             && i.m_shared.m_equipStatusEffect.m_category == category
                                                                                                  );
        if (equippedPhilosopherStone != null)
        {
          ((Humanoid)character).GetInventory().RemoveOneItem(equippedPhilosopherStone);
          var se = _se_CheatDeath;
          se.m_ttl = 6f;
          Player.m_localPlayer.GetSEMan().AddStatusEffect(se);
          // character.Heal(character.GetMaxHealth());
          health = character.GetMaxHealth();
        }

        // ReSharper disable once InconsistentNaming
        var se_pp_philstone = Player.m_localPlayer.GetSEMan().GetStatusEffects().FirstOrDefault(se => se.m_category == category);
        if (se_pp_philstone != null)
        {
          Player.m_localPlayer.GetSEMan().RemoveStatusEffect(se_pp_philstone);
        }
      }
      catch (Exception e)
      {
        Jotunn.Logger.LogError(e);
      }
    }
  }
}
