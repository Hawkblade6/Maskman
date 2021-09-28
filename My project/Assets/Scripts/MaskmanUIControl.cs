using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskmanUIControl : MonoBehaviour
{
    public HealthBar healthBar;
    public int maxHealth = 100;
    public int minHealth = 0;
    public int currentHealth;
    public int healingPower = 30;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(50);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Cure(healingPower);
        }

    }

    private void TakeDamage(int damage) 
    {
        currentHealth -= damage;

        if (currentHealth < minHealth)
        {
            currentHealth = minHealth;
        }

        healthBar.SetHealth(currentHealth);
    }

    private void Cure(int healingPower) 
    {
        if (currentHealth != maxHealth) {

            currentHealth += healingPower;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthBar.SetHealth(currentHealth);
        }
    }
}
