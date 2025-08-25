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
        private const float _DefaultWalkSpeed = 6.5f;
        private const float _DefaultRunSpeed = 9.5f;

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
        private bool _shouldWalk;
        private bool _shouldRun;
        private bool _isWalking;
        private bool _isRunning;


        // Properties
        public bool IsWalking => _isWalking;
        public bool IsRunning => _isRunning;
        public bool IsMoving => _isWalking || _isRunning;


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

            _shouldWalk = true;
            if (_shouldRun)
            {
                _isWalking = false;
                _isRunning = true;
            }
            else
            {
                _isWalking = true;
            }

        }

        public void StopWalking()
        {
            if (!_shouldWalk && !_shouldRun) return;

            _movementDirection = Vector2.zero;
            ChangePhysicsMaterials(_idleMaterial);
            _rigidbody.linearVelocity = Vector3.zero;

            _shouldWalk = false;
            _isWalking = false;
            _isRunning = false;
        }

        public void StartRunning()
        {
            if (_shouldRun) return;

            _movementSpeed = _runningSpeed;

            _shouldRun = true;
            _isRunning = _isWalking;
            _isWalking = false;
        }

        public void StopRunning()
        {
            if (!_shouldRun) return;

            _movementSpeed = _walkingSpeed;

            _shouldRun = false;
            _isRunning = false;
            if (_shouldWalk)
            {
                _isWalking = true;
            }
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