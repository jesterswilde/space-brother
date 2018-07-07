using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    string name;
    public string Name { get { return name; } }
    CharacterTraits traits;
    public CharacterTraits Traits { get { return traits; } }
    [SerializeField]
    List<string> dbTraits;
    CharacterExpression expression;
    public CharacterExpression Expression { get { return expression; } }
    [SerializeField]
    List<string> dbSkills;
    [SerializeField]
    List<string> dbPreference;
    [SerializeField]
    List<string> dbBond;
    CharacterAttraction attraction; 
    public CharacterAttraction Attraction { get { return attraction; } }
    CharacterRelationships relationships;
    public CharacterRelationships Relationships { get { return relationships; } }
    CharacterPlacement placement; 
    public CharacterPlacement Placement { get { return placement; } }
    [SerializeField]
    int dbidentity;
    [SerializeField]
    int dbAttraction;
    [SerializeField]
    int mood;
    public int Mood { get { return mood; } }
    [SerializeField]
    Room homeRoom;
    public Room HomeRoom { get { return homeRoom; } }
    [SerializeField]
    Room workRoom;
    public Room WorkRoom { get { return workRoom; } }

    public void SetWorkRoom(Room room)
    {
        workRoom = room;
    }
    public void SetHomeRoom(Room room)
    {
        homeRoom = room;
    }
    public void ModifyMood(int mod)
    {
        mood += mod; 
    }

    private void Start()
    {
        name = Roll.RandomName();
        gameObject.name = name; 
        traits = new CharacterTraits().Initialize((list)=> dbTraits = list);
        expression = new CharacterExpression().Initialize(
            (skill) => dbSkills = skill,
            (preference) => dbPreference = preference
        );
        attraction = new CharacterAttraction().Initialize((identity, attraction) =>
        {
            dbidentity = identity;
            dbAttraction = attraction;
        });
        relationships = new CharacterRelationships().Initialize(
            (list) => dbBond = list
        ); 
        placement = new CharacterPlacement().Initialize(this);
        GameManager.RegisterCharacter(this); 
    }
}
