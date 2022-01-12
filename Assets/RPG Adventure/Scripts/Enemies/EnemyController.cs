using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Animator m_animator;
    private NavMeshAgent m_navmeshAgent;
    private float m_speedmodifier = 0.7f;
    // Start is called before the first frame update
    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_navmeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    
  private void OnAnimatorMove()
{
        if (m_navmeshAgent.enabled)
        {
            m_navmeshAgent.speed = (m_animator.deltaPosition / Time.fixedDeltaTime).magnitude * m_speedmodifier;
        }
    }
    public bool SetFollowTarget(Vector3 position)
    {
        if (!m_navmeshAgent.enabled)
        {
            m_navmeshAgent.enabled = true;
        }
       return m_navmeshAgent.SetDestination(position);
    }
    public void StopFollowTarget()
    {
        m_navmeshAgent.enabled = false;
    }
}
