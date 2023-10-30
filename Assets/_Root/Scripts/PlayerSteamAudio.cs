using UnityEngine;

public class PlayerSteamAudio : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public Rigidbody rigidbody;

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, translation);

        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector3.up * 10;
        }
    }
}
