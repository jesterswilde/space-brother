using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Room : MonoBehaviour {
    List<Item> items = new List<Item>();
    [SerializeField]
    List<Room> connectingRooms = new List<Room>();
    public List<Room> ConnectingRooms { get { return connectingRooms; } }

    [SerializeField]
    TraitsEnum trait;
    public TraitsEnum Trait { get { return trait; } }
    [SerializeField]
    float bias;
    public float Bias { get { return bias; } }

    List<Doorway> doorways;
    List<Collider> colliders = new List<Collider>();
    Renderer rend;
    List<Character> charactersInRoom = new List<Character>();
    public List<Character> CharactersInRoom { get { return charactersInRoom; } }
    [SerializeField]
    Item item; 
    public ExpressionEnum Expression
    {
        get
        {
            if (item != null)
            {
                return item.Expression;
            }
            var values = (int[])Enum.GetValues(typeof(ExpressionEnum));
            return (ExpressionEnum) values[UnityEngine.Random.Range(0, values.Length)];
        }
    }

    bool isPlacing = false;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        doorways = GetComponentsInChildren<Doorway>().ToList();
    }
    private void Start()
    {
        RoomManager.RegisterRoom(this); 
    }
    private void Update()
    {
        //UpdateColor();
    }
    void UpdateColor()
    {
        if (isPlacing && isOverlapping())
        {
            rend.material.color = Color.red;
        }
        if (isPlacing && isConnecting() && !isOverlapping())
        {
            rend.material.color = Color.green;
        }
        if (isPlacing && !isConnecting() && !isOverlapping())
        {
            rend.material.color = Color.blue;
        }
        if (!isPlacing)
        {
            rend.material.color = Color.white;
        }
    }
    public bool CanPlace()
    {
        return isPlacing && isConnecting() && !isOverlapping();
    }
    public void StartPlacing()
    {
        isPlacing = true; 
    }
    public void Place()
    {
        isPlacing = false; 
    }

    public void ConnectRoom(Room room)
    {
        connectingRooms.Add(room); 
    }
    public void DisconnectRoom(Room room)
    {
        connectingRooms.Add(room); 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isPlacing && RoomManager.RoomsMask.HasCollider(other) && !colliders.Contains(other))
        {
            colliders.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (isPlacing && RoomManager.RoomsMask.HasCollider(other) && colliders.Contains(other))
        {
            colliders.Remove(other);
        }
    }
    bool isOverlapping()
    {
        return colliders.Count > 0; 
    }
    bool isConnecting()
    {
        return doorways.Any((doorway) => doorway.WillConnect());
    }
    public void AddCharacterToRoom (Character character)
    {
        charactersInRoom.Add(character); 
    }
    public void ClearCharactersFromRoom()
    {
        charactersInRoom.Clear(); 
    }
}
