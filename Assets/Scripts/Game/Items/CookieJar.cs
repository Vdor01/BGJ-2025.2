using BGJ_2025_2.Game.Interactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGJ_2025_2.Game.Items
{
    [AddComponentMenu("BGJ 2025.2/Game/Items/Cookie jar")]
    public class CookieJar : MonoBehaviour, IUsable
    {
        // Fields
        private const int _DefaultCookieCount = 50;

        [SerializeField] private Cookie _cookiePrefab;
        [SerializeField] private Transform _cookieAnchor;
        private readonly List<Cookie> _cookies = new(Mathf.NextPowerOfTwo(_DefaultCookieCount));


        // Properties
        public string Name => "Cookie Jar";
        public string Usage => $"Take a cookie ({_cookies.Count} left)";
        public int CookieCount => _cookies.Count;


        // Methods
        private void Start()
        {
            FillUp();
        }

        public void Use()
        {
            TakeOut();
        }

        public void EmptyOut()
        {
            foreach (Cookie cookie in _cookies)
            {
                _cookies.Remove(cookie);
                Destroy(cookie.gameObject);
            }
        }

        public void FillUp(int cookieCount = _DefaultCookieCount)
        {
            EmptyOut();

            AddSome(cookieCount);
        }

        public void AddSome(int cookieCount)
        {
            StartCoroutine(AddSomeCoroutine(cookieCount));
        }

        private IEnumerator AddSomeCoroutine(int cookieCount)
        {
            for (int i = 0; i < cookieCount; ++i)
            {
                Cookie cookie = Instantiate(_cookiePrefab, _cookieAnchor.position, Quaternion.identity, transform);
                _cookies.Add(cookie);

                yield return null;
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
    }
}