using Unity.VisualScripting;
using UnityEngine;
using static PressurePlate;

public class PressurePlate : MonoBehaviour, IInteractable
{
    public enum PlateType
    {
        Step,
        Hold,
    }
    public PlateType plateType;
    public bool isTrue ;
    public void DoInteract()
    {
       if (plateType == PlateType.Step)
        {
          if (!isTrue)
          {
             Debug.Log("Wrong pressure plate type.");
          }
          else
          {
            Debug.Log("Correct pressure plate type!");
          }
        }
        else if (plateType == PlateType.Hold)
        {
          
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        DoInteract();
    }

    public void StopInteract()
    {
        
    }
}
