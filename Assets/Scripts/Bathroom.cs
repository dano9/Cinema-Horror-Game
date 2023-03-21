using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{
    public SpriteRenderer blackness;
    public Transform face;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LightFlicker(float delay)
    {
        StartCoroutine(LightFlickerIE(delay));
    }
    public IEnumerator LightFlickerIE(float delay)
    {
        yield return new WaitForSeconds(delay);
        Color targ = new Color(blackness.color.r,blackness.color.g,blackness.color.b, 0.9f);
        while (Mathf.Abs(targ.a - blackness.color.a) > 0.04f)
        {
            blackness.color = Color.Lerp(blackness.color, targ, 0.15f);
            yield return null;
        }
        targ = new Color(blackness.color.r,blackness.color.g,blackness.color.b, 0);
        yield return new WaitForSeconds(0.15f);
        while (Mathf.Abs(targ.a - blackness.color.a) > 0.04f)
        {
            blackness.color = Color.Lerp(blackness.color, targ, 0.15f);
            yield return null;
        }
    }
}
