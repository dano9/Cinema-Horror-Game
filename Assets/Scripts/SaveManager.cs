using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public string[] dataLabels;
    public float[] defaultDataSet;

    public Dictionary<string, float> curSaveData;

    // Start is called before the first frame update
    void Awake()
    {
        curSaveData = new Dictionary<string, float>();
        ResetToDefault();
    }
    public void ResetToDefault()
    {
        for (int i = 0; i < dataLabels.Length; i++)
        {
           curSaveData.Add(dataLabels[i], defaultDataSet[i]);
        }
        for (int i = 0; i < GameManager.gM.player.items.Length; i++)
        {
            int val = 0;if (GameManager.gM.player.items[i].equipped) {val = 1;}
            curSaveData.Add(GameManager.gM.player.items[i].name, val);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
