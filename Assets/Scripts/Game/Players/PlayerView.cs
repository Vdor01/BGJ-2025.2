using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A játékos játéktérre való betekintéséért felel.
    /// </summary>
    /// <seealso cref="Player"/>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player view")]
    public class PlayerView : PlayerComponent
    {
        // Fields
        private const float _DefaultLookSensitivity = 2.25f;

        [SerializeField] private Camera _camera;
        [SerializeField] private float _lookSensitivity = _DefaultLookSensitivity;


        // Properties
        public Vector3 Forward => _camera.transform.forward;
        public Vector3 Right => _camera.transform.right;
        public Vector3 Up => _camera.transform.up;


        // Methods
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Look(Vector2 direction)
        {
            _player.transform.Rotate(direction.x * Time.deltaTime * _lookSensitivity * Vector3.up, Space.Self);

            Vector3 rotation = _camera.transform.localEulerAngles;
            float xRotation = rotation.x - direction.y * Time.deltaTime * _lookSensitivity;
            float clampedXRotation = Mathf.Clamp(xRotation >= 180 ? xRotation - 360f : xRotation, -90f, 90f);
            _camera.transform.localEulerAngles = new(clampedXRotation, rotation.y, rotation.z);
        }

        public Ray GetRay()
        {
            return new Ray(_camera.transform.position, Forward);
        }
    }
}