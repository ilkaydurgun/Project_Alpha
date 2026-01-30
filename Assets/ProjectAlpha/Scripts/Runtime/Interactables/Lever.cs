using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> objectsToControl; 
    private List<IActivatable> activatables = new List<IActivatable>(); 
    private bool isDown = false; 

    private void Awake()
    {
        foreach (var obj in objectsToControl)
        {
            if (obj != null && obj.TryGetComponent(out IActivatable item))
            {
                activatables.Add(item);
            }
        }
    }

    public void DoInteract()
    {
        isDown = !isDown;

        foreach (var item in activatables)
        {
            if (isDown)
                item.Activate();
            else
                item.Deactivate();
        }
    }

    public void StopInteract()
    {
        
    }
}