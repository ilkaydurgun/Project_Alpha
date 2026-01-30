using UnityEngine;

public class Door : MonoBehaviour, IInteractable, IActivatable
{
    private DoorState m_currentState;

    public enum DoorState
    {
        Open,
        Closed,
        Locked
    }
    public void DoInteract()
    {
       if (m_currentState == DoorState.Locked)
        {
            Debug.Log("The door is locked. You need a key to open it.");
            return;
        }

        if (m_currentState == DoorState.Closed)
        {
            m_currentState = DoorState.Open;
            Debug.Log("You opened the door.");
        }
        else if (m_currentState == DoorState.Open)
        {
            m_currentState = DoorState.Closed;
            Debug.Log("You closed the door.");
        }
    }

    public void Activate()
    {
        
    }

    public void Deactivate()
    {
       
    }

    public void StopInteract()
    {
        
    }
}

