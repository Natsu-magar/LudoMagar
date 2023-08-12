using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{

    AudioSource ads;
    // Start is called before the first frame update
    void Start()
    {
        ads = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        ads.Play();
    }


}