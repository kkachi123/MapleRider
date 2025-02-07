using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossControl : MonoBehaviour
{
    Blackboard blackboard;
    [SerializeField] GameObject hpUI;
    [SerializeField] Slider[] hpBar;
    void Awake()
    {
        blackboard = GetComponent<Blackboard>();
    }
    void OnEnable()
    {
        hpUI.SetActive(true);
    }

    void Update()
    {
        if (!GameManager.Instance.bGameOver)
        {
            ColliderControl();
            if (GameManager.Instance.bossPhase != 5)
                hpBar[GameManager.Instance.bossPhase - 1].value = blackboard.hp / blackboard.maxHp;
            if (blackboard.hp <= 0.0f)
                ChangePhase();
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
        blackboard.hp = blackboard.maxHp;
        if (blackboard.razersCount < 3)
            blackboard.razersCount++;
        if (GameManager.Instance.bossPhase >= 3 && GameManager.Instance.bossPhase < 5)
        {
            blackboard.summonEliteCount++;
            blackboard.eliteSummoner.EliteSpawn(blackboard.summonEliteCount);
        }

        if (GameManager.Instance.bossPhase == 5)
        {
            hpUI.SetActive(false);
            blackboard.time = 0.0f;
            blackboard.isAlive = false;
        }
    }
}
