﻿using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float TANK_VELOCITY = 0.1f;
    public const int VELOCITY_SCALE = 7;

    Rigidbody2D rigidbody;
    public Rigidbody2D bullet;
    public Rigidbody2D bulletInstance;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.turn == GameManager.Turn.ENEMY) {
            PlayTurn();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            StartCoroutine(DestroyBullet(collision));
            GameManager.instance.SubstractLifePoints();
        }
    }

    IEnumerator DestroyBullet(Collider2D collision)
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(collision.gameObject);
    }

    public void PlayTurn() {
        GameObject tower = GameObject.FindWithTag("Tower");
        transform.Rotate(0, 0, getRotationAngle());
        rigidbody.velocity = (new Vector2(tower.transform.position.x, tower.transform.position.y) - rigidbody.position) * TANK_VELOCITY;
    }

    void shoot()
    {
        bulletInstance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
        bulletInstance.velocity = AngleToVector(transform.eulerAngles.z) * VELOCITY_SCALE;
    }

    private Vector2 AngleToVector(float angle)
    {
        double rad = (angle * 2 * Math.PI) / 360; // Converts angle from degrees to radians
        Vector2 vector = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));

        return vector;
    }


    float getRotationAngle() {
        return -45f;
    }

    public void StartTurn()
    {
        GameManager.instance.ChangeTurn();
    }

}
