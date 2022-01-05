using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RpgAdventure
{



    public class PlayerController : MonoBehaviour
    {
        const float k_Acceleration = 20.0f;
        const float k_Deceleration = 800;

        public float maxForwardSpeed = 8.0f;

       

        private PlayerInput m_PlayerInput;
     
        private Animator m_Animator;
        private Camera m_MainCamera;

        private float m_DesiredForwardSpeed;
        private float m_ForwardSpeed;

        private readonly int m_HashForwardSpeed = Animator.StringToHash("ForwardSpeed");
        private void Awake()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_MainCamera = Camera.main;
            m_Animator = GetComponent<Animator>();

        }
        private void FixedUpdate()
        {
            //Vector3 moveInput = m_PlayerInput.moveInput;
            //Quaternion camRotation = m_MainCamera.transform.rotation;
            //Vector3 targetDirection = camRotation * moveInput;
            //targetDirection.y = 0;
           ComputeMovement();
        }
        private void ComputeMovement()
        {
            Vector3 moveInput = m_PlayerInput.moveInput.normalized;
            m_DesiredForwardSpeed = moveInput.magnitude * maxForwardSpeed;
            float acceleration = m_PlayerInput.IsMoveInput ? k_Acceleration : k_Deceleration;
            m_ForwardSpeed = Mathf.MoveTowards(
                m_ForwardSpeed,
                m_DesiredForwardSpeed,
                Time.fixedDeltaTime * acceleration);
        

            //m_ChController.Move(targetDirection.normalized * speed * Time.fixedDeltaTime);
         m_Animator.SetFloat(m_HashForwardSpeed, m_ForwardSpeed);
        }
    }
}