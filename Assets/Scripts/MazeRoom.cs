using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRoom : MonoBehaviour
{
    public GameObject leftDoorCover;
    public GameObject rightDoorCover;
    public GameObject leftDoorInter;
    public GameObject rightDoorInter;
    public int layoutIndex;
    public bool followMapLayout;
    public Transform nodeHolders;
    public int nodeIndex;
    public int endNode;
    // Start is called before the first frame update
    void Start()
    {
        SetLayout();
    }
    public void SetLayout()
    {
        if (layoutIndex == 0) //Both
        {
            leftDoorCover.SetActive(false);
            rightDoorCover.SetActive(false);
        }
        else if (layoutIndex == 1 || layoutIndex == 2) //Left
        {
            leftDoorCover.SetActive(false);
            rightDoorCover.SetActive(true);
        }
        else if (layoutIndex == 3 || layoutIndex == 4) //Right
        {
            leftDoorCover.SetActive(true);
            rightDoorCover.SetActive(false);
        }
        else  //Dead End
        {
            leftDoorCover.SetActive(true);
            rightDoorCover.SetActive(true);
        }
        leftDoorInter.SetActive(!leftDoorCover.active);
        rightDoorInter.SetActive(!rightDoorCover.active);
    }
    public void TravelNextSeg(bool left)
    {
        StartCoroutine(TransitionNextSeg(left));
    }
    public void ReturnToMainHall()
    {

    }
    public IEnumerator TransitionNextSeg(bool left)
    {
        GameManager.gM.rM.isSwitchingRoom = true;
        GameManager.gM.hM.vM.FadeIn();
        while (GameManager.gM.hM.vM.isFading)
        {
            yield return null;
        }
        bool hasReachedEnd = false;
        if (!followMapLayout)
        {
            layoutIndex = Random.Range(0,6);
        }
        else
        {
            int[] childNodes = GetChildNodes();
            int targ = 1;
            if (left) { targ = 0;}
            nodeIndex = childNodes[targ];
            if (nodeIndex == endNode)
            {
                hasReachedEnd = true;
            }
            else 
            {
                childNodes = GetChildNodes();
                if (childNodes[0] != -1 && childNodes[1] != -1)
                {
                    layoutIndex = 0;
                }
                else if (childNodes[0] != -1 && childNodes[1] == -1)
                {
                    layoutIndex = 1;
                }
                else if (childNodes[0] == -1 && childNodes[1] != -1)
                {
                    layoutIndex = 3;
                }
                else
                {
                    layoutIndex = 5;
                }
            }
        }
        if (!hasReachedEnd)
        {
            SetLayout();
        }
        else
        {
            GameManager.gM.svM.curSaveData["cinemaStage"] = 3;
            GameManager.gM.rM.SwitchRoom("cinemaBack");
        }
        GameManager.gM.hM.vM.FadeOut();
        while (GameManager.gM.hM.vM.isFading)
        {
            yield return null;
        }
        GameManager.gM.rM.isSwitchingRoom = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int[] GetChildNodes()
    {
        string childName = nodeHolders.GetChild(nodeIndex).gameObject.name;
        string nodeChildString = childName.Split('|')[1];
        string[] childNodes = nodeChildString.Split(',');

        //Transform l = null; 
        int li = -1; if (childNodes[0] != "N") {int.TryParse(childNodes[0], out li);} 
        if (li >= nodeHolders.childCount) {li = -1;}
        //if (li != -1 && li < nodeHolders.childCount){l = nodeHolders.getChild(li);}

        //Transform r = null;
        int ri = -1; if (childNodes[1] != "N") {ri = int.Parse(childNodes[1]);}
        if (ri >= nodeHolders.childCount) {ri = -1;}
        //if (ri != -1 && ri < nodeHolders.childCount){r = nodeHolders.getChild(ri);}
        return new int[] {li, ri};
    }
}
