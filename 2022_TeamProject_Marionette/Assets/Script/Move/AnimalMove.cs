using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove : MonoBehaviour
{

    public float backMax;   
    public float goMax;
    public float StartY;
    public float StartZ;
    public float rotation;
    float currentPosition;
    public float speed; 
    void Start()
    {
        currentPosition = transform.position.x;
    }

    void Update()
    {
        currentPosition += Time.deltaTime * speed;

        if (currentPosition >= goMax)

        {
            speed *= -1;

            currentPosition = goMax;

            transform.eulerAngles = new Vector3(0,rotation + 180, 0);

        }


        else if (currentPosition <= backMax)

        {
            speed *= -1;

            currentPosition = backMax;

            transform.eulerAngles = new Vector3(0, rotation, 0);

        }
          
        transform.position = new Vector3(currentPosition, StartY, StartZ);

    }
}
