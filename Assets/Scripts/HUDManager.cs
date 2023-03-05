using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public InfoReader iR;
    public InteractabilityUI iUI;
    public RectTransform hotbar;
    public VignetteManager vM;
    bool showingHotbar;
    public int curCursorMode = 0;
    int lastCursorMode = 0;
    public Texture2D[] cursors;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (showingHotbar || Input.mousePosition.x > (Screen.width) - 100)
        {
            hotbar.localPosition = Vector3.Lerp(hotbar.localPosition, Vector2.up * 100, 0.1f);
        }
        else
        {
            hotbar.localPosition = Vector3.Lerp(hotbar.localPosition, Vector2.zero, 0.1f);
        }
    }
    public void Update()
    {
        if (lastCursorMode != curCursorMode)
        {
            Cursor.SetCursor(cursors[curCursorMode], Vector2.zero, CursorMode.Auto);
            lastCursorMode = curCursorMode;
        }
    }
    public IEnumerator ShowHotbar()
    {
        showingHotbar = true;
        yield return new WaitForSeconds(2f);
        showingHotbar = false;
    }
}
