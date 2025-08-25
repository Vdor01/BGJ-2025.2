namespace BGJ_2025_2.Game.Leaderboards
{
    public readonly struct LeaderboardEntry
    {
        // Fields
        public readonly string Name;
        public readonly int Score;


        // Methods
        public LeaderboardEntry(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}