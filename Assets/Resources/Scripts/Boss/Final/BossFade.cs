using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFade : BossBase
{
    bool moveComplete = false;
    public override void Active(BossStateControl manager, BossProfile profile)
    {
        if (!moveComplete && !profile.FadeIn()) 
            moveComplete = true;
        if (moveComplete && !profile.FadeOut()) 
        {
            moveComplete = false;
            manager.ChangeState(new BossIdle());
        }
    }
}
