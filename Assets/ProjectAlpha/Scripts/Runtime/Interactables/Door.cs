using UnityEngine;
using TMPro;

public class Door : MonoBehaviour, IInteractable, IActivatable
{
    public enum DoorState { Locked, Closed, Open }

    [Header("Door Settings")]
    [SerializeField] private DoorState m_currentState = DoorState.Closed;
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
        UpdateDoorUI();

        if (m_uiPoint != null && m_uiCanvasObject != null)
        {
            m_uiCanvasObject.transform.position = m_uiPoint.position;
        }

        TogglePrompt(false);
    }

    public void DoInteract()
    {
        switch (m_currentState)
        {
            case DoorState.Locked:
                TryUnlock();
                break;

            case DoorState.Closed:
                OpenDoor();
                break;

            case DoorState.Open:
                CloseDoor();
                break;
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

    private void TryUnlock()
    {
        if (m_requiredKey != null)
        {
            if (PlayerInventory.Instance.HasItem(m_requiredKey))
            {
                m_currentState = DoorState.Closed;
                UpdateDoorUI();
            }
            else
            {
                Debug.Log("Key needed!");
            }
        }
        else
        {
            m_currentState = DoorState.Closed;
            UpdateDoorUI();
        }
    }

    private void OpenDoor()
    {
        m_currentState = DoorState.Open;
        if (m_animator != null)
        {
            m_animator.SetBool("IsOpen", true);
        }
        UpdateDoorUI();
    }

    private void CloseDoor()
    {
        m_currentState = DoorState.Closed;
        if (m_animator != null)
        {
            m_animator.SetBool("IsOpen", false);
        }
        UpdateDoorUI();
    }

    private void UpdateDoorUI()
    {
        switch (m_currentState)
        {
            case DoorState.Locked:
                UpdateMessage("Unlock Door [E]");
                break;
            case DoorState.Closed:
                UpdateMessage("Open Door [E]");
                break;
            case DoorState.Open:
                UpdateMessage("Close Door [E]");
                break;
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

    public void Activate()
    {
        if (m_currentState != DoorState.Open)
        {
            OpenDoor();
        }
    }

    public void Deactivate()
    {
        if (m_currentState == DoorState.Open)
        {
            CloseDoor();
        }
    }
}