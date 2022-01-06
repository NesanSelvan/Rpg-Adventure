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
        private Cameracontroller m_cameraController;

        private float m_DesiredForwardSpeed;
        private float m_ForwardSpeed;
        private Quaternion m_targetRotation;
        private CharacterController m_chController;
        //private Quaternion TargetRotation;

        private readonly int m_HashForwardSpeed = Animator.StringToHash("ForwardSpeed");
        private void Awake()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_cameraController = GetComponent<Cameracontroller>();
            m_Animator = GetComponent<Animator>();
            m_chController = GetComponent<CharacterController>();

        }
        private void FixedUpdate()
        {
            //Vector3 moveInput = m_PlayerInput.moveInput;
            //Quaternion camRotation = m_MainCamera.transform.rotation;
            //Vector3 targetDirection = camRotation * moveInput;
            //targetDirection.y = 0;
           ComputeMovement();
           ComputeRotation();
            if (m_PlayerInput.IsMoveInput){
                float rotationSpeed = Mathf.Lerp(1200, 400, m_ForwardSpeed / m_DesiredForwardSpeed);
                m_targetRotation = Quaternion.RotateTowards(transform.rotation, m_targetRotation, rotationSpeed * Time.fixedDeltaTime);
                transform.rotation = m_targetRotation;
            }
        }
      private void OnAnimatorMove()
      {
            m_chController.Move(m_Animator.deltaPosition);
        }
        private void ComputeMovement()
        {
            Vector3 MoveInput = m_PlayerInput.moveInput.normalized;
            m_DesiredForwardSpeed = MoveInput.magnitude * maxForwardSpeed;
            float acceleration = m_PlayerInput.IsMoveInput ? k_Acceleration : k_Deceleration;
            m_ForwardSpeed = Mathf.MoveTowards(
                m_ForwardSpeed,
                m_DesiredForwardSpeed,
                Time.fixedDeltaTime * acceleration);
        

           // m_ChController.Move(targetDirection.normalized * speed * Time.fixedDeltaTime);
         m_Animator.SetFloat(m_HashForwardSpeed, m_ForwardSpeed);
        }
        private void ComputeRotation()
        {
            Vector3 MoveInput = m_PlayerInput.moveInput.normalized;
            Vector3 CameraDirection = Quaternion.Euler(0, m_cameraController.freeLookCamera.m_XAxis.Value, 0) * Vector3.forward;
            Quaternion TargetRotation;
            if(Mathf.Approximately( Vector3.Dot(MoveInput,Vector3.forward),-1.0f))
                {
                 TargetRotation = Quaternion.LookRotation(- CameraDirection);
            }
            else
            {
                Quaternion moveRotation = Quaternion.FromToRotation(Vector3.forward, MoveInput);
                 TargetRotation = Quaternion.LookRotation(moveRotation * CameraDirection);
            }
           m_targetRotation = TargetRotation;


        }
    }
}