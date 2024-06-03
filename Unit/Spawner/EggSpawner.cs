using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class EggSpawner : Spawner
    {
        private void Awake()
        {
            EggSpawn = this;
        }

        protected override void OnGamePlay()
        {
            for (int i = 0; i < maxAmount; i++)
            {
                Spawn();
            }
        }

        public void Spawn()
        {
            if (GroupPlayer.Level > 200)
            {
                SpawnObject(GameData.Egg, RandomPosition(24));
            }
            else if (GroupPlayer.Level > 50)
            {
                SpawnObject(GameData.Egg, RandomPosition(17));
            }
            else
            {
                SpawnObject(GameData.Egg, RandomPosition(10));
            }
        }
    }
}
