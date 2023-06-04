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

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;
    bool m_IsStarted = false;

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
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }


    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        imageCanvasGroup.alpha = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                curFloor = 3;
                SceneManager.LoadScene("Floor" + curFloor.ToString());
            }
            else if (curFloor == 1)
            {
                Application.Quit();
            }
            else
            {
                curFloor--;
                SceneManager.LoadScene("Floor" + curFloor.ToString());
            }
        }
    }
}
