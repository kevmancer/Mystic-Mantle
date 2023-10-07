using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFinishEvent : GameEvent
{
    public Image fadeImage;
    public AudioClip[] bossScream;
    public GameObject finishText;

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        GameManager.instance.stopAllMovement = true;
        MusicManager.instance.UpdateIndex(MusicManager.instance.nextIndex + 1, FinishGame);
        AudioFxManager.instance.PlayAudioFxClip(bossScream, transform);
        GameManager.instance.CameraShake(true);
        StartCoroutine(BossDeathObservation());
    }

    IEnumerator BossDeathObservation()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(DoFade());
        StartCoroutine(AfterFade());
    }

    private void FinishGame()
    {
        GameManager.instance.stopAllMovement = false;
        GameManager.instance.LoadNextScene();
    }

    IEnumerator DoFade()
    {
        while (fadeImage.color.a < 1)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a + Time.deltaTime / 4);
            yield return null;
        }
        yield return null;
    }
    IEnumerator AfterFade()
    {
        yield return new WaitForSeconds(5);
        GameManager.instance.CameraShake(false);
        finishText.SetActive(true);
    }
}
