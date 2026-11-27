using UnityEngine;

public class healthPickup : MonoBehaviour
{
    public int healingAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 

            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {

                if (playerHealth.CurrentHealth < playerHealth.maxHealth)
                {
                    playerHealth.ReceiveHealth(healingAmount);
                    Destroy(gameObject);
                }

            }

        }
    }
}
