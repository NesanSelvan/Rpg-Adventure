using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Target;
    private Vector3 m_offset;
    

    // Update is called once per frame
    void LateUpdate()
    {
    if(! Target)
        {
            return;
        }
        float currentRotation = transform.eulerAngles.y;
        float wantedRotation = Target.eulerAngles.y;
        currentRotation = Mathf.LerpAngle(currentRotation, wantedRotation, 0.5f);
        transform.position = new Vector3(Target.position.x, 5.0f, Target.position.z);
        Quaternion currentRotationangle = Quaternion.Euler(0, currentRotation, 0);
        Vector3 rotatedposition = currentRotationangle * Vector3.forward;
        transform.position -= rotatedposition * 10;
        transform.LookAt(Target);
    }
}
