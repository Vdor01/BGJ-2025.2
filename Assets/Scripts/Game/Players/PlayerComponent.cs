using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A j�t�kos f� r�szegys�geinek �soszt�ja.
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