using UnityEngine;

public class Key : MonoBehaviour , IInteractable
{    
    
    public enum KeyType
    {
     Door,
     Chest,
       
    }
    
    [SerializeField] private GameObject gameObjectToActivate;
    public void DoInteract()
    {
        PlayerInventory playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        playerInventory.AddItem(this.gameObject);

        Debug.Log("Key picked up!");
        gameObjectToActivate.SetActive(false);
    }

    
}
