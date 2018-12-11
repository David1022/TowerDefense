﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            DestroyEnemy(collision);
        }
    }

    void DestroyBullet(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    void DestroyEnemy(Collider2D collision)
    {
        DestroyBullet(collision);
        Debug.Log("Enemy touched");
    }
}