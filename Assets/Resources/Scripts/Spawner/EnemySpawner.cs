using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyElite;
    public GameObject enemyPrefab;
    public float firstInterval = 1.0f;
    public float range = 200.0f;
    float interval;
    int preLevel = 1;
    IEnumerator Start()
    {
        interval = firstInterval;
        preLevel = GameManager.Instance.level;
        while (!GameManager.Instance.bGameOver)
        {
            yield return new WaitForSeconds(interval);
            if (GameManager.Instance.elitCount < 1 || GameManager.Instance.level > 3000)
            {
                Vector2 pos = new Vector2(transform.position.x, Random.Range(-range, range));
                Instantiate(enemyPrefab, pos, transform.rotation);
                if (GameManager.Instance.score > 500)
                {
                    interval = firstInterval / (GameManager.Instance.score / 500) ;
                    if(interval < 0.1f)
                        interval = 0.1f;
                    enemyPrefab.GetComponent<Enemy2D>().hp = 10.0f * (GameManager.Instance.score / 500);
                }
            }
            if(GameManager.Instance.score >= 1000 * preLevel)
                ElitSpawn();
        }
    }
    void ElitSpawn()
    {
        int maxEliteCount = GameManager.Instance.score / 1000;
        if(maxEliteCount > enemyElite.Length)
            maxEliteCount = enemyElite.Length;
        for(int i = 0; i < enemyElite.Length; i++)
        {
            if (enemyElite[i].activeSelf == false && GameManager.Instance.elitCount < maxEliteCount)
            {
                Vector2 pos = new Vector2(transform.position.x, Random.Range(-range, range));
                enemyElite[i].SetActive(true);
                enemyElite[i].transform.position = pos;
                enemyElite[i].GetComponent<BossStateControl>().hp = 1000 * GameManager.Instance.level;
                enemyElite[i].GetComponent<BossMove>().moveSpeed = 100.0f * GameManager.Instance.level;
                enemyElite[i].GetComponent<BossAttack>().power = 50.0f;
                enemyElite[i].GetComponent<BossStateControl>().state = BossState.Move;
                enemyElite[i].GetComponent<BossMove>().movePosIndex = i;
                GameManager.Instance.elitCount++;
            }
        }
        preLevel = GameManager.Instance.level;
    }
}
