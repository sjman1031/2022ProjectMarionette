using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFootUp : MonoBehaviour
{
    public Transform rHandTransform;
    public Transform lHandTransform;
    public Transform rFootTransform;
    public Transform lFootTransform;
    public float power = 300f;

    private void Start()
    {
        rHandTransform = GameObject.Find("Hand_R").GetComponent<Transform>();
        lHandTransform = GameObject.Find("Hand_L").GetComponent<Transform>();
        rFootTransform = GameObject.Find("Ankle_R").GetComponent<Transform>();
        lFootTransform = GameObject.Find("Ankle_L").GetComponent<Transform>();
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.A) && rHandTransform.position.y <= 1.3586724996566773f)
            rHandTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        else if (Input.GetKey(KeyCode.S) && lHandTransform.position.y <= 1.3586724996566773f)
            lHandTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        else if (Input.GetKey(KeyCode.D))
            rFootTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        else if (Input.GetKey(KeyCode.F))
            lFootTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
    }
}