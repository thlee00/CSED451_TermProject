using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    public Animator enemyAnimator;
    public WaypointPatrol waypointPatrol;
    public float stunDelay = 5.0f;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            enemyAnimator.SetBool("IsStunned", true);
            waypointPatrol.isStopped = true;
            StartCoroutine(Stun());
        }
    }

    IEnumerator Stun()
    {
        yield return new WaitForSeconds(stunDelay);
        enemyAnimator.SetBool("IsStunned", false);
        waypointPatrol.isStopped = false;
    }
}
