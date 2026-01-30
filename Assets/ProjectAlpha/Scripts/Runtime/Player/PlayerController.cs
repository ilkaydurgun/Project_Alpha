using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputReader m_inputReader;
    [SerializeField] private CharacterController m_characterController;
    
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_rotationSpeed = 10f;
    [SerializeField] private Animator m_animator;
    private Vector2 m_moveInput;

    private IInteractable m_CurrentInteractable;

    private void OnEnable()
    {
        m_inputReader.OnMoveEvent += HandleMove;
        
        m_inputReader.OnInteractEvent += HandleInteract;
    }

    private void OnDisable()
    {
        m_inputReader.OnMoveEvent -= HandleMove;
        m_inputReader.OnInteractEvent -= HandleInteract;
    }



    void Update()
    {
        Move();
    }

    private void HandleMove(Vector2 input)
    {
        m_moveInput = input;
    }

    private void Move()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 move = (forward * m_moveInput.y) + (right * m_moveInput.x);

        m_characterController.Move(move * m_moveSpeed * Time.deltaTime);
        
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_rotationSpeed * Time.deltaTime);
        }
        
        if (m_animator != null)
        {
            m_animator.SetFloat("Speed", m_moveInput.magnitude, 0.15f, Time.deltaTime);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            m_CurrentInteractable = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (m_CurrentInteractable == interactable)
            {
                m_CurrentInteractable.StopInteract();
                m_CurrentInteractable = null;
            }
        }
    }

      private void HandleInteract(bool isPressed)
    {
        if (m_CurrentInteractable == null) return;

        if (isPressed)
        {
            m_CurrentInteractable.DoInteract();
        }
        else
        {
            m_CurrentInteractable.StopInteract();
        }
    }
}