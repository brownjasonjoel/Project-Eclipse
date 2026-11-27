using UnityEngine;

public class ammoPickup : MonoBehaviour
{
    public int ammoAmount = 6;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            pistol playerPistol = other.GetComponentInChildren<pistol>(true);
            if (playerPistol != null)
            {

                if (playerPistol.CurrentRAmmo < playerPistol.maxReserveAmmo)
                {
                    playerPistol.ReceiveAmmo(ammoAmount);
                    Destroy(gameObject);
                }

            }

        }
    }
}
