using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float ENEMY_VELOCITY = 0.1f;
    public const int VELOCITY_SCALE = 1;

    Rigidbody2D rigidbody;
    Rigidbody2D bulletInstance;
    public Rigidbody2D bullet;
    public ParticleSystem particleSystem;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.turn == GameManager.Turn.ENEMY) {
            //PlayTurn();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TowerBullet")
        {
            StartCoroutine("Explode");
            Destroy(collision.gameObject);
            //StartCoroutine(DestroyBullet(collision));
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
        //rigidbody.velocity = (new Vector2(tower.transform.position.x, tower.transform.position.y) - rigidbody.position) * ENEMY_VELOCITY;
        shoot();
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
        PlayTurn();
    }

    IEnumerator Explode()
    {
        Vector3 pos = transform.position;
        particleSystem = Instantiate(particleSystem, new Vector3(pos.x, pos.y, 15f), transform.rotation) as ParticleSystem;
        yield return new WaitForSeconds(0.5f);
        Destroy(particleSystem);
    }

}
