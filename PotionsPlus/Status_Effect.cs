using System.Collections.Generic;
using System.Linq;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;

namespace PotionsPlus
{
    internal partial class PotionsPlus 
    {
        //TODO: Finish adding Gpower animations to all the entries as well as the activation effect calls
        private void GrandTideSe()
        {
            _potion1.name = "Grand Spiritual Healing Tide";
            _potion1.m_name = "Grand Spiritual Healing Tide";
            _potion1.m_icon =
                _grandspiritual.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _potion1.m_cooldownIcon = true;
            _potion1.m_cooldown = _grandTideCooldownTimer.Value;
            _potion1.m_tooltip = "Grand Spiritual Healing Tide Flask";
            _potion1.m_ttl = _grandTideTtl.Value;
            _potion1.m_tickTimer = 10;
            _potion1.m_healthOverTime = _grandHealovertime.Value; // this is piped to health config.value
            _potion1.m_healthPerTick = _grandHotTicks.Value; // piped to cfg
            _potion1.m_healthRegenMultiplier = _grandTideregen.Value; //piped to cfg
            _potion1.m_healthOverTimeDuration = _grandHotDuration.Value; //config pipe
            _potion1.m_healthOverTimeInterval = _grandHotInterval.Value;
            _potion1.m_healthOverTimeTicks = _grandHotTicks.Value;
            _potion1.m_healthOverTimeTimer = _grandHoTtimer.Value;
            _potion1.m_healthOverTimeTickHP = _grandHotTimeTickHp.Value;
            _potion1.ModifyHealthRegen(ref _potion1.m_healthRegenMultiplier);
            _potion1.m_activationAnimation = "gpower";
            _sePotion1 = new CustomStatusEffect(_potion1, true);
            ItemManager.Instance.AddStatusEffect(_sePotion1);
        }
        private void FlaskOfFortificationSe()
        {
            _fortificationSe.name = "Flask of Fortification";
            _fortificationSe.m_name = "Flask of Fortification";
            _fortificationSe.m_icon = _fortificationPrGameObject.GetComponent<ItemDrop>().m_itemData.m_shared.m_icons.FirstOrDefault();
            _fortificationSe.m_cooldownIcon = true;
            _fortificationSe.m_cooldown = 0;
            _fortificationSe.m_activationAnimation = "gpower";
            _fortificationSe.m_ttl = FortificationTtl.Value;
            _fortificationSe.m_mods = new List<HitData.DamageModPair>
            {
                new() {m_modifier = HitData.DamageModifier.VeryResistant, m_type = HitData.DamageType.Blunt},
                new() {m_modifier = HitData.DamageModifier.VeryResistant, m_type = HitData.DamageType.Slash},
                new() {m_modifier = HitData.DamageModifier.VeryResistant, m_type = HitData.DamageType.Pierce}
            };
            _fortificationSe.m_startEffects =  new EffectList
            {
                m_effectPrefabs = new EffectList.EffectData[]
                {
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("VFX_GreenPotionDrink"),
                        m_enabled = true,
                        m_attach = true
                    },
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("potionaudio"),
                        m_enabled = true,
                        m_attach = true
                    }
                }
            };
            _fortification = new CustomStatusEffect(_fortificationSe, true);
            ItemManager.Instance.AddStatusEffect(_fortification);
        }
        private void MagelightSe()
        {

            _magelightSestat.m_name = "Flask of Magelight";
            _magelightSestat.name = "Flask of Magelight";
            _magelightSestat.m_icon = _magelightPrefab.GetComponent<ItemDrop>().m_itemData
                .m_shared.m_icons.FirstOrDefault();
            _magelightSestat.m_startEffects = new EffectList
            {
                m_effectPrefabs = new EffectList.EffectData[]
                {
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("Magelight"),
                        m_enabled = true,
                        m_attach = true
                    },
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("VFX_PurplePotionDrink"),
                        m_enabled = true,
                        m_attach = true
                    },
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("potionaudio"),
                        m_enabled = true,
                        m_attach = true
                    }
                }
            };
            _magelightSestat.m_activationAnimation = "gpower";
            _magelightSestat.m_ttl = MagelightTtl.Value;
            _seMagelight = new CustomStatusEffect(_magelightSestat, true);
            ItemManager.Instance.AddStatusEffect(_seMagelight);
        }
        private void SecondWindSe()
        {
            _secondWindSestat.m_name = "Flask of Second Wind";
            _secondWindSestat.name = "Flask of Second Wind";
            _secondWindSestat.m_runStaminaDrainModifier = _secondWindrunDrain.Value; //placeholder
            _secondWindSestat.m_jumpStaminaUseModifier = _secondWindjumpDrain.Value; //placeholder
            _secondWindSestat.m_activationAnimation = "gpower";
            _secondWindSestat.m_cooldownIcon = true;
            _secondWindSestat.m_cooldown = _secondWindCooldown.Value;//placholder;
            _secondWindSestat.m_ttl = _secondWindTtl.Value;//placeholder;
            _secondWindSestat.m_startEffects =new EffectList
                    {
                        m_effectPrefabs = new EffectList.EffectData[]
                        {
                            new()
                            {
                                m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("VFX_PurplePotionDrink"),
                                m_enabled = true,
                                m_attach = true
                            },
                            new()
                            {
                                m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("potionaudio"),
                                m_enabled = true,
                                m_attach = true
                            }
                        }
                    };
            _secondWindSestat.m_staminaRegenMultiplier = _secondWindRegen.Value;
            _secondWindSestat.ModifyStaminaRegen(ref _secondWindSestat.m_staminaRegenMultiplier);
            _secondWindSestat.ModifyJumpStaminaUsage(Player.m_localPlayer.m_jumpStaminaUsage, ref _secondWindSestat.m_jumpStaminaUseModifier);
            _secondWindSestat.ModifyRunStaminaDrain(Player.m_localPlayer.m_runStaminaDrain, ref _secondWindSestat.m_runStaminaDrainModifier);
            _seSecondWind = new CustomStatusEffect(_secondWindSestat, false);
            ItemManager.Instance.AddStatusEffect(_seSecondWind);

        }
        private void GodsSe()
        {
            _godsSestat.m_name = "Flaks of the Gods";
            _godsSestat.name = "Flask of the Gods";
            _godsSestat.m_healthOverTime = _godsHealovertime.Value;
            _godsSestat.m_healthRegenMultiplier = _godsregen.Value; 
            _godsSestat.m_healthOverTimeDuration = _hotDuration.Value; 
            _godsSestat.m_healthOverTimeInterval = _hotInterval.Value;
            _godsSestat.m_healthOverTimeTicks = _hotTicks.Value;
            _godsSestat.m_healthOverTimeTimer = _hoTtimer.Value;
            _godsSestat.m_healthOverTimeTickHP = _hotTimeTickHp.Value;
            
            _godsSestat.m_icon = _flaskofthegodsPrefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_icons
                .FirstOrDefault();
            _godsSestat.m_startEffects = new EffectList
            {
                m_effectPrefabs = new EffectList.EffectData[]
                {
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("VFX_RedPotionDrink"),
                        m_attach = true,
                        m_enabled = true

                    },
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("potionaudio"),
                        m_attach = true,
                        m_enabled = true
                    }
                }
            };
            _godsSestat.m_ttl = _godsTTl.Value;
            _godsSestat.ModifyHealthRegen(ref _godsSestat.m_healthRegenMultiplier);
            _godsSestat.m_tooltip = "Flask of the Gods will give you strength!";
            _seGods = new CustomStatusEffect(_godsSestat, true);
            ItemManager.Instance.AddStatusEffect(_seGods);
        }
        private void GrandSpiritualSe()
        {
            _grandSpiritualSe.name = "Grand Spiritual Tide";
            _grandSpiritualSe.m_name = "Grand Spiritual Tide";
            _grandSpiritualSe.m_tooltip = "A Grand Spiritual Tide washes over you";
            _grandSpiritualSe.m_icon = _grandspiritual.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _grandSpiritualSe.m_startEffects = new EffectList
            {
                m_effectPrefabs = new EffectList.EffectData[]
                {
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("VFX_RedPotionDrink"),
                        m_attach = true,
                        m_enabled = true

                    },
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("potionaudio"),
                        m_attach = true,
                        m_enabled = true
                    }
                }
            };
            _grandSpiritualSe.m_ttl = _grandSpTtl.Value;
            _grandSpiritualSe.m_healthOverTime = _grandSpHot.Value;
            _grandSpiritualSe.m_healthRegenMultiplier = _godsregen.Value; 
            _grandSpiritualSe.m_healthOverTimeDuration = _grandSphotDur.Value; 
            _grandSpiritualSe.m_healthOverTimeInterval = _grandspHotInt.Value;
            _grandSpiritualSe.m_healthOverTimeTicks = _grandSphoTticks.Value;
            _grandSpiritualSe.m_healthOverTimeTimer = _grandSphoTtimer.Value;
            _grandSpiritualSe.m_healthOverTimeTickHP = _grandSphoTtimeTickHp.Value;
            _seGrandSpiritual = new CustomStatusEffect(_grandSpiritualSe, true);
            ItemManager.Instance.AddStatusEffect(_seGrandSpiritual);
        }
        private void GrandStaminaSe()
        {
            _grandstamSE.name = "Grand Stamina Elixir";
            _grandstamSE.m_name = "Grand Stamina Elixir";
            _grandstamSE.m_staminaRegenMultiplier = _grandstamregen.Value;
            _grandstamSE.m_staminaOverTime = _grandstaminaOt.Value;
            _grandstamSE.m_staminaDrainPerSec = _grandstaminaDps.Value;
            _grandstamSE.m_jumpStaminaUseModifier = _grandstamjump.Value;
            _grandstamSE.m_runStaminaDrainModifier = _grandstamrun.Value;
            _grandstamSE.ModifyStaminaRegen(ref _grandstamSE.m_staminaRegenMultiplier);
            _grandstamSE.ModifyJumpStaminaUsage(Player.m_localPlayer.m_jumpStaminaUsage ,ref _grandstamSE.m_jumpStaminaUseModifier);
            _grandstamSE.ModifyRunStaminaDrain(Player.m_localPlayer.m_runStaminaDrain, ref _grandstamSE.m_runStaminaDrainModifier);
            _grandstamSE.m_icon = _grandstam.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _grandstamSE.m_tooltip = "A boost of stamina over time";
            _grandstamSE.m_cooldownIcon = true;
            _grandstamSE.m_cooldown = _grandstamcooldown.Value;
            _grandstamSE.m_ttl = _grandstamTtl.Value;
            _seGrandStam = new CustomStatusEffect(_grandstamSE, true);
            ItemManager.Instance.AddStatusEffect(_seGrandStam);
            
        }
        private void GrandStealthSe()
        {
            _grandstealthSE.name = "Grand Stealth Elixir";
            _grandstealthSE.m_name = "Grand Stealth Elixir";
            _grandstealthSE.m_tooltip = "A boost to your stealth short term, be almost invisible to enemies";
            _grandstealthSE.m_stealthModifier = _grandstealthstealthmod.Value;
            _grandstealthSE.ModifyStealth(Player.m_localPlayer.m_stealthFactor,ref _grandstealthSE.m_stealthModifier);
            _grandstealthSE.m_ttl = _grandStealthTtl.Value;
            _grandstealthSE.m_cooldown = _grandstealthcooldown.Value;
            _grandstealthSE.m_cooldownIcon = true;
            _grandstealthSE.m_icon = _grandstealth.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _seGrandStealth = new CustomStatusEffect(_grandstealthSE, true);
            ItemManager.Instance.AddStatusEffect(_seGrandStealth);
        }
        private void MediumTideSe()
        {
            _mediumtideSE.name = "Medium Healing Tide Vial";
            _mediumtideSE.m_name = "Medium Healing Tide Vial";
            _mediumtideSE.m_tooltip = "A medium size health increase over a short time";
            _mediumtideSE.m_icon = _mediumtide.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _mediumtideSE.m_cooldownIcon = true;
            _mediumtideSE.m_cooldown = _medTideCooldownTimer.Value;
            _mediumtideSE.m_ttl = _medTideTtl.Value;
            _mediumtideSE.m_tickTimer = 10;
            _mediumtideSE.m_healthOverTime = _medHealovertime.Value; // this is piped to health config.value
            _mediumtideSE.m_healthPerTick = _medHotTicks.Value; // piped to cfg
            _mediumtideSE.m_healthRegenMultiplier = _medTideregen.Value; //piped to cfg
            _mediumtideSE.m_healthOverTimeDuration = _medHotDuration.Value; //config pipe
            _mediumtideSE.m_healthOverTimeInterval = _medHotInterval.Value;
            _mediumtideSE.m_healthOverTimeTicks = _medHotTicks.Value;
            _mediumtideSE.m_healthOverTimeTimer = _medHoTtimer.Value;
            _mediumtideSE.m_healthOverTimeTickHP = _medHotTimeTickHp.Value;
            _mediumtideSE.ModifyHealthRegen(ref _mediumtideSE.m_healthRegenMultiplier);
            _mediumtideSE.m_activationAnimation = "gpower";
            _seMediumTide = new CustomStatusEffect(_mediumtideSE, true);
            ItemManager.Instance.AddStatusEffect(_seMediumTide);
        }
        private void MedSpiritualSe()
        {
            _mediumSpiritualSe.name = "Medium Spiritual Tide";
            _mediumSpiritualSe.m_name = "Medium Spiritual Tide";
            _mediumSpiritualSe.m_tooltip = "A Medium Spiritual Tide washes over you";
            _mediumSpiritualSe.m_icon = _mediumspiritual.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _mediumSpiritualSe.m_startEffects = new EffectList
            {
                m_effectPrefabs = new EffectList.EffectData[]
                {
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("VFX_RedPotionDrink"),
                        m_attach = true,
                        m_enabled = true

                    },
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("potionaudio"),
                        m_attach = true,
                        m_enabled = true
                    }
                }
            };
            _mediumSpiritualSe.m_ttl =_medSpTtl.Value;
            _mediumSpiritualSe.m_healthOverTime =_medSpHot.Value;
            _mediumSpiritualSe.m_healthRegenMultiplier = _godsregen.Value; 
            _mediumSpiritualSe.m_healthOverTimeDuration =_medSphotDur.Value; 
            _mediumSpiritualSe.m_healthOverTimeInterval =_medspHotInt.Value;
            _mediumSpiritualSe.m_healthOverTimeTicks =_medSphoTticks.Value;
            _mediumSpiritualSe.m_healthOverTimeTimer =_medSphoTtimer.Value;
            _mediumSpiritualSe.m_healthOverTimeTickHP =_medSphoTtimeTickHp.Value;
            _seMediumSpiritual = new CustomStatusEffect(_mediumSpiritualSe, true);
            ItemManager.Instance.AddStatusEffect(_seMediumSpiritual);
        }
        private void MedStaminaSe()
        {
            _mediumstamSe.name = "Medium Stamina Elixir";
            _mediumstamSe.m_name = "Medium Stamina Elixir";
            _mediumstamSe.m_staminaRegenMultiplier =_medstamregen.Value;
            _mediumstamSe.m_staminaOverTime =_medstaminaOt.Value;
            _mediumstamSe.m_staminaDrainPerSec =_medstaminaDps.Value;
            _mediumstamSe.m_jumpStaminaUseModifier =_medstamjump.Value;
            _mediumstamSe.m_runStaminaDrainModifier =_medstamrun.Value;
            _mediumstamSe.ModifyStaminaRegen(ref _mediumstamSe.m_staminaRegenMultiplier);
            _mediumstamSe.ModifyJumpStaminaUsage(Player.m_localPlayer.m_jumpStaminaUsage ,ref _mediumstamSe.m_jumpStaminaUseModifier);
            _mediumstamSe.ModifyRunStaminaDrain(Player.m_localPlayer.m_runStaminaDrain, ref _mediumstamSe.m_runStaminaDrainModifier);
            _mediumstamSe.m_icon = _mediumstam.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _mediumstamSe.m_tooltip = "A boost of stamina over time";
            _mediumstamSe.m_cooldownIcon = true;
            _mediumstamSe.m_cooldown =_medstamcooldown.Value;
            _mediumstamSe.m_ttl =_medstamTtl.Value;
            _seMediumStam = new CustomStatusEffect(_mediumstamSe, true);
            ItemManager.Instance.AddStatusEffect(_seMediumStam);
            
        }
        private void LesserTideSe()
        {
            _lessertideSe.name = "Lesser Healing Tide Vial";
            _lessertideSe.m_name = "Lesser Healing Tide Vial";
            _lessertideSe.m_tooltip = "A lesser size health increase over a short time";
            _lessertideSe.m_icon = _lessertide.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _lessertideSe.m_cooldownIcon = true;
            _lessertideSe.m_cooldown = _lesserTideCooldownTimer.Value;
            _lessertideSe.m_ttl = _lesserTideTtl.Value;
            _lessertideSe.m_tickTimer = 10;
            _lessertideSe.m_healthOverTime = _lesserHealovertime.Value; // this is piped to health config.value
            _lessertideSe.m_healthPerTick = _lesserHotTicks.Value; // piped to cfg
            _lessertideSe.m_healthRegenMultiplier = _lesserTideregen.Value; //piped to cfg
            _lessertideSe.m_healthOverTimeDuration = _lesserHotDuration.Value; //config pipe
            _lessertideSe.m_healthOverTimeInterval = _lesserHotInterval.Value;
            _lessertideSe.m_healthOverTimeTicks = _lesserHotTicks.Value;
            _lessertideSe.m_healthOverTimeTimer = _lesserHoTtimer.Value;
            _lessertideSe.m_healthOverTimeTickHP = _lesserHotTimeTickHp.Value;
            _lessertideSe.ModifyHealthRegen(ref _lessertideSe.m_healthRegenMultiplier);
            _lessertideSe.m_activationAnimation = "gpower";
            _seLesserTide = new CustomStatusEffect(_lessertideSe, true);
            ItemManager.Instance.AddStatusEffect(_seLesserTide);
        }
        private void LesserSpiritualSe()
        {
            _lesserSpiritualSe.name = "Lesser Spiritual Tide";
            _lesserSpiritualSe.m_name = "Lesser Spiritual Tide";
            _lesserSpiritualSe.m_tooltip = "A Lesser Spiritual Tide washes over you";
            _lesserSpiritualSe.m_icon = _lesserspiritual.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _lesserSpiritualSe.m_startEffects = new EffectList
            {
                m_effectPrefabs = new EffectList.EffectData[]
                {
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("VFX_RedPotionDrink"),
                        m_attach = true,
                        m_enabled = true

                    },
                    new()
                    {
                        m_prefab = PrefabManager.Cache.GetPrefab<GameObject>("potionaudio"),
                        m_attach = true,
                        m_enabled = true
                    }
                }
            };
            _lesserSpiritualSe.m_ttl =_lesserSpTtl.Value;
            _lesserSpiritualSe.m_healthOverTime =_lesserSpHot.Value;
            _lesserSpiritualSe.m_healthRegenMultiplier = _godsregen.Value; 
            _lesserSpiritualSe.m_healthOverTimeDuration =_lesserSphotDur.Value; 
            _lesserSpiritualSe.m_healthOverTimeInterval =_lesserspHotInt.Value;
            _lesserSpiritualSe.m_healthOverTimeTicks =_lesserSphoTticks.Value;
            _lesserSpiritualSe.m_healthOverTimeTimer =_lesserSphoTtimer.Value;
            _lesserSpiritualSe.m_healthOverTimeTickHP =_lesserSphoTtimeTickHp.Value;
            _seLesserSpiritual = new CustomStatusEffect(_lesserSpiritualSe, true);
            ItemManager.Instance.AddStatusEffect(_seLesserSpiritual);
        }
        private void LesserStaminaSe()
        {
            _lesserstamSe.name = "Lesser Stamina Elixir";
            _lesserstamSe.m_name = "Lesser Stamina Elixir";
            _lesserstamSe.m_staminaRegenMultiplier =_lesserstamregen.Value;
            _lesserstamSe.m_staminaOverTime =_lesserstaminaOt.Value;
            _lesserstamSe.m_staminaDrainPerSec =_lesserstaminaDps.Value;
            _lesserstamSe.m_jumpStaminaUseModifier =_lesserstamjump.Value;
            _lesserstamSe.m_runStaminaDrainModifier =_lesserstamrun.Value;
            _lesserstamSe.ModifyStaminaRegen(ref _lesserstamSe.m_staminaRegenMultiplier);
            _lesserstamSe.ModifyJumpStaminaUsage(Player.m_localPlayer.m_jumpStaminaUsage ,ref _lesserstamSe.m_jumpStaminaUseModifier);
            _lesserstamSe.ModifyRunStaminaDrain(Player.m_localPlayer.m_runStaminaDrain, ref _lesserstamSe.m_runStaminaDrainModifier);
            _lesserstamSe.m_icon = _lesserstam.ItemDrop.m_itemData.m_shared.m_icons.FirstOrDefault();
            _lesserstamSe.m_tooltip = "A boost of stamina over time";
            _lesserstamSe.m_cooldownIcon = true;
            _lesserstamSe.m_cooldown =_lesserstamcooldown.Value;
            _lesserstamSe.m_ttl =_lesserstamTtl.Value;
            _seLesserStam = new CustomStatusEffect(_lesserstamSe, true);
            ItemManager.Instance.AddStatusEffect(_seLesserStam);
            
        }
        
    }
}