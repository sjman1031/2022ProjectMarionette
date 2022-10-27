using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marionette;
using UnityEditor.PackageManager;

namespace Marionette
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        private void Awake()
        {
            instance = this;
        }

        public static GameManager Instance
        {
            get
            {
                if (null == instance)
                    return null;

                return instance;
            }
        }

        public float handPower;
        public float footPower;
        public float moveSpeed;

        private void Start()
        {
            DataManager.Instance.Load_Data();
        }


        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
                MoveManager.Instance.MoveRHand(handPower);
            else if (Input.GetKey(KeyCode.S))
                MoveManager.Instance.MoveLHand(handPower);
            else if (Input.GetKey(KeyCode.D))
                MoveManager.Instance.MoveRFoot(footPower);
            else if (Input.GetKey(KeyCode.F))
                MoveManager.Instance.MoveLFoot(footPower);

            if (Input.GetKey(KeyCode.UpArrow))
                MoveManager.Instance.HandleTransform.Translate(new Vector3(0f, 0f, moveSpeed * Time.deltaTime));
            else if (Input.GetKey(KeyCode.DownArrow))
                MoveManager.Instance.HandleTransform.Translate(new Vector3(0f, 0f, -moveSpeed * Time.deltaTime));
            else if (Input.GetKey(KeyCode.RightArrow))
                MoveManager.Instance.HandleTransform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
            else if (Input.GetKey(KeyCode.LeftArrow))
                MoveManager.Instance.HandleTransform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));
        }
    }
}