using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private AudioSource _as;

    private void Start()
    {
        
        _as = GetComponent<AudioSource>();
    }

    public void PlayFootStep()
    {
        _as.Play();
    }
}
