using BGJ_2025_2.Game.Interactions;
using UnityEngine;

namespace BGJ_2025_2.Game.Players
{
    /// <summary>
    /// A játéktérrel való interakcióért felel.
    /// </summary>
    /// <seealso cref="Player"/>
    [AddComponentMenu("BGJ 2025.2/Game/Players/Player interaction")]
    public class PlayerInteraction : PlayerComponent
    {
        // Fields
        private const float _DefaultInteractionDistance = 2.75f;
        private const float _DefaultThrowPower = 15f;

        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private float _interactionDistance = _DefaultInteractionDistance;
        [SerializeField] private float _throwPower = _DefaultThrowPower;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        private int _interactionLayerMask;
        private RaycastHit _raycastHit;
        private GameObject _interactableObject;
        private IInteractable _interactable;
        private IDescriptable _descriptable;
        private IGrabbable _grabbable;
        private IUsable _usable;
        private IGrabbable _grabbedGrabbable;
        private GameObject _grabbedObject;
        private Rigidbody _grabbedObjectRigidbody;
        private Collider[] _grabbedObjectColliders;
        private Transform _grabbedObjectParent;
        private Transform _mainHand;


        // Properties
        public GameObject InteractableObject => _interactableObject;
        public IInteractable Interactable => _interactable;
        public IDescriptable Descriptable => _descriptable;
        public IGrabbable Grabable => _grabbable;
        public IUsable Usable => _usable;
        public GameObject GrabbedObject => _grabbedObject;


        // Methods
        private void Awake()
        {
            _interactionLayerMask = ~(1 << _interactionLayer);
            _mainHand = _rightHand;
        }

        private void Update()
        {
            if (Raycast(out _raycastHit, _interactionDistance, _interactionLayerMask))
            {
                _interactableObject = _raycastHit.collider.gameObject;
                if (_interactableObject.TryGetComponent(out _interactable))
                {
                    _descriptable = _interactable.Descriptable;
                    _grabbable = _interactable.Grabbable;
                    _usable = _interactable.Usable;
                }
                else if (_interactableObject.transform.parent.gameObject.TryGetComponent(out _interactable))
                {
                    _interactableObject = _interactableObject.transform.parent.gameObject;

                    _descriptable = _interactable.Descriptable;
                    _grabbable = _interactable.Grabbable;
                    _usable = _interactable.Usable;
                }
                else
                {
                    _interactableObject = null;
                }
            }
            else if (_interactableObject != null || _interactable != null)
            {
                _interactableObject = null;

                _interactable = null;
                _descriptable = null;
                _grabbable = null;
                _usable = null;
            }
        }

        public void Grab()
        {
            if (!_grabbedObject && _grabbable != null)
            {
                // Felvett tárgy beállítása
                _grabbedObject = _interactableObject;
                _grabbedGrabbable = _grabbable;

                // A felvett tárgy saját felvevõ logikájának aktiválása
                _grabbedGrabbable.Grab();

                // A felvett tárgy rigidbody-jának kinematic-ra állítása, hogy ne mozogjon
                _grabbedObjectRigidbody = _grabbedObject.GetComponent<Rigidbody>();
                if (_grabbedObjectRigidbody != null)
                {
                    _grabbedObjectRigidbody.isKinematic = true;
                }

                // A felvett tárgy colliderjeinek kikapcsolása, hogy ne ütközzön bele dolgokba
                _grabbedObjectColliders = _grabbedObject.GetComponentsInChildren<Collider>();
                ChangeGrabbedObjectColliderStates(false);

                // A felvett tárgy szülõjének ideiglenes átállítása a player fõ kezére
                _grabbedObjectParent = _grabbedObject.transform.parent;
                _grabbedObject.transform.position = _mainHand.position;
                _grabbedObject.transform.rotation = Quaternion.identity;
                _grabbedObject.transform.parent = _mainHand.transform;
            }
        }

        private void ReleaseGrabbedObjectTransform()
        {
            _grabbedObject.transform.parent = _grabbedObjectParent;
            _grabbedObjectParent = null;
        }

        private void ReleaseGrabbedObjectColliders()
        {
            if (_grabbedObjectColliders.Length == 0) return;

            ChangeGrabbedObjectColliderStates(true);

            _grabbedObjectColliders = null;
        }

        private void ReleaseGrabbedObjectRigidbody()
        {
            if (_grabbedObjectRigidbody == null) return;

            _grabbedObjectRigidbody.isKinematic = false;
            _grabbedObjectRigidbody = null;
        }

        private void ReleaseGrabbedObject()
        {
            ReleaseGrabbedObjectTransform();

            ReleaseGrabbedObjectColliders();

            ReleaseGrabbedObjectRigidbody();

            _grabbedObject = null;
        }

        public void Place()
        {
            if (!_grabbedObject) return;

            ReleaseGrabbedObjectTransform();

            ChangeGrabbedObjectColliderStates(true);
            if (Raycast(out _raycastHit))
            {
                _grabbedObject.transform.position = _raycastHit.point +
                    Vector3.Scale(_grabbedObjectColliders[0].bounds.size / 2f, _raycastHit.normal);
            }

            _grabbedObjectColliders = null;

            ReleaseGrabbedObjectRigidbody();

            _grabbedGrabbable.Place();

            _grabbedObject = null;
            _grabbedGrabbable = null;
        }

        public void Throw()
        {
            if (!_grabbedObject || _grabbedObjectRigidbody == null) return;

            ReleaseGrabbedObjectTransform();

            ReleaseGrabbedObjectColliders();

            Vector3 throwDirection = Raycast(out _raycastHit, float.PositiveInfinity)
                ? (_raycastHit.point - _mainHand.transform.position).normalized
                : _player.View.Forward;
            Debug.Log(_raycastHit.collider.gameObject.name);

            _grabbedObjectRigidbody.isKinematic = false; ;
            _grabbedObjectRigidbody.AddForce(_throwPower * throwDirection, ForceMode.Impulse);
            _grabbedObjectRigidbody = null;

            _grabbedObject = null;
        }

        public void Use()
        {
            if (_usable == null) return;

            _usable.Use();
        }

        private bool Raycast(out RaycastHit raycastHit, float maxDistance = _DefaultInteractionDistance, int layerMask = Physics.DefaultRaycastLayers)
        {
            return Physics.Raycast(_player.View.GetRay(), out raycastHit, maxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)
                && ((1 << raycastHit.collider.gameObject.layer) & layerMask) != 0;
        }

        private void ChangeGrabbedObjectColliderStates(bool grabbedObjectColliderState)
        {
            if (_grabbedObjectColliders == null) return;

            foreach (Collider grabbedObjectCollider in _grabbedObjectColliders)
            {
                grabbedObjectCollider.enabled = grabbedObjectColliderState;
            }
        }
    }
}
