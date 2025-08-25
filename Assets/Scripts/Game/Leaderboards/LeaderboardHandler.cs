using UnityEngine;

namespace BGJ_2025_2.Game.Leaderboards
{
    [AddComponentMenu("BGJ 2025.2/Game/Leaderboards/Leaderboard handler")]
    public class LeaderboardHandler : MonoBehaviour
    {
        // Fields
        [SerializeField] private GameManager _game;


        // Properties
        public GameManager Game => _game;


        // Methods
        public void Retrieve()
        {

        }

        public void Submit()
        {

        }
    }
}