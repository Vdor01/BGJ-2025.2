using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A játékos térben való mozgatásáért felel.
    /// </summary>
    /// <seealso cref="Player"/>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player movement")]
    public class PlayerMovement : PlayerComponent
    {
        // Fields
        private const float _DefaultWalkSpeed = 7f;
        private const float _DefaultRunSpeed = 10f;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider[] _colliders;
        [SerializeField] private PhysicsMaterial _movementMaterial;
        [SerializeField] private PhysicsMaterial _idleMaterial;
        [SerializeField] private float _walkingSpeed = _DefaultWalkSpeed;
        [SerializeField] private float _runningSpeed = _DefaultRunSpeed;
        private float _movementSpeed;
        private Vector2 _movementDirection;
        private Vector3 _movementVector;
        private Vector3 _linearVelocity;
        private bool _shouldRun;
        private bool _isWalking;
        private bool _isRunning;


        // Properties
        public bool IsWalking => _isWalking;
        public bool IsRunning => _isRunning;


        // Methods
        private void Awake()
        {
            _movementSpeed = _walkingSpeed;
        }

        private void FixedUpdate()
        {
            if (_movementDirection != Vector2.zero)
            {
                _movementVector = _player.transform.forward * _movementDirection.y + _player.transform.right * _movementDirection.x;
                _linearVelocity = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);

                _rigidbody.AddForce(_movementVector * _movementSpeed - _linearVelocity, ForceMode.Impulse);
            }
        }

        public void StartWalking(Vector2 direction)
        {
            _movementDirection = direction;
            ChangePhysicsMaterials(_movementMaterial);

            _isWalking = true;
            if (_shouldRun)
            {
                _isRunning = true;
            }
        }

        public void StopWalking()
        {
            if (!_isWalking) return;

            _movementDirection = Vector2.zero;
            ChangePhysicsMaterials(_idleMaterial);
            _rigidbody.linearVelocity = Vector3.zero;

            _isWalking = false;
            _isRunning = false;
        }

        public void StartRunning()
        {
            if (_shouldRun) return;

            _shouldRun = true;
            _movementSpeed = _runningSpeed;
            _isRunning = _isWalking;
        }

        public void StopRunning()
        {
            if (!_shouldRun) return;

            _shouldRun = false;
            _movementSpeed = _walkingSpeed;
            _isRunning = false;
        }

        private void ChangePhysicsMaterials(PhysicsMaterial physicsMaterial)
        {
            foreach (Collider collider in _colliders)
            {
                collider.material = physicsMaterial;
            }
        }
    }
}