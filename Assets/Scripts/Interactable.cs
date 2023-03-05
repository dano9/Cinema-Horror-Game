using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public int hoverIcon = 1;
    public string[] info;
    public bool readInfo;
    public UnityEvent[] interactActions;
    public void Interact()
    {
        if (readInfo) { GameManager.gM.hM.iR.ReadInfo(info);}
        foreach (UnityEvent uE in interactActions)
        {
            uE.Invoke();
        }
    }
    public void TestFunc()
    {
    }
    public void GivePlayerItem(int i)
    {
        GameManager.gM.player.AquireItem(i);
    }
    public void SwitchRoom(string newRoom)
    {
        GameManager.gM.rM.SwitchRoom(newRoom, true);
    }
}
