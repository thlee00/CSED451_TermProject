using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    public TextMeshProUGUI floorNum;
    public GameObject startUI;
    public GameObject inGameUI;
    public GameObject[] respawnPoints = new GameObject[4];

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;
    bool m_IsStarted = false;
    bool m_LockPlayer = false;
    bool m_HasPlayerRespawned = false;

    static int curFloor = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            m_IsStarted = true;
        }
        if (!m_IsStarted)
        {
            startUI.SetActive(true);
            inGameUI.SetActive(false);
        }
        else
        {
            startUI.SetActive(false);
            inGameUI.SetActive(true);
        }

        floorNum.text = "Floor " + curFloor.ToString();
        if (m_IsPlayerAtExit)
        {
            if (!m_LockPlayer)
            {
                player.GetComponent<ItemManager>().SaveNumItem();
                player.GetComponent<ItemManager>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
                m_LockPlayer = true;
            }
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            if (!m_LockPlayer)
            {
                player.GetComponent<ItemManager>().SaveNumItem();
                player.GetComponent<ItemManager>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
                m_LockPlayer = true;
            }
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }


    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        inGameUI.SetActive(false);
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration && ! m_HasPlayerRespawned)
        {
            if (!doRestart && curFloor > 0)
            {
                curFloor--;
            }
            if (curFloor > 0) {
                player.transform.position = respawnPoints[curFloor].transform.position;
            }
            m_HasPlayerRespawned = true;
        }

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (!doRestart && curFloor == 0)
            {
                Application.Quit();
            }
            else 
            {
                ResetFloor(imageCanvasGroup);
            }
        }
    }

    void ResetFloor(CanvasGroup imageCanvasGroup)
    {
        inGameUI.SetActive(true);
        m_IsPlayerAtExit = false;
        m_IsPlayerCaught = false;
        m_Timer = 0f;
        m_HasAudioPlayed = false;
        imageCanvasGroup.alpha = 0;
        player.GetComponent<ItemManager>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<ItemManager>().LoadNumItem();
        m_LockPlayer = false;
        m_HasPlayerRespawned = false;
    }
}
