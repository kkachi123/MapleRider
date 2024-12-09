using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EliteBase 
{
    public float deltaTime = 0.0f;
    public virtual void Active(EliteStateControl manager , EliteProfile profile) { }
}
