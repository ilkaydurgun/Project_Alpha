using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    private Vector2 moveInput;
    private Transform camTransform;
   
    private void Awake()
    {
        camTransform = Camera.main.transform;
    }
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
  
    Vector3 forward = Camera.main.transform.forward;
    Vector3 right = Camera.main.transform.right;

    
    forward.y = 0;
    right.y = 0;
    forward.Normalize();
    right.Normalize();


    Vector3 move = (forward * moveInput.y) + (right * moveInput.x);
    transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    if (move != Vector3.zero)
    {
        Quaternion targetRotation = Quaternion.LookRotation(move);
        transform.rotation = Quaternion.Slerp(transform.rotation,
         targetRotation, rotationSpeed * Time.deltaTime);
    
    }
    if (animator != null)
    {
      animator.SetFloat("Speed", moveInput.magnitude, 0.15f, Time.deltaTime);
    }
}
}
