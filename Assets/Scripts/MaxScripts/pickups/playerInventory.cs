using UnityEngine;

public class playerInventory : MonoBehaviour
{
    [SerializeField] private int keyCount = 0;
    public int KeyCount => keyCount; //read only

    public void RecieveKey()
    {
        keyCount++;
    }

    public bool UseKey()
    {
        if (keyCount > 0)
        {
            keyCount--;
            return true;
        }
        else
        {
            return false;
        }
    }

}