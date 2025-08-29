using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace BGJ_2025_2.Game.Leaderboards
{
    [AddComponentMenu("BGJ 2025.2/Game/Leaderboards/Leaderboard handler")]
    public class LeaderboardHandler : MonoBehaviour
    {
        // Fields
        private const string _QueryURL = "https://bgj20252.eu.pythonanywhere.com/getleaderboard";
        private const string _SubmitURL = "https://bgj20252.eu.pythonanywhere.com/submit";
        private const string _ClearURL = "https://bgj20252.eu.pythonanywhere.com/deleteall";
        private const int _MaxCapacity = 16;

        [SerializeField] private GameManager _game;

        private readonly List<LeaderboardEntry> _entries = new(_MaxCapacity);


        // Properties
        public GameManager Game => _game;

        public int Count => _entries.Count;
        public List<LeaderboardEntry> Entries => _entries;
        public LeaderboardEntry this[int index] => _entries[index];


        // Methods
        public void Query(Action actionAfter = null)
        {
            StartCoroutine(QueryCoroutine(actionAfter));
        }

        private IEnumerator QueryCoroutine(Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log("Querying leaderboard...");
#endif
            UnityWebRequest request = UnityWebRequest.Get(_QueryURL);

            yield return request.SendWebRequest();

            if (request.error != null)
            {
#if UNITY_EDITOR
                LogWarning(request.error);
#endif
                string response = request.downloadHandler.text;
            }
            else
            {
#if UNITY_EDITOR
                Log("Successfully queried leaderboard! :D");
#endif
                actionAfter?.Invoke();
            }
        }

        public void Submit(string content, Action actionAfter = null)
        {
            StartCoroutine(SubmitCoroutine(content, actionAfter));
        }

        private IEnumerator SubmitCoroutine(string content, Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log("Submitting to leaderboard...");
#endif
            UnityWebRequest request = UnityWebRequest.Get($"{_SubmitURL}?{content}");

            yield return request.SendWebRequest();

            if (request.error != null)
            {
#if UNITY_EDITOR
                LogWarning(request.error);
#endif
            }
            else
            {
#if UNITY_EDITOR
                Log("Successfully submitted to leaderboard! :D");
#endif
                actionAfter?.Invoke();
            }
        }

        public void Clear(Action actionAfter = null)
        {
            StartCoroutine(ClearCoroutine(actionAfter));
        }

        private IEnumerator ClearCoroutine(Action actionAfter = null)
        {
#if UNITY_EDITOR
            Log("Clearing leaderboard...");
#endif
            UnityWebRequest request = UnityWebRequest.Get($"{_ClearURL}");

            yield return request.SendWebRequest();

            if (request.error != null)
            {
#if UNITY_EDITOR
                LogWarning(request.error);
#endif
            }
            else
            {
#if UNITY_EDITOR
                Log("Successfully cleared! :D");
#endif
                actionAfter?.Invoke();
            }
        }

        private static void Log(string message)
        {
            Debug.Log($"[LEADERBOARD] {message}");
        }

        private static void LogWarning(string message)
        {
            Debug.LogWarning($"[LEADERBOARD] {message}");
        }
    }
}