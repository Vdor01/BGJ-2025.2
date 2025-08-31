using BGJ_2025_2.Game.Interactions;
using BGJ_2025_2.Game.Levels;
using BGJ_2025_2.Game.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Cookie jar")]
    public class CookieJar : MonoBehaviour, IUsable
    {
        // Fields
        public const int DefaultCookieCount = 100;
        private const float _VerticalAnchorOffset = 0.0015f;

        [SerializeField] private Office _office;

        [SerializeField] private Cookie _cookiePrefab;
        [SerializeField] private Transform[] _cookieAnchors;
        [SerializeField] private Vector3[] _rotations;
        private readonly List<Cookie> _cookies = new(Mathf.NextPowerOfTwo(DefaultCookieCount));


        // Properties
        public Office Office => _office;

        public string Name => "Cookie Jar";
        public string Usage => $"Take a cookie ({_cookies.Count} left)";
        public int CookieCount => _cookies.Count;
        public bool IsEmpty => _cookies.Count == 0;
        public bool IsNotEmpty => !IsEmpty;
        public float Fullness => _cookies.Count / (float)DefaultCookieCount;
        public float Emptiness => 1f - Fullness;


        // Methods
        private void Start()
        {
            FillUp();
        }

        public void Use()
        {
            if (IsEmpty) return;

            TakeOut();

            ++_office.Player.Data.Cookies;
            _office.Player.Audio.Eat();
            _office.Boss.NotifyFromCookieJar();

            if (IsEmpty && _office.Game.Tasks.FinishedTaskCount == TaskHandler.MinTaskCount)
            {
                _office.Game.EndDay();
            }
        }

        public void EmptyOut()
        {
            foreach (Cookie cookie in _cookies)
            {
                Destroy(cookie.gameObject);
            }
            _cookies.Clear();
        }

        public void FillUp(int cookieCount = DefaultCookieCount)
        {
            EmptyOut();

            AddSome(cookieCount);
        }

        public void AddSome(int cookieCount)
        {
            for (int i = 0; i < cookieCount; ++i)
            {
                Cookie cookie = Instantiate(
                    _cookiePrefab,
                    _cookieAnchors[i % _cookieAnchors.Length].position + i * _VerticalAnchorOffset * Vector3.down,
                    Quaternion.Euler(_rotations[i % _rotations.Length]),
                    transform);
                _cookies.Add(cookie);
            }
        }

        public void TakeOut(int cookieCount = 1)
        {
            int cookieIndex;
            Cookie takenOutCookie;
            for (int i = 0; i < cookieCount && _cookies.Count > 0; ++i)
            {
                cookieIndex = Random.Range(0, _cookies.Count);
                takenOutCookie = _cookies[cookieIndex];


                _cookies.RemoveAt(cookieIndex);
                Destroy(takenOutCookie.gameObject);
            }
        }

        public void Reload()
        {
            AddSome(DefaultCookieCount - _cookies.Count);
        }
    }
}