using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;


namespace SCP_076
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("Увеличивает скорость передвижения игрока ( 0 - 255% )")]
        public byte MovementBoost { get; set; } = 75;

        [Description("Разрешает SCP-914 перерабатывать Item076 в другой")]
        public bool CanUpgradeIn914 { get; set; } = false;

        [Description("Разрешает игроку выбрасывать Item076")]
        public bool CanDrop { get; set; } = false;
        
        [Description("Урон наносимый обычным ударом Item076")]
        public float MeleeDamage { get; set; } = 100f;
        
        [Description("Урон наносимый заряженным ударом Item076")]
        public float ChargeDamage { get; set; } = 500f;

        [Description("Указывает причину смерти от SCP-076")]
        public string DeathReason { get; set; } = "На теле обнаружены огромные резаные раны!";

        [Description("Указывает шанс спавна SCP-076 в начале раунда")]
        public float SpawnChance { get; set; } = 100f;

        [Description("Указывает требуемое минимальное количество игроков на сервере для автоспавна")]
        public uint MinPlayersForSpawn { get; set; } = 10;
        
        [Description("Минимальное количество живых SCP в раунде")]
        public byte MinScpUnits { get; set; } = 5;
        
        [Description("Список разрешенных на подбор предметов")]
        public List<ItemType> AllowedItemsList { get; set; } = new List<ItemType>()
        {
            ItemType.Medkit,
            ItemType.Painkillers,
            ItemType.Adrenaline,
            ItemType.SCP500,
            ItemType.ArmorLight,
            ItemType.ArmorCombat,
            ItemType.ArmorHeavy,
            ItemType.KeycardJanitor,
            ItemType.KeycardScientist,
            ItemType.KeycardResearchCoordinator,
            ItemType.KeycardZoneManager,
            ItemType.KeycardGuard,
            ItemType.KeycardMTFPrivate,
            ItemType.KeycardMTFOperative,
            ItemType.KeycardMTFCaptain,
            ItemType.KeycardChaosInsurgency,
            ItemType.KeycardContainmentEngineer,
            ItemType.KeycardFacilityManager,
            ItemType.KeycardO5,
        };

        public role076 Role076 { get; set; } = new role076();

        public item076 Item076 { get; set; } = new item076();
    }
}