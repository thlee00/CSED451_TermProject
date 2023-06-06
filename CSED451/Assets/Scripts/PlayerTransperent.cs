using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransperent : MonoBehaviour
{
    public float transperentDelay = 5.0f;
    public bool isPlayerTransperented;
    public AudioClip audioBegin;
    AudioSource m_AudioSource;

    List<Renderer> renderers;
    int numOfChild;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        renderers = new List<Renderer>();
        numOfChild = transform.childCount;
        for (int i = 0; i < numOfChild; i++)
        {
            Renderer temp = transform.GetChild(i).GetComponent<Renderer>();
            if (temp != null)
            {
                renderers.Add(temp);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use()
    {
        isPlayerTransperented = true;
        m_AudioSource.clip = audioBegin;
        m_AudioSource.Play();
        StartCoroutine(startTransperent());
    }

    IEnumerator startTransperent()
    {
        for (int r = 0; r < renderers.Count; r++)
        {
            Color c = renderers[r].material.color;
            c.r = 0.6f;
            renderers[r].material.color = c;
        }
        yield return new WaitForSeconds(transperentDelay);
        StartCoroutine(endTransperent());
        isPlayerTransperented = false;
    }

    IEnumerator endTransperent()
    {
        int i = 0;
        while (i < 10)
        {
            i++;
            for (int r = 0; r < renderers.Count; r++)
            {
                Color c = renderers[r].material.color;
                c.r = 0.6f + i * 0.04f;
                renderers[r].material.color = c;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
