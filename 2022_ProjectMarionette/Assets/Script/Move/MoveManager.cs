using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Marionette;

namespace Marionette
{
    public class MoveManager : MonoBehaviour
    {
        private static MoveManager instance;

        private void Awake()
        {
            instance = this;
        }

        public static MoveManager Instance
        {
            get
            {
                if (null == instance)
                    return null;

                return instance;
            }

        }

        [SerializeField]
        private Transform rHandTransform;
        [SerializeField]
        private Transform lHandTransform;
        [SerializeField]
        private Transform rFootTransform;
        [SerializeField]
        private Transform lFootTransform;
        [SerializeField]
        private Transform handleTransform;
        [SerializeField]
        private float waitTime;

        private void Start()
        {
            rHandTransform = GameObject.Find("Hand_R").GetComponent<Transform>();
            lHandTransform = GameObject.Find("Hand_L").GetComponent<Transform>();
            rFootTransform = GameObject.Find("Ankle_R").GetComponent<Transform>();
            lFootTransform = GameObject.Find("Ankle_L").GetComponent<Transform>();
            handleTransform = GameObject.Find("Handle").GetComponent<Transform>();

            waitTime = DataManager.Instance.data.waitTime;
        }

        public Transform HandleTransform
        {
            get
            {
                if (null == handleTransform)
                    return null;
                return handleTransform;
            }
        }

        public void MoveRHand(float power)
        {
            if (rHandTransform.position.y <= 1.3586724996566773f)
                rHandTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        }

        public void MoveLHand(float power)
        {
            if (lHandTransform.position.y <= 1.3586724996566773f)
                lHandTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
        }

        public void MoveRFoot(float power)
        {
            StartCoroutine(AddRFootPower(power));
        }

        public void MoveLFoot(float power)
        {
            StartCoroutine(AddLFootPower(power));
        }

        private IEnumerator AddRFootPower(float power)
        {
            rFootTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
            yield return new WaitForSeconds(waitTime);
        }

        private IEnumerator AddLFootPower(float power)
        {
            lFootTransform.GetComponent<Rigidbody>().AddForce(new Vector3(0f, power, 0f));
            yield return new WaitForSeconds(waitTime);
        }
    }
}