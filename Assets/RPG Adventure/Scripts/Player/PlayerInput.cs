using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RpgAdventure
{
    public class PlayerInput : MonoBehaviour
    {
        // Start is called before the first frame update
        private Vector3 m_Movement;
        public Vector3 moveInput
        {
            get
            {
                return m_Movement;
            }
        }
        // Update is called once per frame
        void Update()
        {
            m_Movement.Set(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
        }
    }
}
