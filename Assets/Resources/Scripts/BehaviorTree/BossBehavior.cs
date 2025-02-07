using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] BehaviorTree behaviorTree;
    Blackboard blackboard;
    void Awake()
    {
        blackboard = GetComponent<Blackboard>();
        blackboard.Init(0);
        behaviorTree.SetTarget(gameObject);
    }
    void Update()
    {
        if (behaviorTree != null)
        {
            behaviorTree.Operate();
        }
    }
    #region decorator
    bool isAlive()
    {
        return !blackboard.isAlive;
    }
    bool isFade()
    {
        return blackboard.isFade;
    }
    #endregion
    #region action
    Node.STATE Idle()
    {
        if(blackboard.isFade)
            return Node.STATE.FAILED;
        blackboard.time += Time.deltaTime;
        if (blackboard.time >= 3.0f)
        {
            blackboard.time = 0.0f;
            blackboard.animator.SetBool("Fade", true);
            blackboard.PosSet();
            blackboard.isFade = true;
            return Node.STATE.SUCCESS;
        }
        return Node.STATE.RUN;
    }
    Node.STATE Fading()
    {
        Color imgAlpha = blackboard.bossImage.color;
        if(!blackboard.preFade)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
            if (imgAlpha.a > 0)
            {
                blackboard.time += Time.deltaTime / blackboard.fadeTime;
                imgAlpha.a = Mathf.Lerp(1, 0, blackboard.time);
                blackboard.bossImage.color = imgAlpha;
                return Node.STATE.RUN;
            }
            blackboard.time = 0.0f;
            imgAlpha.a = 0;
            transform.position = blackboard.movePos;
            switch (blackboard.movePosIndex)
            {
                case 0:
                    blackboard.movePosIndex = 1;
                    break;
                case 1:
                    blackboard.movePosIndex = 2;
                    break;
                case 2:
                    blackboard.movePosIndex = 0;
                    break;
            }
            blackboard.preFade = true;
        }
        else
        {
            if (imgAlpha.a < 1)
            {
                blackboard.time += Time.deltaTime / blackboard.fadeTime;
                imgAlpha.a = Mathf.Lerp(0, 1, blackboard.time);
                blackboard.bossImage.color = imgAlpha;
                return Node.STATE.RUN;
            }
            blackboard.time = 0.0f;
            imgAlpha.a = 1;
            blackboard.animator.SetBool("Fade", false);
            blackboard.bossImage.color = imgAlpha;
            this.GetComponent<CircleCollider2D>().enabled = true;
            blackboard.preFade = false;
            blackboard.isFade = false;
            return Node.STATE.SUCCESS;
        }
        return Node.STATE.FAILED;
    }

    #endregion

}
