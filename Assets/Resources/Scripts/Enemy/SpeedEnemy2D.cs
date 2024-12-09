using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpeedEnemy2D : Enemy2D
{
    public GameObject player;
    Vector2 shootDirection;
    protected override void Start()
    {
        base.Start();
        SettingDirection();
    }
    protected override void Move_2D()
    {
        Vector2 pos = rb.transform.position;
        rb.MovePosition(pos + shootDirection * speed * Time.deltaTime);
    }
    void SettingDirection()
    {
        if (player != null)
            shootDirection = (player.transform.position - transform.position).normalized;
    }
}
