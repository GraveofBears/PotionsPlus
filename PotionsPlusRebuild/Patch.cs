using HarmonyLib;
using JetBrains.Annotations;
using System;

namespace PotionsPlus
{
  /// <summary>
  /// Harmony Patches
  /// </summary>
  public static class Patch
  {
    /// <summary>
    /// Patch the Players Inventory
    /// </summary>
    [HarmonyPatch(typeof(Inventory), nameof(Inventory.AddItem), typeof(string), typeof(int), typeof(int), typeof(int), typeof(long), typeof(string))]
    public static class PatchInventory
    {
      /// <summary>
      /// Patch the AddItem method
      /// </summary>
      /// <param name="name">Name of the item</param>
      /// <param name="stack">Stack size</param>
      /// <param name="quality">Quality level</param>
      /// <param name="variant">Variant to use</param>
      /// <param name="crafterID">Id of the player who is crafting</param>
      /// <param name="crafterName">Name of the player who is crafting</param>
      [HarmonyPostfix]
      [HarmonyPriority(Priority.Normal)]
      [UsedImplicitly]
      public static void Postfix(string name, int stack, int quality, int variant, long crafterID, string crafterName)
      {
        try
        {
          Jotunn.Logger.LogDebug($"PatchInventoryPostfix");
          if (Player.m_localPlayer == null)
          {
            Jotunn.Logger.LogDebug("Player is null");
            return;
          }
          PotionsPlus.Instance.OnInventoryAddItemPostFix(name, stack, quality, variant, crafterID, crafterName);
        }
        catch (Exception e)
        {
          Jotunn.Logger.LogError(e);
        }
      }
    }
  }
}
