using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossBase
{
    float shootTerm = 0.5f;
    public override void Active(BossStateControl manager, BossProfile profile)
    {
        shootTerm -= Time.deltaTime;
        if (shootTerm <= 0.0f)
        {
            shootTerm = 0.5f;
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Attack);

            if (!profile.IsShooting())
            {
                manager.ChangeState(new BossIdle());
            }
        }
    }
}
