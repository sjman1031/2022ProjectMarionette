using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    float backMax = -5.0f;

    float goMax = 5.0f;

    float currentPosition; 

    float direction = 3.0f; 
    void Start()
    {
        currentPosition = transform.position.z;
    }

    void Update()
    {
        currentPosition += Time.deltaTime * direction;

        if (currentPosition >= goMax)

        {

            direction *= -1;

            currentPosition = goMax;

            

        }


        else if (currentPosition <= backMax)

        {

            direction *= -1;

            currentPosition = backMax;

            

        }
          
        transform.position = new Vector3(22, 0, currentPosition);

    }
}
