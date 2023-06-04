using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public Transform handTransform;
    public float minHorizontalForce = 1.0f;
    public float maxHorizontalForce = 4.0f;
    public float verticalForce = 5.0f;
    public GameObject coinPrefab;
    public GameObject shellPrefab;
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

    public void Use(float percent, int itemCode)
    {
        m_handPos = handTransform.position;
        GameObject toThrow;
        if (itemCode == 0) toThrow = coinPrefab;
        else if (itemCode == 1) toThrow = shellPrefab;
        else return; // error
        GameObject proj = Instantiate(toThrow, m_handPos, transform.rotation);
        Rigidbody projRigid = proj.GetComponent<Rigidbody>();
        Vector3 force = ((maxHorizontalForce - minHorizontalForce) * percent + minHorizontalForce) * transform.forward + transform.up * verticalForce;
        projRigid.AddForce(force, ForceMode.Impulse);
    }
}
