using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public bool equipped;
    public Sprite icon;
    public bool reusable;
    public int index;
}
public class Player : MonoBehaviour
{
    public Item[] items;
    public LayerMask lM; 
    public CursorMode cursMode;
    public Vector2 worldMousePos;

    public Texture2D grabTex;

    List<string> interStoppers;
    public Transform debugPointer;

    public Interactable curInteractable;
    public int grabbedItem;
    public SpriteRenderer grabbedItemSprite;
    // Start is called before the first frame update
    void Start()
    {
        interStoppers = new List<string>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameManager.gM.hM.iUI.UpdateItemSlots(items);
    }
    public void Update()
    {
        ManageMouse();
    }
    public void AquireItem(int i)
    {
        items[i].equipped = true;
        GameManager.gM.svM.curSaveData[items[i].name] = 1;
        GameManager.gM.hM.iR.ReadInfo(new string[]{"Aquired " + items[i].name});
        StartCoroutine(GameManager.gM.hM.ShowHotbar());
    }
    public void ManageMouse()
    {
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        debugPointer.position = worldMousePos;
        int cursorDet = 0;
        if (grabbedItem == -1) {grabbedItemSprite.sprite = null;} else {grabbedItemSprite.sprite = items[grabbedItem].icon;  cursorDet = 5;}
        if (interStoppers.Count == 0)
        {
            if (curInteractable == null)
            {
                Interactable selectedInter = null;
                RaycastHit2D[] hits = Physics2D.CircleCastAll(worldMousePos, 0.05f, Vector3.forward, Mathf.Infinity, lM, -Mathf.Infinity, Mathf.Infinity);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider.gameObject.GetComponent<Interactable>() != null && (selectedInter == null || (selectedInter.transform.position.z < hit.collider.transform.position.z)))
                    {
                        Interactable newInter = hit.collider.gameObject.GetComponent<Interactable>();
                        if (newInter.specificItem == -1 || grabbedItem != -1)
                        {selectedInter = newInter;}
                    }
                }

                if (selectedInter != null)
                {
                    if (grabbedItem == -1)
                    {
                        if (Input.GetMouseButtonDown(0) && selectedInter.specificItem == -1)
                        {
                            selectedInter.Interact();
                            if (selectedInter.prolongInteraction)
                            {
                                curInteractable = selectedInter;
                            }
                        }
                        cursorDet = selectedInter.hoverIcon;
                    }
                    else
                    {
                        cursorDet = 2;
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (selectedInter.specificItem == grabbedItem)
                            {
                                selectedInter.Interact();
                                if (!items[grabbedItem].reusable)
                                {
                                    items[grabbedItem].equipped = false;
                                    grabbedItem = -1;
                                }
                            }
                            else {GameManager.gM.hM.iR.ReadInfo(new string[] {"I can't use that here..."});}
                        }
                    }
                }
            }
            else
            {
                cursorDet = 5;
                if (!Input.GetMouseButton(0))
                {
                    curInteractable.EndInteract();
                    curInteractable = null;
                }
            }

        }
        else {cursorDet = 4;}
        if (!Input.GetMouseButton(0))
        {
            grabbedItem = -1;
        }
        GameManager.gM.hM.curCursorMode = cursorDet;
    }

    public void PauseMouseInter(string sID)
    {
        bool alreadyAdded = false;
        foreach (string s in interStoppers) {if (s == sID) {alreadyAdded = true;}}
        if (!alreadyAdded){ interStoppers.Add(sID);}
    }
    public void ResumeMouseInter(string sID)
    {
        bool exists = false;
        foreach (string s in interStoppers) {if (s == sID) {exists = true;}}
        if (exists){ interStoppers.Remove(sID);}
    }
}
