using BepInEx.Configuration;

namespace PotionsPlus
{
    internal partial class PotionsPlus
    {
        private static ConfigEntry<float> _tick;
        private static ConfigEntry<int> _healthtime;
        private static ConfigEntry<int> _healthtick;
        private static ConfigEntry<int> _healthTimer;
        private static ConfigEntry<int> _cooldown;
        public ConfigEntry<int> Ttl;

        public ConfigEntry<int> FortificationTtl;
        public ConfigEntry<int> MagelightTtl;

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

        private ConfigEntry<int> _grandSpTtl;
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
        
        private ConfigEntry<int> _medSpTtl;
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
        
        private ConfigEntry<int> _lesserSpTtl;
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
        
        
        public void ConfigEntries()
        {
            Ttl = Config.Bind("test", "test", 1, "test");
            FortificationTtl = Config.Bind("Fortification", "TTL", 300, "The Time to last for the fortification potion");
            MagelightTtl = Config.Bind("Magelight", "TTL", 300, "Time To Last for Magelight Potion");
            _secondWindTtl = Config.Bind("Second Wind", "TTL", 120, "Time To last For Second Wind");
            _godsTTl = Config.Bind("Flask of the Gods", "TTL", 300, "The time toe last");
            _grandTideTtl = Config.Bind("Grand Healing Tide", "TTL", 120, "The time toe last");
            _grandSpTtl = Config.Bind("Grand Spiritual Tide", "TTL", 240, "The time to last");
            _grandstamTtl = Config.Bind("Grand Stamina Elixir", "TTL", 240, "The time to last");
            _grandStealthTtl = Config.Bind("Grand Stealth Elixir", "TTL", 120, "The time to last");
            _medTideTtl = Config.Bind("Medium Healing Tide", "TTL", 120, "The time toe last");
            _medSpTtl = Config.Bind("Medium Spiritual Tide", "TTL", 120, "The time toe last");
            _medstamTtl = Config.Bind("Medium Stamina", "TTL", 120, "The time toe last");
            _lesserTideTtl = Config.Bind("Lesser Tide", "TTL", 120, "The time toe last");
            _lesserSpTtl = Config.Bind("Lesser Spiritual Tide", "TTL", 120, "The time toe last");
            _lesserstamTtl = Config.Bind("Lesser Stamina", "TTL", 120, "The time toe last");
            
            //TODO: Add the TTL for all potion types in here as Config.Bind()'s

            
            #region Second Wind Config
            _secondWindCooldown = Config.Bind("Second Wind", "Cooldown", 10, "Cooldown Timer for Second Wind");
            _grandTideCooldownTimer = Config.Bind("Grand Spiritual Tide", "Cooldown", 0, "Cooldown Timer for Second Wind");
           
            _secondWindjumpDrain = Config.Bind("Second Wind", "Jump Drain", -0.25f,"Jump drain multiplicative factor for stamina drain");
            _secondWindrunDrain = Config.Bind("Second Wind", "Run Drain", -0.25f,"Run drain multiplicative factor for stamina drain");
            _secondWindRegen = Config.Bind("Second Wind", "Stam Regen Factor", 1.05f,"Overall multiplicative factor for stamina regen during potion");
            #endregion
            #region Flask of the Gods
            _godsregen = Config.Bind("Flask of the Gods", "Health Regen multiplier", 1.05f,
                "The multiplier used for health regeneration during consumption");
            _godsHealovertime = Config.Bind("Flask of the Gods", "Heal Over Time", 250, "The volume of health to heal");
            _hotDuration = Config.Bind("Flask of the Gods", "Health over time Duration", 1,
                "The health over time duration");
            _hotInterval = Config.Bind("Flask of the Gods", "Health over time Interval", 1,
                "The Interval for health over time");
            _hotTicks = Config.Bind("Flask of the Gods", "Heath over time ticks", 1, "Ticks of health over time");
            _hoTtimer = Config.Bind("Flask of the Gods", "Health over time timer", 1,
                "The timer for health over time. Its confusing dont mess with it unless you know maths");
            _hotTimeTickHp = Config.Bind("Flask of the Gods", "Heath over time tick per hp", 1,
                "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");
            #endregion
            #region  GrandTide


            _grandTideregen = Config.Bind("Grand Healing Tide", "Health Regen multiplier", 1f,
                "The multiplier used for health regeneration during consumption");
             _grandHealovertime = Config.Bind("Grand Healing Tide", "Heal Over Time", 95, "The volume of health to heal");
             _grandHotDuration = Config.Bind("Grand Healing Tide", "Health over time Duration", 10,
                "The health over time duration");
             _grandHotInterval = Config.Bind("Grand Healing Tide", "Health over time Interval", 5,
                "The Interval for health over time");
             _grandHotTicks = Config.Bind("Grand Healing Tide", "Heath over time ticks", 0, "Ticks of health over time");
             _grandHoTtimer = Config.Bind("Grand Healing Tide", "Health over time timer", 0,
                "The timer for health over time. Its confusing dont mess with it unless you know maths");
             _grandHotTimeTickHp = Config.Bind("Grand Healing Tide", "Heath over time tick per hp", 0,
                "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

             #endregion
            #region  Grand Spiritual Tide

             _grandSpCooldown = Config.Bind("Grand Spritual Tide", "Cooldown Timer", 0,
                 "The cooldown timer for this potion after consumption");
             _grandSpHot = Config.Bind("Grand Spiritual Tide", "Health Over Time", 100,
                 "The Health value you over time");
             _grandSphotDur = Config.Bind("Grand Spiritual Tide", "Health Over Time Duration", 0,
                 "The duration of which health over time is applied");
             _grandspHotInt = Config.Bind("Grand Spiritual Tide", "The Interval of Health Over Time", 0,
                 "Don't play with this one ");
             _grandSphoTticks = Config.Bind("Grand Spiritual Tide", "The ticks of health over time", 0,
                 "Dont mess with this unless setting up advanced use case");
             _grandSphoTtimer = Config.Bind("Grand Spiritual Tide", "Health over time Timer", 0,
                 "The timer for Health over time");
             _grandSphoTtimeTickHp = Config.Bind("Grand Spiritual Tide", "The time per tick of health over time", 0,
                 "For advanced users only");
             

             #endregion
            #region GrandStamSE

             _grandstamcooldown = Config.Bind("Grand Stamina Elixir", "Cooldown", 100,
                 "Cooldown timer for the stamina elixir");
             _grandstaminaOt = Config.Bind("Grand Stamina Elixir", "Stamina over time", 10, "Stamina Over Time");
             _grandstaminaDps = Config.Bind("Grand Stamina Elixir", "Stamina Drain Per Second", 10, "Drain Per Second");
             _grandstamjump = Config.Bind("Grand Stamina Elixir", "Jump stamina drain factor", 10,
                 "Jump stamina drain");
             _grandstamrun = Config.Bind("Grand Stamina Elixir", "Run stamina drain factor", 10,
                 "Run stamina drain factor for stamina elixir");
             _grandstamregen = Config.Bind("Grand Stamina Elixir", "Stamina regen factor overall", 10,
                 "Overall stamina regen factor while consumed");
             

             #endregion
            #region GrandStealthSE
            _grandstealthcooldown = Config.Bind("Grand Stealth Elixir", "Cooldown", 10, "The Cooldown period");
            _grandstealthstealthmod = Config.Bind("Grand Stealth Elixir", "Stealth modifier", 10,
                "the modifier value for stealth potion");
            #endregion
            #region  MediumTide
            _medTideCooldownTimer = Config.Bind("Medium Healing Tide", "Cooldown", 100,
                "Cooldown timer for the stamina elixir");

            _medTideregen = Config.Bind("Medium Healing Tide", "Health Regen multiplier", 1f,
                "The multiplier used for health regeneration during consumption");
            _medHealovertime = Config.Bind("Medium Healing Tide", "Heal Over Time", 95, "The volume of health to heal");
            _medHotDuration = Config.Bind("Medium Healing Tide", "Health over time Duration", 10,
                "The health over time duration");
            _medHotInterval = Config.Bind("Medium Healing Tide", "Health over time Interval", 5,
                "The Interval for health over time");
            _medHotTicks = Config.Bind("Medium Healing Tide", "Heath over time ticks", 0, "Ticks of health over time");
            _medHoTtimer = Config.Bind("Medium Healing Tide", "Health over time timer", 0,
                "The timer for health over time. Its confusing dont mess with it unless you know maths");
            _medHotTimeTickHp = Config.Bind("Medium Healing Tide", "Heath over time tick per hp", 0,
                "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

            #endregion
            #region  Medium Spiritual Tide
            _medSpCooldown = Config.Bind("Medium Spritual Tide", "Cooldown Timer", 0,
                "The cooldown timer for this potion after consumption");
            _medSpHot = Config.Bind("Medium Spiritual Tide", "Health Over Time", 100,
                "The Health value you over time");
            _medSphotDur = Config.Bind("Medium Spiritual Tide", "Health Over Time Duration", 0,
                "The duration of which health over time is applied");
            _medspHotInt = Config.Bind("Medium Spiritual Tide", "The Interval of Health Over Time", 0,
                "Don't play with this one ");
            _medSphoTticks = Config.Bind("Medium Spiritual Tide", "The ticks of health over time", 0,
                "Dont mess with this unless setting up advanced use case");
            _medSphoTtimer = Config.Bind("Medium Spiritual Tide", "Health over time Timer", 0,
                "The timer for Health over time");
            _medSphoTtimeTickHp = Config.Bind("Medium Spiritual Tide", "The time per tick of health over time", 0,
                "For advanced users only");
            #endregion
            #region MedStamSE

            _medstamcooldown = Config.Bind("Medium Stamina Elixir", "Cooldown", 100,
                "Cooldown timer for the stamina elixir");
            _medstaminaOt = Config.Bind("Medium Stamina Elixir", "Stamina over time", 10, "Stamina Over Time");
            _medstaminaDps = Config.Bind("Medium Stamina Elixir", "Stamina Drain Per Second", 10, "Drain Per Second");
            _medstamjump = Config.Bind("Medium Stamina Elixir", "Jump stamina drain factor", 10,
                "Jump stamina drain");
            _medstamrun = Config.Bind("Medium Stamina Elixir", "Run stamina drain factor", 10,
                "Run stamina drain factor for stamina elixir");
            _medstamregen = Config.Bind("Medium Stamina Elixir", "Stamina regen factor overall", 10,
                "Overall stamina regen factor while consumed");
             

            #endregion
            #region  Lesser Tide
            _lesserTideCooldownTimer = Config.Bind("Lesser Healing Tide", "Cooldown", 100,
                "Cooldown timer for the stamina elixir");

            _lesserTideregen = Config.Bind("Lesser Healing Tide", "Health Regen multiplier", 1f,
                "The multiplier used for health regeneration during consumption");
            _lesserHealovertime = Config.Bind("Lesser Healing Tide", "Heal Over Time", 95, "The volume of health to heal");
            _lesserHotDuration = Config.Bind("Lesser Healing Tide", "Health over time Duration", 10,
                "The health over time duration");
            _lesserHotInterval = Config.Bind("Lesser Healing Tide", "Health over time Interval", 5,
                "The Interval for health over time");
            _lesserHotTicks = Config.Bind("Lesser Healing Tide", "Heath over time ticks", 0, "Ticks of health over time");
            _lesserHoTtimer = Config.Bind("Lesser Healing Tide", "Health over time timer", 0,
                "The timer for health over time. Its confusing dont mess with it unless you know maths");
            _lesserHotTimeTickHp = Config.Bind("Lesser Healing Tide", "Heath over time tick per hp", 0,
                "This is more that goes with the previous few confusing ones dont touch unless you understand some maths");

            #endregion
            #region  Lesser Spiritual Tide
            _lesserSpCooldown = Config.Bind("Lesser Spritual Tide", "Cooldown Timer", 0,
                "The cooldown timer for this potion after consumption");
            _lesserSpHot = Config.Bind("Lesser Spiritual Tide", "Health Over Time", 100,
                "The Health value you over time");
            _lesserSphotDur = Config.Bind("Lesser Spiritual Tide", "Health Over Time Duration", 0,
                "The duration of which health over time is applied");
            _lesserspHotInt = Config.Bind("Lesser Spiritual Tide", "The Interval of Health Over Time", 0,
                "Don't play with this one ");
            _lesserSphoTticks = Config.Bind("Lesser Spiritual Tide", "The ticks of health over time", 0,
                "Dont mess with this unless setting up advanced use case");
            _lesserSphoTtimer = Config.Bind("Lesser Spiritual Tide", "Health over time Timer", 0,
                "The timer for Health over time");
            _lesserSphoTtimeTickHp = Config.Bind("Lesser Spiritual Tide", "The time per tick of health over time", 0,
                "For advanced users only");
            #endregion
            #region Lesser StamSE

            _lesserstamcooldown = Config.Bind("Medium Stamina Elixir", "Cooldown", 100,
                "Cooldown timer for the stamina elixir");
            _lesserstaminaOt = Config.Bind("Medium Stamina Elixir", "Stamina over time", 10, "Stamina Over Time");
            _lesserstaminaDps = Config.Bind("Medium Stamina Elixir", "Stamina Drain Per Second", 10, "Drain Per Second");
            _lesserstamjump = Config.Bind("Medium Stamina Elixir", "Jump stamina drain factor", 10,
                "Jump stamina drain");
            _lesserstamrun = Config.Bind("Medium Stamina Elixir", "Run stamina drain factor", 10,
                "Run stamina drain factor for stamina elixir");
            _lesserstamregen = Config.Bind("Medium Stamina Elixir", "Stamina regen factor overall", 10,
                "Overall stamina regen factor while consumed");
             

            #endregion
        }
    }
}