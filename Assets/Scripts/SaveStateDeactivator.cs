using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStateDeactivator : MonoBehaviour
{
    public string property;
    public float desiredValue;
    public bool invert;
    // Start is called before the first frame update
    void Start()
    {
        print("SS: " + GameManager.gM.svM.curSaveData[property]);
        gameObject.SetActive(GameManager.gM.svM.curSaveData[property] == desiredValue);
    }
}
