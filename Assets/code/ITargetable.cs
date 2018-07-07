using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable  {
    void Targeted();
    void Untargeted();
    void Activated();
    void AltActivated(); 
}
