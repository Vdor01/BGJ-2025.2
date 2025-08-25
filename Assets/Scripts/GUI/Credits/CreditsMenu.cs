using TMPro;
using UnityEngine;

namespace BGJ_2025_2.GUI.Credits
{
    /// <summary>
    /// A kredit men�ben vannak felt�ntetve a k�sz�t�k, a felhaszn�lt szoftverek �s eszk�z�k.
    /// </summary>
    /// <see cref="Menu"/>
    [AddComponentMenu("BGJ 2025.2/GUI/Credits/Credits menu")]
    public class CreditsMenu : Menu
    {
        // Fields
        [SerializeField] private TextMeshProUGUI _versionLabel;


        // Methods
        private void Start()
        {
            SetUp();
        }

        public override void SetUp()
        {
            _versionLabel.SetText($"V. {Application.version}");
        }
    }
}