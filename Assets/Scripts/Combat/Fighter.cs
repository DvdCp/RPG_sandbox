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
        [Range(0, 1f)]
        [SerializeField] float chaseSpeedFraction;

        Health _target;
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
            if (_target.IsDead) return;

            if (!GetIsInRange())
            {
                _mover.MoveTo(_target.transform.position, chaseSpeedFraction);
            }
            else
            {
                AttackBehaviour();
                _mover.Cancel();
            }

        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            Health targetHealth = combatTarget.GetComponent<Health>();
            return targetHealth != null && !targetHealth.IsDead;

        }

        public void Attack(GameObject enemy)
        {
            _scheduler.StartAction(this);
            _target = enemy.GetComponent<Health>(); 
        }

        public void Cancel()
        {
            _animator.SetTrigger("stopAttack");
            _animator.ResetTrigger("attack");
            _target = null;
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.GetComponent<Transform>().position);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0.0f;
            }

        }

        private void TriggerAttack()
        {
            // Setting triggers in animator
            _animator.ResetTrigger("stopAttack"); 
            _animator.SetTrigger("attack");    // This line start animation and Hit() event
        }
         

        //Animation event
        void Hit()
        {
            if (_target == null) return;
            _target.TakeDamage(weaponDamage); 
        }
    }
}