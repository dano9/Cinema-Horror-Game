using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float autoTurnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        wheelInter.value = 200 * movieFrames.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (!wheelInter.isGrabbed)
        {
            wheelInter.value += autoTurnSpeed * Time.deltaTime;
        }
        foreach (Transform wheel in wheels)
        {
            wheel.localRotation = Quaternion.Euler(0,0, wheelInter.value * wheelTurnMultiplier);
        }
        frameIndex = Mathf.Clamp((int)Mathf.Floor((valueToFrameIndexMulti * wheelInter.value))  % movieFrames.Length, 0, movieFrames.Length);
        projection.sprite = movieFrames[frameIndex];
        
    }
    void LateUpdate()
    {
        leverHandle.position = wheelInter.handle.position;
    }
}
