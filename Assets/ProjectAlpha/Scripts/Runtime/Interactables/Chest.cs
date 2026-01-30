using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour, IInteractable
{
    [Header("Chest Settings")]
    [SerializeField] private bool m_isLocked = true;
    [SerializeField] private ItemData m_requiredKey;

    [Header("Visuals")]
    [SerializeField] private Animator m_animator;

    [Header("UI Settings")]
    [SerializeField] private GameObject m_uiCanvasObject;
    [SerializeField] private TextMeshProUGUI m_promptText;
    [SerializeField] private Transform m_uiPoint; 
    [SerializeField] private string m_promptMessage = "Interact [E]";

    private void Start()
    {
        if (m_isLocked)
        {
            UpdateMessage("Unlock Chest [E]");
        }
        else
        {
            UpdateMessage("Open Chest [E]");
        }
        
        if (m_uiPoint != null && m_uiCanvasObject != null)
        {
            m_uiCanvasObject.transform.position = m_uiPoint.position;
        }

        TogglePrompt(false);
    }

    public void DoInteract()
    {
        if (m_isLocked)
        {
            if (m_requiredKey != null)
            {
                if (PlayerInventory.Instance.HasItem(m_requiredKey))
                {
                    UnlockAndOpen();
                }
                else
                {
                    Debug.Log("Need key: " + m_requiredKey.itemName);
                }
            }
            else
            {
                UnlockAndOpen();
            }
        }
        else
        {
            OpenChest();
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

    private void UnlockAndOpen()
    {
        m_isLocked = false;
        OpenChest();
    }

    private void OpenChest()
    {
        if (m_animator != null)
        {
            m_animator.SetTrigger("Open");
        }

        TogglePrompt(false);
        GetComponent<Collider>().enabled = false;
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