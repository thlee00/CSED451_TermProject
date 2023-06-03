using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransperentPotion : MonoBehaviour
{
    public KeyCode itemKey = KeyCode.Space;
    public GameObject player;
    bool m_IsPlayerInRange;

    PlayerTransperent playerTransperent;

    // Start is called before the first frame update
    void Start()
    {
        playerTransperent = player.GetComponent<PlayerTransperent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(itemKey) && m_IsPlayerInRange)
        {
            playerTransperent.ObtainPotion();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            m_IsPlayerInRange = false;
        }
    }
}