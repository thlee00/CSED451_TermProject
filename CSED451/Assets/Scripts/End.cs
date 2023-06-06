using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneQuit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SceneQuit() {
        yield return new WaitForSeconds(5.0f);
        Application.Quit();
        Debug.Log("Done");
    }
}
