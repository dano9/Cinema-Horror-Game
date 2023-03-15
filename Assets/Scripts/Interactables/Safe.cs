using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour
{
    public string passcode;
    public string curInput;
    public bool isOpened;
    public GameObject insides;
    public GameObject keyPad;
    public AudioSource aS;
    public AudioClip[] tapSounds;
    public AudioClip successSound;
    public AudioClip failSound;
    int soundIndex = 0;

    public void Start()
    {
        print("Safe stage:" + GameManager.gM.svM.curSaveData["safeStage"]);
        if (GameManager.gM.svM.curSaveData["safeStage"] == 1)
        {
            isOpened = true;
            insides.SetActive(true);
            keyPad.SetActive(false);
        }
    }
    public void enterKey(string key)
    {
        curInput += key;
        if (curInput == passcode)
        {
            isOpened = true;
            GameManager.gM.svM.curSaveData["safeStage"] = 1;
            insides.SetActive(true);
            keyPad.SetActive(false);
            aS.clip = successSound;
            aS.Play();
        }
        else if (curInput.Length == 4)
        {
            curInput = "";
            aS.clip = failSound;
            aS.Play();
        }
        else
        {
            aS.clip = tapSounds[soundIndex];
            aS.Play();
            soundIndex = (soundIndex + 1) % tapSounds.Length;
        }
    }
}
