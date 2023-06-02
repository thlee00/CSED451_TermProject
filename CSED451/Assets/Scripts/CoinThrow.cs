using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinThrow : MonoBehaviour
{
    public int numCoin = 5;
    public float coolDown = 2.0f;
    public float minHorizontalForce = 3.0f;
    public float maxHorizontalForce = 15.0f;
    public float verticalForce = 5.0f;
    public KeyCode throwKey = KeyCode.Mouse0;
    public Transform handTransform;
    public GameObject coinPrefab;
    Vector3 m_handPos;
    bool m_ready;
    bool m_isCharging;
    float m_horizontalForce;
    // Start is called before the first frame update
    void Start()
    {
        m_handPos = handTransform.position;
        m_ready = true;
        m_isCharging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isCharging) {
            Charging();
        }

        if (Input.GetKeyDown(throwKey) && m_ready && numCoin > 0)
        {
            m_horizontalForce = minHorizontalForce;
            m_isCharging = true;
        }
        else if (Input.GetKeyUp(throwKey))
        {
            if (m_isCharging) Throw();
            m_isCharging = false;
        }
    }

    void Charging()
    {
        m_horizontalForce += (maxHorizontalForce - minHorizontalForce) * Time.deltaTime;
        m_horizontalForce = Mathf.Clamp(m_horizontalForce, minHorizontalForce, maxHorizontalForce);
    }


    void Throw()
    {
        m_ready = false;
        m_handPos = handTransform.position;
        GameObject coin = Instantiate(coinPrefab, m_handPos, transform.rotation);
        Rigidbody coinRigid = coin.GetComponent<Rigidbody>();
        Vector3 force = transform.forward * m_horizontalForce + transform.up * verticalForce;
        coinRigid.AddForce(force, ForceMode.Impulse);
        m_horizontalForce = 0;
        numCoin--;
        StartCoroutine(ThrowCool());
    }

    IEnumerator ThrowCool()
    {
        yield return new WaitForSeconds(coolDown);
        m_ready = true;
    }
}
