using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    public float rotationSpeed = 60f;
    public float moveSpeed = 1f;
    public float moveLength = 0.3f;
    float m_angle;
    float m_yPos;
    float m_runningTime;
    Vector3 m_orgPos;
    // Start is called before the first frame update
    void Start()
    {
        m_orgPos = transform.position;
        m_angle = 0f;
        m_yPos = 0f;
        m_runningTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        m_angle += Time.deltaTime * rotationSpeed;
        m_runningTime += Time.deltaTime * moveSpeed;
        m_yPos = Mathf.Sin(m_runningTime) * moveLength;
        transform.SetPositionAndRotation(new Vector3(m_orgPos.x, m_orgPos.y + m_yPos, m_orgPos.z), Quaternion.Euler(0, m_angle, 0));
    }
}
