using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAnimation : MonoBehaviour
{
    public float rotationSpeed = 10f;
    float m_angle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_angle += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, m_angle, 0);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Map") || col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
