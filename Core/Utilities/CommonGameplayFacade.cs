using CKB.Core;
using CKB.Database;
using CKB.Gameplay;
using UnityEngine;

namespace CKB.Utilities
{
    public static class CommonGameplayFacade
    {
        public static GameSettings GameData => GameSettings.Instance;
        public static CoreSettings CoreData => CoreSettings.Instance;

        public static GameStateMachine StateMachine
        {
            get
            {
                if (_stateMachine == null)
                {
                    _stateMachine = LazySugar.FindLazy<GameStateMachine>();
                }

                return _stateMachine;
            }
        }
        private static GameStateMachine _stateMachine;

        public static SceneLoader LoaderScene
        {
            get
            {
                if (_sceneLoader == null)
                {
                    _sceneLoader = LazySugar.FindLazy<SceneLoader>();
                }

                return _sceneLoader;
            }
        }
        private static SceneLoader _sceneLoader;

        public static GameDatabase DB
        {
            get
            {
                if (_db == null)
                {
                    _db = LazySugar.FindLazy<GameDatabase>();
                }

                return _db;
            }
        }
        private static GameDatabase _db;

        public static PoolManager ObjectPool
        {
            get
            {
                if (_poolManager == null)
                {
                    _poolManager = GameObject.FindObjectOfType<PoolManager>();
                }

                return _poolManager;
            }
            set
            {
                _poolManager = value;
            }
        }
        private static PoolManager _poolManager;

        public static GameProgress Progress
        {
            get
            {
                if (_gameProgress == null)
                {
                    _gameProgress = GameObject.FindObjectOfType<GameProgress>();
                }

                return _gameProgress;
            }
            set
            {
                _gameProgress = value;
            }
        }
        private static GameProgress _gameProgress;

        public static Ranking Rank
        {
            get
            {
                if (_ranking == null)
                {
                    _ranking = GameObject.FindObjectOfType<Ranking>();
                }

                return _ranking;
            }
            set
            {
                _ranking = value;
            }
        }
        private static Ranking _ranking;

        public static EggSpawner EggSpawn
        {
            get
            {
                if (_eggSpawner == null)
                {
                    _eggSpawner = GameObject.FindObjectOfType<EggSpawner>();
                }

                return _eggSpawner;
            }
            set
            {
                _eggSpawner = value;
            }
        }
        private static EggSpawner _eggSpawner;

        public static ItemSpawner ItemSpawn
        {
            get
            {
                if (_itemSpawner == null)
                {
                    _itemSpawner = GameObject.FindObjectOfType<ItemSpawner>();
                }

                return _itemSpawner;
            }
            set
            {
                _itemSpawner = value;
            }
        }
        private static ItemSpawner _itemSpawner;

        public static OpponentSpawner OpponentSpawn
        {
            get
            {
                if (_opponentSpawner == null)
                {
                    _opponentSpawner = GameObject.FindObjectOfType<OpponentSpawner>();
                }

                return _opponentSpawner;
            }
            set
            {
                _opponentSpawner = value;
            }
        }
        private static OpponentSpawner _opponentSpawner;

        public static PlayerGroup GroupPlayer
        {
            get
            {
                if (_playerGroup == null)
                {
                    _playerGroup = GameObject.FindObjectOfType<PlayerGroup>();
                }

                return _playerGroup;
            }
            set
            {
                _playerGroup = value;
            }
        }
        private static PlayerGroup _playerGroup;

        public static BossField BossBattle
        {
            get
            {
                if (_bossField == null)
                {
                    _bossField = GameObject.FindObjectOfType<BossField>();
                }

                return _bossField;
            }
            set
            {
                _bossField = value;
            }
        }
        private static BossField _bossField;

        public static BossFieldPlayer BossPlayer
        {
            get
            {
                if (_bossFieldPlayer == null)
                {
                    _bossFieldPlayer = GameObject.FindObjectOfType<BossFieldPlayer>();
                }

                return _bossFieldPlayer;
            }
            set
            {
                _bossFieldPlayer = value;
            }
        }
        private static BossFieldPlayer _bossFieldPlayer;

        public static Boss BossChicken
        {
            get
            {
                if (_boss == null)
                {
                    _boss = GameObject.FindObjectOfType<Boss>();
                }

                return _boss;
            }
            set
            {
                _boss = value;
            }
        }
        private static Boss _boss;

        public static AudioManager Audio
        {
            get
            {
                if (_audioManager == null)
                {
                    _audioManager = GameObject.FindObjectOfType<AudioManager>();
                }

                return _audioManager;
            }
            set
            {
                _audioManager = value;
            }
        }
        private static AudioManager _audioManager;
    }
}
