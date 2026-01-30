using UnityEngine;
using Unity.Cinemachine;

public class CameraInputHandler : MonoBehaviour
{
    [SerializeField] private PlayerInputReader inputReader;
    [SerializeField] private CinemachineCamera virtualCamera;
    [SerializeField] private float sensitivityX = 1.0f;
    [SerializeField] private float sensitivityY = 1.0f;

    private Vector2 lookInput;

   private void OnEnable()
    {
        if (inputReader != null)
        {
            inputReader.OnLookEvent += HandleLook;
        }
    }

    private void OnDisable()
    {
        if (inputReader != null)
        {
            inputReader.OnLookEvent -= HandleLook;
        }
    }

    private void HandleLook(Vector2 lookDelta)
    {
        if (virtualCamera == null) return;

     
        virtualCamera.OnTargetObjectWarped(transform, Vector3.zero); 
        
 
    }
}