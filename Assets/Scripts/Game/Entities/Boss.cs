using BGJ_2025_2.Game.Interactions;
using BGJ_2025_2.Game.Levels;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace BGJ_2025_2.Game.Entities
{
    [AddComponentMenu("BGJ 2025.2/Game/Entities/Boss")]
    public class Boss : Human, IDescriptable
    {
        // Fields
        [SerializeField] private Office _office;

        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        private bool _isWandering;
        private float _alertness;
        Vector3 previousWanderPosition = Vector3.zero;


        // Properties
        public bool IsMoving => _agent.velocity != Vector3.zero;
        public bool IsIdle => !IsMoving;
        public bool IsWandering => _isWandering;
        public float Alertness => _alertness;
        public string Name => "Gilbert Johnson";
        public string Description => "(Your boss)";


        // Methods
        private void Start()
        {
            transform.position = _office.GetRandomPosition();
        }

        private void Update()
        {
            //_agent.SetDestination(_office.Player.transform.position);
        }

        public void Wander(Vector3 position, Action actionAfter = null)
        {
            StartCoroutine(WanderCoroutine(position));
        }

        private IEnumerator WanderCoroutine(Vector3 position, Action actionAfter = null)
        {
            _agent.SetDestination(position);
            _isWandering = true;

            yield return new WaitUntil(() => _agent.isStopped);

            _isWandering = false;
            actionAfter?.Invoke();
        }

        public void DecideNextAction()
        {

        }

        public void WanderToRoom()
        {
            _isWandering = true;
        }

        public void WanderInRoom()
        {

        }



        public void AssertDominance()
        {

        }

        private IEnumerator AssertDominanceCoroutine()
        {
            yield return null;
        }
    }
}