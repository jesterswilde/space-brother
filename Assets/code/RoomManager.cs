using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    static RoomManager t;
    List<Room> rooms = new List<Room>();
    public static List<Room> AllRooms { get { return t.rooms; } }
    [SerializeField]
    LayerMask roomsMask; 
    public static LayerMask RoomsMask { get { return t.roomsMask; } }
    [SerializeField]
    LayerMask doorMask;
    public static LayerMask DoorMask { get { return t.doorMask; } }

    public static void RegisterRoom(Room room)
    {
        t.rooms.Add(room);
    }
    public static void RemoveRoom(Room room)
    {
        t.rooms.Remove(room);
    }
    public static void MakeRoom(Doorway door)
    {

    }
    private void Awake()
    {
        t = this; 
    }
}
