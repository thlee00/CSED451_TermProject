using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransperent : MonoBehaviour
{
    public float transperentDelay = 5.0f;
    public bool isPlayerTransperented;

    List<Renderer> renderers;
    int numOfChild;

    // Start is called before the first frame update
    void Start()
    {
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
        StartCoroutine(startTransperent());
    }

    IEnumerator startTransperent()
    {
        print("startTransperent");
        for (int r = 0; r < renderers.Count; r++)
        {
            print(renderers[r]);
            Color c = renderers[r].material.color;
            print(c);
            c.r = 0.7f;
            renderers[r].material.color = c;
            print(c);
        }
        yield return new WaitForSeconds(transperentDelay);
        StartCoroutine(endTransperent());
        isPlayerTransperented = false;
    }

    IEnumerator endTransperent()
    {
        print("endTransperent");
        for (int r = 0; r < renderers.Count; r++)
        {
            print(renderers[r]);
            Color c = renderers[r].material.color;
            print(c);
            c.r = 1.0f;
            renderers[r].material.color = c;
            print(c);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
