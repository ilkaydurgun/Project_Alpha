using System.Collections.Generic;

using UnityEngine;



public class PlayerInventory : MonoBehaviour

{
    public static PlayerInventory Instance { get; private set; }
    public List<GameObject> inventoryItems = new List<GameObject>();

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    public void AddItem(GameObject item)
    {
        inventoryItems.Add(item);

        Debug.Log($"Item {item.name} added to inventory.");
    }

    public bool HasItem(GameObject item)
    {
        return inventoryItems.Contains(item);
    }

}
