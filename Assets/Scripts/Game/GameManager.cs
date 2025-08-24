using BGJ_2025_2.GUI;
using UnityEngine;

namespace BGJ_2025_2.Game
{
    /// <summary>
    /// A j�t�k modell-view (MV) architekt�r�j�ban a modell r�sz hierarchi�j�nak tetej�n �ll.
    /// </summary>
    /// <see cref="GUIManager"/>
    [AddComponentMenu("BGJ 2025.2/Game/Game manager")]
    public class GameManager : MonoBehaviour
    {
        // Fields
        [SerializeField] private GUIManager _gui;


        // Properties
        public GUIManager GUI => _gui;
    }
}