using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateControl : MonoBehaviour
{
    BossBase action;
    BossProfile profile;
    void Start()
    {
        profile = GetComponent<BossProfile>();
        action = new BossMove();
    }

    void Update()
    {
        if (!GameManager.Instance.bGameOver)
        {
            if (profile.hp <= 0.0f)
                ChangeState(new BossDeath());
            else
                TransitionState();
        }
    }

    public void ChangeState(BossBase bossBase)
    {
        action = bossBase;
        TransitionState();
    }
    void TransitionState()
    {
        if (action != null)
        {
            action.Active(this , profile);
        }
    }
}
