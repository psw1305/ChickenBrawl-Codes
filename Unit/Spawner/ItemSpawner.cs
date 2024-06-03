using static CKB.Utilities.CommonGameplayFacade;

namespace CKB
{
    public class ItemSpawner : Spawner
    {
        private void Awake()
        {
            ItemSpawn = this;
        }

        protected override void OnGamePlay()
        {
            for (int i = 0; i < maxAmount; i++)
            {
                if (i < 7)
                {
                    Spawn(0);
                }
                else
                {
                    Spawn(1);
                }
            }
        }

        public void Spawn(int index)
        {
            SpawnObject(GameData.Items[index], RandomPosition(24));
        }
    }
}
