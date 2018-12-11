using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public Transform tower;
    public const int CAMERA_OFFSET = 5;

	// Use this for initialization
	void Start () {
        transform.position = tower.position - Vector3.forward * CAMERA_OFFSET;
    }

    // Update is called once per frame
    void Update () {
    }
}
