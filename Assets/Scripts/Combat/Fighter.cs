using System.Collections;
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;    // 2 meters
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 10f;

        Transform _target;
        Mover _mover;
        ActionScheduler _scheduler;
        Animator _animator;
        float timeSinceLastAttack = 0.0f;
        

        private void Start()
        {
            _mover = GetComponent<Mover>();
            _scheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (_target == null) return;

            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.position);
                // transform.LookAt(_target.GetComponent<Collider>().bounds.center);

            }
            else
            {
                AttackBehaviour();
                _mover.Cancel();
            }

        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }

        public void Attack(CombatAttacker enemy)
        {
            _scheduler.StartAction(this);
            _target = enemy.transform;
        }

        public void Cancel()
        {
            _target = null;
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                _animator.SetTrigger("attack");
                timeSinceLastAttack = 0.0f;
            }
                
        }

        //Animation event
        void Hit()
        {
            print("Hit animation event");
            _target.GetComponent<Health>().TakeDamage(weaponDamage);
        }
    }
}