using UnityEngine;
using CKB.Core;
using CKB.Utilities;

namespace CKB.Database
{
    public class GameDatabase : GameStateMachineUser, ILazy
    {
        public readonly DataContainer<int> PassedLevel = new("PassedLevel", 1);
        public readonly DataContainer<int> StartIncreaseLevel = new("StartIncreaseLevel", 1);
        public readonly DataContainer<int> SpeedUpIncreaseLevel = new("SpeedUpIncreaseLevel", 1);
        public readonly DataContainer<int> ItemUpgradeIncreaseLevel = new("ItemUpgradeIncreaseLevel", 1);

        public readonly TrackableValue<float> Money = new(value: 0, firstGet: () => PlayerPrefs.GetInt("Money"));

        public float TimeOfSceneLoad { get; private set; }
        public float TimeElapsedFromSceneLoad => Time.time - TimeOfSceneLoad;

        private void SaveMoney(float value)
        {
            PlayerPrefs.SetInt("Money", Money.Value.Round());
        }

        #region Unity Flow

        protected override void OnEnable()
        {
            base.OnEnable();

            Money.Changed += SaveMoney;
            TimeOfSceneLoad = Time.time;
        }

        protected override void OnDisable() 
        {
            base.OnDisable();

            Money.Changed -= SaveMoney;
        }

        private void OnApplicationQuit()
        {
            SaveMoney(Money.Value.Round());
        }

        #endregion

        public static void Clear()
        {
            ES3.Save("PassedLevel", 1);
            ES3.Save("StartIncreaseLevel", 1);
            ES3.Save("SpeedUpIncreaseLevel", 1);
            ES3.Save("ItemUpgradeIncreaseLevel", 1);

            PlayerPrefs.SetInt("Money", 0);
        }
    }
}
