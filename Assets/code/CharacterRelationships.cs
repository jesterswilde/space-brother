using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq; 

public class CharacterRelationships  {
    Action<List<string>> updateBond;
    Dictionary<Character, Relationship> relationships = new Dictionary<Character, Relationship>();
    public int GetBond(Character character)
    {
        Relationship relationship;
        if (relationships.TryGetValue(character, out relationship)){
            return relationship.Bond;
        }
        else
        {
            relationships[character] = new Relationship(); 
        }
        return 0; 
    }
    public CharacterRelationships Initialize(Action<List<string>> bondList)
    {
        updateBond = bondList;
        return this; 
    }
    internal void GenerateRelationships(List<Character> characters)
    {
        characters.ForEach((character) =>
        {
            if (character.Relationships != this)
            {
                relationships[character] = new Relationship();
            }
        });
    }

    public int GetLust(Character character)
    {
        Relationship relationship;
        if (relationships.TryGetValue(character, out relationship)){
            return relationship.Lust;
        }
        else
        {
            relationships[character] = new Relationship();
        }
        return 0;
    }

    public void ModifyBond(Character character, int mod)
    {
        Relationship relationship;
        if (!relationships.TryGetValue(character, out relationship)){
            relationships[character] = new Relationship();
        }
        relationships[character].Bond += mod;
        updateBond(relationships.Select((kvp) => {
            return kvp.Key.name + " Bond: " + kvp.Value.Bond + " | Lust: " + kvp.Value.Lust; 
        }).ToList());
    }

    public void ModifyLust(Character character, int mod)
    {
        Relationship relationship;
        if (!relationships.TryGetValue(character, out relationship)){
            relationships[character] = new Relationship();
        }
        relationships[character].Lust += mod;
    }
}

public class Relationship
{
    public int Bond = 0;
    public int Lust = 0; 
}
