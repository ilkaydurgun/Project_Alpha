using System.Data.Common;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{

    private bool isLocked = true;
    public void DoInteract()
    {

        if (!isLocked)
        {
            Debug.Log("You opened the chest and found treasure!");
            return;
        }

        if (PlayerInventory.Instance.HasItem(this.gameObject))
        {
            isLocked = false;
            Debug.Log("You used the key to unlock the chest!");
           
        }

        Debug.Log("The chest is locked. You need a key to open it.");
        
    }

    public void StopInteract()
    {
        
    }
}
