using UnityEngine;
using static CKB.Utilities.CommonGameplayFacade;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        Audio = this;
    }

    public void PlayOneShot(AudioClip audioClip, float volume = 1f)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }
}