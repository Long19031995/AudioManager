using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static AudioManager Instance => _instance;
    private static AudioManager _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public async Task FetchAndSetAudio(string url, AudioSource audioSource)
    {
        AudioClip audioClip = await FetchAudio(url);
        if (audioClip != null)
        {
            SetAudio(audioSource, audioClip);
        }
    }

    public async Task<AudioClip> FetchAudio(string url)
    {
        UnityWebRequest unityWebRequest = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);

        await unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.Success)
        {
            return DownloadHandlerAudioClip.GetContent(unityWebRequest);
        }
        else
        {
            Debug.LogError(unityWebRequest.error);
        }

        return null;
    }

    public void SetAudio(AudioSource audioSource, AudioClip audioClip)
    {
        audioSource.clip = audioClip;
    }
}
