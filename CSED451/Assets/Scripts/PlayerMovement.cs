using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float dashCool = 5f;
    public AudioClip audioWalk;
    public AudioClip audioDash;
    public bool invincible = false;
    public Image dashPanel;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    
    AudioSource m_AudioSource;

    bool isDashOnCool;
    bool moveFast;
    float m_dashCoolTime = 0;

    PlayerWardrobe playerWardrobe;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.loop = false;
        playerWardrobe = transform.gameObject.GetComponent<PlayerWardrobe>();
        isDashOnCool = false;
        moveFast = false;
    }

    void Update()
    {
        if (isDashOnCool)
        {
            m_dashCoolTime += Time.deltaTime;
            dashPanel.rectTransform.localScale = new Vector3(m_dashCoolTime / 1.0f, 0.5f, 1.0f);
        }
        else
        {
            m_dashCoolTime = 0;
        }

        if (!playerWardrobe.isPlayerInWardrobe)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isDashOnCool)
            {
                m_AudioSource.clip = audioDash;
                m_AudioSource.Play();

                invincible = true;
                m_Animator.SetBool("IsDashing", true);
                moveFast = true;
                StartCoroutine(DashDuration());
                isDashOnCool = true;
                StartCoroutine(DashCoolDown());
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!playerWardrobe.isPlayerInWardrobe)
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
                if (m_AudioSource.clip == audioWalk)
                {
                    m_AudioSource.Stop();
                }
            }


            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);
        }
    }

    void OnAnimatorMove()
    {
        if (!playerWardrobe.isPlayerInWardrobe)
        {
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
    }

    IEnumerator DashDuration()
    {
        yield return new WaitForSeconds(1f);
        m_Animator.SetBool("IsDashing", false);
        moveFast = false;
        invincible = false;
        //m_AudioSource.Stop();
    }

    IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashCool);
        isDashOnCool = false;
    }
}