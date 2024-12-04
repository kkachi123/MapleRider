using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float interval = 3.0f;
    public float range = 220.0f;

    IEnumerator Start()
    {
        while (!GameManager.Instance.bGameOver)
        {
            yield return new WaitForSeconds(interval);
            Vector2 pos = new Vector2(transform.position.x, Random.Range(-range, range));
            Instantiate(cloudPrefab, pos, transform.rotation);
        }
    }
}
