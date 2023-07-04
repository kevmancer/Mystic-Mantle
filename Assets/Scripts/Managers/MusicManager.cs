using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource master;
    public AudioSource[] slaves;

    private void Start()
    {
        if (slaves.Length > 0)
        {
            StartCoroutine(SyncSources());
        }
    }

    private IEnumerator SyncSources()
    {
        while (true)
        {
            if (master.isPlaying&&master.isActiveAndEnabled) 
            {
                foreach (var slave in slaves)
                {
                    if(slave.isPlaying&&slave.isActiveAndEnabled)
                    if (transform.position.x > 15)
                    {
                        slave.volume = 1;
                    }
                    else
                    {
                        slave.volume = 0;
                    }
                    if(master.timeSamples>=0&&master.timeSamples<slave.clip.length)
                    slave.timeSamples = master.timeSamples;
                    yield return null;
                }
            }
            
        }
    }
}
