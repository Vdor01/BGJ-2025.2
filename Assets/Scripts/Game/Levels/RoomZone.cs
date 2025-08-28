using UnityEngine;

namespace BGJ_2025_2.Game.Levels
{
    [AddComponentMenu("BGJ 2025.2/Game/Levels/Room zone")]
    public class RoomZone : MonoBehaviour
    {
        // Fields
        private const float _BoundingBoxMargin = 0.1f;

        [SerializeField] private Room _room;

        [SerializeField] private BoxCollider _boundingBox;


        // Properties
        public Room Room => _room;

        public BoxCollider BoundingBox => _boundingBox;
        public Vector3 Center => _boundingBox.bounds.center;


        // Methods
        private void OnTriggerEnter(Collider other)
        {
            _room.Enter(other);
        }

        public Vector3 GetRandomPosition()
        {
            float xPosition = Random.Range(
                -_boundingBox.bounds.extents.x + _BoundingBoxMargin,
                _boundingBox.bounds.extents.x - _BoundingBoxMargin);
            float zPosition = Random.Range(
                -_boundingBox.bounds.extents.z + _BoundingBoxMargin,
                _boundingBox.bounds.extents.z - _BoundingBoxMargin);

            return _boundingBox.bounds.center + new Vector3(xPosition, 0f, zPosition);
        }

        public string GetEdgeString()
        {
            float xCenter = _boundingBox.bounds.center.x;
            float zCenter = _boundingBox.bounds.center.z;

            float xTranslated = xCenter;
            float zTranslated = zCenter;

            float xExtent = -_boundingBox.bounds.extents.x;
            float zExtent = -_boundingBox.bounds.extents.z;

            return $"X: {-xExtent + xTranslated} | {xTranslated + xExtent}, " +
                $"Z: {-zExtent + zTranslated} | {zExtent + zTranslated}";
        }
    }
}