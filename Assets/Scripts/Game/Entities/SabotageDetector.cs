using BGJ_2025_2.Game.Levels;
using UnityEngine;

namespace BGJ_2025_2.Game.Entities
{
    [AddComponentMenu("BGJ 2025.2/Game/Entities/Sabotage detector")]
    public class SabotageDetector : MonoBehaviour
    {
        // Fields
        private const string _RoomTag = "Room";

        [SerializeField] Boss _boss;
        private bool _isTriggered;


        // Properties
        public Boss Boss => _boss;
        public bool IsTriggered => _isTriggered;


        // Methods
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_RoomTag))
            {
                _isTriggered = true;

                Room room = other.gameObject.GetComponent<RoomZone>().Room;
                _boss.NotifyOfPotentialSabotage(room);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_RoomTag))
            {
                _isTriggered = false;
            }
        }
    }
}