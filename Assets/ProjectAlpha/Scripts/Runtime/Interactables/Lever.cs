using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> objectsToControl;
    
    [Header("UI Settings")]
    [SerializeField] private GameObject m_uiCanvasObject;
    [SerializeField] private TextMeshProUGUI m_promptText;
    [SerializeField] private Transform m_uiPoint; 
    [SerializeField] private string m_promptMessage = "Pull Lever [E]";

    private List<IActivatable> activatables = new List<IActivatable>();
    private bool isDown = false;

    private void Start()
    {
        foreach (var obj in objectsToControl)
        {
            if (obj != null && obj.TryGetComponent(out IActivatable item))
            {
                activatables.Add(item);
            }
        }
        
        UpdateMessage("Pull Lever [E]");

        if (m_uiPoint != null && m_uiCanvasObject != null)
        {
            m_uiCanvasObject.transform.position = m_uiPoint.position;
        }

        TogglePrompt(false);
    }

    public void DoInteract()
    {
        isDown = !isDown;

        foreach (var item in activatables)
        {
            if (isDown)
            {
                item.Activate();
            }
            else
            {
                item.Deactivate();
            }
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