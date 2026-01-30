using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    public enum ButtonType { Hold, Press }

    [Header("Button Settings")]
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private float m_HoldTime = 2f;
    [SerializeField] private List<GameObject> m_doorToOpen;

    private List<IActivatable> activatables = new List<IActivatable>();
    private bool m_IsHolding = false; 
    private Coroutine currentHoldRoutine; 

    private void Awake()
    {
        foreach (var obj in m_doorToOpen)
        {
            if (obj != null && obj.TryGetComponent(out IActivatable item))
                activatables.Add(item);
        }
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
            if (currentHoldRoutine != null) StopCoroutine(currentHoldRoutine);
            m_IsHolding = false;
            Debug.Log("Interact Cancelled");
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
        foreach (var item in activatables) item.Activate();
    }
}