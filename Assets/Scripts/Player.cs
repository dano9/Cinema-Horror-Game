using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public bool equipped;
    public Sprite icon;
}
public class Player : MonoBehaviour
{
    public Item[] items;
    public LayerMask lM;
    public CursorMode cursMode;

    public Texture2D grabTex;

    List<string> interStoppers;
    public Transform debugPointer;
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
        GameManager.gM.hM.iR.ReadInfo(new string[]{"Aquired " + items[i].name});
        StartCoroutine(GameManager.gM.hM.ShowHotbar());
    }
    public void ManageMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        debugPointer.position = mousePos;
        int cursorDet = 0;
        if (interStoppers.Count == 0)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePos, 0.05f, Vector3.forward, Mathf.Infinity, lM, -Mathf.Infinity, Mathf.Infinity);
            foreach (RaycastHit2D hit in hits)
            {
                print(hit.collider.name);
                if (hit.collider.gameObject.GetComponent<Interactable>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        hit.collider.gameObject.GetComponent<Interactable>().Interact();
                    }
                    cursorDet = hit.collider.gameObject.GetComponent<Interactable>().hoverIcon;
                }
            }
        }
        else {cursorDet = 4;}
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
