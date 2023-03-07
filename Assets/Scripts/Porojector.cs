using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Movie
{
    public Sprite[] movieFrames;
}
public class Porojector : MonoBehaviour
{
    public TurnWheel wheelInter;
    public Transform[] wheels;
    public Transform leverHandle;
    public float wheelTurnMultiplier;
    public SpriteRenderer projection;
    public Sprite[] movieFrames;
    public float valueToFrameIndexMulti = 10;
    public int frameIndex;
    public string curMovie;
    public float autoTurnSpeed;
    public GameObject wrench;
    public GameObject noWrenchInter;
    public bool hasWrench;
    // Start is called before the first frame update
    void Start()
    {
        wheelInter.value = 200 * movieFrames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (curMovie == "")
        {
            projection.enabled = false;
        }
        else
        {
            projection.enabled = true;
        }
        if (!hasWrench)
        {
            if (wrench.active) {wrench.SetActive(false); noWrenchInter.SetActive(true);}
        }
        else 
        {
            if (!wrench.active) {wrench.SetActive(true); noWrenchInter.SetActive(false);}
            if (!wheelInter.isGrabbed)
            {
                wheelInter.value += autoTurnSpeed * Time.deltaTime;
            }
            frameIndex = Mathf.Clamp((int)Mathf.Floor((valueToFrameIndexMulti * wheelInter.value))  % movieFrames.Length, 0, movieFrames.Length);
            projection.sprite = movieFrames[frameIndex];
        }
        foreach (Transform wheel in wheels)
        {
            wheel.localRotation = Quaternion.Euler(0,0, wheelInter.value * wheelTurnMultiplier);
        }
        
    }
    void LateUpdate()
    {
        leverHandle.position = wheelInter.handle.position;
    }
    public void PlaceWrench()
    {
        hasWrench = true;
    }
}
