using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour {

    static Roll t;

    [SerializeField]
    List<string> names; 

    public static string RandomName()
    {
        return t.names[Random.Range(0, t.names.Count)];
    }
    public static int Bias(int diceNum, int diceSize, int offset = 0)
    {
        int result = offset; 
        for(int i = 0; i < diceNum; i++)
        {
            result += Random.Range(0, diceSize) + 1; 
        }
        return result; 
    }

    private void Awake()
    {
        t = this; 
    }
}
