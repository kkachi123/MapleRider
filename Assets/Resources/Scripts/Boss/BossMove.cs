using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : BossBase
{
    public override void Active(BossStateControl manager, BossProfile profile)
    {
        if(!profile.IsMoving())
            manager.ChangeState(new BossAttack());
    }
}
