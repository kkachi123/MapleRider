using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class EliteBase 
{
    public float deltaTime = 0.0f;
    public abstract void Active(EliteStateControl manager , EliteProfile profile);
}
