using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float attackRange = 1f; // Adjust this value to set the attack range
    public LayerMask enemyLayer; // Set this in the Unity inspector to the layer where your enemies are

    private bool isAttacking = false;
    private float attackCooldownTimer = 0f;
    public float attackCooldown = 1f;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && attackCooldownTimer <= 0f)
        {
            Attack();
            attackCooldownTimer = attackCooldown;
        }

        if (!isAttacking)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }

        animator.SetBool("isAttacking", isAttacking);
    }

    void Attack()
    {
        isAttacking = true;

        // Detect enemies within attack range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        // Damage each enemy within attack range
        foreach (Collider enemy in hitEnemies)
        {
            // Assuming the enemy has a script with a method to take damage
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(10); // Adjust the damage value as needed
            }
        }

        // Set isAttacking to false after attack animation finishes
        Invoke("ResetAttack", 0.5f); // Adjust the delay as per your attack animation length
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    // Visualize the attack range in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}