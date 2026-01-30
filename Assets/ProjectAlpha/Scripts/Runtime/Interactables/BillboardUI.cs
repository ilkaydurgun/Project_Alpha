using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform m_camTransform;
    
    void Start()
    {
        m_camTransform = Camera.main.transform;
    }
    
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_camTransform.forward);
    }
}