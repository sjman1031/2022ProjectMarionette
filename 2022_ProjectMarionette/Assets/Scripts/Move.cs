using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float moveSpeed = 500f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Translate(new Vector3(0f, 0f, moveSpeed * Time.deltaTime));
        else if (Input.GetKey(KeyCode.LeftArrow))
            transform.Translate(new Vector3(0f, 0f, -moveSpeed * Time.deltaTime));
    }
}
