using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public enum ClipItem { Select, Swap, Clear, Attack };
    public static SoundManager instance;

    private AudioSource[] sfx;

    // Use this for initialization
    void Start()
    {
        instance = GetComponent<SoundManager>();
        sfx = GetComponents<AudioSource>();
    }

    public void PlaySFX(ClipItem audioClip)
    {
        sfx[(int)audioClip].Play();
    }
}
