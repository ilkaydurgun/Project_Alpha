using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerInputReader m_inputReader;
    [SerializeField] private CharacterController m_characterController;
    [SerializeField] private Animator m_animator;
    
    [Header("Interaction Settings")]
    [SerializeField] private Transform m_cameraTransform; 
    [SerializeField] private float m_interactionRange = 3f;
    [SerializeField] private LayerMask m_interactionLayerMask; 

    [Header("Movement Settings")]
    [SerializeField] private float m_moveSpeed = 5f;
    [SerializeField] private float m_rotationSpeed = 10f;
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
        HandleInteractionCheck();
    }
    
    private void HandleMove(Vector2 input)
    {
        m_moveInput = input;
    }
    
    private void Move()
    {
        Vector3 forward = m_cameraTransform.forward;
        Vector3 right = m_cameraTransform.right;
        
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

    private void HandleInteractionCheck()
    {
        Ray ray = new Ray(m_cameraTransform.position, m_cameraTransform.forward);
        RaycastHit hit;

        Debug.DrawRay(m_cameraTransform.position, m_cameraTransform.forward * m_interactionRange, Color.red);

        if (Physics.Raycast(ray, out hit, m_interactionRange, m_interactionLayerMask))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                if (m_CurrentInteractable != interactable)
                {
                    if (m_CurrentInteractable != null)
                    {
                        m_CurrentInteractable.TogglePrompt(false);
                    }

                    m_CurrentInteractable = interactable;
                    m_CurrentInteractable.TogglePrompt(true);
                }
            }
            else
            {
                ClearCurrentInteractable();
            }
        }
        else
        {
            ClearCurrentInteractable();
        }
    }

    private void ClearCurrentInteractable()
    {
        if (m_CurrentInteractable != null)
        {
            m_CurrentInteractable.TogglePrompt(false);
            m_CurrentInteractable = null;
        }
    }
    
    private void HandleInteract(bool isPressed)
    {
        if (m_CurrentInteractable == null)
        {
            return;
        }
        
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