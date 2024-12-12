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
        action = new BossFade();
    }
    void OnEnable()
    {
        hpUI.SetActive(true);
        action = new BossFade();
    }

    void Update()
    {
        if (!GameManager.Instance.bGameOver)
        {
            ColliderControl();
            if (GameManager.Instance.bossPhase != 5)
                hpBar[GameManager.Instance.bossPhase - 1].value = profile.hp / profile.maxHp;
            if (profile.hp <= 0.0f)
                ChangePhase();
            else
                TransitionState();
        }
    }
    void ColliderControl()
    {
        if (GameManager.Instance.elitCount > 0)
            this.GetComponent<CircleCollider2D>().enabled = false;
        else
            this.GetComponent<CircleCollider2D>().enabled = true;
    }
    void ChangePhase()
    {
        GameManager.Instance.bossPhase++;
        profile.hp = profile.maxHp;
        if (profile.razersCount < 3)
            profile.razersCount++;
        if (GameManager.Instance.bossPhase >= 3 && GameManager.Instance.bossPhase < 5)
        { 
            profile.summonEliteCount++;
            profile.EliteSummon();
        }

        if (GameManager.Instance.bossPhase == 5)
        {
            hpUI.SetActive(false);
            ChangeState(new BossDeath());
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
