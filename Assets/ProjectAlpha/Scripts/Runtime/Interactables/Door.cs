using UnityEngine;

public class Door : InteractableBase, IActivatable
{
    public enum DoorState { Locked, Closed, Open }
    
    [Header("Door Settings")]
    [SerializeField] private DoorState m_currentState = DoorState.Closed;
    [SerializeField] private ItemData m_requiredKey;
    
    [Header("Visuals")]
    [SerializeField] private Animator m_animator;
    
    protected override void Start()
    {
        base.Start();
        UpdateDoorUI();
    }
    
    public override void DoInteract()
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
        if(m_animator != null) m_animator.SetBool("IsOpen", true);
        UpdateDoorUI();
    }
    
    private void CloseDoor()
    {
        m_currentState = DoorState.Closed;
        if(m_animator != null) m_animator.SetBool("IsOpen", false);
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
    
    public void Activate()
    {
        if (m_currentState != DoorState.Open) OpenDoor();
    }
    
    public void Deactivate()
    {
        if (m_currentState == DoorState.Open) CloseDoor();
    }
}