using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float _suspectDuration = 5f;
        
        Vector3 _positionToPatrol;
        float _timeSinceLastsawPlayer = Mathf.Infinity;
        Fighter _fighter;
        Mover _mover;
        Health _health;
        GameObject _player;
        ActionScheduler _actionScheduler;


        private void Start()
        {
            _positionToPatrol = transform.position;
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
                _timeSinceLastsawPlayer = 0f;
                AttackBehaviour();

            }
            // Suspicion state
            else if (_timeSinceLastsawPlayer < _suspectDuration)
            {
                SuspicionBehaviour();
            }
            // Return to guard state
            else
            {
                GuardBehaviour();
            }

            _timeSinceLastsawPlayer += Time.deltaTime;

        }

        private void GuardBehaviour()
        {
            _mover.StartMovementAction(_positionToPatrol);
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

