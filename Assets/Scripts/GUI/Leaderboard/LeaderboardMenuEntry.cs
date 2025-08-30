using BGJ_2025_2.Game.Leaderboards;
using TMPro;
using UnityEngine;

namespace BGJ_2025_2.GUI.Leaderboard
{
    [AddComponentMenu("BGJ 2025.2/GUI/Leaderboard/Leaderboard menu entry")]
    public class LeaderboardMenuEntry : MonoBehaviour
    {
        // Fields
        [SerializeField] TextMeshProUGUI _rankLabel;
        [SerializeField] TextMeshProUGUI _nameLabel;
        [SerializeField] TextMeshProUGUI _scoreLabel;


        // Methods
        public void SetUp(int rank, LeaderboardEntry entry)
        {
            _rankLabel.SetText(rank.ToString());
            _nameLabel.SetText(entry.Name);
            _nameLabel.SetText(entry.Score.ToString());
        }
    }
}