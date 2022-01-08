using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RpgAdventure
{
    public class BanditBehaviour : MonoBehaviour
    {
        public float detectionRadius;
        public float detectionAngle;
        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log(PlayerController.instance);
        }
        
        private void Update()
        {
            lookForPlayer();
        }
        private PlayerController lookForPlayer()
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
                if(Vector3.Dot(toPlayer.normalized,transform.forward)>Mathf.Cos(detectionAngle*0.5f*Mathf.Deg2Rad))
                {
                    Debug.Log("toplayerNormalized" + toPlayer.normalized);
                    Debug.Log("transformForward" + transform.forward);
                    Debug.Log("Dotvector" + Vector3.Dot(toPlayer.normalized, transform.forward));
                    Debug.Log("cos value" + Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad));
                    //Debug.Log("Detecting player");
                }
                //Debug.Log("toplayerNormalized" + toPlayer.normalized);
                //Debug.Log("transformForward" + transform.forward);
                //Debug.Log("Dotvector" + Vector3.Dot(toPlayer.normalized, transform.forward));
                //Debug.Log("cos value" + Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad));
            }
           
            return null;
        }
       
        private void OnDrawGizmosSelected()
        {
              Color c = new Color(0, 0, 0.7f, 0.4f);
            UnityEditor.Handles.color = c;
            Vector3 rotatedforward = Quaternion.Euler(0, -detectionAngle*0.5f , 0)*transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedforward, detectionAngle, detectionRadius);
        }

    }
}
