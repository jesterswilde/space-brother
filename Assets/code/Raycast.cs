using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class Raycast : MonoBehaviour {

    [SerializeField]
    LayerMask doorMask;

    ITargetable currentlyTargeted; 


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if(Physics.Raycast(ray, out hit, doorMask)){
            ITargetable targeted = null; 
            if (doorMask.HasCollider(hit.collider))
            {
                targeted = GetTargetable(hit.collider.gameObject);
            }
            SetNewTarget(targeted);
            Target(); 
        }
    }
    ITargetable GetTargetable(GameObject go) {
            return go.GetComponents<Component>()
            .FirstOrDefault((comp) => comp is ITargetable) as ITargetable;
    }
    void SetNewTarget(ITargetable target)
    {
        if(currentlyTargeted != target)
        {
            if (currentlyTargeted != null) currentlyTargeted.Untargeted();
            currentlyTargeted = target;
            if(currentlyTargeted != null)
            {
                currentlyTargeted.Targeted();
            }
        }
    }
    void Target()
    {
        if(currentlyTargeted != null)
        {
            currentlyTargeted.Targeted(); 
        }
    }
    void CheckClicks() {
        if (Input.GetMouseButtonDown(0) && currentlyTargeted != null)
        {
            currentlyTargeted.Activated(); 
        }
        if(Input.GetMouseButtonDown(1) && currentlyTargeted != null)
        {
            currentlyTargeted.AltActivated(); 
        }
    }
}
