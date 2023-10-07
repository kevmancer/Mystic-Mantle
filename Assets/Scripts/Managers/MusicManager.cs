using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }
    public AudioClip[] gameMusicClips;
    public AudioClip menuMusicClip;
    public AudioSource[] gameMusicSources;
    public AudioSource menuMusicSource;
    public int songIndex=0, nextIndex = 0;
    private bool activeSource = false;
    public delegate void TestDelegate();
    public TestDelegate onAfterSongFinished = null;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        gameMusicSources[activeSource ? 0 : 1].clip = gameMusicClips[songIndex];
        gameMusicSources[activeSource ? 0 : 1].Play();
        gameMusicSources[!activeSource ? 0 : 1].clip = gameMusicClips[songIndex];
    }

    private void Update()
    {
        if (nextIndex != 0&&nextIndex != 1)
        {
            if (GameManager.instance.gamePaused)
            {
                if (gameMusicSources[activeSource ? 0 : 1].isPlaying)
                {
                    gameMusicSources[activeSource ? 0 : 1].Pause();
                    menuMusicSource.Play();
                }
            }
            else
            {
                if (menuMusicSource.isPlaying)
                {
                    gameMusicSources[activeSource ? 0 : 1].Play();
                    menuMusicSource.Stop();
                }
                if (!gameMusicSources[activeSource ? 0 : 1].isPlaying)
                {
                    activeSource = !activeSource;
                    gameMusicSources[activeSource ? 0 : 1].Play();
                    gameMusicSources[!activeSource ? 0 : 1].clip = gameMusicClips[nextIndex];
                    if (onAfterSongFinished != null && songIndex == nextIndex)
                    {
                        onAfterSongFinished();
                        onAfterSongFinished = null;
                    }
                    songIndex = nextIndex;
                }
            }
        }
        else
        {
            if (!gameMusicSources[activeSource ? 0 : 1].isPlaying)
            {
                activeSource = !activeSource;
                songIndex = nextIndex;
                gameMusicSources[activeSource ? 0 : 1].Play();
                gameMusicSources[!activeSource ? 0 : 1].clip = gameMusicClips[songIndex];
            }
        }      
    }

    public void UpdateIndex(int index, TestDelegate method)
    {
        if (index != nextIndex)
        {
            nextIndex = index;
            if ((songIndex + 1 != nextIndex)||index==4)
            {
                gameMusicSources[activeSource ? 0 : 1].Stop();
                gameMusicSources[activeSource ? 0 : 1].clip = gameMusicClips[nextIndex];
            }
            gameMusicSources[!activeSource ? 0 : 1].clip = gameMusicClips[nextIndex];
            onAfterSongFinished=method;
        }
    }

}
