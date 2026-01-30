using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : InteractableBase
{
    public enum ButtonType { Hold, Press }
    
    [Header("Button Settings")]
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private float m_HoldTime = 2f;
    [SerializeField] private List<GameObject> m_objectsToControl;
    
    private List<IActivatable> activatables = new List<IActivatable>();
    private bool m_IsHolding = false; 
    private Coroutine currentHoldRoutine; 
    
    protected override void Start()
    {
        base.Start();
        
        foreach (var obj in m_objectsToControl)
        {
            if (obj != null && obj.TryGetComponent(out IActivatable item))
                activatables.Add(item);
        }
        
        UpdateMessage(buttonType == ButtonType.Hold ? "Hold [E]" : "Press [E]");
    }
    
    public override void DoInteract()
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
    
    public override void StopInteract()
    {
        if (buttonType == ButtonType.Hold && m_IsHolding)
        {
            if (currentHoldRoutine != null) StopCoroutine(currentHoldRoutine);
            m_IsHolding = false;
            Debug.Log("Button Released");
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
        foreach (var item in activatables) item.Activate();
    }
}