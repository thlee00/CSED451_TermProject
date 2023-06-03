using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransperent : MonoBehaviour
{
    public KeyCode itemKey = KeyCode.F1;
    public KeyCode useKey = KeyCode.F2;
    public float transperentDelay = 5.0f;
    public bool isPlayerTransperented;

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
                print(i);
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

        if (Input.GetKeyDown(useKey) && m_IsPotionSelected && m_IsPotionObtained)
        {
            isPlayerTransperented = true;
            StartCoroutine(startTransperent());
            m_IsPotionSelected = false;
        }
    }

    public void ObtainPotion()
    {
        print("ObtainPotion");
        m_IsPotionObtained = true;
    }

    IEnumerator startTransperent()
    {
        print("startTransperent");
        for (int r = 0; r < renderers.Count; r++)
        {
            Color c = renderers[r].material.color;
            print(c);
            c.a = c.a / 10;
            renderers[r].material.color = c;
            print(c);
        }
        yield return new WaitForSeconds(transperentDelay);
        isPlayerTransperented = false;
        print("endTransperent");
    }
}
