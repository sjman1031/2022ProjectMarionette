using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColSfx : MonoBehaviour
{
    SoundManager SM;
    private void OnCollisionEnter(Collision collision)
    {
        SM.CoinSound();
    }

}
