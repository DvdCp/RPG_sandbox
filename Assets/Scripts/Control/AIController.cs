using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Vector3 _positionToPatrol;
        Fighter _fighter;
        Mover _mover;
        Health _health;
        GameObject _player;


        private void Start()
        {
            _positionToPatrol = transform.position;
            _player = GameObject.FindGameObjectWithTag("Player");
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_health.IsDead) return;

            if (InAttackRange(_player) && _fighter.CanAttack(_player.gameObject))
            {
                _fighter.Attack(_player.gameObject);

            }
            else 
            {
                _mover.StartMovementAction(_positionToPatrol);
                
            }

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

