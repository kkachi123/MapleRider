using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : MonoBehaviour, IActive
{
    float deltaTime = 0.0f;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public BossState Active()
    {
        deltaTime += Time.deltaTime;
        if(deltaTime >= 3.0f)
        {
            deltaTime = 0.0f;
            animator.SetFloat("Speed", 10.0f);
            return BossState.Move;
        }
        return BossState.Idle;
    }
    
}
