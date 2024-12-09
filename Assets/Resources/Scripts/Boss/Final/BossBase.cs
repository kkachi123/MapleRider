using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase
{
    public float deltaTime = 0.0f;
    public virtual void Active(BossStateControl manager, BossProfile profile) { }
}
