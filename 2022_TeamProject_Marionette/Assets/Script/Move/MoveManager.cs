using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marionette;
using Unity.VisualScripting;
using BaseFrame;

namespace Marionette
{
    public class MoveManager : SingletonT<MoveManager>
    {
        protected override void onInit()
        {
            base.onInit();
        }

        public void Player_Move(Transform player_TR, float fMoveSpeed)
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");

            Vector3 velocity = new Vector3(inputX, 0, inputY);
            velocity *= fMoveSpeed;
            player_TR.GetComponent<Rigidbody>().velocity = velocity;
        }

   
        void Player_Jump(Transform player_TR, float fJumpSpeed, bool is_Jump)
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
            is_jump = false;
        }
    }
}