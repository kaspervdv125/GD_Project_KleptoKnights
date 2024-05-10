using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;

    public GameObject blood;

    public UI Ui;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int attackDamage)
    {
        Debug.Log("Enemy taking damage!");
        Instantiate(blood, transform.position, Quaternion.identity);
        currentHealth -= attackDamage;

        Ui.HealthBar.value = currentHealth;

        // Check if the enemy has been defeated
        if (currentHealth <= 0)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Die();
        }
    }

    void Die()
    {
        // Handle enemy death, such as playing death animation, giving player points, etc.
        Destroy(gameObject); // Destroy the enemy game object
    }

}
