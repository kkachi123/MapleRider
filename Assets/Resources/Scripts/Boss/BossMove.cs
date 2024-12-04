using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour, IActive
{
    public Transform[] bossPos;
    public int movePosIndex = 0;

    Vector3 movePos;

    public float moveSpeed = 30.0f;
    Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        movePos = bossPos[movePosIndex].position;
    }
    public BossState Active()
    {
        movePos = bossPos[movePosIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, movePos, moveSpeed * Time.deltaTime);
        if(Vector3.Distance(transform.position, movePos) <= 0.1f)
        {
            switch (movePosIndex)
            {
                case 0:
                    movePosIndex = 1;
                    break;
                case 1:
                    movePosIndex = 2;
                    break;
                case 2:
                    movePosIndex = 0;
                    break;
            }
            animator.SetBool("IsAttack", true);
            return BossState.Attack;
        }
        return BossState.Move;
    }
}
