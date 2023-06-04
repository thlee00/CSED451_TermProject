using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWardrobe : MonoBehaviour
{
    public KeyCode useKey = KeyCode.E;
    public bool isPlayerInWardrobe = false;

    Camera m_MainCamera;
    GameObject m_Wardrobe;
    GameObject[] wardrobeCameras;
    bool m_IsPlayerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        wardrobeCameras = GameObject.FindGameObjectsWithTag("WardrobeCamera");
        foreach (GameObject wardrobeCamrea in wardrobeCameras)
        {
            wardrobeCamrea.GetComponent<Camera>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useKey) && m_IsPlayerInRange && !isPlayerInWardrobe)
        {
            print("in wardrobe");
            print(m_Wardrobe);
            isPlayerInWardrobe = true;
            SetWardrobeCamera();
        }
        else if (Input.GetKeyDown(useKey) && isPlayerInWardrobe)
        {
            print("out wardrobe");
            isPlayerInWardrobe = false;
            SetMainCamera();
        }
    }

    void SetWardrobeCamera()
    {
        m_MainCamera.enabled = false;
        GetComponent<AudioListener>().enabled = false;
        Camera wardrobeCamera = m_Wardrobe.gameObject.GetComponentInChildren<Camera>();
        print(wardrobeCamera);
        wardrobeCamera.enabled = true;
        m_Wardrobe.gameObject.GetComponentInChildren<AudioListener>().enabled = true;
        OffLayerMask(wardrobeCamera, 9);
    }

    void SetMainCamera()
    {
        m_MainCamera.enabled = true;
        GetComponent<AudioListener>().enabled = true;
        Camera wardrobeCamera = m_Wardrobe.gameObject.GetComponentInChildren<Camera>();
        wardrobeCamera.enabled = false;
        m_Wardrobe.gameObject.GetComponentInChildren<AudioListener>().enabled = false;
        Everything(wardrobeCamera);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("WardrobeRange"))
        {
            m_Wardrobe = col.transform.parent.gameObject;
            m_IsPlayerInRange = true;
            print(m_Wardrobe);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("WardrobeRange"))
        {
            m_IsPlayerInRange = false;
        }
    }

    void OffLayerMask(Camera camera, int layerIndex)
    {
        camera.cullingMask = ~(1 << layerIndex);
    }

    void Everything(Camera camera)
    {
        camera.cullingMask = -1;
    }
}