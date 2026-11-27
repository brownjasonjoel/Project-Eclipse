using UnityEngine;

public class keyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerInventory inventory = other.GetComponent<playerInventory>();
            if (inventory != null)
            {
                inventory.RecieveKey();
                Destroy(gameObject);
            }

        }
    }
}
