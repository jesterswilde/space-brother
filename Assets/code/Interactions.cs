using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Interactions  {
    public static void SimulateInteraction(Room room, List<Character> characters)
    {
        //RelationshipEvents
            //Fight
            //Fuck
            //Bromance
            //Love Triangle
            //From Fight to Friend or from bromance to enemies
        if(characters.Count > 0)
        {
            characters.ForEach((character) =>
            {
                float bias = character.Traits.GetTraitBias(room.Trait);
                int withRoom = 1;
                if (room.Bias * bias < 0) withRoom = -1;
                int biasMod = (int)(bias * withRoom / 3); //the 3 is just to make the bias mean a bit less
                int moodMod = Roll.Bias(5, 21, biasMod-55) / 4; //4 is to get the total mood mod in a reasonable 
                character.ModifyMood(moodMod);
                Debug.Log(character.Name + " mood: " + character.Mood); 
            });
        }
        if(characters.Count > 1)
        {
            ModBond(characters, room.Trait);
            ModifyLust(characters, room.Expression);
        }
    }
    static void ModBond(List<Character> characters, TraitsEnum roomTrait)
    {
        for(int i = 0; i < characters.Count; i++)
        {
            for (int j = i + 1; j < characters.Count; j++)
            {
                Character charA = characters[i];
                Character charB = characters[j];
                float distance = charA.Traits.GetTraitBias(roomTrait) - charB.Traits.GetTraitBias(roomTrait);
                distance = 25 - Mathf.Abs(distance);
                float biasMod = distance / 4;
                int result = Roll.Bias(5, 21, (int)biasMod - 55);
                charA.Relationships.ModifyBond(charB, result);
                charB.Relationships.ModifyBond(charA, result);
                Debug.Log("Bond for " + charA.Name + " and " + charB.Name + ": " + charA.Relationships.GetBond(charB) + " \\ " + charB.Relationships.GetBond(charA) + " | Distance:" + distance);
            }
        }
    }
    static void ModifyLust(List<Character> characters, ExpressionEnum expression)
    {
        List<int> expressionValue = characters.Select((character) =>
        {
            return character.Expression.ExprsesionSkillnRoll(expression);
        }).ToList();
        for(int i = 0; i < characters.Count; i++)
        {
            for(int j = 0; j < characters.Count; j++)
            {
                if(j != i)
                {
                    Character charA = characters[i];
                    Character charB = characters[j];
                    int expressRoll = expressionValue[j];
                    float attractionBias = charA.Attraction.AttractionBias(charB.Attraction.Identity);
                    float preference = charA.Expression.GetPreference(expression);
                    int amount = (int)(((expressRoll + preference) * attractionBias / 2) / 4);
                    charA.Relationships.ModifyLust(charB, amount);
                    Debug.Log(charA.name + " lusting for " + charB.name + ": " + amount); 
                }
            }
        }
    }
}

public struct Interaction
{

}
