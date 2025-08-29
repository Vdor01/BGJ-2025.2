using TMPro;
using UnityEngine;

namespace BGJ_2025_2.GUI.Credits
{
    /// <summary>
    /// A kredit menüben vannak feltüntetve a készítõk, a felhasznált szoftverek és eszközök.
    /// </summary>
    /// <see cref="Menu"/>
    [AddComponentMenu("BGJ 2025.2/GUI/Credits/Credits menu")]
    public class CreditsMenu : Menu
    {
        // Fields
        private const string _GameTitlePlaceholder = "#GAME_TITLE#";
        private const string _EngineVersionPlaceholder = "#ENGINE_VERSION#";

        [SerializeField] private TextMeshProUGUI _developersTitleLabel;
        [SerializeField] private TextMeshProUGUI _engineContentLabel;
        [SerializeField] private TextMeshProUGUI _versionLabel;


        // Methods
        private void Start()
        {
            SetUp();
        }

        public override void SetUp()
        {
            _developersTitleLabel.SetText(_developersTitleLabel.text.Replace(_GameTitlePlaceholder, Application.productName));
            _engineContentLabel.SetText(_engineContentLabel.text.Replace(_EngineVersionPlaceholder, Application.unityVersion));
            _versionLabel.SetText($"V. {Application.version}");
        }

        public override void Cancel()
        {
            Close();

            _gui.MainMenu.Open();
        }
    }
}