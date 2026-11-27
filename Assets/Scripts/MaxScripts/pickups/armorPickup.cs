using UnityEngine;

public class armorPickup : MonoBehaviour
{
    public int armorAmount = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {

                if (playerHealth.CurrentArmor < playerHealth.maxArmor)
                {
                    playerHealth.ReceiveArmor(armorAmount);
                    Destroy(gameObject);
                }

            }

        }
    }
}