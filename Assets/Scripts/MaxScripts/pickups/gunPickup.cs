using UnityEngine;
using UnityEngine.InputSystem;

public class gunPickup : MonoBehaviour
{
    bool playerInRange = false;
    private weaponManager playerWeaponManager;

    void Update()
    {
        if (playerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (playerWeaponManager != null)
            {
                playerWeaponManager.UnlockPistol();
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerWeaponManager = other.GetComponentInChildren<weaponManager>(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerWeaponManager = null;
        }
    }
}
