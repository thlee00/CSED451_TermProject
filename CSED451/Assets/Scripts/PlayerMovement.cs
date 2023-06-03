using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public int dashCool = 5;
    public AudioClip audioWalk;
    public AudioClip audioDash;
    public bool invincible = false;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public bool isDashing = false;
    bool isDashOnCool = false;
    bool moveFast = false;

    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isDashing = Input.GetKeyDown(KeyCode.Space);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        

        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.clip = audioWalk;
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        if (isDashing && !isDashOnCool)
        {
            m_AudioSource.clip = audioDash;
            m_AudioSource.Play();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        //m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        if (isDashing && !isDashOnCool)
        {
            invincible = true;
            m_Animator.SetBool("IsDashing", true);
            moveFast = true;
            StartCoroutine(DashDuration());
            isDashOnCool = true;
            StartCoroutine(DashCoolDown());
        }

        if (moveFast)
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * 0.1f);
        }
        else
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * 0.03f);
        }
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(1);
        m_Animator.SetBool("IsDashing", false);
        moveFast = false;
        invincible = false;
    }

    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashCool);
        isDashOnCool = false;
    }
}