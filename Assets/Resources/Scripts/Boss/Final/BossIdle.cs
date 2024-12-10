using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossBase
{
    public override void Active(BossStateControl manager, BossProfile profile)
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= 3.0f)
        {
            deltaTime = 0.0f;
            if(profile.preFade)
            {
                profile.animator.SetBool("Shoot", true);
                manager.ChangeState(new BossAttack());
                profile.preFade = false;
                int rand = Random.Range(1, GameManager.Instance.bossPhase + 1);
                profile.attackType = (BossAttackType)rand;
            }
            else
            {
                profile.animator.SetBool("Fade", true);
                profile.PosSet();
                manager.ChangeState(new BossFade());
                profile.preFade = true;
            }
        }
    }
}
