using UnityEngine;

public class Chest : InteractableBase
{
    [Header("Chest Settings")]
    [SerializeField] private bool m_isLocked = true;
    [SerializeField] private ItemData m_requiredKey;
    
    [Header("Visuals")]
    [SerializeField] private Animator m_animator;
    
    protected override void Start()
    {
        base.Start();
        
        if (m_isLocked)
            UpdateMessage("Unlock Chest [E]");
        else
            UpdateMessage("Open Chest [E]");
    }
    
    public override void DoInteract()
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
}