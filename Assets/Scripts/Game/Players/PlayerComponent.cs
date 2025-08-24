using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A játékos fõ részegységeinek õsosztája.
    /// </summary>
    /// <seealso cref="Player"/>
    public abstract class PlayerComponent : MonoBehaviour
    {
        // Fields
        [SerializeField] private Player _player;


        // Properties
        public Player Player => _player;
    }
}