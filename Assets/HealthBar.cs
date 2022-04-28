using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    float currentHealth;
    float maxShield;
    float currentShield;
    [SerializeField] float shieldRegenDelay = 5f;
    [SerializeField] float shieldRegenSpeed = 1f;
    [SerializeField] float energyPerShield = 1f;
    float timeSinceDamage;
    [SerializeField] Slider shieldBar;
    [SerializeField] Slider healthBar;

    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        maxShield = maxHealth;
        currentShield = maxShield;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        shieldBar.value = currentShield / maxHealth;
        healthBar.value = currentHealth / maxHealth;
        timeSinceDamage += Time.deltaTime;
        if (currentShield < maxShield)
        {
            if (timeSinceDamage > shieldRegenDelay)
            {
                if(gameObject.GetComponent<EnergyBar>().currentEnergy > 0f)
                {
                    float increaseAmount = shieldRegenSpeed * Time.deltaTime;
                    currentShield += increaseAmount;
                    currentShield = Mathf.Clamp(currentShield, 0f, maxShield);
                    gameObject.GetComponent<EnergyBar>().ReduceEnergy(increaseAmount * energyPerShield);
                }
                
            }
        }
    }
    // Update is called once per frame
    public void Damage(float amount)
    {
        timeSinceDamage = 0f;
        if (currentShield > 0f)
        {
            currentShield -= amount;
            currentShield = Mathf.Clamp(currentShield, 0f, maxShield);
            if (currentShield == 0f)
            {
                Debug.Log("Shield Break");
            }
        }
        else
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            maxShield = currentHealth;
            if (currentShield == 0f)
            {
                Debug.Log("Dead");
            }
        }

    }

    public void ShieldOnlyDamage(float amount)
    {
        timeSinceDamage = 0f;
        if (currentShield > 0f)
        {
            currentShield -= amount;
            currentShield = Mathf.Clamp(currentShield, 0f, maxShield);
            if (currentShield == 0f)
            {
                Debug.Log("Shield Break");
            }
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
    }

    public void CollisionDetected(Collision collision)
    {
        Debug.Log(collision);
    }
}
