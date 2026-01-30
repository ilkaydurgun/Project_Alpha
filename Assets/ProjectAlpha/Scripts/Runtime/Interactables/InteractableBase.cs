using UnityEngine;
using TMPro;


public class InteractableBase : MonoBehaviour, IInteractable
{
    [Header("UI Ayarları")]
    [Tooltip("World Space Canvas Objesi (Text'in babası)")]
    [SerializeField] private GameObject m_uiCanvasObject; 
    
    [Tooltip("Mesajın yazılacağı Text bileşeni")]
    [SerializeField] private TextMeshProUGUI m_promptText;
    
    [Tooltip("Inspector'dan ayarlanacak mesaj (Örn: Al [E])")]
    [SerializeField] private string m_promptMessage = "Interact [E]";
    
    protected virtual void Start()
    {
        if(m_promptText != null)
        {
            m_promptText.text = m_promptMessage;
        }
        
        TogglePrompt(false);
    }
    
    public void TogglePrompt(bool state)
    {
        if (m_uiCanvasObject != null)
        {
            m_uiCanvasObject.SetActive(state);
        }
    }
    
    public virtual void DoInteract()
    {
        Debug.Log("Etkileşim: " + gameObject.name);
    }
    
    public virtual void StopInteract()
    {
    }
    
    public void UpdateMessage(string newMessage)
    {
        m_promptMessage = newMessage;
        if(m_promptText != null) m_promptText.text = newMessage;
    }
}