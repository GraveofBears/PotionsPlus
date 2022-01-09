using BepInEx.Configuration;
using System.Reflection;

namespace PotionsPlus
{
  public partial class PotionsPlus
  {
    private ConfigEntry<float> PhilosopherStoneXpGain;
    private static ConfigEntry<float> _tick;
    private static ConfigEntry<int> _healthtime;
    private static ConfigEntry<int> _healthtick;
    private static ConfigEntry<int> _healthTimer;
    private static ConfigEntry<int> _cooldown;

    public ConfigEntry<int> _elementsTtl;
    public ConfigEntry<int> _fortificationTtl;
    public ConfigEntry<int> _magelightTtl;

    private ConfigEntry<int> _secondWindTtl;
    private ConfigEntry<float> _secondWindrunDrain;
    private ConfigEntry<float> _secondWindjumpDrain;
    private ConfigEntry<float> _secondWindRegen;
    private ConfigEntry<int> _secondWindCooldown;

    private ConfigEntry<int> _godsTTl;
    private ConfigEntry<float> _godsregen;
    private ConfigEntry<int> _godsHealovertime;
    private ConfigEntry<int> _hotDuration;
    private ConfigEntry<int> _hotInterval;
    private ConfigEntry<int> _hotTicks;
    private ConfigEntry<int> _hoTtimer;
    private ConfigEntry<int> _hotTimeTickHp;

    private ConfigEntry<int> _grandTideTtl;
    private ConfigEntry<int> _grandTideCooldownTimer;
    private ConfigEntry<bool> _grandTideCooldown;
    private ConfigEntry<float> _grandTideregen;
    private ConfigEntry<int> _grandHealovertime;
    private ConfigEntry<int> _grandHotDuration;
    private ConfigEntry<int> _grandHotInterval;
    private ConfigEntry<int> _grandHotTicks;
    private ConfigEntry<int> _grandHoTtimer;
    private ConfigEntry<int> _grandHotTimeTickHp;

    private ConfigEntry<int> _grandSpHTtl;
    private ConfigEntry<int> _grandSpCooldown;
    private ConfigEntry<int> _grandSpHot;
    private ConfigEntry<int> _grandSphotDur;
    private ConfigEntry<int> _grandspHotInt;
    private ConfigEntry<int> _grandSphoTticks;
    private ConfigEntry<int> _grandSphoTtimer;
    private ConfigEntry<int> _grandSphoTtimeTickHp;

    private ConfigEntry<int> _grandstamTtl;
    private ConfigEntry<int> _grandstamcooldown;
    private ConfigEntry<int> _grandstaminaOt;
    private ConfigEntry<int> _grandstaminaDps;
    private ConfigEntry<int> _grandstamjump;
    private ConfigEntry<int> _grandstamrun;
    private ConfigEntry<int> _grandstamregen;

    private ConfigEntry<int> _grandStealthTtl;
    private ConfigEntry<int> _grandstealthcooldown;
    private ConfigEntry<int> _grandstealthstealthmod;

    private ConfigEntry<int> _medTideTtl;
    private ConfigEntry<int> _medTideCooldownTimer;
    private ConfigEntry<bool> _medTideCooldown;
    private ConfigEntry<float> _medTideregen;
    private ConfigEntry<int> _medHealovertime;
    private ConfigEntry<int> _medHotDuration;
    private ConfigEntry<int> _medHotInterval;
    private ConfigEntry<int> _medHotTicks;
    private ConfigEntry<int> _medHoTtimer;
    private ConfigEntry<int> _medHotTimeTickHp;

    private ConfigEntry<int> _medstamTtl;
    private ConfigEntry<int> _medstamcooldown;
    private ConfigEntry<int> _medstaminaOt;
    private ConfigEntry<int> _medstaminaDps;
    private ConfigEntry<int> _medstamjump;
    private ConfigEntry<int> _medstamrun;
    private ConfigEntry<int> _medstamregen;

    private ConfigEntry<int> _medSpHtl;
    private ConfigEntry<int> _medSpCooldown;
    private ConfigEntry<int> _medSpHot;
    private ConfigEntry<int> _medSphotDur;
    private ConfigEntry<int> _medspHotInt;
    private ConfigEntry<int> _medSphoTticks;
    private ConfigEntry<int> _medSphoTtimer;
    private ConfigEntry<int> _medSphoTtimeTickHp;

