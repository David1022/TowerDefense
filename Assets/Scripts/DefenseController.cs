using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseController : MonoBehaviour
{

    public Transform canon;
    public Rigidbody2D bullet;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(0, 0, -90);
    }

    // Update is called once per frame
    void Update()
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

    void RotateLeft()
    {
        float currentRotation = transform.rotation.z;
        currentRotation += 5;
        transform.Rotate(0, 0, currentRotation);
    }

    void RotateRight()
    {
        float currentRotation = transform.rotation.z;
        currentRotation -= 5;
        transform.Rotate(0, 0, currentRotation);
    }

    void shoot()
    {
        Rigidbody2D bulletInstance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody2D;
        bulletInstance.velocity = new Vector2(transform.rotation.z, transform.rotation.z) * -2;
    }
}
