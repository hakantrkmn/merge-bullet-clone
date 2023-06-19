using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.position += new Vector3(0, 0, -1) * (speed * Time.deltaTime);
    }
}
