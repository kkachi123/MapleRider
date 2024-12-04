using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBress : MonoBehaviour
{
    public GameObject shotEffectPrefab;
    public float power = 5.0f;

    void Start()
    {
        Application.runInBackground = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy2D>().hp -= power;
            Instantiate(shotEffectPrefab, collision.transform.position, collision.transform.rotation);
        }
        else if(collision.tag == "Boss")
        {
            collision.GetComponent<BossStateControl>().hp -= power;
            Instantiate(shotEffectPrefab, collision.transform.position, collision.transform.rotation);
        }
    }
}
