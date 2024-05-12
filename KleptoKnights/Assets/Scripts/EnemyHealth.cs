using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    public ParticleSystem hitParticle;


    public UI Ui;

    void Start()
    {
        currentHealth = maxHealth;
        hitParticle = GetComponentInChildren<ParticleSystem>();

    }

    public void TakeDamage(int attackDamage)
    {
        Debug.Log(gameObject.name + " took damage!");
        //Debug.Log("Enemy taking damage!");
        currentHealth -= attackDamage;

        if (hitParticle != null)
        {
            hitParticle.Play();
        }

        Ui.HealthBar.value = currentHealth;

        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death, such as playing death animation, giving player points, etc.
        Destroy(gameObject); // Destroy the enemy game object
    }

}
