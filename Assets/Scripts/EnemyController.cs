using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private const float TANK_VELOCITY = 0.1f;

    Rigidbody2D rigidbody;

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
            DestroyBullet(collision);
        }
    }

    void DestroyBullet(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    void SubstractLifePoints() {
        Debug.Log("Substract");
    }

    public void PlayTurn() {
        GameObject tower = GameObject.FindWithTag("Tower");
        transform.Rotate(0, 0, getRotationAngle());
        rigidbody.velocity = (new Vector2(tower.transform.position.x, tower.transform.position.y) - rigidbody.position) * TANK_VELOCITY;
        Invoke("ChangeTurn", 2);
    }

    float getRotationAngle() {
        return -45f;
    }

    void ChangeTurn()
    {
        GameManager.instance.ChangeTurn();
    }

}
