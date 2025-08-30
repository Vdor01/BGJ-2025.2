using BGJ_2025_2.Game.Interactions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        // Elrejti a kurzor feletti �s alatti labeleket
        public override void SetUp()
        {
            _topInteractionLabel.gameObject.SetActive(false);
            _bottomInteractionLabel.gameObject.SetActive(false);
            _map.SetActive(false);
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

                // Felvehet� / letehet� / eldobhat�
                if (_playerInteractable.IsGrabbable)
                {
                    // Az el� frame-n�l m�g nem olyan volt
                    if (_previousPlayerInteractable == null)
                    {
                        _cursor.sprite = _grabSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
                    }
                }
                else
                {
                    // Az el� frame-n�l m�g olyan volt
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

                // Haszn�lhat� t�rgy
                if (_playerInteractable.IsUsable)
                {
                    // Az el� frame-n�l m�g nem olyan volt
                    if (_previousPlayerInteractable == null)
                    {
                        _cursor.sprite = _pointSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
                    }
                }
                else
                {
                    // Az el� frame-n�l m�g olyan volt
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
                    // A kurzort is friss�teni kell
                    if (_previousPlayerInteractable == null)
                    {
                        _bottomInteractionLabel.gameObject.SetActive(true);

                        _cursor.sprite = _pointSprite;
                        _cursor.rectTransform.sizeDelta = _LargeCursorSize;
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