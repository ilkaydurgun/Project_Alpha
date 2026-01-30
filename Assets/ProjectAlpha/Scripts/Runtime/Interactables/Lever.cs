using System.Collections.Generic;
using UnityEngine;

public class Lever : InteractableBase
{
    [SerializeField] private List<GameObject> objectsToControl; 
    private List<IActivatable> activatables = new List<IActivatable>(); 
    private bool isDown = false; 
    
    protected override void Start()
    {
        base.Start();
        
        foreach (var obj in objectsToControl)
        {
            if (obj != null && obj.TryGetComponent(out IActivatable item))
            {
                activatables.Add(item);
            }
        }
        UpdateMessage("Pull Lever [E]");
    }
    
    public override void DoInteract()
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
}