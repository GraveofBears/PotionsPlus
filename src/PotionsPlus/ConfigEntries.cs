using BepInEx.Configuration;
using System.Reflection;

namespace PotionsPlus
{
    public partial class PotionsPlus
    {
        private ConfigEntry<float> PhilosopherStoneXpGain;

        public ConfigEntry<int> _elementsTtl, _fortificationTtl, _magelightTtl;

        private ConfigEntry<int> _secondWindTtl;
        private ConfigEntry<float> _secondWindRunDrain, _secondWindJumpDrain, _secondWindRegen;
        private ConfigEntry<int> _secondWindCooldown;

        private ConfigEntry<int> _godsTTl, _godsHealovertime, _hotDuration, _hotInterval, _hotTicks, _hoTtimer, _hotTimeTickHp;
        private ConfigEntry<float> _godsregen;

        private ConfigEntry<int> _grandTideTtl, _grandTideCooldownTimer, _grandHealthOvertime, _grandHealthOvertimeDuration, _grandHealthOvertimeInterval, _grandHealthOvertimeTicks, _grandHealthOvertimeTimer, _grandHealthOvertimeTickHp;
        private ConfigEntry<float> _grandTideRegen;

        private ConfigEntry<int> _grandSiritualHealingTtl, _grandSpiritualHealingCooldownTimer, _grandSpiritualHealingOvertime, _grandSpiritualHealingOvertimeDuration, _grandSpiritualHealingOvertimeInterval, _grandSpiritualHealingOvertimeTicks, _grandSpiritualHealingOvertimeTimer, _grandSpiritualHealingOvertimeTickHp;
        private ConfigEntry<int> _grandStaminaTtl, _grandStaminaCooldown, _grandStaminaOvertime, _grandStaminaDrainPS, _grandStaminaJump, _grandStaminaRun, _grandStaminaRegen;
        private ConfigEntry<int> _grandStealthTtl, _grandStealthCooldown, _grandStealthStealthModifier;

        private ConfigEntry<int> _medTideTtl, _medTideCooldownTimer, _medHealingOvertime, _medHotDuration, _medHotInterval, _medHotTicks, _medHoTtimer, _medHotTimeTickHp;
        private ConfigEntry<float> _medTideRegen;

        private ConfigEntry<int> _medStaminaTtl, _medStaminaCooldown, _medStaminaOvertime, _medStaminaDrainPS, _medStaminaJump, _medStaminaRun, _medStaminaRegen;
        private ConfigEntry<int> _medSpHtl, _medSpCooldown, _medSpHotOvertime, _medSphotDuration, _medSpHotInterval, _medSphoTticks, _medSphoTtimer, _medSphoTtimeTickHp;

        private ConfigEntry<int> _lesserTideTtl, _lesserTideCooldownTimer, _lesserHealthOvertime, _lesserHealthOvertimeDuration, _lesserHealthOvertimeInterval, _lesserHealthOvertimeTicks, _lesserHealthOverTimeTimer, _lesserHealthOvertimeTickHp;
        private ConfigEntry<float> _lesserTideRegen;

        private ConfigEntry<int> _lesserSpHtl, _lesserSpCooldown, _lesserSpHot, _lesserSpHotDuration, _lesserSpHotInterval, _lesserSpHoTticks, _lesserSpHoTtimer, _lesserSpHoTtimeTickHp;
        private ConfigEntry<int> _lesserStaminaTtl, _lesserStaminaCooldown, _lesserStaminaOvertime, _lesserStaminaDrainPS, _lesserStaminaJump, _lesserStaminaRun, _lesserStaminaRegen;

        public ConfigEntry<bool> AlchemySkillEnable;
        public ConfigEntry<bool> AlchemySkillBonusWhenCraftingEnabled;

