using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    internal static object instance;
    public List<GameObject> inventoryItems = new List<GameObject>();
    private object itemName;

    public void AddItem(GameObject item)
    {
       
            inventoryItems.Add(item);
            Debug.Log($"Item {itemName} added to inventory.");


    }

    public bool HasItem(GameObject itemName)
    {
        return inventoryItems.Contains(itemName);
    }
}
