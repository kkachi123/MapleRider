using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    public GameObject shotEffectPrefab;
    [HideInInspector] public Rigidbody2D rb;

    public float power = 10.0f;
    public float maxSpeed = 400.0f;
    void Awake()
    {
        Application.runInBackground = true;
        rb = GetComponent<Rigidbody2D>();
    }
}
