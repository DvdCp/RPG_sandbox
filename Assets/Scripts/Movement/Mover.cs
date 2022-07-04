using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        NavMeshAgent _agent;
        Animator _animator;
        ActionScheduler _scheduler;
        Health _health;

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _scheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
        }

        void Update()
        {
            _agent.enabled = !_health.IsDead;

            UpdateAnimator();
        }

        public void MoveTo(Vector3 pointToReach)
        {
            _agent.isStopped = false;
            _agent.destination = pointToReach;

        }

        public void StartMovementAction(Vector3 destination)
        {
            _scheduler.StartAction(this);
            MoveTo(destination);
        }

        public void Cancel()
        {
            _agent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 _velocity = _agent.velocity;
            Vector3 _localVelocity = transform.InverseTransformDirection(_velocity);
            float _speed = _localVelocity.z;

            _animator.SetFloat("MovementSpeed", _speed);

        }
    }
}

