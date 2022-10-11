using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFootUp : MonoBehaviour
{
    public Transform[] transforms;
    public float power = 300f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && transforms[0].position.y <= 1.3586724996566773f)
            transforms[0].GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        else if (Input.GetKey(KeyCode.S) && transforms[0].position.y <= 1.3586724996566773f)
            transforms[1].GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        else if (Input.GetKey(KeyCode.D))
            transforms[2].GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        else if (Input.GetKey(KeyCode.F))
            transforms[3].GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
    }
}