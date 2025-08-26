using BGJ_2025_2.Game.Interactions;
using TMPro;
using UnityEngine;

namespace BGJ_2025_2.GUI.Overlay
{
    /// <summary>
    /// A játék közben folyamatosan képernyõn lévõ felület.
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

        // Elrejti a kurzor feletti és alatti labeleket
        public override void SetUp()
        {
            _topInteractionLabel.gameObject.SetActive(false);
            _bottomInteractionLabel.gameObject.SetActive(false);
        }

        // Beállítja a kurzor feletti és alatti labeleket, ha a játékos épp valamilyen interaktálható tárgyat néz.
        public override void Refresh()
        {
            _playerInteractable = Player.Interaction.Interactable;

            // Épp valamilyen interaktálható tárgyat néz a játékos
            if (_playerInteractable != null)
            {
                // Az elõzõ frame-nél még nem interaktálható dolgot nézett, ezért aktiválni kell a kurzor feletti labelt
                if (_previousPlayerInteractable == null)
                {
                    _topInteractionLabel.gameObject.SetActive(true);
                }

                // Kurzor feletti label szövegének beállítása a tárgy nevére
                _topInteractionLabel.SetText(_playerInteractable.Name);

                // Tartozik az interaktálható tárgyhoz leírás
                if (_playerInteractable.IsDescriptable)
                {
                    // Az elõzõ frame-nél még nem interaktálható dolgot nézett, ezért aktiválni kell a kurzor alatti labelt
                    if (_previousPlayerInteractable == null)
                    {
                        _bottomInteractionLabel.gameObject.SetActive(true);
                    }

                    // Kurzor alatti label beállítása a tárgy leírására
                    // Ha használható és van használati leírása is, akkor az új sorba alá kerül
                    _bottomInteractionLabel.SetText(
                        _playerInteractable.IsUsable && _playerInteractable.Usable.Usage != null
                        ? $"{_playerInteractable.Descriptable.Description}\n{_playerInteractable.Usable.Usage}"
                        : _playerInteractable.Descriptable.Description);
                }
                else if (_playerInteractable.IsUsable)
                {
                    // Az elõzõ frame-nél még nem interaktálható dolgot nézett, ezért aktiválni kell a kurzor alatti labelt
                    if (_previousPlayerInteractable == null)
                    {
                        _bottomInteractionLabel.gameObject.SetActive(true);
                    }

                    // Ha van használati leírása is, akkor jelenjen meg
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