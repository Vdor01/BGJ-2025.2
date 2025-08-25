using BGJ_2025_2.Game.Entities;
using BGJ_2025_2.Game.Items;
using BGJ_2025_2.Game.Players;
using UnityEngine;

namespace BGJ_2025_2.Game.Levels
{
    [AddComponentMenu("BGJ 2025.2/Game/Levels/Office")]
    public class Office : MonoBehaviour
    {
        // Fields
        [SerializeField] private GameManager _game;

        [SerializeField] private Boss _boss;
        [SerializeField] private CookieJar _cookieJar;
        [SerializeField] private Room[] _rooms;
        private Room _playerRoom;
        private Room _bossRoom;


        // Properties
        public GameManager Game => _game;
        public Player Player => _game.Player;

        public Boss Boss => _boss;
        public CookieJar CookieJar => _cookieJar;
        public Room[] Rooms => _rooms;
        public Room this[int index] => _rooms[index];
        public Room PlayerRoom => _playerRoom;
        public Room BossRoom => _bossRoom;


        // Methods
        public Room GetRandomRoom()
        {
            return _rooms[Random.Range(0, _rooms.Length)];
        }

        public Vector3 GetRandomPosition()
        {
            return GetRandomRoom().GetRandomPosition();
        }
    }
}