﻿using UnityEngine;

public class Orca : EntityType
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] private float xMargin = 2;
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed = 5;
    [SerializeField] private float maxSpeed = 15;

    private int xDirection = 1;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    public override void StartEntity()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        if (Random.Range(1, 3) == 1)
        {
            xDirection *= -1;
        }
        else
        {
            sr.flipX = true;
        }
        
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(xDirection * speed, rb.velocity.y);
    }

    private void Update()
    {
        if (transform.position.x > xMargin && xDirection == 1 || transform.position.x < -xMargin && xDirection == -1)
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        xDirection *= -1;
        sr.flipX = !sr.flipX;
        Move();
    }
}