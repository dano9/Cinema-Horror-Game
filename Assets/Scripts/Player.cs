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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GameManager.gM.hM.iUI.UpdateItemSlots(items);
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
        RaycastHit2D[] hits = Physics2D.CircleCastAll(mousePos, 0.05f, Vector2.up, Mathf.Infinity, lM, -Mathf.Infinity, Mathf.Infinity);
        int cursorDet = 0;
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
        GameManager.gM.hM.curCursorMode = cursorDet;
    }
}
