using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnWheel : Interactable
{
    public Transform handle;
    public Transform backLever;
    public bool isGrabbed;
    public float radius = 1;
    public Transform center;
    public float maxTurnSpeed = 0.1f;

    public float value = 0;
    public float valueSensitivity = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        prolongInteraction = true;
    }

    // Update is called once per frame
    void Update()
    {
        ManageWheel();
    }
    public override void Interact()
    {
        base.Interact();
        isGrabbed = true;
    }
    public override void EndInteract()
    {
        base.EndInteract();
        isGrabbed = false;
    }
    public void ManageWheel()
    {
        
        if (isGrabbed)
        {
            Vector2 tangent = CalculateTangent(handle.localPosition.x, handle.localPosition.y).normalized;
            Vector2 targPos = GameManager.gM.player.worldMousePos;
            Vector2 tdir = targPos - (Vector2)handle.position;
            int moveDir = 0;
            if (Vector2.Distance(tdir.normalized, tangent) < Vector2.Distance(tdir.normalized, -tangent))
            {
                moveDir = 1;
            }
            else
            {
                moveDir = -1;
            }
            value += valueSensitivity * -moveDir;
            //handle.position += (Vector3)tangent * maxTurnSpeed * moveDir;
            //handle.position = Vector3.MoveTowards(handle.position,targPos,maxTurnSpeed);
        }
        handle.localPosition = CalculatePointOnCircle(-value % 1);
        Vector2 dir = (handle.localPosition);
        if (dir.magnitude != radius)
        {
            handle.localPosition = (Vector3)(dir.normalized * radius);
        }
        handle.right = dir.normalized;
        backLever.up = dir.normalized;
    }
    public Vector2 CalculateTangent(float x, float y)
    {
        float theta = Mathf.Atan2(y, x); // angle of point relative to center of circle
        float tangentAngle = theta + (Mathf.PI / 2f); // add 90 degrees to get tangent angle
        float tangentX = Mathf.Cos(tangentAngle) * radius; // x coordinate of tangent
        float tangentY = Mathf.Sin(tangentAngle) * radius; // y coordinate of tangent
        return new Vector2(tangentX, tangentY);
    }
    public Vector2 CalculatePointOnCircle(float t)
    {
        float angle = t * 2f * Mathf.PI; // convert t value to angle in radians
        float x = Mathf.Cos(angle) * radius; // calculate x coordinate
        float y = Mathf.Sin(angle) * radius; // calculate y coordinate
        return new Vector2(x, y);
    }
}
