using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class CharacterTraits {

    Dictionary<TraitsEnum, float> traitLeaning = new Dictionary<TraitsEnum, float>();
    Action<List<string>> updateDB;
    int totalAmount = 0;
    public int TotalAmount { get { return totalAmount; } }

    void GenerateTraits()
    {
        Util.EnumList<TraitsEnum>()
            .ForEach((trait) =>
            {
                traitLeaning[trait] = Roll.Bias(5, 40, -100);
            });
        SumTraits(); 
        SerializeTraits(); 
    }
    void SumTraits()
    {
        totalAmount = traitLeaning.Aggregate(0, (total, trait) => total + (int)Math.Abs(trait.Value));
    }
    void SerializeTraits()
    {
        List<string> dbTraitLeaning = new List<string>();
        traitLeaning.ForEach((kvp) =>
        {
            dbTraitLeaning.Add(kvp.Key + ": " + kvp.Value);
        });
        updateDB(dbTraitLeaning); 
    }
    public float GetTraitBias(TraitsEnum trait)
    {
        return traitLeaning[trait]; 
    }

    public CharacterTraits Initialize(Action <List<string>> action)
    {
        updateDB = action; 
        GenerateTraits();
        return this; 
    }
}

[Serializable]
public struct KVPTraitFloat
{
    public TraitsEnum Trait;
    public float Value;

    public KVPTraitFloat(TraitsEnum trait, float value)
    {
        Trait = trait;
        Value = value; 
    }
}
