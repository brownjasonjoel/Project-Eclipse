using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxArmor = 50;
    [SerializeField] private int currentHealth;
    [SerializeField] private int currentArmor;
    public int CurrentHealth => currentHealth; //read only 
    public int CurrentArmor => currentArmor; //read only
    bool isDead = false;
    public UnityEvent onDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        currentArmor = 0;
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        if (damage < 0) //negative damage is not allowed
        {
            Debug.LogWarning("Negative damage recieved and is not allowed");
            return;
        }

        if (currentArmor > 0)
        {
            int armorDamage = Mathf.Min(damage, currentArmor);
            currentArmor -= armorDamage;
            damage -= armorDamage;
        }

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ReceiveHealth(int healing)
    {
        if (isDead)
            return; //dead people cant heal

        if (healing < 0) //negative healing is not allowed
        {
            Debug.LogWarning("Negative healing recieved and is not allowed");
            return;
        }

        currentHealth += Mathf.Min(healing, maxHealth - currentHealth); //uses Mathf.Min to prevet overhealing

    }

    public void ReceiveArmor(int armor)
    {
        if (isDead)
            return; //dead people cant get armor

        if (armor < 0) //negative armor is not allowed
        {
            Debug.LogWarning("Negative armor recieved and is not allowed");
            return;
        }

        currentArmor += Mathf.Min(armor, maxArmor - currentArmor); //uses Mathf.Min to prevent overarmor
    }

    private void Die()
    {
        currentHealth = 0;
        isDead = true;
        onDeath?.Invoke();
        Debug.Log("Die has been called");
    }
}
