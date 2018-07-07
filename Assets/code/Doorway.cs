using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : MonoBehaviour {


    bool isActive; 
    public bool IsActive { get { return IsActive; } }
    List<Collider> colliders = new List<Collider>();
    Room parentRoom; 
    public Room ParentRoom { get { return ParentRoom; } }

    private void OnTriggerEnter(Collider other)
    {
        if (RoomManager.DoorMask.HasCollider(other) && !colliders.Contains(other))
        {
            colliders.Add(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (RoomManager.DoorMask.HasCollider(other) && colliders.Contains(other))
        {
            colliders.Remove(other);
        }
    }
    public bool WillConnect()
    {
        return colliders.Count > 0; 
    }
    private void Awake()
    {
        parentRoom = GetComponentInParent<Room>(); 
    }
    public Room GetConnectedRoom()
    {
        if(colliders.Count > 0)
        {
            return null; 
        }
        return colliders[0].GetComponent<Doorway>().ParentRoom; 
    }
}
