using BGJ_2025_2.Game.Players;
using TMPro;
using UnityEngine;

namespace BGJ_2025_2.GUI.Leaderboard
{
    [AddComponentMenu("BGJ 2025.2/GUI/Leaderboard/Leaderboard menu")]
    public class LeaderboardMenu : Menu
    {
        // Fields
        private const string _MinPlaceholder = "#MIN#";
        private const string _MaxPlaceholder = "#MAX#";

        [SerializeField] private TextMeshProUGUI _loadingLabel;
        [SerializeField] private TextMeshProUGUI _emptyLabel;
        [SerializeField] private TextMeshProUGUI _warningLabel;
        [SerializeField] private LeaderboardMenuEntry _entryPrefab;
        [SerializeField] private Transform _entryGroup;
        [SerializeField] private TMP_InputField _nameInput;


        // Methods
        private void Start()
        {
            SetUp();
        }

        public override void Open()
        {
            Refresh();

            base.Open();
        }

        public override void SetUp()
        {
            _nameInput.characterLimit = PlayerData.MaxNameLength;
            _warningLabel.SetText(_warningLabel.text
                .Replace(_MinPlaceholder, PlayerData.MinNameLength.ToString())
                .Replace(_MaxPlaceholder, PlayerData.MaxNameLength.ToString()));

            _warningLabel.gameObject.SetActive(false);
        }

        public override void Refresh()
        {
            _loadingLabel.gameObject.SetActive(true);
            _emptyLabel.gameObject.SetActive(false);

            int entryCount = _entryGroup.childCount;
            for (int i = 0; i < entryCount; ++i)
            {
                Destroy(_entryGroup.GetChild(i).gameObject);
            }

            Game.Leaderboards.Query(() =>
            {
                if (Game.Leaderboards.Count == 0)
                {
                    _emptyLabel.gameObject.SetActive(true);
                }
                else
                {
                    for (int i = 0; i < Game.Leaderboards.Count; ++i)
                    {
                        LeaderboardMenuEntry entry = Instantiate(_entryPrefab, _entryGroup);
                        entry.SetUp(i + 1, Game.Leaderboards[i]);
                    }
                }

                _loadingLabel.gameObject.SetActive(false);

                return;
            });

            _loadingLabel.gameObject.SetActive(false);
            _emptyLabel.gameObject.SetActive(true);
        }

        public void SetName()
        {
            Player.Data.Name = _nameInput.text;

            _warningLabel.gameObject.SetActive(Player.Data.Name == null && _nameInput.text.Length > 0);
        }

        public override void Cancel()
        {
            if (_nameInput.isFocused) return;

            Close();
            _gui.MainMenu.Open();
        }
    }
}