using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    #region Rigidbody
    [SerializeField]
    Rigidbody rightFootRigidbody;
    [SerializeField]
    Rigidbody leftFootRigidbody;
    [SerializeField]
    Rigidbody rightHandRigidbody;
    [SerializeField]
    Rigidbody leftHandRigidbody;
    [SerializeField]
    Rigidbody cubeRigidbody;
    #endregion

    #region Pos Vector
    Vector3 rightFootPos;
    Vector3 leftFootPos;
    Vector3 rightHandPos;
    Vector3 leftHandPos;
    Vector3 headPos;
    #endregion

    // Update is called once per frame
    void Update()
    {
        #region Foot and Hand
        if (Input.GetKeyDown(KeyCode.A))
            rightFootRigidbody.AddForce(new Vector3(0f, 0f, 100f));
        else if (Input.GetKeyDown(KeyCode.S))
            leftFootRigidbody.AddForce(new Vector3(0f, 0f, 100f));
        else if (Input.GetKeyDown(KeyCode.D))
            rightHandRigidbody.AddForce(new Vector3(0f, 0f, 300f));
        else if (Input.GetKeyDown(KeyCode.F))
            leftHandRigidbody.AddForce(new Vector3(0f, 0f, 300f));
        #endregion

        #region Head
        if(Input.GetKey(KeyCode.RightArrow))
            cubeRigidbody.transform.Translate(new Vector3(0.01f, 0f, 0f));
        else if (Input.GetKey(KeyCode.LeftArrow))
            cubeRigidbody.transform.Translate(new Vector3(-0.01f, 0f, 0f));
        else if (Input.GetKey(KeyCode.UpArrow))
            cubeRigidbody.transform.Translate(new Vector3(0f, 0f, 0.01f));
        else if (Input.GetKey(KeyCode.DownArrow))
            cubeRigidbody.transform.Translate(new Vector3(0f, 0f, -0.01f));
        #endregion
    }
}
