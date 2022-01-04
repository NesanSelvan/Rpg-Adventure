using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    namespace RpgAdventure
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController m_Cc;
        

        // Start is called before the first frame update
        public float speed;
        public float rotationSpeed;
        private Vector3 m_Movement;
        private PlayerInput m_playerInput;
        private Quaternion m_Rotation;
        public Camera m_MainCamera;
        private void Start()
        {
            m_Cc = GetComponent<CharacterController>();
            m_playerInput = GetComponent<PlayerInput>();
         //   m_MainCamera = Camera.main;
        }
        private void FixedUpdate()
    {
            Vector3 moveInput = m_playerInput.moveInput;
             Quaternion camRotation = m_MainCamera.transform.rotation;
            Vector3 targetDirection = camRotation * moveInput;
            targetDirection.y = 0;
            targetDirection.Normalize();
            

         //   Vector3 desiredForward = Vector3.RotateTowards(
                      //    transform.forward,
                    //      m_Movement,
                  //        Time.fixedDeltaTime * rotationSpeed,
                //          0
              //        );
            //m_Rotation = Quaternion.LookRotation(desiredForward);


            m_Cc.Move( targetDirection * speed * Time.fixedDeltaTime);
            m_Cc.transform.rotation = Quaternion.Euler(0, camRotation.eulerAngles.y, 0);
           // m_rigidbody.MoveRotation(m_Rotation);
        }
}
    }

    // Update is called once per frame
    
