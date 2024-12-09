using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : BossBase
{
    bool couroutine = false;
    public override void Active(BossStateControl manager, BossProfile profile)
    {
        if (!couroutine)
        {
            couroutine = true;
            profile.BossDeath();
        }
    }
}
