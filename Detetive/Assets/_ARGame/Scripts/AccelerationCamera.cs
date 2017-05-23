using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationCamera : MonoBehaviour {

    public float speed = 10.0F;
    void Update()
    {
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;

        transform.Rotate(new Vector3(0, 0, dir.z * speed * 10f));
        //transform.Translate(dir * speed);
    }
}
