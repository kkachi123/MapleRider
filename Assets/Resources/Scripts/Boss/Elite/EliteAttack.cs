using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteAttack : EliteBase
{
    float shootTerm = 0.3f;
    public override void Active(EliteStateControl manager, EliteProfile profile)
    {
        shootTerm -= Time.deltaTime;
        if (shootTerm <= 0.0f)
        {
            shootTerm = 0.3f;
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Attack);

            if (!profile.IsShooting())
            {
                manager.ChangeState(new EliteIdle());
            }
        }
    }
}
