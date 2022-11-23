using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMove1 : MonoBehaviour
{
    public float X,Y,Z;
    public float X1,Y1,Z1;
    public float a;

    bool go = true;
    void Update()
    {
        Vector3 Start = new Vector3(X, Y, Z);
        Vector3 Target = new Vector3(X1, Y1, Z1);

        if (go)
        {
            transform.position = Vector3.MoveTowards
                (gameObject.transform.position, Target, 0.2f);

            transform.eulerAngles = new Vector3(0, a, 0);

            Debug.Log("1");

            go = false;

        }

        if (transform.position.x <= X1 && transform.position.z <= Z1)
        {
            transform.position = Vector3.MoveTowards
                (gameObject.transform.position, Start, 0.1f);

            transform.eulerAngles = new Vector3(0, a + 180, 0);

            Debug.Log("2");
        }

       

    }
}
