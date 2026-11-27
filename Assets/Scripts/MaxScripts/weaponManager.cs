using UnityEngine;
using UnityEngine.InputSystem;

public class weaponManager : MonoBehaviour
{
    private int activeWeapon; //1 is for the knife, 2 is for the pistol
   [SerializeField] private bool pistolUnlocked; //Starts false, flip to true when the pistol is found
    public GameObject knife;
    public knife theKnife; // Reference to the knife script
    public GameObject pistol;
    public pistol thePistol; // Reference to the pistol script

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeWeapon = 1;
        pistolUnlocked = false;
        knife.SetActive(true);
        pistol.SetActive(false);
    }

    private void SwitchWeapon(int weapon)
    {
        if (activeWeapon == weapon)
        {
            return;
        }
        else if (weapon == 2 && !pistolUnlocked)
        {
            return;
        }
        else if(weapon == 1)
        {
            knife.SetActive(true);
            pistol.SetActive(false);
            activeWeapon = 1;
        }
        else if(weapon == 2)
        {
            knife.SetActive(false);
            pistol.SetActive(true);
            activeWeapon = 2;
        }
    }

    public void OnEquipKnife(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchWeapon(1);
        }
    }

    public void OnEquipPistol(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Debug.Log("Pistol equip input received");
            SwitchWeapon(2);
        }
    }

    public void UnlockPistol()
    {
        pistolUnlocked = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (activeWeapon == 1)
            {
                theKnife.Attack();
            }
            else if (activeWeapon == 2)
            {
                thePistol.Attack();
            }
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed && activeWeapon == 2)
        {
            thePistol.Reload();
        }
    }
}
