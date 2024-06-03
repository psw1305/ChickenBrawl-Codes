using Sirenix.OdinInspector;
using UnityEngine;
using CKB.Core;
using CKB.Utilities;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB.Gameplay
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "SO/GameSettings")]
    public class GameSettings : SingletonData<GameSettings>
    {
        [Header("Common")]
        [TabGroup("Common")] public Material[] groundMaterials;
        [TabGroup("Common")] public int ChickMax = 100;
        [TabGroup("Common")] public int PointChick = 3;
        [TabGroup("Common")] public int PointChicken = 5;
        [TabGroup("Common")] public Color PlayerRankColor = new Color32(96, 44, 44, 255);

        public Material LevelFloorMaterial()
        {
            int result = (DB.PassedLevel.Value - 1) % 10;
            return groundMaterials[result];
        }

        [Header("Level")]
        [TabGroup("Level")] public int bossLevelBase = 1000;
        [TabGroup("Level")] public int bossLevelIncrease = 200;

        private int CalculateBossLevel(int level)
        {
            return bossLevelBase + (bossLevelIncrease * (level - 1));
        }

        public int BossLevel => CalculateBossLevel(DB.PassedLevel.Value);

        [Header("Upgrades")]
        [TabGroup("Upgrades")][SerializeField] private float upgradeInitPrice = 800;
        [TabGroup("Upgrades")] public int MaxLevelUpgrade = 9999;
        [TabGroup("Upgrades")] public float SpeedIncreaseRate = 0.005f;
        [TabGroup("Upgrades")] public float MagnetIncreaseRate = 0.005f;
        [TabGroup("Upgrades")] public float BoostInitRewards = 900;

        private float CalculateUpgradePrice(float level)
        {
            var result = upgradeInitPrice * (level - 1);
            return result.Round();
        }

        public float StartLevelUpgradePrice => CalculateUpgradePrice(DB.StartIncreaseLevel.Value);
        public float StartLevelNextUpgradePrice => CalculateUpgradePrice(DB.StartIncreaseLevel.Value + 1);

        public float SpeedUpUpgradePrice => CalculateUpgradePrice(DB.SpeedUpIncreaseLevel.Value);
        public float SpeedUpNextUpgradePrice => CalculateUpgradePrice(DB.SpeedUpIncreaseLevel.Value + 1);

        public float ItemUpgradePrice => CalculateUpgradePrice(DB.ItemUpgradeIncreaseLevel.Value);
        public float ItemNextUpgradePrice => CalculateUpgradePrice(DB.ItemUpgradeIncreaseLevel.Value + 1);

        public float BoostCoinRewards()
        {
            int sumLevels = DB.StartIncreaseLevel.Value + DB.SpeedUpIncreaseLevel.Value + DB.ItemUpgradeIncreaseLevel.Value - 3;
            var result = 900 + (sumLevels * 500);
            return result;
        }

        [Header("Sounds")]
        [TabGroup("Sounds")] public AudioClip UnitGrowClip;
        [TabGroup("Sounds")] public AudioClip UnitDeathClip;
        [TabGroup("Sounds")] public AudioClip UpgradeClip;

        [Header("Models")]
        [TabGroup("Models")] public GameObject Chick;
        [TabGroup("Models")] public GameObject Opponent;
        [TabGroup("Models")] public GameObject OpponentChick;
        [TabGroup("Models")] public GameObject Egg;
        [TabGroup("Models")] public GameObject[] Items;
        [TabGroup("Models")] public PoolType UnitDeathVFX;
    }
}
