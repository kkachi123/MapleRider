using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1000.0f;
    [SerializeField] int addScore = 100;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos = new Vector2(pos.x - moveSpeed * Time.deltaTime, pos.y);
        rb.MovePosition(pos);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EndLine")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.score += addScore;
            SoundManager.sInstance.PlaySFXPlayer(SoundManager.PlayerSFX.GetCoin);
            Destroy(gameObject);
        }
        
    }
}
