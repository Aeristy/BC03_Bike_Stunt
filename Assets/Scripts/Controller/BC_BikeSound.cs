using Kamgam.BikeAndCharacter25D;
using UnityEngine;

public class BC_BikeSound : MonoBehaviour
{
    AudioSource audioSource;

    BikeAndCharacter bikeController;
    float prevPitch;
    public float EngineFlow = 1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bikeController = GetComponent<BikeAndCharacter>();
        prevPitch = audioSource.pitch;
    }

    void Update()
    {
        if (bikeController.IsAccel > 0)
        {
            audioSource.pitch = 1.1f + 1 + bikeController.CurrentSpeed * 0.05f;

            audioSource.pitch = Mathf.Lerp(prevPitch, audioSource.pitch, Time.deltaTime * EngineFlow);
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1, Time.deltaTime * EngineFlow);
            prevPitch = audioSource.pitch;
        }
        else
        {
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, 1.2f, Time.deltaTime * EngineFlow);
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0.5f, Time.deltaTime * EngineFlow);
        }
    }
}
