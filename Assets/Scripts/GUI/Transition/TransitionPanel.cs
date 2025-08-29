using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BGJ_2025_2.GUI
{
    [AddComponentMenu("BGJ 2025.2/GUI/Transition/Transition panel")]
    public class TransitionPanel : MonoBehaviour
    {
        // Fields
        private static readonly Color _DefaultBackgroundColor = Color.black;
        private static readonly Color _DefaultLabelColor = Color.white;
        private static readonly Color _TransparentColor = new(0f, 0f, 0f, 0f);
        private const float _DefaultBackgroundTransitionDuration = 1.5f;
        private const float _DefaultLabelTransitionDuration = 1.5f;

        [SerializeField] private GUIManager _gui;

        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Color _backgroundColor = _DefaultBackgroundColor;
        [SerializeField] private Color _labelColor = _DefaultLabelColor;
        [SerializeField] private float _backgroundTransitionDuration = _DefaultBackgroundTransitionDuration;
        [SerializeField] private float _labelTransitionDuration = _DefaultLabelTransitionDuration;
#if UNITY_EDITOR
        [Header("Development options")]
        [SerializeField] private bool _skip;
#endif

        // Properties
        public GUIManager GUI => _gui;

        public Image Background => _background;
        public TextMeshProUGUI Label => _label;


        // Methods
        private void Start()
        {
            _label.gameObject.SetActive(false);
        }

        public void TransitionIn(Action actionAfter = null, string text = null)
        {
#if UNITY_EDITOR
            if (_skip)
            {
                actionAfter?.Invoke();
                gameObject.SetActive(false);

                return;
            }
#endif
            Enable();
            StopAllCoroutines();

            StartCoroutine(TransitionInCoroutine(actionAfter, text));
        }

        public void TransitionOut(Action actionAfter = null, string text = null)
        {
#if UNITY_EDITOR
            if (_skip)
            {
                actionAfter?.Invoke();
                gameObject.SetActive(false);

                return;
            }
#endif
            Enable();
            StopAllCoroutines();

            StartCoroutine(TransitionOutCoroutine(() =>
            {
                actionAfter?.Invoke();

                Disable();
            }, text));
        }

        public void TransitionInAndOut(Action actionBetween = null, Action actionAfter = null, string text = null)
        {
            TransitionIn(() =>
            {
                actionBetween?.Invoke();

                TransitionOut(actionAfter, text);
            }, text);
        }

        private IEnumerator TransitionInCoroutine(Action actionAfter = null, string text = null)
        {
            float elapsedTime = 0f;

            _background.color = _TransparentColor;
            while (elapsedTime < _backgroundTransitionDuration)
            {
                _background.color = Color.Lerp(_TransparentColor, _backgroundColor, elapsedTime);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            if (text != null)
            {
                _label.SetText(text);
                _label.color = _TransparentColor;
                _label.gameObject.SetActive(true);

                elapsedTime = 0f;
                while (elapsedTime < _labelTransitionDuration)
                {
                    _label.color = Color.Lerp(_TransparentColor, _labelColor, elapsedTime);

                    elapsedTime += Time.deltaTime;

                    yield return null;
                }
            }

            actionAfter?.Invoke();
        }

        private IEnumerator TransitionOutCoroutine(Action actionAfter = null, string text = null)
        {
            float elapsedTime;

            if (text != null)
            {
                _label.SetText(text);
                _label.color = _labelColor;
                _label.gameObject.SetActive(true);

                elapsedTime = 0f;
                while (elapsedTime < _labelTransitionDuration)
                {
                    _label.color = Color.Lerp(_labelColor, _TransparentColor, elapsedTime);

                    elapsedTime += Time.deltaTime;

                    yield return null;
                }
            }

            elapsedTime = 0f;

            _background.color = _backgroundColor;
            while (elapsedTime < _backgroundTransitionDuration)
            {
                _background.color = Color.Lerp(_backgroundColor, _TransparentColor, elapsedTime);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            actionAfter?.Invoke();
        }

        public void Enable()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }

        public void Disable()
        {
            StopAllCoroutines();

            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }

            if (_label.gameObject.activeSelf)
            {
                _label.gameObject.SetActive(false);
            }
        }
    }
}