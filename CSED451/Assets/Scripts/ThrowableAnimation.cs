using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAnimation : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public AudioClip audioHit;
    public AudioClip audioThrow;
    float m_angle;
    // Start is called before the first frame update
    void Start()
    {
        PlayThrow();
    }

    // Update is called once per frame
    void Update()
    {
        m_angle += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, m_angle, 0);
    }

    public void PlayHit()
    {
        GameObject audioObject = new("HitAudioObject");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = audioHit;
        audioSource.Play();
        Destroy(audioObject, 2f);
    }

    public void PlayThrow()
    {
        GameObject audioObject = new("ThrowAudioObject");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();
        audioSource.clip = audioThrow;
        audioSource.Play();
        Destroy(audioObject, 2f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Map") || col.gameObject.CompareTag("Enemy"))
        {
            PlayHit();
            Destroy(gameObject);
        }
    }
}
