using BGJ_2025_2.Game.Levels;
using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A j�t�kot j�tsz� j�t�kost reprezent�lja. Els�sorban �sszefogja a f� r�szegys�geket, amik a k�l�nb�z� <br/>
    /// aspektusok�rt felelnek.
    /// </summary>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player")]
    public class Player : MonoBehaviour
    {
        // Fields
        [SerializeField] private GameManager _game;

        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerView _view;
        [SerializeField] private PlayerInteraction _interaction;
        [SerializeField] private PlayerAppearance _appearance;


        // Properties
        public GameManager Game => _game;
        public Office Office => _game.Office;

        public PlayerInput Input => _input;
        public PlayerMovement Movement => _movement;
        public PlayerView View => _view;
        public PlayerInteraction Interaction => _interaction;
        public PlayerAppearance Appearance => _appearance;
    }
}