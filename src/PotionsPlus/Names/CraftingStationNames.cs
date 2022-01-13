using System.Collections.Generic;

namespace PotionsPlus.Names
{
  public static class CraftingStationNames
  {
    // Custom
    public const string AlchemyTable = "opalchemy";
    public const string AlchemyCauldron = "opcauldron";

    // Vanilla
    public static readonly IEnumerable<string> AllNames = Utils.AllNames(typeof(CraftingStationNames));
    public const string ArtisanTable = "piece_artisanstation";
    public const string Cauldron = "piece_cauldron";
    public const string Forge = "forge";
    public const string Stonecutter = "piece_stonecutter";
    public const string Workbench = "piece_workbench";
  }
}
