using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceDetectors : MonoBehaviour {

    List<Collider> colliders = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (Viewer.InsideMask.HasCollider(other) && !colliders.Contains(other))
        {
            colliders.Add(other);
            Debug.Log("added " + colliders.Count); 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Viewer.InsideMask.HasCollider(other) && colliders.Contains(other))
        {
            colliders.Remove(other);
            Debug.Log("removed " + colliders.Count); 
        }
    }
    public bool isInside()
    {
        return colliders.Count > 0; 
    }
}
