using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    static GameManager t;
    List<Character> characters = new List<Character>();
    public static List<Character> Characters { get { return t.characters; } }

    public static void RegisterCharacter(Character character)
    {
        t.characters.Add(character); 
    }
    public static void RemoveCharacter(Character character)
    {
        t.characters.Remove(character); 
    }

    void GenerateRelationships()
    {
        characters.ForEach((character) => character.Relationships.GenerateRelationships(characters));
    }

    private void Update()
    {
        InputManager.CollectInput();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Phase.BeginRound();
        }
    }
    private void Awake()
    {
        t = this; 
    }
}
