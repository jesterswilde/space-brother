using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class PFTest : MonoBehaviour {
    [SerializeField]
    Room startingPoint;
    [SerializeField]
    Room endingPoint;

    private void Start()
    {
        List<Room> allRooms = FindObjectsOfType<Room>().ToList();
        List<Room> path =  Pathfinding.FromAToB(allRooms, startingPoint, endingPoint);
        path.ForEach((node) => node.GetComponent<Renderer>().material.color = Color.black); 
        Dictionary<Room, float> biases = Pathfinding.GetRoomBiasByDistance(path, 0.6f);
        biases.ForEach((kvp) => kvp.Key.gameObject.GetComponent<MeshRenderer>().material.color = new Color(kvp.Value, kvp.Value, kvp.Value));
    }
}
