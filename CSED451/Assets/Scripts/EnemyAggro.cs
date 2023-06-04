using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public float aggroDistance = 5.0f;
    public float aggroTime = 4.0f;
    public float turnSpeed = 0.015f;
    public EnemyStun enemyStun;
    WaypointPatrol m_waypointPatrol;
    bool m_isAggro;
    Vector3 m_targetPosition;
    Vector3 m_targetDirection;
    Quaternion m_rotGoal;
    // Start is called before the first frame update
    void Start()
    {
        m_waypointPatrol = GetComponent<WaypointPatrol>();
        m_isAggro = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isAggro)
        {
            m_targetDirection = (m_targetPosition - transform.position).normalized;
            m_rotGoal = Quaternion.LookRotation(m_targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, m_rotGoal, turnSpeed);
        }
    }

    public void TryAggro(Vector3 aggroPos) {
        if (Vector3.Distance(aggroPos, transform.position) <= aggroDistance && !m_isAggro && !enemyStun.isStunned)
        {
            Aggro(aggroPos);
        }
    }

    void Aggro(Vector3 aggroPos) {
        m_waypointPatrol.isStopped = true;
        m_isAggro = true;
        m_targetPosition = aggroPos;
        StartCoroutine(Delay(aggroTime));
    }
    IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (!enemyStun.isStunned) m_waypointPatrol.isStopped = false;
        m_isAggro = false;
    }
}
