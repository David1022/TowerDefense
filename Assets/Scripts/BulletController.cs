using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    private void OnDestroy()
    {
        GameManager.instance.ChangeTurn();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyDamage" || collision.tag == "TowerDamage") {
            GameManager.instance.SubstractLifePoints();
        }
    }
}
