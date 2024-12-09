using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossBase
{
    public override void Active(BossStateControl manager, BossProfile profile)
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= 1.5f)
        {
            deltaTime = 0.0f;
            //profile.animator.SetFloat("BulletCount", profile.bulletCount);
            profile.animator.SetBool("Shoot", true);
            //profile.PosSet();
            manager.ChangeState(new BossAttack());
        }
    }
}
