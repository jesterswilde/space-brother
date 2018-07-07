using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class CharacterAttraction {

    Action<int, int> updateValuesDB;
    int identity;
    public int Identity { get { return identity; } }
    int attraction; 
    public int Attraction { get { return attraction; } }

    public float AttractionBias(int ident)
    {
        int diff = Math.Abs(ident - attraction);
        if (diff == 0) return 1;
        if (diff == 1) return 0.8f;
        if (diff == 2) return 0.5f;
        if (diff== 3) return 0.2f;
        return 0f; 
    }

    void GenerateAttraction()
    {
        identity = Roll.Bias(1, 10);
        attraction = Roll.Bias(1, 10);
        SerializeAttraction(); 
    }
    void SerializeAttraction()
    {
        updateValuesDB(identity, attraction);
    }

	public CharacterAttraction Initialize(Action<int, int> updateValues)
    {
        updateValuesDB = updateValues;
        GenerateAttraction(); 
        return this; 
    }
}
