using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody m_rigidbody;

        // Start is called before the first frame update
        public float speed;
        public float rotationSpeed;
        private Vector3 m_Movement;
        private Quaternion m_Rotation;
        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
            m_Movement = new Vector3(horizontalInput, 0, verticalInput);
            m_Movement.Normalize();
            Vector3 desiredForward = Vector3.RotateTowards(
                          transform.forward,
                          m_Movement,
                          Time.fixedDeltaTime * rotationSpeed,
                          0
                      );
            m_Rotation = Quaternion.LookRotation(desiredForward);


            m_rigidbody.MovePosition(m_rigidbody.position + m_Movement * speed * Time.fixedDeltaTime);
            m_rigidbody.MoveRotation(m_Rotation);
        }
}
    }

    // Update is called once per frame
    
