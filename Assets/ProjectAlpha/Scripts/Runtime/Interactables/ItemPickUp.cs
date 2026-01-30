using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    [SerializeField] private ItemData m_itemData;

    [Header("UI Settings")]
    [SerializeField] private GameObject m_uiCanvasObject;
    [SerializeField] private TextMeshProUGUI m_promptText;
    [SerializeField] private Transform m_uiPoint; 
    [SerializeField] private string m_promptMessage = "Pick Up [E]";

    private void Start()
    {
        if (m_itemData != null)
        {
            UpdateMessage($"Press [E] to pick up {m_itemData.itemName}");
        }

        if (m_uiPoint != null && m_uiCanvasObject != null)
        {
            m_uiCanvasObject.transform.position = m_uiPoint.position;
        }
        
        TogglePrompt(false);
    }

    public void DoInteract()
    {
        if (m_itemData != null)
        {
            PlayerInventory.Instance.AddItem(m_itemData);
            Debug.Log($"{m_itemData.itemName} added to inventory.");
            
            TogglePrompt(false);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("ItemData is null in ItemPickup script! Please assign it in the Inspector.");
        }
    }

    public void StopInteract()
    {
    }

    public void TogglePrompt(bool state)
    {
        if (m_uiCanvasObject != null)
        {
            m_uiCanvasObject.SetActive(state);
        }
    }

    private void UpdateMessage(string newMessage)
    {
        m_promptMessage = newMessage;
        if (m_promptText != null)
        {
            m_promptText.text = newMessage;
        }
    }
}