    private ConfigEntry<int> _lesserTideTtl;
    private ConfigEntry<int> _lesserTideCooldownTimer;
    private ConfigEntry<bool> _lesserTideCooldown;
    private ConfigEntry<float> _lesserTideregen;
    private ConfigEntry<int> _lesserHealovertime;
    private ConfigEntry<int> _lesserHotDuration;
    private ConfigEntry<int> _lesserHotInterval;
    private ConfigEntry<int> _lesserHotTicks;
    private ConfigEntry<int> _lesserHoTtimer;
    private ConfigEntry<int> _lesserHotTimeTickHp;

    private ConfigEntry<int> _lesserSpHtl;
    private ConfigEntry<int> _lesserSpCooldown;
    private ConfigEntry<int> _lesserSpHot;
    private ConfigEntry<int> _lesserSphotDur;
    private ConfigEntry<int> _lesserspHotInt;
    private ConfigEntry<int> _lesserSphoTticks;
    private ConfigEntry<int> _lesserSphoTtimer;
    private ConfigEntry<int> _lesserSphoTtimeTickHp;

    private ConfigEntry<int> _lesserstamTtl;
    private ConfigEntry<int> _lesserstamcooldown;
    private ConfigEntry<int> _lesserstaminaOt;
    private ConfigEntry<int> _lesserstaminaDps;
    private ConfigEntry<int> _lesserstamjump;
    private ConfigEntry<int> _lesserstamrun;
    private ConfigEntry<int> _lesserstamregen;

    public ConfigEntry<bool> AlchemySkillEnable;
    public ConfigEntry<bool> AlchemySkillBonusWhenCraftingEnabled;

