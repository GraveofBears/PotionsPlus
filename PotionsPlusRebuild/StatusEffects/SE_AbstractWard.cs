using System.Collections.Generic;
using System.Text;

namespace PotionsPlus.StatusEffects
{
  // ReSharper disable once InconsistentNaming
  public abstract class SE_AbstractWard : SE_Stats
  {
    protected SE_AbstractWard()
    {
      m_addMaxCarryWeight = 75f;
    }
    
    public override string GetTooltipString()
    {
      var text = new StringBuilder();
      
      if (m_tooltip.Length > 0)
      {
        text.AppendLine(m_tooltip);
      }
      if (m_addMaxCarryWeight != 0f)
      {
        text.Append("$se_max_carryweight ").AppendLine(m_addMaxCarryWeight.ToString("+0;-0"));
      }
      if (m_mods.Count > 0)
      {
        text.AppendLine(GetDamageModifiersTooltipString(m_mods));
      }
      if (m_speedModifier != 0f)
      {
        text.Append("$item_movement_modifier ").Append((m_speedModifier * 100f).ToString("+0;-0")).AppendLine("%");
      }
      return text.ToString();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public new static string GetDamageModifiersTooltipString(List<HitData.DamageModPair> mods)
    {
      if (mods.Count == 0)
      {
        return "";
      }
      string text = "";
      foreach (HitData.DamageModPair mod in mods)
      {
        if (mod.m_modifier != HitData.DamageModifier.Ignore && mod.m_modifier != 0)
        {
          switch (mod.m_modifier)
          {
            case HitData.DamageModifier.Immune:
              text += "\n$inventory_dmgmod: <color=orange>$inventory_immune</color> VS ";
              break;
            case HitData.DamageModifier.Resistant:
              text += "\n$inventory_dmgmod: <color=orange>$inventory_resistant</color> VS ";
              break;
            case HitData.DamageModifier.VeryResistant:
              text += "\n$inventory_dmgmod: <color=orange>$inventory_veryresistant</color> VS ";
              break;
            case HitData.DamageModifier.Weak:
              text += "\n$inventory_dmgmod: <color=orange>$inventory_weak</color> VS ";
              break;
            case HitData.DamageModifier.VeryWeak:
              text += "\n$inventory_dmgmod: <color=orange>$inventory_veryweak</color> VS ";
              break;
          }
          text += "<color=orange>";
          switch (mod.m_type)
          {
            case HitData.DamageType.Blunt:
              text += "$inventory_blunt";
              break;
            case HitData.DamageType.Slash:
              text += "$inventory_slash";
              break;
            case HitData.DamageType.Pierce:
              text += "$inventory_pierce";
              break;
            case HitData.DamageType.Chop:
              text += "$inventory_chop";
              break;
            case HitData.DamageType.Pickaxe:
              text += "$inventory_pickaxe";
              break;
            case HitData.DamageType.Fire:
              text += "$inventory_fire";
              break;
            case HitData.DamageType.Frost:
              text += "$inventory_frost";
              break;
            case HitData.DamageType.Lightning:
              text += "$inventory_lightning";
              break;
            case HitData.DamageType.Poison:
              text += "$inventory_poison";
              break;
            case HitData.DamageType.Spirit:
              text += "$inventory_spirit";
              break;
          }
          text += "</color>";
        }
      }
      return text + "\n";
    }
  }

  // ReSharper disable once InconsistentNaming
  public class SE_FireWard : SE_AbstractWard
  {
    public void Awake()
    {
      m_mods.Add(new HitData.DamageModPair
      {
        m_modifier = HitData.DamageModifier.Resistant,
        m_type = HitData.DamageType.Fire
      });
    }
  }
}
