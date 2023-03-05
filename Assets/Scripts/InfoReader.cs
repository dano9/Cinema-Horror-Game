using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoReader : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public float characterInterval = 0.04f;
    public bool isReading;
    private Coroutine currentReadCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReadInfo(string[] info)
    {
        if (isReading)
        {
            StopCoroutine(currentReadCoroutine);
        }
       currentReadCoroutine = StartCoroutine(ReadInfoIE(info));
    }
    public IEnumerator ReadInfoIE(string[] infos)
    {
        isReading = true;
        infoText.text = "";
        infoText.gameObject.SetActive(true);
        foreach (string info in infos)
        {
            string infoProg = "";
            yield return new WaitForSeconds(characterInterval);
            for (int i = 0; i < info.Length; i++)
            {
                infoProg += info[i];
                infoText.text = infoProg;
                yield return new WaitForSeconds(characterInterval);
            }
            while (!Input.GetKeyDown("space"))
            {
                yield return null;
            }
        }
        infoText.gameObject.SetActive(false);
        isReading = false;
    }
}
