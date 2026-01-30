using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    
    public List<ItemData> inventoryItems = new List<ItemData>();
    
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this.gameObject);
        else Instance = this;
    }
    
    public void AddItem(ItemData item)
    {
        if (!inventoryItems.Contains(item))
        {
            inventoryItems.Add(item);
            Debug.Log($"Item added: {item.itemName}");
        }
    }
    
    public void RemoveItem(ItemData item)
    {
        if (inventoryItems.Contains(item))
            inventoryItems.Remove(item);
    }
    
    public bool HasItem(ItemData item)
    {
        return inventoryItems.Contains(item);
    }
}