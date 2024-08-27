using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NonPlayerCharacter : MonoBehaviour
{
    public float displayTime = 4.0f;
    public GameObject dialogBox;
    public GameObject talkBox;
    public GameObject bulletUI;
    public GameObject moveUI;
    public GameObject talkUI;
    public TMP_Text talk;
    public TMP_Text talkpanel;
    float timerDisplay;
   

    void Start()
    {
        talkBox.SetActive(false);
        dialogBox.SetActive(false);
        timerDisplay = -1.0f;
    }

    void Update()
    {
        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogBox.SetActive(false);
                talkBox.SetActive(false);
                UIOnOff(true);
            }
        }
    }
    public void DisplayDialog()
    {
        timerDisplay = displayTime;
#if (!UNITY_ANDROID)
        dialogBox.SetActive(true);
#else
        talkBox.SetActive(true);
        UIOnOff(false);
#endif
    }
    public void ChangeDialog()
    {
        talk.text = $"Wow! Good Job!";
        talkpanel.text = $"Wow! Good Job!";
    }

    public void UIOnOff(bool value)
    {
        moveUI.SetActive(value);
        bulletUI.SetActive(value);
        talkUI.SetActive(value);
    }
}
