using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 4f;
        [SerializeField] float waypointDwellTime = 5f;
        [SerializeField] PatrolRoute patrolRoute;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        [SerializeField] float waypointsTollerance = .5f;

        int _currentWaypointIndex = 0;
        Vector3 _guardPosition;
        float _timeSinceLastSawPlayer = Mathf.Infinity;
        float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        Fighter _fighter;
        Mover _mover;
        Health _health;
        GameObject _player;
        ActionScheduler _actionScheduler;


        private void Start()
        {
            _guardPosition = transform.position;
            _player = GameObject.FindGameObjectWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if (_health.IsDead) return;

            // Attack state
            if (InAttackRange(_player) && _fighter.CanAttack(_player.gameObject))
            {
                _timeSinceLastSawPlayer = 0f;
                AttackBehaviour();

            }
            // Suspicion state
            else if (_timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            // Return to guard state
            else
            {
                PatrolBehaviour();
            }

            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            // Using patroleRoute waypoints like in a circula way
            Vector3 nextPosition = _guardPosition;

            if (patrolRoute != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0;
                    GoToNextWaypoint();
                }
                    

                nextPosition = GetCurrentWaypoint();
            }

            if (_timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                _mover.StartMovementAction(nextPosition, patrolSpeedFraction);
            }

        }
        private bool AtWaypoint()
        {
            // If is enough near the waypoint, return True, False otherwhise
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) < waypointsTollerance;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolRoute.GetWapoint(_currentWaypointIndex);
        }

        private void GoToNextWaypoint()
        {
            // Getting next waypoint's index
            _currentWaypointIndex =  patrolRoute.GetNextIndex(_currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            _actionScheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {   
            _fighter.Attack(_player.gameObject);
        }

        private bool InAttackRange(GameObject player)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return chaseDistance > distanceToPlayer;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}

