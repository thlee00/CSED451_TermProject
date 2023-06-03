using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransperent : MonoBehaviour
{
    public KeyCode itemKey = KeyCode.Keypad1;
    public KeyCode UseKey = KeyCode.Space;
    public float transperentDelay = 5.0f;

    List<Renderer> renderers;
    int numOfChild;
    bool m_IsPotionSelected = false;
    bool m_IsPotionObtained = false;

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
        if (Input.GetKeyDown(itemKey))
        {
            print("potionSelected");
            m_IsPotionSelected = true;
        }

        if (m_IsPotionSelected && m_IsPotionObtained && Input.GetKeyDown(UseKey))
        {
            
            m_IsPotionSelected = false;
            StartCoroutine(startTransperent());
            StartCoroutine(endTransperent());
        }
    }

    public void ObtainPotion()
    {
        print("ObtainPotion");
        m_IsPotionObtained = true;
    }

    IEnumerator startTransperent()
    {
        int i = 10;
        while (i > 0)
        {
            i -= 1;
            float f = i / 10.0f;
            for (int r = 0; r < renderers.Count; r++) {
                print(renderers[r]);
                Color c = renderers[r].material.color;
                c.a = f;
                renderers[r].material.color = c;
            }
            yield return new WaitForSeconds(transperentDelay);
        }
    }

    IEnumerator endTransperent()
    {
        int i = 0;
        while (i < 10)
        {
            i += 1;
            float f = i / 10.0f;
            for (int r = 0; r < renderers.Count; r++)
            {
                print(renderers[r]);
                Color c = renderers[r].material.color;
                c.a = f;
                renderers[r].material.color = c;
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}
