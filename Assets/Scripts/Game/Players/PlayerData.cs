using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player data")]
    public class PlayerData : PlayerComponent
    {
        // Fields
        public const int _MinNameLength = 3;

        private string _name;
        private int _days;
        private int _cookies;


        // Properties
        public string Name
        {
            get => _name;
            set
            {
                string trimmedName = value.Trim();
                _name = trimmedName.Length < _MinNameLength ? null : trimmedName;
            }
        }
        public int Days
        {
            get => _days;
            set => _days = Mathf.Clamp(value, 0, int.MaxValue);
        }

        public int Cookies
        {
            get => _cookies;
            set => _cookies = Mathf.Clamp(value, 0, int.MaxValue);
        }


        // Methods
        public void Reload()
        {
            _days = 0;
            _cookies = 0;
        }

        public string ToURL()
        {
            return $"name={_name}&score={_cookies}";
        }

        public void FromJson(string json)
        {

        }
    }
}