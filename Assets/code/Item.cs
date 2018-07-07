using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    [SerializeField]
    ExpressionEnum expression;
    public ExpressionEnum Expression { get { return expression; } }
    [SerializeField]
    float bias; 
    public float Bias { get { return bias; } }

}
