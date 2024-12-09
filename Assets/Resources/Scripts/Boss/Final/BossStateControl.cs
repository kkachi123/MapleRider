using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStateControl : MonoBehaviour
{
    BossBase action;
    BossProfile profile;
    [SerializeField] GameObject hpUI;
    [SerializeField] Slider[] hpBar;
    void Awake()
    {
        profile = GetComponent<BossProfile>();
        action = new BossIdle();
    }
    void OnEnable()
    {
        hpUI.SetActive(true);
        action = new BossIdle();
    }

    void Update()
    {
        if (!GameManager.Instance.bGameOver)
        {

            if (GameManager.Instance.bossPhase != 5)
                hpBar[GameManager.Instance.bossPhase - 1].value = profile.hp / profile.maxHp;
            if (profile.hp <= 0.0f)
            {
                GameManager.Instance.bossPhase++;
                profile.hp = profile.maxHp;

                if(GameManager.Instance.bossPhase == 5)
                {
                    hpUI.SetActive(false);
                    ChangeState(new BossDeath());
                }
            }
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
            action.Active(this, profile);
        }
    }
}
