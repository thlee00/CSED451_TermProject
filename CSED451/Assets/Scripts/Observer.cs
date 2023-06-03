using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    PlayerTransperent playerTransperent;

    bool m_IsPlayerInRange;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        playerTransperent = player.gameObject.GetComponent<PlayerTransperent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if(raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player && !playerMovement.invincible && !playerTransperent.isPlayerTransperented)
        {
            m_IsPlayerInRange = true;
        }
        else if (playerTransperent.isPlayerTransperented)
            print("TRANSPERENTED");
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
}
