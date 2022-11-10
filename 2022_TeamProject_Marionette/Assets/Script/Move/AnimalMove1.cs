using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove1 : MonoBehaviour
{
    public float X,Y,Z;
    public float X1,Z1;
    private int sign = -1;
    public float a;

    void Update()
    {
        Vector3 target = new Vector3(X, Y, Z);

        transform.position =
            Vector3.MoveTowards(transform.position, target*-sign, 0.01f);
        if(transform.position.x >= X)
        {            
            sign *= -1;
            transform.eulerAngles = new Vector3(0, a + 180, 0);
        }
        else if(transform.position.x <= X1)
        {
            sign *= -1;
            transform.eulerAngles = new Vector3(0, a, 0);
        }

        

    }
}
