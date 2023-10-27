using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tortoise : MonoBehaviour
{
    public AudioSource audioSource;
    public float time;
    public float interval;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > interval)
        {
            audioSource.Play();
            time = 0;
        }
    }
}
