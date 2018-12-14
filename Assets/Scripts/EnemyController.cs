using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private const float ENEMY_VELOCITY = 0.1f;
    public const int VELOCITY_SCALE = 7;
    private const float FIX_ANGLE = -90f; // Fixes initial angle of tank view
    private Animator anim;

    Rigidbody2D rigidbody;
    Rigidbody2D bulletInstance;
    public Rigidbody2D bullet;
    public ParticleSystem particleSystem;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("Damaged", false);
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TowerBullet")
        {
            StartCoroutine("Explode");
            anim.SetBool("Damaged", true);
            Invoke("StopAnim", 1f);
            Destroy(collision.gameObject);
            GameManager.instance.SubstractLifePoints();
        }
    }

    void StopAnim() {
        anim.SetBool("Damaged", false);
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
        Vector3 pos = transform.position;
        bulletInstance = Instantiate(bullet, new Vector3(pos.x, pos.y, 15f), transform.rotation) as Rigidbody2D;
        bulletInstance.velocity = ShootVector(transform.eulerAngles.z) * VELOCITY_SCALE;
        GameManager.instance.CancelCountDowns();
    }

    private Vector2 ShootVector(float angle)
    {
        double rad = ((angle - FIX_ANGLE) * 2 * Math.PI) / 360; // Converts angle from degrees to radians
        Vector2 vector = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));

        return vector;
    }


    float getRotationAngle() {
        return 0;
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
