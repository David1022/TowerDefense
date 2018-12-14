using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TowerBullet" || collision.tag == "TankBullet")
        {
            DestroyBullet(collision);
        }
    }

    void DestroyBullet(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
