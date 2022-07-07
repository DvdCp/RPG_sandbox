using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [Range(0, 5.90f)]
        [SerializeField] float maxSpeed = 5.66f;

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
        public void StartMovementAction(Vector3 destination, float speedFraction)
        {
            _scheduler.StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 pointToReach, float speedFraction)
        {
            _agent.destination = pointToReach;
            _agent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            _agent.isStopped = false;

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

