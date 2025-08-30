using BGJ_2025_2.Game.Interactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private static readonly Vector2 _DefaultCursorSize = new(15, 15);
        private static readonly Vector2 _LargeCursorSize = new(35, 35);

        [SerializeField] private TextMeshProUGUI _topInteractionLabel;
        [SerializeField] private TextMeshProUGUI _bottomInteractionLabel;
        [SerializeField] private Image _cursor;
        [SerializeField] private Sprite _grabSprite;
        [SerializeField] private Sprite _pointSprite;
        [SerializeField] private TextMeshProUGUI _dayLabel;
        [SerializeField] private Slider _dayProgressBar;
        [SerializeField] private TextMeshProUGUI _dayProgressLabel;
        [SerializeField] private GameObject _map;
        [SerializeField] private GameObject _taskDescriptions;
        private IInteractable _previousPlayerInteractable;
        private IInteractable _playerInteractable;
        private int _previousDay = -1;
        private int _previousProgressPercentage = -1;


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
            _map.SetActive(false);
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

                // Felvehetõ / letehetõ / eldobható
                if (_playerInteractable.IsGrabbable)
                {
                    // Az elõ frame-nél még nem olyan volt
                    if (_previousPlayerInteractable == null)
                    {
                        _cursor.sprite = _grabSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
                    }
                }
                else
                {
                    // Az elõ frame-nél még olyan volt
                    if (_previousPlayerInteractable == null)
                    {
                        _cursor.sprite = null;
                        _cursor.rectTransform.sizeDelta = _DefaultCursorSize;
                    }
                    else if (_previousPlayerInteractable.IsUsable)
                    {
                        _cursor.sprite = _pointSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
                    }
                }

                // Használható tárgy
                if (_playerInteractable.IsUsable)
                {
                    // Az elõ frame-nél még nem olyan volt
                    if (_previousPlayerInteractable == null)
                    {
                        _cursor.sprite = _pointSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
                    }
                }
                else
                {
                    // Az elõ frame-nél még olyan volt
                    if (_previousPlayerInteractable == null)
                    {
                        _cursor.sprite = null;
                        _cursor.rectTransform.sizeDelta = _DefaultCursorSize;
                    }
                    else if (_previousPlayerInteractable.IsGrabbable)
                    {
                        _cursor.sprite = _grabSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
                    }
                }

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
                    // A kurzort is frissíteni kell
                    if (_previousPlayerInteractable == null)
                    {
                        _bottomInteractionLabel.gameObject.SetActive(true);

                        _cursor.sprite = _pointSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
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

                    _cursor.sprite = null;
                    _cursor.rectTransform.sizeDelta = _DefaultCursorSize;
                }
            }

            _previousPlayerInteractable = _playerInteractable;

            if (_previousDay < 0 || _previousDay != Game.Day)
            {
                _previousDay = Game.Day;
                _dayLabel.SetText($"Day {_previousDay}");
            }

            if (_previousProgressPercentage < 0 || _previousProgressPercentage != Game.ProgressPercentage)
            {
                _previousProgressPercentage = Game.ProgressPercentage;

                _dayProgressBar.value = Game.Progress;
                _dayProgressLabel.SetText($"{_previousProgressPercentage}%");
            }
        }

        public override void Cancel()
        {
            if (_map.activeSelf)
            {
                _map.SetActive(false);
            }
        }

        public void OpenMap()
        {
            _map.SetActive(true);
        }

        public void CloseMap()
        {
            _map.SetActive(false);
        }

        public void ToggleMap()
        {
            _map.SetActive(!_map.activeSelf);
        }

        public void OpenTaskDescriptions()
        {
            _taskDescriptions.SetActive(true);
        }

        public void CloseTaskDescriptions()
        {
            _taskDescriptions.SetActive(false);
        }

        public void ToggleTaskDescriptions()
        {
            _taskDescriptions.SetActive(!_taskDescriptions.activeSelf);
        }
    }
}