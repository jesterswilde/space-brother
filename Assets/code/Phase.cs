using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Phase {
    static int numRounds = 10; 

    public static void BeginRound()
    {
        Debug.Log("Starting Round"); 
        GameManager.Characters.ForEach((character) => character.Placement.CalculateBiases());
        Enumerable.Range(0, 10).ForEach((i) => BeginPhase(i));
    }
    static void BeginPhase(int phaseNum)
    {
        Debug.Log("Startin Phase"); 
        GameManager.Characters.ForEach((character) =>
        {
            Room room = character.Placement.PickRoom(phaseNum);
            room.AddCharacterToRoom(character);
        });
        RoomManager.AllRooms.ForEach((room) =>
        {
            Interactions.SimulateInteraction(room, room.CharactersInRoom);
        });
        RoomManager.AllRooms.ForEach((room) => room.ClearCharactersFromRoom()); 
    }


}
