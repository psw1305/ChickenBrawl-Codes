
namespace CKB.Database
{
    public class DataContainer<T> : TrackableValue<T>
    {
        public override T Value 
        {
            get
            {
                if (_alwaysLoad)
                {
                    return ES3.Load(_key, _defaultValue);
                }

                if (!_wasLoad)
                {
                    Load();
                    _wasLoad = true;
                }

                return _value;
            }
            set
            {
                base.Value = value;
                _value = value;

                if (_alwaysSave)
                {
                    Save();
                }
            }
        }

        private T _value;

        private readonly string _key;
        private readonly T _defaultValue;
        private bool _wasLoad;

        private bool _alwaysLoad;
        private bool _alwaysSave;

        public DataContainer(string key, T defaultValue, bool alwaysLoad = true, bool alwaysSave = true)
            : base(defaultValue)
        {
            _key = key;
            _defaultValue = defaultValue;
            _alwaysLoad = alwaysLoad;
            _alwaysSave = alwaysSave;
        }

        public void Save() => ES3.Save(_key, _value);
        public void Load() => _value = ES3.Load(_key, _defaultValue);
        public void Clear() => ES3.DeleteKey(_key);
    }
}

