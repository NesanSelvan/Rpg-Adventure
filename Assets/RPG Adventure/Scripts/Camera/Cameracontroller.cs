using Cinemachine;
using UnityEngine;

public class Cameracontroller : MonoBehaviour
{
    [SerializeField]
    CinemachineFreeLook freeLookCamera;    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 400;
            freeLookCamera.m_YAxis.m_MaxSpeed = 5;
        }
        if (Input.GetMouseButtonUp(0))
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = 0;
            freeLookCamera.m_YAxis.m_MaxSpeed = 0;
        }
    
    }
}
