using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonExitEvent : GameEvent
{
    public AudioClip[] audioRumble, audioCrash;
    public ParticleSystem particles;
    public Image fadeImage;
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        GameManager.instance.stopAllMovement = true;
        GameManager.instance.CameraShake(true);
        AudioFxManager.instance.PlayAudioFxClip(audioRumble, transform);
        StartCoroutine(EventDuration());
        particles.Play();
        StartCoroutine(DoFade());
    }

    IEnumerator EventDuration()
    {
        yield return new WaitForSeconds(audioRumble[0].length);
        StartCoroutine(AfterCrash());
        AudioFxManager.instance.PlayAudioFxClip(audioCrash, transform);
    }

    IEnumerator AfterCrash()
    {
        yield return new WaitForSeconds(audioCrash[0].length);
        GameManager.instance.CameraShake(false);
        GameManager.instance.LoadNextScene();
    }

    IEnumerator DoFade()
    {
        yield return new WaitForSeconds(3);
        while (fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + Time.deltaTime / 2);
            yield return null;
        }
        yield return null;
    }
}
