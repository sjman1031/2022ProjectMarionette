using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{

    public float backMax;   //z축으로 최소 거리
    public float goMax;     //z축으로 최대 거리

    float currentPosition; 

    float direction = 3.0f; 
    void Start()
    {
        currentPosition = transform.position.x;
    }

    void Update()
    {
        currentPosition += Time.deltaTime * direction;

        if (currentPosition >= goMax)

        {

            direction *= -1;

            currentPosition = goMax;

            transform.eulerAngles = new Vector3(0, 180, 0);

        }


        else if (currentPosition <= backMax)

        {

            direction *= -1;

            currentPosition = backMax;

            transform.eulerAngles = new Vector3(0, 0, 0);

        }
          
        transform.position = new Vector3(0, 0, currentPosition);

    }
}
