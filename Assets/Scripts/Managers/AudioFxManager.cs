using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioFxManager : MonoBehaviour
{
    public static AudioFxManager instance { get; private set; }
    public AudioSource audioFxObject;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudioFxClip(AudioClip[] audioClips, Transform spawnTransform)
    {
        if (audioClips.Length > 0)
        {
            int rand = Random.Range(0, audioClips.Length);
            AudioSource audioSource = Instantiate(audioFxObject, spawnTransform.position, Quaternion.identity);
            audioSource.clip = audioClips[rand];
            audioSource.Play();
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }
}
