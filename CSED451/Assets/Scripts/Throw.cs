using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Transform handTransform;
    public float minHorizontalForce = 2.0f;
    public float maxHorizontalForce = 5.0f;
    public float verticalForce = 5.0f;
    public GameObject coinPrefab;
    Vector3 m_handPos;
    // Start is called before the first frame update
    void Start()
    {
        m_handPos = handTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(float percent)
    {
        m_handPos = handTransform.position;
        GameObject coin = Instantiate(coinPrefab, m_handPos, transform.rotation);
        Rigidbody coinRigid = coin.GetComponent<Rigidbody>();
        Vector3 force = (maxHorizontalForce - minHorizontalForce) * percent * transform.forward + transform.up * verticalForce;
        coinRigid.AddForce(force, ForceMode.Impulse);
    }
}
