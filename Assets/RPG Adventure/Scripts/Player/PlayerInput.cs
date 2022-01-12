using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerInput : MonoBehaviour
    {
        // Start is called before the first frame update
        private Vector3 m_Movement;
        private bool m_IsAttack;
        public Vector3 moveInput
        {
            get
            {
                return m_Movement;
            }
        }
        public bool IsMoveInput
        {
            get
            {
                return ! Mathf.Approximately(moveInput.magnitude, 0);
            }
        }
        public bool IsAttack
        {
           get           
           {
                return m_IsAttack;
            }
        }
        void Update()
        {
            m_Movement.Set(Input.GetAxis("Horizontal"),0,  Input.GetAxis("Vertical"));
        if(Input.GetButtonDown("Fire1") && !m_IsAttack )
            {
                StartCoroutine(AttackAndWait());
            }
        
        }
        private IEnumerator AttackAndWait()
        {
            m_IsAttack = true;
            yield return new WaitForSeconds(0.02f);
            m_IsAttack = false;
        }
    }
}
