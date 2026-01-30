using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;
   
    private void OnEnable()
    {
        inputReader.OnMoveEvent += HandleMove;
        inputReader.OnInteractEvent += HandleInteract;
    }
    private void OnDisable()
    {
        inputReader.OnMoveEvent -= HandleMove;
        inputReader.OnInteractEvent -= HandleInteract;
    }
    
    void Update()
    {
        Move();
    }
    private void HandleInteract()
    {
        Debug.Log("Interact pressed");  
    }
  
    private void HandleMove(Vector2 input   )
    {
        moveInput = input;
    }



    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }
}
