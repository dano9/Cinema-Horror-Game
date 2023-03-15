using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] private Room[] roomsArray;
    [SerializeField] private string[] roomNames;
    public Dictionary<string, Room> rooms;
    public int currentRoom;
    public Room instantiatedRoom;
    public Transform roomHolder;
    public bool isSwitchingRoom;
    private Coroutine curRoomSwitchCor;
    
    // Start is called before the first frame update
    void Start()
    {
        rooms = new Dictionary<string, Room>();
        for (int r = 0; r < roomsArray.Length; r++)
        {
            rooms[roomsArray[r].roomName] = roomsArray[r];
        }
       SwitchRoom(roomsArray[(int)GameManager.gM.svM.curSaveData["curRoom"]].roomName);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            currentRoom = (currentRoom + 1) % roomNames.Length;
            SwitchRoom(roomNames[currentRoom], true);
        }
    }
    public void SwitchRoom(string targRoom, bool transition = false)
    {
        if (curRoomSwitchCor != null) {StopCoroutine(curRoomSwitchCor);}
        if (!transition)
        {
            if (instantiatedRoom != null) {Destroy(instantiatedRoom.gameObject);}
            instantiatedRoom = Instantiate(rooms[targRoom].gameObject, Vector2.zero, Quaternion.identity, roomHolder).GetComponent<Room>();
            GameManager.gM.svM.curSaveData["curRoom"] = rooms[targRoom].roomIndex;
        }
        else
        {
            curRoomSwitchCor = StartCoroutine(SwitchRoomIE(targRoom));
        }
    }
    public IEnumerator SwitchRoomIE(string targRoom)
    {
        isSwitchingRoom = true;
        GameManager.gM.hM.vM.FadeIn();
        while (GameManager.gM.hM.vM.isFading)
        {
            yield return null;
        }
        SwitchRoom(targRoom);
        GameManager.gM.hM.vM.FadeOut();
        while (GameManager.gM.hM.vM.isFading)
        {
            yield return null;
        }
        isSwitchingRoom = false;
    }
}
