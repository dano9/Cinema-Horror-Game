using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaRoom : MonoBehaviour
{
    public int stage;
    public Porojector projector;
    public GameObject doorCover;
    public GameObject doorInter;
    int lastStage;
    // Start is called before the first frame update
    void Start()
    {
        stage = (int)GameManager.gM.svM.curSaveData["cinemaStage"];
        SetUpRoom();
    }
    void OnDestroy()
    {
        if (stage == 0) {stage = 1;}
        GameManager.gM.svM.curSaveData["cinemaStage"] = stage;
    }
    public void Update()
    {
        if (stage != lastStage)
        {
            
            SetUpRoom();
            lastStage = stage;
        }
        if (projector.hasWrench && stage < 2)
        {
            stage = 2;
            GameManager.gM.svM.curSaveData["cinemaStage"] = stage;
        }
    }
    public void SetUpRoom()
    {
        if (stage == 0)
        {
            //if (projector.projection.gameObject.active){projector.projection.gameObject.SetActive(false);}
            projector.autoTurnSpeed = 0;
            projector.curMovie = "";
        }
        else 
        {
            //if (!projector.projection.gameObject.active){projector.projection.gameObject.SetActive(true);}
        }
        if (stage > 1)
        {
            projector.hasWrench = true;
        }
        else
        {
            projector.hasWrench = false;
        }
        if (stage == 1 || stage == 2)
        {
            projector.autoTurnSpeed = 0;
            projector.curMovie = "mapCodeClips";
        }
        if (stage == 3)
        {
            projector.autoTurnSpeed = 0.1f;
            if (doorInter.active) {doorInter.SetActive(false);}
            projector.curMovie = "mapCodeClips";
            //projector.curMovie = "afterMaze";
        }
        else
        {
            if (!doorInter.active) {doorInter.SetActive(true);}
        }
        if (doorCover.active == doorInter.active) {doorCover.SetActive(!doorInter.active);}
        print("SETUP");
    }
}
