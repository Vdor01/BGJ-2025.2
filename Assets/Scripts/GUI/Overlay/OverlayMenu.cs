using BGJ_2025_2.Game.Interactions;
using TMPro;
using UnityEngine;

namespace BGJ_2025_2.GUI.Overlay
{
    /// <summary>
    /// A j�t�k k�zben folyamatosan k�perny�n l�v� fel�let.
    /// </summary>
    /// <seealso cref="Menu"/>
    [AddComponentMenu("BGJ 2025.2/GUI/Overlay/Overlay menu")]
    public class OverlayMenu : Menu
    {
        // Fields
        [SerializeField] private TextMeshProUGUI _topInteractionLabel;
        [SerializeField] private TextMeshProUGUI _bottomInteractionLabel;
        IInteractable _previousPlayerInteractable;
        IInteractable _playerInteractable;


        // Methods
        private void Start()
        {
            SetUp();
        }

        private void Update()
        {
            Refresh();
        }

        // Elrejti a kurzor feletti �s alatti labeleket
        public override void SetUp()
        {
            _topInteractionLabel.gameObject.SetActive(false);
            _bottomInteractionLabel.gameObject.SetActive(false);
        }

        // Be�ll�tja a kurzor feletti �s alatti labeleket, ha a j�t�kos �pp valamilyen interakt�lhat� t�rgyat n�z.
        public override void Refresh()
        {
            _playerInteractable = Player.Interaction.Interactable;

            // �pp valamilyen interakt�lhat� t�rgyat n�z a j�t�kos
            if (_playerInteractable != null)
            {
                // Az el�z� frame-n�l m�g nem interakt�lhat� dolgot n�zett, ez�rt aktiv�lni kell a kurzor feletti labelt
                if (_previousPlayerInteractable == null)
                {
                    _topInteractionLabel.gameObject.SetActive(true);
                }

                // Kurzor feletti label sz�veg�nek be�ll�t�sa a t�rgy nev�re
                _topInteractionLabel.SetText(_playerInteractable.Name);

                // Tartozik az interakt�lhat� t�rgyhoz le�r�s
                if (_playerInteractable.IsDescriptable)
                {
                    // Az el�z� frame-n�l m�g nem interakt�lhat� dolgot n�zett, ez�rt aktiv�lni kell a kurzor alatti labelt
                    if (_previousPlayerInteractable == null)
                    {
                        _bottomInteractionLabel.gameObject.SetActive(true);
                    }

                    // Kurzor alatti label be�ll�t�sa a t�rgy le�r�s�ra
                    // Ha haszn�lhat� �s van haszn�lati le�r�sa is, akkor az �j sorba al� ker�l
                    _bottomInteractionLabel.SetText(
                        _playerInteractable.IsUsable && _playerInteractable.Usable.Usage != null
                        ? $"{_playerInteractable.Descriptable.Description}\n{_playerInteractable.Usable.Usage}"
                        : _playerInteractable.Descriptable.Description);
                }
                else if (_playerInteractable.IsUsable)
                {
                    // Az el�z� frame-n�l m�g nem interakt�lhat� dolgot n�zett, ez�rt aktiv�lni kell a kurzor alatti labelt
                    if (_previousPlayerInteractable == null)
                    {
                        _bottomInteractionLabel.gameObject.SetActive(true);
                    }

                    // Ha van haszn�lati le�r�sa is, akkor jelenjen meg
                    if (_playerInteractable.Usable.Usage != null)
                    {
                        _bottomInteractionLabel.SetText(_playerInteractable.Usable.Usage);
                    }
                }
            }
            else
            {
                if (_previousPlayerInteractable != null)
                {
                    _topInteractionLabel.gameObject.SetActive(false);
                    _bottomInteractionLabel.gameObject.SetActive(false);
                }
            }

            _previousPlayerInteractable = _playerInteractable;
        }
    }
}