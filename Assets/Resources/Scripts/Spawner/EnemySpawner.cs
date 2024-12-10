using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] enemyElite;
    public GameObject[] enemyPrefab;
    public float firstInterval = 1.0f;
    public float range = 200.0f;
    float interval;
    int preLevel = 1;
    IEnumerator Start()
    {
        interval = firstInterval;
        preLevel = GameManager.Instance.level;
        while (!GameManager.Instance.bGameOver && GameManager.Instance.bossPhase < 1)
        {
            yield return new WaitForSeconds(interval);
            if (GameManager.Instance.elitCount < 1)
            {
                Vector2 pos = new Vector2(transform.position.x, Random.Range(-range, range));
                int rand = Random.Range(0, 9);
                int enemyIndex;
                if (rand >= 3)
                    enemyIndex = 0;
                else
                    enemyIndex = 1;
                GameObject enemy = Instantiate(enemyPrefab[enemyIndex], pos, transform.rotation);
                if(enemyIndex == 1)
                    enemy.GetComponent<SpeedEnemy2D>().player = player;
                if (GameManager.Instance.score > 500)
                {
                    interval = firstInterval / (GameManager.Instance.score / 500) ;
                    if(interval < 0.1f)
                        interval = 0.1f;
                }
            }
            if (GameManager.Instance.score >= 1000 * preLevel && GameManager.Instance.score <= 4000)
                ElitSpawn();
            else if(GameManager.Instance.score > 4000)
                GameManager.Instance.bossPhase++;
            
        }
    }

    void ElitSpawn()
    {
        int maxEliteCount = GameManager.Instance.score / 1000;
        if (maxEliteCount > enemyElite.Length)
            maxEliteCount = enemyElite.Length;
        for (int i = 0; i < enemyElite.Length; i++)
        {
            if (enemyElite[i].activeSelf == false && GameManager.Instance.elitCount < maxEliteCount)
            {
                Vector2 pos = new Vector2(transform.position.x, Random.Range(-range, range));
                enemyElite[i].SetActive(true);
                enemyElite[i].transform.position = pos;
                enemyElite[i].GetComponent<EliteProfile>().Init(i);
                GameManager.Instance.elitCount++;
            }
        }
        preLevel = GameManager.Instance.level;
    }

}