        private void ConfigEntries()
        {
            Jotunn.Logger.LogDebug($"{GetType().Namespace}.{GetType().Name}.{MethodBase.GetCurrentMethod().Name}()");
            _elementsTtl = Config.Bind(PotionNames.FlaskOfElements, ConfigKeyNames.Duration, 300, $"Duration for the {PotionNames.FlaskOfElements}");
            _fortificationTtl = Config.Bind(PotionNames.FlaskOfFortification, ConfigKeyNames.Duration, 300, $"Duration for the {PotionNames.FlaskOfFortification}");
            _magelightTtl = Config.Bind(PotionNames.FlaskOfMagelight, ConfigKeyNames.Duration, 300, $"Duration for {PotionNames.FlaskOfMagelight}");

            AlchemySkillEnable = Config.Bind("Alchemy Skill", "Enable Alchemy Skill", true, new ConfigDescription("Enable Alchemy skill.", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Order = 1, Browsable = false }));
            AlchemySkillBonusWhenCraftingEnabled = Config.Bind("Alchemy Skill", "Enable Alchemy Bonus", true, new ConfigDescription("Enable Alchemy Bonus when crafting.", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Order = 2, Browsable = false }));
            PhilosopherStoneXpGain = Config.Bind("Alchemy Skill", "Philosopher Stone XP Gain", 5f, new ConfigDescription("XP Gain multiplier when brewing while using a Philosopher Stone.", new AcceptableValueRange<float>(0f, 100f), new ConfigurationManagerAttributes { IsAdminOnly = true, Order = 3, Browsable = false }));


            #region Second Wind Config

