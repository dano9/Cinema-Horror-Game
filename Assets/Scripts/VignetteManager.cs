using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VignetteManager : MonoBehaviour
{
    public bool isFading;
    public Image vignette;
    public Image blackScreen;
    public Coroutine curFadeCor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    public void FadeIn(float fadeSpeed = 0.1f)
    {
        if (isFading)
        {
            StopCoroutine(curFadeCor);
        }
        curFadeCor = StartCoroutine(FadeVignette(1, fadeSpeed));
    }
    public void FadeOut(float fadeSpeed = 0.1f)
    {
        if (isFading)
        {
            StopCoroutine(curFadeCor);
        }
        curFadeCor = StartCoroutine(FadeVignette(0, fadeSpeed));
    }
    public IEnumerator FadeVignette(float targetOpacity, float fadeSpeed = 0.1f)
    {
        if (targetOpacity == 1) {blackScreen.gameObject.SetActive(true); vignette.gameObject.SetActive(true);}
        isFading = true;
        Color targCol =  new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, targetOpacity);
        Color targVCol =  new Color(vignette.color.r, vignette.color.g, vignette.color.b, targetOpacity);
        while (Mathf.Abs(blackScreen.color.a - targetOpacity) > 0.05f)
        {
            vignette.color = Color.Lerp(vignette.color, targVCol, fadeSpeed * 2);
            blackScreen.color = Color.Lerp(blackScreen.color, targCol, fadeSpeed);
            yield return new WaitForFixedUpdate();
        }
        blackScreen.color = targCol;
        vignette.color = targVCol;
        yield return new WaitForSeconds(0.1f);
        isFading = false;
        if (targetOpacity == 0) {blackScreen.gameObject.SetActive(false); vignette.gameObject.SetActive(false);}
    }
}
