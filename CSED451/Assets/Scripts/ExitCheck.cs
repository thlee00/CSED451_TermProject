using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCheck : MonoBehaviour
{
    GameEnding m_gameEnding;
    
    void Start()
    {
        m_gameEnding = GetComponentInParent<GameEnding>();
    }

    void OnTriggerEnter(Collider other)
    {
        m_gameEnding.ExitPlayer(other);
    }
}