    private void ConfigEntries()
    {
      Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
      _elementsTtl = Config.Bind(PotionNames.FlaskOfElements, ConfigKeyNames.Duration, 300, $"Duration for the {PotionNames.FlaskOfElements}");
      _fortificationTtl = Config.Bind(PotionNames.FlaskOfFortification, ConfigKeyNames.Duration, 300, $"Duration for the {PotionNames.FlaskOfFortification}");
      _magelightTtl = Config.Bind(PotionNames.FlaskOfMagelight, ConfigKeyNames.Duration, 300, $"Duration for {PotionNames.FlaskOfMagelight}");

      _godsTTl = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.Duration, 300, $"Duration for {PotionNames.FlaskOfTheGods}");
      _grandTideTtl = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.GrandHealingTideElixir}");
      _grandSpHTtl = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.Duration, 240, $"Duration for {PotionNames.GrandSpiritualHealingElixir}");
      _grandstamTtl = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.Duration, 240, $"Duration for {PotionNames.GrandStaminaElixir}");
      _grandStealthTtl = Config.Bind(PotionNames.GrandStealthElixir, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.GrandStealthElixir}");
      _medTideTtl = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.MediumHealingTidePotion}");
      _medSpHtl = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.MediumSpiritualHealingPotion}");
      _medstamTtl = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.MediumStaminaPotion}");
      _lesserTideTtl = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.LesserHealingTideVial}");
      _lesserSpHtl = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.LesserSpiritualHealingVial}");
      _lesserstamTtl = Config.Bind(PotionNames.LesserStaminaVial, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.LesserStaminaVial}");

      AlchemySkillEnable = Config.Bind("Alchemy Skill", "Enable Alchemy Skill", true, new ConfigDescription("Enable Alchemy skill.", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Order = 1 }));
      AlchemySkillBonusWhenCraftingEnabled = Config.Bind("Alchemy Skill", "Enable Alchemy Bonus", true, new ConfigDescription("Enable Alchemy Bonus when crafting.", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Order = 2 }));
      PhilosopherStoneXpGain = Config.Bind("Alchemy Skill", "Philosopher Stone XP Gain", 5f, new ConfigDescription("XP Gain multiplier when brewing while using a Philosopher Stone.", new AcceptableValueRange<float>(0f, 100f), new ConfigurationManagerAttributes { IsAdminOnly = true, Order = 3 }));


      #region Second Wind Config

      _secondWindTtl = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.Duration, 120, $"Duration");
      _secondWindCooldown = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.Cooldown, 0, "Cooldown Timer");
      _secondWindjumpDrain = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.JumpDrain, -0.25f, "Jump drain multiplicative factor for stamina drain");
      _secondWindrunDrain = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.RunDrain, -0.25f, "Run drain multiplicative factor for stamina drain");
      _secondWindRegen = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.StaminaRegenFactor, 1.05f, "Overall multiplicative factor for stamina regen during potion");

      #endregion

      #region Flask of the Gods

      _godsregen = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthRegenFactor, 1.05f, "The multiplier used for health regeneration during consumption");
      _godsHealovertime = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealOverTime, 250, "The volume of health to heal");
      _hotDuration = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthOverTimeDuration, 1, "The health over time duration");
      _hotInterval = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthOverTimeInterval, 1, "The Interval for health over time");
      _hotTicks = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HeathOverTimeTicks, 1, "Ticks of health over time");
      _hoTtimer = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthOverTimeTimer, 1, "The timer for health over time. Its confusing dont mess with it unless you know maths");
      _hotTimeTickHp = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HeathOverTimeTickPerHp, 1, "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

      #endregion

      #region GrandHealingTideElixir

      _grandTideCooldownTimer = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.Cooldown, 0, "Cooldown Timer for Second Wind");

      _grandTideregen = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthRegenFactor, 1f, "The multiplier used for health regeneration during consumption");
      _grandHealovertime = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealOverTime, 95, "The volume of health to heal");
      _grandHotDuration = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthOverTimeDuration, 10, "The health over time duration");
      _grandHotInterval = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthOverTimeInterval, 5, "The Interval for health over time");
      _grandHotTicks = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HeathOverTimeTicks, 0, "Ticks of health over time");
      _grandHoTtimer = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for health over time. Its confusing dont mess with it unless you know maths");
      _grandHotTimeTickHp = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

      #endregion

      #region Grand Spiritual Healing

      _grandSpCooldown = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.Cooldown, 0, "The cooldown timer for this potion after consumption");
      _grandSpHot = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealOverTime, 100, "The Health value you over time");
      _grandSphotDur = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealthOverTimeDuration, 0, "The duration of which health over time is applied");
      _grandspHotInt = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealthOverTimeInterval, 0, "Don't play with this one ");
      _grandSphoTticks = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HeathOverTimeTicks, 0, "Dont mess with this unless setting up advanced use case");
      _grandSphoTtimer = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for Health over time");
      _grandSphoTtimeTickHp = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "For advanced users only");

      #endregion

      #region GrandStamSE

      _grandstamcooldown = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
      _grandstaminaOt = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.StaminaOverTime, 10, ConfigKeyNames.StaminaOverTime);
      _grandstaminaDps = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.StaminaDrainPerSecond, 10, "Drain Per Second");
      _grandstamjump = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.JumpStaminaDrainFactor, 10, "Jump stamina drain");
      _grandstamrun = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.RunStaminaDrainFactor, 10, "Run stamina drain factor for stamina elixir");
      _grandstamregen = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.StaminaRegenFactorOverall, 10, "Overall stamina regen factor while consumed");

      #endregion

      #region GrandStealthSE

      _grandstealthcooldown = Config.Bind(PotionNames.GrandStealthElixir, ConfigKeyNames.Cooldown, 10, "The Cooldown period");
      _grandstealthstealthmod = Config.Bind(PotionNames.GrandStealthElixir, ConfigKeyNames.StealthModifier, 10, "the modifier value for stealth potion");

      #endregion

      #region MediumHealingTidePotion

      _medTideCooldownTimer = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
      _medTideregen = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthRegenFactor, 1f, "The multiplier used for health regeneration during consumption");
      _medHealovertime = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealOverTime, 95, "The volume of health to heal");
      _medHotDuration = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthOverTimeDuration, 10, "The health over time duration");
      _medHotInterval = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthOverTimeInterval, 5, "The Interval for health over time");
      _medHotTicks = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HeathOverTimeTicks, 0, "Ticks of health over time");
      _medHoTtimer = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for health over time. Its confusing dont mess with it unless you know maths");
      _medHotTimeTickHp = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

      #endregion

      #region Medium Spiritual Healing

      _medSpCooldown = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.Cooldown, 0, "The cooldown timer for this potion after consumption");
      _medSpHot = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealOverTime, 100, "The Health value you over time");
      _medSphotDur = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealthOverTimeDuration, 0, "The duration of which health over time is applied");
      _medspHotInt = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealthOverTimeInterval, 0, "Don't play with this one ");
      _medSphoTticks = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HeathOverTimeTicks, 0, "Don't mess with this unless setting up advanced use case");
      _medSphoTtimer = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for Health over time");
      _medSphoTtimeTickHp = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "For advanced users only");

      #endregion

      #region MedStamSE

      _medstamcooldown = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
      _medstaminaOt = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaOverTime, 10, ConfigKeyNames.StaminaOverTime);
      _medstaminaDps = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaDrainPerSecond, 10, "Drain Per Second");
      _medstamjump = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.JumpStaminaDrainFactor, 10, "Jump stamina drain");
      _medstamrun = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.RunStaminaDrainFactor, 10, "Run stamina drain factor for stamina elixir");
      _medstamregen = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaRegenFactorOverall, 10, "Overall stamina regen factor while consumed");

      #endregion

      #region Lesser Tide

      _lesserTideCooldownTimer = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.Cooldown, 100, "The cooldown timer for this potion after consumption");
      _lesserTideregen = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthRegenFactor, 1f, "The multiplier used for health regeneration during consumption");
      _lesserHealovertime = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealOverTime, 95, "The volume of health to heal");
      _lesserHotDuration = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthOverTimeDuration, 10, "The health over time duration");
      _lesserHotInterval = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthOverTimeInterval, 5, "The Interval for health over time");
      _lesserHotTicks = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HeathOverTimeTicks, 0, "Ticks of health over time");
      _lesserHoTtimer = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for health over time. Its confusing don't mess with it unless you know maths");
      _lesserHotTimeTickHp = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "This is more that goes with the previous few confusing ones don't touch unless you understand some maths");

      #endregion

      #region Lesser Spiritual Healing

      _lesserSpCooldown = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.Cooldown, 0, "The cooldown timer for this potion after consumption");
      _lesserSpHot = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealOverTime, 100, "The Health value you over time");
      _lesserSphotDur = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealthOverTimeDuration, 0, "The duration of which health over time is applied");
      _lesserspHotInt = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealthOverTimeInterval, 0, "Don't play with this one ");
      _lesserSphoTticks = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HeathOverTimeTicks, 0, "Don't mess with this unless setting up advanced use case");
      _lesserSphoTtimer = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for Health over time");
      _lesserSphoTtimeTickHp = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "For advanced users only");

      #endregion

      #region Lesser StamSE

      _lesserstamcooldown = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
      _lesserstaminaOt = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaOverTime, 10, ConfigKeyNames.StaminaOverTime);
      _lesserstaminaDps = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaDrainPerSecond, 10, "Drain Per Second");
      _lesserstamjump = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.JumpStaminaDrainFactor, 10, "Jump stamina drain");
      _lesserstamrun = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.RunStaminaDrainFactor, 10, "Run stamina drain factor for stamina elixir");
      _lesserstamregen = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaRegenFactorOverall, 10, "Overall stamina regen factor while consumed");

      #endregion
    }

    public static class PotionNames
    {
      public static string FlaskOfElements = "Flask of Elements";
      public static string FlaskOfFortification = "Flask of Fortification";
      public static string FlaskOfMagelight = "Flask of Magelight";
      public static string FlaskOfSecondWind = "Flask of Second Wind";
      public static string FlaskOfTheGods = "Flask of the Gods";

      public static string GrandHealingTideElixir = "Grand Healing Tide Potion";
      public static string GrandSpiritualHealingElixir = "Grand Spiritual Healing Potion";
      public static string GrandStaminaElixir = "Grand Stamina Elixir";
      public static string GrandStealthElixir = "Grand Stealth Elixir";

      public static string MediumHealingTidePotion = "Medium Healing Tide Flask";
      public static string MediumSpiritualHealingPotion = "Medium Spiritual Healing Flask";
      public static string MediumStaminaPotion = "Medium Stamina Flask";

      public static string LesserHealingTideVial = "Lesser Healing Tide Vial";
      public static string LesserSpiritualHealingVial = "Lesser Spiritual Healing Vial";
      public static string LesserStaminaVial = "Lesser Stamina Vial";
    }

    public static class ConfigKeyNames
    {
      public static string Duration = nameof(Duration);
      public static string Cooldown = nameof(Cooldown);
      public static string HealOverTime = "Heal Over Time";
      public static string JumpDrain = "Jump Drain";
      public static string RunDrain = "Run Drain";
      public static string StaminaRegenFactor = "Stamina regen factor";
      public static string HealthRegenFactor = "Health regen multiplier";
      public static string HealthOverTimeDuration = "Health over time duration";
      public static string HealthOverTimeInterval = "Health over time interval";
      public static string HeathOverTimeTicks = "Heath over time ticks";
      public static string HealthOverTimeTimer = "Health over time timer";
      public static string HeathOverTimeTickPerHp = "Heath over time tick per hp";
      public static string StealthModifier = "Stealth modifier";
      public static string StaminaOverTime = "Stamina over time";
      public static string StaminaDrainPerSecond = "Stamina Drain Per Second";
      public static string JumpStaminaDrainFactor = "Jump stamina drain factor";
      public static string RunStaminaDrainFactor = "Run stamina drain factor";
      public static string StaminaRegenFactorOverall = "Stamina regen factor overall";
    }
  }
}
