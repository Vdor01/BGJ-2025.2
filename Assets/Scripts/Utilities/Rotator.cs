using UnityEngine;

namespace BGJ_2025_2
{
    [AddComponentMenu("BGJ 2025.2/Rotator")]
    public class Rotator : MonoBehaviour
    {
        // Fields
        [SerializeField] private Vector3 _rotationSpeed;


        // Methods
        private void Update()
        {
            transform.Rotate(_rotationSpeed * Time.deltaTime);
        }
    }
}