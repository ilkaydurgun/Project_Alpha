using UnityEngine;

public interface IInteractable
{
    void DoInteract();
}







   /* public enum ObjectType
    {
      Key,Button,Lever,Door,Chest
    }

    public ObjectType objectType;

    [SerializeField] private GameObject interactPrompt;

    private bool canInteract = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
           
            Debug.Log("Etkileşime girmek için E'ye bas!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("Etkileşimden çıktınız.");
        }
    }

    public void DoInteract()
    {
        switch (objectType)
        {
            case ObjectType.Key:
                Debug.Log("Key interacted");
                break;
           case ObjectType.Button:
                Debug.Log("Button interacted");
                break;
            case ObjectType.Lever:
                Debug.Log("Lever interacted");
                break;
            case ObjectType.Door:
                Debug.Log("Door interacted");
                break;
            case ObjectType.Chest:
                Debug.Log("Chest interacted");
                break;
       
        }




    }*/
