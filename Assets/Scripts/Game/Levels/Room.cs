using UnityEngine;

namespace BGJ_2025_2.Game.Levels
{
    [AddComponentMenu("BGJ 2025.2/Game/Levels/Room")]
    public class Room : MonoBehaviour
    {
        // Fields
        private const int _DefaultWanderCount = 2;

        [SerializeField] private Office _office;

        [SerializeField] private string _name;
        [SerializeField] private int _wanderCount = _DefaultWanderCount;
        [SerializeField] private RoomZone[] _zones;
        private Vector3 _center = Vector3.zero;


        // Properties
        public Office Office => _office;

        public string Name => _name;
        public int WanderCount => _wanderCount;
        public RoomZone[] Zones => _zones;
        public Vector3 Center => _center;


        // Methods
        private void Awake()
        {
            foreach (RoomZone zone in _zones)
            {
                _center += zone.Center;
            }
            _center /= _zones.Length;
            _center = new Vector3(_center.x, 0f, _center.z);

            /*
            string debug = $"{name} | position: {transform.position}, local position: {transform.localPosition}, center: {_center}";
            foreach (RoomZone zone in _zones)
            {
                debug += $"\n\t - {zone.name} | " +
                    $"position: {zone.transform.position}, " +
                    $"local position: {zone.transform.localPosition}, " +
                    $"size: {zone.BoundingBox.size}, " +
                    //$"center: {zone.BoundingBox.center}, " +
                    $"bounds size: {zone.BoundingBox.bounds.size}, " +
                    $"bounds center: {zone.BoundingBox.bounds.center}, " +
                    $"bound extents: {zone.BoundingBox.bounds.extents}," +
                    $"edges: {zone.GetEdgeString()}";
            }
            Debug.Log(debug);
            */
        }

        public void Enter(Collider other)
        {

        }

        public RoomZone GetRandomZone()
        {
            return _zones[Random.Range(0, _zones.Length)];
        }

        public Vector3 GetRandomPosition()
        {
            return GetRandomZone().GetRandomPosition();
        }
    }
}