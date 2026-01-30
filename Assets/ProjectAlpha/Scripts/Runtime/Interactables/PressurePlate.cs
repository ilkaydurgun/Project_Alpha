
using UnityEngine;
using static PressurePlate;

public class PressurePlate : MonoBehaviour
{
    public enum PlateType
    {
        Step,
        Hold,
    }
    public PlateType plateType;
    public bool isTrue ;
  

    private void OnTriggerEnter(Collider other)
    {
       
    }
    private void OnTriggerExit(Collider other)
    {
       
    }
}
