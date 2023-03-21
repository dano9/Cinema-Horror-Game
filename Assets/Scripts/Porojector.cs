using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
    public Movie[] movies;
    public string[] movieLabels;
    public static Dictionary<string, int> movieDict;
    public float valueToFrameIndexMulti = 10;
    public int frameIndex;
    int lastFI;
    int delFrameI;
    public string curMovie;
    public float autoTurnSpeed;
    public GameObject wrench;
    public GameObject noWrenchInter;
    public bool hasWrench;
    public Animator projAnim;
    public bool animBool1;

    // Start is called before the first frame update
    void Start()
    {
        wheelInter.value = 200 * 20;
        wheelInter.value = GameManager.gM.svM.curSaveData["projectorTurn"];
        if (movieDict == null) {
            movieDict = new Dictionary<string, int>();
            for (int m = 0; m < movies.Length; m++)
            {
                movieDict.Add(movieLabels[m], m);
            }
        }
        lastFI = frameIndex;
    }
    void OnDestroy()
    {
        GameManager.gM.svM.curSaveData["projectorTurn"] = wheelInter.value;
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
            if (wrench.active || !noWrenchInter.active) {wrench.SetActive(false); noWrenchInter.SetActive(true);}
        }
        else 
        {
            if (!wrench.active || noWrenchInter.active) {wrench.SetActive(true); noWrenchInter.SetActive(false);}
            if (!wheelInter.isGrabbed)
            {
                wheelInter.value += autoTurnSpeed * Time.deltaTime;
            }
            if (curMovie != "")
            {
                int movieFrameLength = movies[movieDict[curMovie]].movieFrames.Length;
                frameIndex = Mathf.Clamp((int)Mathf.Floor((valueToFrameIndexMulti * wheelInter.value))  % movieFrameLength, 0, movieFrameLength);
                projection.sprite = movies[movieDict[curMovie]].movieFrames[delFrameI];
            }
        }
        foreach (Transform wheel in wheels)
        {
            wheel.localRotation = Quaternion.Euler(0,0, wheelInter.value * wheelTurnMultiplier);
        }
        if (frameIndex != lastFI)
        {
            print("Switched Frame");
            if (frameIndex > lastFI) {projAnim.SetTrigger("SwitchFrame");}
            else {projAnim.SetTrigger("SwitchFrameBack");}
            lastFI = frameIndex;
        }
        if (delFrameI != frameIndex)
        {
            if (animBool1) { delFrameI = frameIndex; }
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
    public void SetMovie(string newMovie)
    {
        curMovie = newMovie;
        wheelInter.value = 200;
        GameManager.gM.svM.curSaveData["projectorTurn"] = wheelInter.value;
    }
}
