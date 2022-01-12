using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RpgAdventure
{
    public class BanditBehaviour : MonoBehaviour
    {
        
        private PlayerController m_target;
        public float detectionRadius = 10;
        public float detectionAngle = 360;
        private EnemyController m_EnemyController;
        private float m_TimesSinceLostTarget = 0;
        public float TimetoStoppursuit=2.0f;
        private Vector3 originalPosition;
        private Animator m_animator;
        private Quaternion originalRotation;
        private readonly int m_HashinPursuit = Animator.StringToHash("InPursuit");
        private readonly int m_HashNearBase = Animator.StringToHash("NearBase");
        private readonly int m_HashAttack = Animator.StringToHash("Attack");
        private readonly int m_HashDistanceBetweenPlayer = Animator.StringToHash("NearPlayer");

        private float m_ForwardSpeed =0 ;
        private float m_DesiredSpeed;

        // Start is called before the first frame update
        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_EnemyController = GetComponent<EnemyController>();
            originalPosition = transform.position ;
            originalRotation = transform.rotation;
           
        }
       
        void Update()
        {
            var target = lookForPlayer();
            if (m_target == null)
            {
                if (target != null)
                {
                    m_target = target;
                  }
            }
            else
            {
                if(target == null)
                {
                    m_TimesSinceLostTarget += Time.deltaTime;
                    if (m_TimesSinceLostTarget > TimetoStoppursuit)
                    {
                        target = null;
                     
                        m_animator.SetBool(m_HashinPursuit, false);

                        StartCoroutine(WaitOnPursuit());

                      //  m_animator.SetFloat(m_HashForwardSpeed, m_navemeshAgent.speed);

                   //     Debug.Log(originalPosition * m_ForwardSpeed);
                    }
                }
                else
                {
                    m_TimesSinceLostTarget = 0;


                    m_EnemyController.SetFollowTarget(m_target.transform.position);
                    m_animator.SetBool(m_HashinPursuit, true);
                  
                }
               
            }

            Vector3 toBase = originalPosition - transform.position;
            toBase.y = 0;
            
            m_animator.SetBool(m_HashNearBase, toBase.magnitude < 0.01f);
           //  Debug.Log(toBase.magnitude);
            bool nearbase = toBase.magnitude < 0.01f;
            if(nearbase)
            {
                Quaternion targetRotation = Quaternion.RotateTowards(transform.rotation, originalRotation, 360 * Time.fixedDeltaTime);
                transform.rotation = targetRotation;
            }
            Vector3 DistancebetweenPlayer = PlayerController.instance.transform.position - transform.position;
            if(DistancebetweenPlayer.magnitude <= 2.0f)
            {
                m_animator.SetBool(m_HashDistanceBetweenPlayer, true);
             //   m_animator.SetBool(m_HashinPursuit, false);

                m_animator.SetTrigger(m_HashAttack);
       //         m_animator.SetBool(m_HashinPursuit, false);

            }
            else
            {
                m_animator.SetBool(m_HashDistanceBetweenPlayer, false);
            }
        //    Debug.Log(DistancebetweenPlayer.magnitude);
           
        }
        private IEnumerator WaitOnPursuit()
        {
            yield return new WaitForSeconds(2.0f);

             //m_animator.SetBool(m_HashinPursuit, tr);
            m_EnemyController.SetFollowTarget(originalPosition );
        }
        public PlayerController lookForPlayer()
        {
            if (PlayerController.instance == null)
            {
                return null;
            }
            Vector3 EnemyPosition = transform.position;
            Vector3 toPlayer = PlayerController.instance.transform.position - EnemyPosition;
            toPlayer.y = 0;
            if (toPlayer.magnitude <= detectionRadius)
            {
                if (Vector3.Dot(toPlayer.normalized, transform.forward) > Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                {

                    return PlayerController.instance;
                  //  m_animator.SetBool(m_HashinPursuit, false);
                    //Debug.Log("toplayerNormalized" + toPlayer.normalized);
                    //Debug.Log("transformForward" + transform.forward);
                    //Debug.Log("Dotvector" + Vector3.Dot(toPlayer.normalized, transform.forward));
                    //Debug.Log("cos value" + Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad));
                    //Debug.Log("Detecting player");
                }
                //else
                //{
                //    Debug.Log("toplayerNormalized" + toPlayer.normalized);
                //    Debug.Log("transformForward" + transform.forward);
                //    Debug.Log("Dotvector" + Vector3.Dot(toPlayer.normalized, transform.forward));
                //    Debug.Log("cos value" + Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad));
                //}
                //
            }

            return null;
        }

        private void OnDrawGizmosSelected()
        {
              Color c = new Color(0, 0, 0.7f, 0.4f);
            UnityEditor.Handles.color = c;
            Vector3 rotatedforward = Quaternion.Euler(0, -detectionAngle * 0.5f, 0)*transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedforward, detectionAngle, detectionRadius);
        }

    }
}
