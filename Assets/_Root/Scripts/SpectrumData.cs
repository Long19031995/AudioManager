using System.Collections.Generic;
using UnityEngine;


public class SpectrumData : MonoBehaviour
{
    public AudioSource AudioSource;
    public NumberSample NumberSample = NumberSample.S1024;
    public FFTWindow window = FFTWindow.Blackman;

    public float[] Left => left;
    public float[] Right => right;

    float[] left;
    float[] right;
    NumberSample numberSample;

    public Transform sample;
    public List<Transform> samples;

    private void Awake()
    {
        GetSamples();
        for (int i = 0; i < (int)NumberSample; i++)
        {
            samples.Add(Instantiate(sample, transform));
        }
        for (int i = 0; i < (int)NumberSample; i++)
        {
            if (i % 2 == 0)
            {
                samples[i].position = sample.position + Vector3.left * i * 0.5f;
            }
            else
            {
                samples[i].position = sample.position + Vector3.right * (i - 1) * 0.5f;
            }
        }
        sample.gameObject.SetActive(false);
    }


    public void GetSamples()
    {
        left = new float[(int)NumberSample];
        right = new float[(int)NumberSample];
    }


    private void Update()
    {
        if (numberSample != NumberSample)
        {
            numberSample = NumberSample;
            GetSamples();
        }
        GetSpectrumData();
        for (int i = 0; i < (int)NumberSample; i++)
        {
            samples[i].localScale = new Vector3(1, (left[i] + right[i]) * 200, 1);
        }
    }


    public void GetSpectrumData()
    {
        if (AudioSource == null)
        {
            AudioListener.GetSpectrumData(left, 0, window);
            AudioListener.GetSpectrumData(right, 1, window);
        }
        else
        {
            AudioSource.GetSpectrumData(left, 0, window);
            AudioSource.GetSpectrumData(right, 1, window);
        }
    }
}


public enum NumberSample
{
    S64 = 64,
    S128 = 128,
    S256 = 256,
    S512 = 512,
    S1024 = 1024,
    S2048 = 2048,
    S4096 = 4096,
    S8192 = 8192
}
