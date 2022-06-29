using System.Collections;
using UnityEngine;
using RPG.Combat;


public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;

    bool isDead = false;
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
        if (isDead) return;

        //... else die 
        isDead = true;
        _animator.SetTrigger("die");
    }
}
