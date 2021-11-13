using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskmanUIControl : MonoBehaviour
{
    public HealthBar healthBar;
    public StaminaBar staminaBar;   
    public int maxHealth = 100;             //TODO leer de guardado
    public int minHealth = 0;
    public int currentHealth;
    public int healingPower = 30;
    //public int maxStamina = 100;
    //public int currentStamina;
    //public int minStamina = 0;

    //private bool staminaControl = false;

    // Start is called before the first frame update
    void Start()
    {
        //currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
        //currentStamina = maxStamina;
        //staminaBar.SetMaxStamina(maxStamina);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //TakeDamage(50);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
           // Cure(healingPower);
        }
        if (Input.GetMouseButtonDown(0)) 
        {
            // ataque
        }
        if (Input.GetMouseButtonDown(1)) 
        {
           //esquive
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

    //private void TakeStamina(int stamina) 
    //{
    //    currentStamina -= stamina;

    //    if (currentStamina < minStamina) 
    //    {
    //        currentStamina = minStamina;
    //    }

    //    staminaBar.SetStamina(currentStamina);
    //}

}
