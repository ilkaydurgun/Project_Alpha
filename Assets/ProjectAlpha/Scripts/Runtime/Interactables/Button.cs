using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour, IInteractable
{
    public enum ButtonType { Hold, Press }

    [Header("Button Settings")]
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private float m_HoldTime = 2f;
    [SerializeField] private List<GameObject> m_objectsToControl;

    [Header("UI Settings")]
    [SerializeField] private GameObject m_uiCanvasObject;
    [SerializeField] private TextMeshProUGUI m_promptText;
    [SerializeField] private Transform m_uiPoint; 
    [SerializeField] private string m_promptMessage = "Interact [E]";

    private List<IActivatable> activatables = new List<IActivatable>();
    private bool m_IsHolding = false;
    private Coroutine currentHoldRoutine;

    private void Start()
    {
        foreach (var obj in m_objectsToControl)
        {
            if (obj != null && obj.TryGetComponent(out IActivatable item))
            {
                activatables.Add(item);
            }
        }

        if (buttonType == ButtonType.Hold)
        {
            UpdateMessage("Hold [E]");
        }
        else
        {
            UpdateMessage("Press [E]");
        }
        
        if (m_uiPoint != null && m_uiCanvasObject != null)
        {
            m_uiCanvasObject.transform.position = m_uiPoint.position;
        }

        TogglePrompt(false);
    }

    public void DoInteract()
    {
        if (buttonType == ButtonType.Press)
        {
            ActivateAll();
        }
        else if (buttonType == ButtonType.Hold)
        {
            if (!m_IsHolding)
            {
                currentHoldRoutine = StartCoroutine(HoldProcess());
            }
        }
    }

    public void StopInteract()
    {
        if (buttonType == ButtonType.Hold && m_IsHolding)
        {
            if (currentHoldRoutine != null)
            {
                StopCoroutine(currentHoldRoutine);
            }
            m_IsHolding = false;
            Debug.Log("Button Released");
        }
    }

    public void TogglePrompt(bool state)
    {
        if (m_uiCanvasObject != null)
        {
            m_uiCanvasObject.SetActive(state);
        }
    }

    private IEnumerator HoldProcess()
    {
        m_IsHolding = true;
        yield return new WaitForSeconds(m_HoldTime);
        ActivateAll();
        m_IsHolding = false;
    }

    private void ActivateAll()
    {
        Debug.Log("Button Activated!");
        foreach (var item in activatables)
        {
            item.Activate();
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