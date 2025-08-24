using UnityEngine;

namespace BGJ_2025_2.Game.Tasks
{
    public abstract class Task : MonoBehaviour
    {
        // Fields
        [SerializeField] private string _name;
        [SerializeField][TextArea(3, 7)] private string _description;


        // Properties
        public string Name => _name;
        public string Description => _description;


        // Methods
        public virtual void Begin() { }

        public virtual void Finish() { }
    }
}