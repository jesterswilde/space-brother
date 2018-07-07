using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 

public class Pathfinding {

	public static List<Room> FromAToB(List<Room> allRooms, Room startingRoom, Room endingRoom)
    {
        Queue<Room> toCheck = new Queue<Room>();
        toCheck.Enqueue(startingRoom); 
        Dictionary<Room, int> distances = new Dictionary<Room, int>();
        distances[startingRoom] = 0;
        int currentShortest = int.MaxValue; 
        while(toCheck.Count > 0)
        {
            Room checking = toCheck.Dequeue();
            int stepsTo = distances[checking];
            if(stepsTo < currentShortest)
            {
                checking.ConnectingRooms.ForEach((connectedRoom) =>
                {
                    if (!distances.ContainsKey(connectedRoom) || distances[connectedRoom] > stepsTo + 1)
                    {
                        distances[connectedRoom] = stepsTo + 1;
                        toCheck.Enqueue(connectedRoom);
                        if(connectedRoom == endingRoom && (stepsTo + 1) < currentShortest)
                        {
                            currentShortest = stepsTo + 1; 
                        }
                    }
                });
            }
        }
        List<Room> path = new List<Room>();
        path.Add(endingRoom);
        Room currentRoom = endingRoom;
        int steps = distances[endingRoom]; 
        while(steps > 0)
        {
            int nextSteps = int.MaxValue;
            Room nextRoom = null;
            currentRoom.ConnectingRooms.ForEach((room) =>
            {
                int distance;
                if (distances.TryGetValue(room, out distance))
                {
                    if(distance < nextSteps)
                    {
                        nextSteps = distance;
                        nextRoom = room; 
                    }
                }
            });
            steps = nextSteps;
            path.Add(nextRoom);
            currentRoom = nextRoom; 
        }
        return path; 
    }

    public static Dictionary<Room, float> GetRoomBiasByDistance(List<Room> path, float decay)
    {
        Dictionary<Room, float> biases = new Dictionary<Room, float>();
        Queue<Room> toCheck = new Queue<Room>();
        path.ForEach((room) =>
        {
            toCheck.Enqueue(room);
            biases[room] = 1f;
        });
        while(toCheck.Count > 0)
        {
            Room checking = toCheck.Dequeue();
            float bias = biases[checking] * decay;
            checking.ConnectingRooms.ForEach((room) =>
            {
                if (!biases.ContainsKey(room) || biases[room] < bias)
                {
                    biases[room] = bias;
                    toCheck.Enqueue(room);
                }
            });
        }
        return biases; 
    }
}
