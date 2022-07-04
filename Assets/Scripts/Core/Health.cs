using UnityEngine;


namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        private bool _isDead;
        public bool IsDead { get => _isDead; set => _isDead = value; }

        Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
                Die();
        }

        private void Die()
        {
            // If is already dead, returns...
            if (IsDead) return;

            //... else die 
            IsDead = true;
            _animator.SetTrigger("die");

            // When dying, cancel current action
            GetComponent<ActionScheduler>().CancelCurrentAction();

        }
    }

}

