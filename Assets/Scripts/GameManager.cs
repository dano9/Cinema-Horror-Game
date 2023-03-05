using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;
    public SaveManager sM;
    public HUDManager hM;
    public Player player;
    public RoomManager rM;
    

    // Start is called before the first frame update
    void Start()
    {
        gM = this.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
