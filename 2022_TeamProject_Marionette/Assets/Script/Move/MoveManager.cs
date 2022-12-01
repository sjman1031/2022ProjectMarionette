using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marionette;
using Unity.VisualScripting;
using BaseFrame;

namespace Marionette
{
    public class MoveManager : MonoBehaviour
    {
        //protected override void onInit()
        //{
        //    base.onInit();
        //}

        bool is_Jump = false;

        private void Update()
        {
            Player_Move(transform, 5f);
            Player_Jump(transform, 5f);
        }


        public void Player_Move(Transform player_TR, float fMoveSpeed)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            float fallSpeed = player_TR.GetComponent<Rigidbody>().velocity.y;
            Vector3 velocity = new Vector3(inputX, 0, inputY);
            velocity *= fMoveSpeed;
            velocity.y = fallSpeed;
            player_TR.GetComponent<Rigidbody>().velocity = velocity;
        }
   
        public void Player_Jump(Transform player_TR, float fJumpSpeed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!is_Jump)
                { 
                    player_TR.GetComponent<Rigidbody>().AddForce(Vector3.up * fJumpSpeed, ForceMode.Impulse);
                    is_Jump = true;
                }

                return;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            is_Jump = false;
        }
    }
}
