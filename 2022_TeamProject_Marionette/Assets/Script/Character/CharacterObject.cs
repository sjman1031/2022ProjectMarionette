using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseFrame;

namespace Marionette
{
    public class Charatcter_Data : TableData
    {
        public float fMoveSpeed = 5f;
        public float fJumpSpeed = 5f;
    }

    public class CharacterObject : MonoBehaviour
    {
        public Charatcter_Data data;
        public Rigidbody player_RG;

        public bool bIsJump;

        private void Start()
        {
            player_RG = transform.GetComponent<Rigidbody>();
            data = new Charatcter_Data();
        }

        private void FixedUpdate()
        {
            Move();
            Jump();
        }

        private void Move()
        {
            float InputX = Input.GetAxis("Horizontal");
            float InputY = Input.GetAxis("Vertical");

            float fallSpeed = player_RG.velocity.y;
            Vector3 velocity = new Vector3(InputX, 0, InputY);
            velocity *= data.fMoveSpeed * Time.smoothDeltaTime;
            velocity.y = fallSpeed;
            player_RG.velocity = velocity;
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(!bIsJump)
                {
                    player_RG.AddForce(new Vector3(0, data.fJumpSpeed, 0));
                    bIsJump = true;
                }

                return;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.tag == "Ground")
                bIsJump = false;
        }

    }
}