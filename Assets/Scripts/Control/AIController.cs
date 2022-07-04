using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter _fighter;
        Health _health;
        GameObject _player;


        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _fighter = GetComponent<Fighter>();
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
                _fighter.Cancel();
            }

        }

        private bool InAttackRange(GameObject player)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return chaseDistance > distanceToPlayer;
        }
    }

}

