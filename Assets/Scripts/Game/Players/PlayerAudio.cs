using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player audio")]
    public class PlayerAudio : PlayerComponent
    {
        // Fields
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _eatingClip;
        [SerializeField] private AudioClip _alertClip;


        // Properties
        public AudioSource Source => _source;


        // Methods
        public void Eat()
        {
            _source.clip = _eatingClip;
            _source.Play();
        }

        public void Alert()
        {
            _source.clip = _alertClip;
            _source.Play();
        }
    }
}