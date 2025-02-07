using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] BehaviorTree SCbehaviorTree;
    BehaviorTree behaviorTree;
    Blackboard blackboard;
    void Awake()
    {
        blackboard = GetComponent<Blackboard>();
        blackboard.Init(0);
        behaviorTree = SCbehaviorTree.Clone();
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
    bool IsAlive()
    {
        return !blackboard.isAlive;
    }
    bool IsFade()
    {
        return blackboard.isFade;
    }
    #endregion
    #region action
    Node.STATE Shooting()
    {
        if(blackboard.attackType != BossAttackType.Shoot)
            return Node.STATE.FAILED;
        blackboard.time += Time.deltaTime;
        if(blackboard.time < 0.5f)
        {
            return Node.STATE.RUN;
        }
        blackboard.time = 0.0f;
        float angleStart = 0;
        switch (blackboard.movePosIndex)
        {
            case 0:
                angleStart = 0;
                break;
            case 1:
                angleStart = 90;
                break;
            case 2:
                angleStart = 180;
                break;
        }
        angleStart += blackboard.addShootAngle;
        blackboard.addShootAngle += 30.0f;
        for (int i = 1; i < 6; i++)
        {
            float angle = angleStart + i * 30f;
            float radian = angle * Mathf.Deg2Rad;
            Vector2 shootDirection = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
            GameObject bullet = Instantiate(blackboard.bulletPrefab, blackboard.shootPoint.transform.position, blackboard.shootPoint.transform.rotation);
            bullet.GetComponent<BossBullet>().power = blackboard.power;
            bullet.GetComponent<BossBullet>().spreadDirection = shootDirection;
        }

        blackboard.bulletCount--;
        SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.Attack);
        if (blackboard.bulletCount <= 0)
        {
            blackboard.bulletCount = blackboard.maxBulletCount;
            blackboard.addShootAngle = 30.0f;
            blackboard.animator.SetBool("Shoot", false);
            blackboard.isFade = true;
            blackboard.doPattern = false;
            return Node.STATE.SUCCESS;
        }
        return Node.STATE.RUN;
    }
    Node.STATE EliteSummon()
    {
        if (blackboard.attackType != BossAttackType.EliteSummon)
            return Node.STATE.FAILED;
        blackboard.time += Time.deltaTime;
        if (blackboard.time >= 1.0f)
        {
            blackboard.time = 0.0f;
            blackboard.eliteSummoner.EliteSpawn(blackboard.summonEliteCount);
            blackboard.animator.SetBool("Shoot", false);
            blackboard.isFade = true;
            blackboard.doPattern = false;
            return Node.STATE.SUCCESS;
        }
        return Node.STATE.RUN;
    }
    Node.STATE Razer()
    {
        if (blackboard.Razers[0].activeSelf) return Node.STATE.RUN;
        Color imgAlpha = blackboard.RazersWarning[0].color;
        for (int i = 0; i < blackboard.RazersWarningPrefab.Length; i++)
            blackboard.RazersWarningPrefab[i].GetComponent<RazerMove>().isWarning = true;
        if (imgAlpha.a < 1)
        {
            SoundManager.sInstance.PlaySFXEnemy(SoundManager.EnemySFX.BossAttackStand);
            blackboard.time += Time.deltaTime / blackboard.fadeTime;
            imgAlpha.a = Mathf.Lerp(0, 1, blackboard.time);
            for (int i = 0; i < blackboard.razersCount; i++)
                blackboard.RazersWarning[i].color = imgAlpha;
            return Node.STATE.RUN;
        }
        blackboard.time = 0.0f;
        imgAlpha.a = 0;
        for (int i = 0; i < blackboard.RazersWarning.Length; i++)
        {
            blackboard.RazersWarning[i].color = imgAlpha;
            blackboard.RazersWarningPrefab[i].GetComponent<RazerMove>().isWarning = false;
        }
        for (int i = 0; i < blackboard.razersCount; i++)
        {
            blackboard.Razers[i].SetActive(true);
        }
        blackboard.time = 0.0f;
        blackboard.animator.SetBool("Shoot", false);
        blackboard.isFade = true;
        blackboard.doPattern = false;
        return Node.STATE.SUCCESS;
    }
    Node.STATE Die()
    {
        blackboard.animator.SetBool("Death", true);
        blackboard.time += Time.deltaTime;
        if (blackboard.time >= blackboard.animator.GetCurrentAnimatorStateInfo(0).length)
        {
            blackboard.time = 0.0f;
            gameObject.SetActive(false);
            GameManager.Instance.score += 5000;
            GameManager.Instance.bGameOver = true;
            return Node.STATE.SUCCESS;
        }
        return Node.STATE.RUN;
    }
    Node.STATE Idle()
    {
        if(blackboard.doPattern)
            return Node.STATE.FAILED;
        blackboard.time += Time.deltaTime;
        if (blackboard.time >= 3.0f)
        {
            blackboard.time = 0.0f;
            blackboard.PosSet();
            blackboard.doPattern = true;
            if(blackboard.isFade)
                blackboard.animator.SetBool("Fade", true);
            else
            {
                int rand = Random.Range(1, GameManager.Instance.bossPhase + 1);
                blackboard.attackType = (BossAttackType)rand;
                if(blackboard.attackType == BossAttackType.Max)
                {
                    blackboard.animator.SetBool("Fade", true);
                    blackboard.isFade = true;
                }
                else
                    blackboard.animator.SetBool("Shoot", true);
            }
           return Node.STATE.SUCCESS;
        }
        return Node.STATE.RUN;
    }
    Node.STATE Fading()
    {
        Color imgAlpha = blackboard.bossImage.color;
        if (!blackboard.preFade)
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
            blackboard.doPattern = false;
            return Node.STATE.SUCCESS;
        }
        return Node.STATE.FAILED;
    }

    #endregion

}
