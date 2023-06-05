using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWardrobe : MonoBehaviour
{
    public KeyCode useKey = KeyCode.E;
    public bool isPlayerInWardrobe = false;
    public GameObject wardrobeDoor;
    public GameObject wardrobeMessage;

    Camera m_MainCamera;
    GameObject m_Wardrobe;
    GameObject[] wardrobeCameras;
    bool m_IsPlayerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        wardrobeCameras = GameObject.FindGameObjectsWithTag("WardrobeCamera");
        wardrobeDoor.SetActive(false);
        wardrobeMessage.SetActive(false);
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
            wardrobeDoor.SetActive(true);
            SetWardrobeCamera();
        }
        else if (Input.GetKeyDown(useKey) && isPlayerInWardrobe)
        {
            print("out wardrobe");
            isPlayerInWardrobe = false;
            wardrobeDoor.SetActive(false);
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
            wardrobeMessage.SetActive(true);
            print(m_Wardrobe);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("WardrobeRange"))
        {
            m_IsPlayerInRange = false;
            wardrobeMessage.SetActive(false);
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