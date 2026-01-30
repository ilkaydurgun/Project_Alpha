using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputReader m_inputReader;
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_rotationSpeed = 10f;
    [SerializeField] private Animator m_animator;
    [SerializeField] private float gravity = -19.62f; 

    private float m_verticalVelocity;
    private Vector2 m_moveInput;
    private IInteractable m_CurrentInteractable; 
    
    private void OnEnable()
    {
        m_inputReader.OnInteractEvent += HandleInteract;
        m_inputReader.OnMoveEvent += HandleMove;
    }
    
    private void OnDisable()
    {
        m_inputReader.OnInteractEvent -= HandleInteract;
        m_inputReader.OnMoveEvent -= HandleMove;
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
        
        if (m_characterController.isGrounded)
        {
            m_verticalVelocity = -2f;
        }
        else
        {
            m_verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalVelocity = move * m_moveSpeed;
        finalVelocity.y = m_verticalVelocity;
        
        m_characterController.Move(finalVelocity * Time.deltaTime);
        
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
            m_CurrentInteractable.TogglePrompt(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (m_CurrentInteractable == interactable)
            {
                m_CurrentInteractable.TogglePrompt(false);
                m_CurrentInteractable.StopInteract();
                m_CurrentInteractable = null;
            }
        }
    }
    
    private void HandleInteract(bool isPressed)
    {
        if (m_CurrentInteractable == null) return;
        
        if (isPressed)
            m_CurrentInteractable.DoInteract();
        else
            m_CurrentInteractable.StopInteract();
    }
}