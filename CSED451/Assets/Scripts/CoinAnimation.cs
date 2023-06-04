using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : ThrowableAnimation
{
    GameObject[] m_enemies;
    EnemyAggro m_enemyAggro;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Map") || col.gameObject.CompareTag("Enemy"))
        {
            m_enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in m_enemies)
            {
                m_enemyAggro = enemy.GetComponent<EnemyAggro>();
                if (m_enemyAggro != null)
                {
                    m_enemyAggro.TryAggro(transform.position);
                }
            }
            PlayHit();
            Destroy(gameObject);
        }
    }
}
