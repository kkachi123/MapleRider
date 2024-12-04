using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateControl : MonoBehaviour
{
    [HideInInspector] public BossState state;
    
    IActive[] actives = new IActive[4];

    Animator animator;
    public float hp = 1000.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        state = BossState.Move;
        actives[0] = GetComponent<BossIdle>();
        actives[1] = GetComponent<BossMove>();
        actives[2] = GetComponent<BossAttack>();
        actives[3] = GetComponent<BossDeath>();
    }
    void Update()
    {
        if (state == BossState.Death) return;
        if(hp <= 0.0f && state != BossState.Death)
        {
            state = BossState.Death;
            animator.SetBool("Death", true);
        }
        state = actives[(int)state].Active();
    }
}
