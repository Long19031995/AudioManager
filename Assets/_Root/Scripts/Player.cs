using UnityEngine;
using UnityEngine.Rendering;

public enum PlayerState
{
    Air,
    Ground,
    Water
}

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public Vector3 originPosition;
    public Rigidbody rigidbody;
    public AudioSource audioSplash;
    public AudioSource audioOceanWaves;
    public AudioSource audioOceanWaves2;
    public AudioSource audioUnderWater;
    public AudioSource audioGrassStep;
    public AudioSource audioWaterStep;
    public AudioSource audioStep;
    public float oldY = 1;
    public float timeStep;
    public float intervalStep = 0.5f;
    public bool isReverb;

    private void Start()
    {
        originPosition = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        audioStep = audioGrassStep;
    }

    public void Reset()
    {
        transform.position = originPosition;
        audioOceanWaves.gameObject.SetActive(true);
    }

    private void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, translation);

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector3.up * 10;
        }

        if (oldY <= 0 && transform.position.y > 0)
        {
            audioSplash.Play();
        }

        oldY = transform.position.y;

        if (transform.position.y < 0)
        {
            AudioManager.Instance.audioMixer.FindSnapshot("Water").TransitionTo(1);
            audioUnderWater.gameObject.SetActive(true);
            audioOceanWaves.gameObject.SetActive(false);
        }
        else
        {
            if (!isReverb)
            {
                AudioManager.Instance.audioMixer.FindSnapshot("Ground").TransitionTo(1);
                audioOceanWaves.gameObject.SetActive(true);
                audioUnderWater.gameObject.SetActive(false);
            }
            if (audioStep != null)
            {
                if (translation != 0)
                {
                    timeStep += Time.deltaTime;
                    if (timeStep > intervalStep)
                    {
                        timeStep = 0;
                        audioStep.Play();
                    }
                }
                else
                {
                    timeStep = 0;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Mountain"))
        {
            audioStep = audioWaterStep;
        }

        if (other.collider.CompareTag("Ground"))
        {
            audioStep = audioGrassStep;
        }

        if (other.collider.CompareTag("Corridor"))
        {
            audioStep = audioGrassStep;
            AudioManager.Instance.audioMixer.FindSnapshot("Reverb").TransitionTo(1);
            isReverb = true;
            audioOceanWaves.gameObject.SetActive(false);
            audioOceanWaves2.gameObject.SetActive(false);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Mountain") || other.collider.CompareTag("Ground"))
        {
            audioStep = null;
        }

        if (other.collider.CompareTag("Corridor"))
        {
            isReverb = false;
            audioOceanWaves.gameObject.SetActive(true);
            audioOceanWaves2.gameObject.SetActive(true);
        }
    }
}
