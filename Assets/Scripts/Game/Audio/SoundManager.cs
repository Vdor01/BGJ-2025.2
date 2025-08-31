using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGJ_2025_2.Game.Audio
{
    [AddComponentMenu("BGJ 2025.2/Game/Audio/Sound manager")]
    public class SoundManager : MonoBehaviour
    {
        private const float _SegmentLength = 60f / 112f * 48f;

        public AudioSource audioSource;
        public List<AudioClip> songs;

        // Rule system: for each song index, list of possible next song indices
        private Dictionary<int, int[]> songRules = new Dictionary<int, int[]>
    {
        { 1, new int[] { 2, 3 } },
        { 2, new int[] { 3, 4 } },
        { 3, new int[] { 2, 4 } },
        { 4, new int[] { 5, 6 } },
        { 5, new int[] { 7, 1, 2, 3 } },
        { 6, new int[] { 7, 1, 2, 3 } },
        { 7, new int[] { 1, 2, 3 } }
    };

        private int currentSongIndex;

        void Start()
        {
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            // Start with song 1 (index = 0 in your songs list)
            PlaySong(1);
        }

        /*
        void Update()
        {
            if (!audioSource.isPlaying && songs.Count > 0)
            {
                PlayNextSong();
            }
        }
        */

        void PlaySong(int songNumber)
        {
            StartCoroutine(PlaySongCoroutine(songNumber));
        }

        private IEnumerator PlaySongCoroutine(int songNumber)
        {
            if (songNumber < 1 || songNumber > songs.Count)
            {
                Debug.LogWarning("Invalid song number: " + songNumber);
                yield break;
            }

            currentSongIndex = songNumber;
            audioSource.clip = songs[songNumber - 1]; // list is 0-based
            audioSource.Play();

            float elapsedTime = 0f;
            while (elapsedTime < _SegmentLength)
            {
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            PlayNextSong();
        }

        void PlayNextSong()
        {
            if (!songRules.ContainsKey(currentSongIndex))
            {
                Debug.LogWarning("No rules defined for song " + currentSongIndex);
                return;
            }

            int[] possibleNext = songRules[currentSongIndex];
            int nextSong = possibleNext[Random.Range(0, possibleNext.Length)];
            StopAllCoroutines();
            PlaySong(nextSong);
        }
    }
}