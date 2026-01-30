using UnityEngine;

public class Key : MonoBehaviour , IInteractable
{    
    
 
    
    [SerializeField] private GameObject m_gameObjectToActivate;
    public void DoInteract()
    {
        PlayerInventory playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();
        playerInventory.AddItem(this.gameObject);

        Debug.Log("Key picked up!");
        m_gameObjectToActivate.SetActive(false);
    }

    public void StopInteract()
    {
        
    }
}
