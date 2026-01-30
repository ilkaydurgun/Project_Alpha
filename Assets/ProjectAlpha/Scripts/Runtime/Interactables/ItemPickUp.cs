using UnityEngine;

public class ItemPickup : InteractableBase
{
    [Header("Item Settings")]
    [SerializeField] private ItemData m_itemData;
    
    protected override void Start()
    {
        base.Start();

        if (m_itemData != null)
        {
            UpdateMessage($"Press [E] to pick up {m_itemData.itemName}");
        }
    }
    
    public override void DoInteract()
    {
        if (m_itemData != null)
        {
            PlayerInventory.Instance.AddItem(m_itemData);
            Debug.Log($"{m_itemData.itemName} envantere eklendi.");
            TogglePrompt(false);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("ItemPickup scriptinde ItemData boş! Lütfen Inspector'dan atama yap.");
        }
    }
}