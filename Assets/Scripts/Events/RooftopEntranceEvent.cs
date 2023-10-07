using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RooftopEntranceEvent : GameEvent
{
    public Image fadeImage;
    public GameObject boss;
    public AudioClip[] bossScream, audioLand;

    protected override void Start()
    {
        base.Start();
        TriggerEvent();
    }

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        GameManager.instance.CameraShake(false);
        GameManager.instance.stopAllMovement = true;
        StartCoroutine(InitWait());
    }

    IEnumerator InitWait()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(DoFade());
        StartCoroutine(AfterFade());
    }

    IEnumerator AfterFade()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        StartCoroutine(AfterPlatformDestroy());
    }

    IEnumerator AfterBossLand()
    {
        yield return new WaitForSeconds(audioLand[0].length + 1);
        AudioFxManager.instance.PlayAudioFxClip(bossScream, transform);
        GameManager.instance.CameraShake(true);
        StartCoroutine(AfterBossScream());
    }

    IEnumerator AfterPlatformDestroy()
    {
        yield return new WaitForSeconds((float)0.65);
        AudioFxManager.instance.PlayAudioFxClip(audioLand, transform);
        GameManager.instance.CameraBump();
        StartCoroutine(AfterBossLand());
    }

    IEnumerator AfterBossScream()
    {
        yield return new WaitForSeconds(bossScream[0].length);
        GameManager.instance.CameraShake(false);
        StartCoroutine(WaitBeforeContinue());
    }

    IEnumerator WaitBeforeContinue()
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.stopAllMovement = false;
    }

    IEnumerator DoFade()
    {
        while (fadeImage.color.a >0)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadeImage.color.a - Time.deltaTime / 2);
            yield return null;
        }
        yield return null;
    }
}
