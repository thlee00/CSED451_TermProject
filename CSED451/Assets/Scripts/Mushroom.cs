using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public AudioClip audioSmall;
    public AudioClip audioBig;
    public float scaleSpeed = 0.0035f;
    bool isSmall = false;

    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        if (!isSmall)
        {
            StartCoroutine(SizeDown());
        }
    }

    IEnumerator SizeDown()
    {
        m_AudioSource.clip = audioSmall;
        m_AudioSource.Play();
        isSmall = true;

        for (float f = 0f; f <= 0.35f; f += scaleSpeed)
        {
            transform.localScale = new Vector3(0.7f - f, 0.7f - f, 0.7f - f);
            yield return null;
        }
        //m_AudioSource.Stop();
        StartCoroutine(DownDuration());
    }

    IEnumerator DownDuration()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(SizeUp());
    }

    IEnumerator SizeUp()
    {
        m_AudioSource.clip = audioBig;
        m_AudioSource.Play();
        isSmall = false;

        for (float f = 0f; f <= 0.35f; f += scaleSpeed)
        {
            transform.localScale = new Vector3(0.35f + f, 0.35f + f, 0.35f + f);
            yield return null;
        }
        //m_AudioSource.Stop();
    }
}
