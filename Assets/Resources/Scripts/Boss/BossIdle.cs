using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossBase
{
    public override void Active(BossStateControl manager, BossProfile profile)
    {
        deltaTime += Time.deltaTime;
        if(deltaTime >= 1.5f)
        {
            deltaTime = 0.0f;
            profile.animator.SetFloat("Speed", 10.0f);
            profile.PosSet();
            manager.ChangeState(new BossMove());
        }
    }
    
}
