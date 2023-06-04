using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    public Animator enemyAnimator;
    public WaypointPatrol waypointPatrol;
    public float stunTime = 6.0f;
    public float recoverTime = 3.0f;
    public Collider povCol;

    public bool isStunned;

    void Start()
    {
        isStunned = false;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shell") && !isStunned)
        {
            enemyAnimator.SetBool("IsStunned", true);
            waypointPatrol.isStopped = true;
            povCol.enabled = false;
            isStunned = true;
            StartCoroutine(Recover(recoverTime));
            StartCoroutine(Stun(stunTime));
        }
    }

    IEnumerator Stun(float duration)
    {
        yield return new WaitForSeconds(duration);
        enemyAnimator.SetBool("IsStunned", false);
        waypointPatrol.isStopped = false;
        povCol.enabled = true;
        isStunned = false;
    }

    IEnumerator Recover(float duration)
    {
        yield return new WaitForSeconds(duration);
        enemyAnimator.SetTrigger("TriggerRecover");
    }
}
