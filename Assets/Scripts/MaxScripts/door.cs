using UnityEngine;
using UnityEngine.InputSystem;

public class door : MonoBehaviour
{
    private bool playerInRange = false;
    private playerInventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            inventory = other.GetComponent<playerInventory>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            inventory = null;
        }
    }

    private void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (inventory != null && inventory.KeyCount > 0)
            {
                inventory.UseKey();
                Destroy(gameObject);
            }
        }
    }
}