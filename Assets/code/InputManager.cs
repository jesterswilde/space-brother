using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager  {

    static bool isMaking = false;
    static bool isRotatingBuilding = false; 
    public static bool IsRotatingBuildling { get { return isRotatingBuilding; } }
    public static bool IsMaking { get { return isMaking; } }

    public static void CollectInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) isMaking = !isMaking;
        isRotatingBuilding = Input.GetKey(KeyCode.Space); 
    }
}
