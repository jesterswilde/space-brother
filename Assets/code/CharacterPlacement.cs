using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class CharacterPlacement {

    Character character;
    int workStart;
    int workEnd;
    int roundsOfWork = 3;
    float biasBase = 0.2f;
    float totalBias;
    Dictionary<Room, float> distanceBias; 
    public void CalculateBiases()
    {
        workStart = Roll.Bias(3, 3, -2);
        workEnd = workStart + roundsOfWork;
        List<Room> path = Pathfinding.FromAToB(RoomManager.AllRooms, character.HomeRoom, character.WorkRoom);
        distanceBias = Pathfinding.GetRoomBiasByDistance(path, 0.7f);
        totalBias = distanceBias.Aggregate(0f,(total, current) => total + current.Value + biasBase); 
    }
    public Room PickRoom(int phaseNumber)
    {
        if(phaseNumber >= workStart && phaseNumber < workEnd)
        {
            return character.WorkRoom; 
        }
        return GetBiasedRoom(); 
    }

   Room GetBiasedRoom()
    {
        float guess = Random.Range(0, totalBias);
        float currentAmount = 0;
        foreach(KeyValuePair<Room, float> kvp in distanceBias)
        {
            currentAmount += kvp.Value + biasBase; 
            if(currentAmount > guess)
            {
                return kvp.Key; 
            }
        }
        return null;
    }

    public CharacterPlacement Initialize(Character charac)
    {
        character = charac; 
        return this; 
    }
}