            _secondWindTtl = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.Duration, 120, new ConfigDescription("Duration", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _secondWindCooldown = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.Cooldown, 0, new ConfigDescription("Cooldown Timer", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _secondWindJumpDrain = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.JumpDrain, -0.25f, new ConfigDescription("Jump drain multiplicative factor for stamina drain", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _secondWindRunDrain = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.RunDrain, -0.25f, new ConfigDescription("Run drain multiplicative factor for stamina drain", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _secondWindRegen = Config.Bind(PotionNames.FlaskOfSecondWind, ConfigKeyNames.StaminaRegenFactor, 1.05f, new ConfigDescription("Overall multiplicative factor for stamina regen during potion", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));

            #endregion

            #region Flask of the Gods
            _godsTTl = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.Duration, 300, new ConfigDescription($"Duration for {PotionNames.FlaskOfTheGods}", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _godsregen = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthRegenFactor, 1.05f, new ConfigDescription("The multiplier used for health regeneration during consumption", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _godsHealovertime = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealOverTime, 250, new ConfigDescription("The volume of health to heal", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _hotDuration = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthOverTimeDuration, 1, new ConfigDescription("The health over time duration", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _hotInterval = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthOverTimeInterval, 1, new ConfigDescription("The Interval for health over time", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _hotTicks = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HeathOverTimeTicks, 1, new ConfigDescription("Ticks of health over time", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _hoTtimer = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HealthOverTimeTimer, 1, new ConfigDescription("The timer for health over time. Its confusing dont mess with it unless you know maths", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));
            _hotTimeTickHp = Config.Bind(PotionNames.FlaskOfTheGods, ConfigKeyNames.HeathOverTimeTickPerHp, 1, new ConfigDescription("This is more that goes with the previous few confusing ones dont touch unless you understand some maths", null, new ConfigurationManagerAttributes { IsAdminOnly = true, Browsable = false }));

            #endregion

            #region GrandHealingTideElixir

            _grandTideTtl = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.GrandHealingTideElixir}");
            _grandTideCooldownTimer = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.Cooldown, 0, "Cooldown Timer for Second Wind");

            _grandTideRegen = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthRegenFactor, 1f, "The multiplier used for health regeneration during consumption");
            _grandHealthOvertime = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealOverTime, 95, "The volume of health to heal");
            _grandHealthOvertimeDuration = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthOverTimeDuration, 10, "The health over time duration");
            _grandHealthOvertimeInterval = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthOverTimeInterval, 5, "The Interval for health over time");
            _grandHealthOvertimeTicks = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HeathOverTimeTicks, 0, "Ticks of health over time");
            _grandHealthOvertimeTimer = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for health over time. Its confusing dont mess with it unless you know maths");
            _grandHealthOvertimeTickHp = Config.Bind(PotionNames.GrandHealingTideElixir, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

            #endregion

            #region Grand Spiritual Healing

            _grandSiritualHealingTtl = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.Duration, 240, $"Duration for {PotionNames.GrandSpiritualHealingElixir}");
            _grandSpiritualHealingCooldownTimer = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.Cooldown, 0, "The cooldown timer for this potion after consumption");
            _grandSpiritualHealingOvertime = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealOverTime, 100, "The Health value you over time");
            _grandSpiritualHealingOvertimeDuration = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealthOverTimeDuration, 0, "The duration of which health over time is applied");
            _grandSpiritualHealingOvertimeInterval = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealthOverTimeInterval, 0, "Don't play with this one ");
            _grandSpiritualHealingOvertimeTicks = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HeathOverTimeTicks, 0, "Dont mess with this unless setting up advanced use case");
            _grandSpiritualHealingOvertimeTimer = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for Health over time");
            _grandSpiritualHealingOvertimeTickHp = Config.Bind(PotionNames.GrandSpiritualHealingElixir, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "For advanced users only");

            #endregion

            #region GrandStamSE
            _grandStaminaTtl = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.Duration, 240, $"Duration for {PotionNames.GrandStaminaElixir}");
            _grandStaminaCooldown = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
            _grandStaminaOvertime = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.StaminaOverTime, 10, ConfigKeyNames.StaminaOverTime);
            _grandStaminaDrainPS = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.StaminaDrainPerSecond, 10, "Drain Per Second");
            _grandStaminaJump = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.JumpStaminaDrainFactor, 10, "Jump stamina drain");
            _grandStaminaRun = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.RunStaminaDrainFactor, 10, "Run stamina drain factor for stamina elixir");
            _grandStaminaRegen = Config.Bind(PotionNames.GrandStaminaElixir, ConfigKeyNames.StaminaRegenFactorOverall, 10, "Overall stamina regen factor while consumed");

            #endregion

            #region GrandStealthSE

            _grandStealthTtl = Config.Bind(PotionNames.GrandStealthElixir, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.GrandStealthElixir}");
            _grandStealthCooldown = Config.Bind(PotionNames.GrandStealthElixir, ConfigKeyNames.Cooldown, 10, "The Cooldown period");
            _grandStealthStealthModifier = Config.Bind(PotionNames.GrandStealthElixir, ConfigKeyNames.StealthModifier, 10, "the modifier value for stealth potion");

            #endregion

            #region MediumHealingTidePotion

            _medTideTtl = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.MediumHealingTidePotion}");
            _medTideCooldownTimer = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
            _medTideRegen = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthRegenFactor, 1f, "The multiplier used for health regeneration during consumption");
            _medHealingOvertime = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealOverTime, 95, "The volume of health to heal");
            _medHotDuration = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthOverTimeDuration, 10, "The health over time duration");
            _medHotInterval = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthOverTimeInterval, 5, "The Interval for health over time");
            _medHotTicks = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HeathOverTimeTicks, 0, "Ticks of health over time");
            _medHoTtimer = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for health over time. Its confusing dont mess with it unless you know maths");
            _medHotTimeTickHp = Config.Bind(PotionNames.MediumHealingTidePotion, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

            #endregion

            #region Medium Spiritual Healing

            _medSpHtl = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.MediumSpiritualHealingPotion}");
            _medSpCooldown = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.Cooldown, 0, "The cooldown timer for this potion after consumption");
            _medSpHotOvertime = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealOverTime, 100, "The Health value you over time");
            _medSphotDuration = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealthOverTimeDuration, 0, "The duration of which health over time is applied");
            _medSpHotInterval = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealthOverTimeInterval, 0, "Don't play with this one ");
            _medSphoTticks = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HeathOverTimeTicks, 0, "Don't mess with this unless setting up advanced use case");
            _medSphoTtimer = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for Health over time");
            _medSphoTtimeTickHp = Config.Bind(PotionNames.MediumSpiritualHealingPotion, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "For advanced users only");

            #endregion

            #region MedStamSE

            _medStaminaTtl = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.MediumStaminaPotion}");
            _medStaminaCooldown = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
            _medStaminaOvertime = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaOverTime, 10, ConfigKeyNames.StaminaOverTime);
            _medStaminaDrainPS = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaDrainPerSecond, 10, "Drain Per Second");
            _medStaminaJump = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.JumpStaminaDrainFactor, 10, "Jump stamina drain");
            _medStaminaRun = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.RunStaminaDrainFactor, 10, "Run stamina drain factor for stamina elixir");
            _medStaminaRegen = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaRegenFactorOverall, 10, "Overall stamina regen factor while consumed");

            #endregion

            #region Lesser Tide

            _lesserTideTtl = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.LesserHealingTideVial}");
            _lesserTideCooldownTimer = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.Cooldown, 100, "The cooldown timer for this potion after consumption");
            _lesserTideRegen = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthRegenFactor, 1f, "The multiplier used for health regeneration during consumption");
            _lesserHealthOvertime = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealOverTime, 95, "The volume of health to heal");
            _lesserHealthOvertimeDuration = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthOverTimeDuration, 10, "The health over time duration");
            _lesserHealthOvertimeInterval = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthOverTimeInterval, 5, "The Interval for health over time");
            _lesserHealthOvertimeTicks = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HeathOverTimeTicks, 0, "Ticks of health over time");
            _lesserHealthOverTimeTimer = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for health over time. Its confusing don't mess with it unless you know maths");
            _lesserHealthOvertimeTickHp = Config.Bind(PotionNames.LesserHealingTideVial, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "This is more that goes with the previous few confusing ones don't touch unless you understand some maths");

            #endregion

            #region Lesser Spiritual Healing

            _lesserSpHtl = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.LesserSpiritualHealingVial}");
            _lesserSpCooldown = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.Cooldown, 0, "The cooldown timer for this potion after consumption");
            _lesserSpHot = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealOverTime, 100, "The Health value you over time");
            _lesserSpHotDuration = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealthOverTimeDuration, 0, "The duration of which health over time is applied");
            _lesserSpHotInterval = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealthOverTimeInterval, 0, "Don't play with this one ");
            _lesserSpHoTticks = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HeathOverTimeTicks, 0, "Don't mess with this unless setting up advanced use case");
            _lesserSpHoTtimer = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HealthOverTimeTimer, 0, "The timer for Health over time");
            _lesserSpHoTtimeTickHp = Config.Bind(PotionNames.LesserSpiritualHealingVial, ConfigKeyNames.HeathOverTimeTickPerHp, 0, "For advanced users only");

            #endregion

            #region Lesser StamSE

            _lesserStaminaTtl = Config.Bind(PotionNames.LesserStaminaVial, ConfigKeyNames.Duration, 120, $"Duration for {PotionNames.LesserStaminaVial}");
            _lesserStaminaCooldown = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.Cooldown, 100, "Cooldown timer for the stamina elixir");
            _lesserStaminaOvertime = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaOverTime, 10, ConfigKeyNames.StaminaOverTime);
            _lesserStaminaDrainPS = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaDrainPerSecond, 10, "Drain Per Second");
            _lesserStaminaJump = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.JumpStaminaDrainFactor, 10, "Jump stamina drain");
            _lesserStaminaRun = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.RunStaminaDrainFactor, 10, "Run stamina drain factor for stamina elixir");
            _lesserStaminaRegen = Config.Bind(PotionNames.MediumStaminaPotion, ConfigKeyNames.StaminaRegenFactorOverall, 10, "Overall stamina regen factor while consumed");

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
