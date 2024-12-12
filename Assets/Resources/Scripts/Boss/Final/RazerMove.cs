using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerMove : MonoBehaviour
{
    public int razerNum = 0;
    public float moveRange = 0;
    public float moveSpeed = 10.0f;
    Vector2 moveDir;
    public bool isWarning = false;
    void Start()
    {
        moveDir = Vector2.one;
        switch(razerNum)
        {
            case 0:
                moveDir = Vector2.up;
                break;
            case 1:
                moveDir = Vector2.right;
                break;
            case 2:
                moveDir = Vector2.down;
                break;
        }
    }
    void Update()
    {
        if(isWarning)
            Move();
    }
    private void Move()
    {
        switch(razerNum)
        {
            case 0:
                transform.Translate(moveDir * moveSpeed * Time.deltaTime);
                if (Mathf.Abs(transform.position.y) >= moveRange)
                    moveDir *= -1;
                break;
            case 1:
                transform.Translate(moveDir * moveSpeed * Time.deltaTime);
                if (Mathf.Abs(transform.position.x) >= moveRange)
                    moveDir *= -1;
                break;
            case 2:
                
                transform.Translate(moveDir * moveSpeed * Time.deltaTime);
                if (Mathf.Abs(transform.position.y) >= moveRange)
                    moveDir *= -1;
                break;
        }
    }
}
