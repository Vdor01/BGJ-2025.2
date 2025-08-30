using UnityEditor;
using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player data")]
    public class PlayerData : PlayerComponent
    {
        // Fields
        public const int MinNameLength = 3;
        public const int MaxNameLength = 10;
        private const string _NameKey = "name";

        private string _name;
        private int _days;
        private int _tasks;
        private int _cookies;


        // Properties
        public string Name
        {
            get => _name;
            set
            {
                string trimmedName = value.Trim();
                _name = trimmedName.Length < MinNameLength ? null : trimmedName[..Mathf.Min(trimmedName.Length, MaxNameLength)];

                if (_name == null)
                {
                    PlayerPrefs.DeleteKey(_NameKey);
                }
                else
                {
                    PlayerPrefs.SetString(_NameKey, _name);
                }
            }
        }
        public int Days
        {
            get => _days;
            set => _days = Mathf.Clamp(value, 0, int.MaxValue);
        }
        public int Tasks
        {
            get => _tasks;
            set => _tasks = Mathf.Clamp(value, 0, int.MaxValue);
        }
        public int Cookies
        {
            get => _cookies;
            set => _cookies = Mathf.Clamp(value, 0, int.MaxValue);
        }


        // Methods
        private void Awake()
        {
            _name = PlayerPrefs.GetString(_NameKey, null);
        }

        public void Reload()
        {
            _days = 0;
            _tasks = 0;
            _cookies = 0;
        }

        public void Clear()
        {
            PlayerPrefs.DeleteKey(_NameKey);
        }

        public string ToURL()
        {
            return $"name={_name}&score={_cookies}";
        }

        public void FromJson(string json)
        {

        }

#if UNITY_EDITOR
        [MenuItem("BGJ 2025.2/Clear player data")]
        public static void DebugClear()
        {
            PlayerPrefs.DeleteKey(_NameKey);
            Debug.Log("Player data cleared");
        }
#endif
    }
}