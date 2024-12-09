using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoinSpawner : MonoBehaviour
{
    public GameObject[] coinPrefab;
    public float interval = 15.0f;
    public float range = 220.0f;

    IEnumerator Start()
    {
        while (!GameManager.Instance.bGameOver)
        {
            yield return new WaitForSeconds(interval);
            int rand = Random.Range(0, 9);
            Vector2 pos = new Vector2(transform.position.x, Random.Range(-range, range));
            if (rand >= 3)
                Instantiate(coinPrefab[0], pos, transform.rotation);
            else
                Instantiate(coinPrefab[1], pos, transform.rotation);
        }
    }
}
