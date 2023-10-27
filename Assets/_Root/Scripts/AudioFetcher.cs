using UnityEngine;

public class AudioFetcher : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] string url;

    private async void Awake()
    {
        await AudioManager.Instance.FetchAndSetAudio(url, audioSource);
    }

    private void Update()
    {
        if (audioSource.clip != null && Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Play();
        }
    }
}
