using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour
{
    private const float INCREMENT_ANGLE = 3f;
    private const int FIX_ANGLE = 90;   //Fixes initial angle of canon view
    public const int VELOCITY_SCALE = 7;

    public Transform canon;
    public Rigidbody2D bullet;
    private Rigidbody2D bulletInstance;
    public ParticleSystem particleSystem;

    private bool canMove;
    
    // Use this for initialization
    void Start()
    {
        transform.Rotate(0, 0, 180);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.turn == GameManager.Turn.PLAYER && canMove)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateLeft();

            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateRight();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                shoot();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TankBullet")
        {
            StartCoroutine("Explode");
            DestroyBullet(collision);
            GameManager.instance.SubstractLifePoints();
        }
    }

    void DestroyBullet(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    void RotateLeft()
    {
        float currentRotation = transform.rotation.z;
        currentRotation += INCREMENT_ANGLE;
        transform.Rotate(0, 0, currentRotation);
    }

    void RotateRight()
    {
        float currentRotation = transform.rotation.z;
        currentRotation -= INCREMENT_ANGLE;
        transform.Rotate(0, 0, currentRotation);
    }

    void shoot()
    {
        canMove = false;
        bulletInstance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
        bulletInstance.velocity = (AngleToVector(transform.eulerAngles.z + FIX_ANGLE)) * VELOCITY_SCALE;
        GameManager.instance.CancelCountDowns();
    }

    private Vector2 AngleToVector(float angle) {
        double rad = (angle * 2 * Math.PI) / 360; // Converts angle from degrees to radians
        Vector2 vector = new Vector2((float)Math.Cos(rad), (float)Math.Sin(rad));

        return vector;
    }

    public void StartTurn() {
        canMove = true;
    }

    IEnumerator Explode()
    {
        Vector3 pos = transform.position;
        particleSystem = Instantiate(particleSystem, new Vector3(pos.x, pos.y, 15f), transform.rotation) as ParticleSystem;
        yield return new WaitForSeconds(0.5f);
        Destroy(particleSystem);
    }
}
