using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float firstInterval = 3.0f;
    public float range = 220.0f;
    float interval;
    IEnumerator Start()
    {
        interval = firstInterval;
        while (!GameManager.Instance.bGameOver)
        {
            yield return new WaitForSeconds(interval);
            if (GameManager.Instance.score > 500)
                interval = GameManager.Instance.score / 500 * firstInterval;
            Vector2 pos = new Vector2(transform.position.x, Random.Range(-range, range));
            Instantiate(coinPrefab, pos, transform.rotation);
        }
    }
}
