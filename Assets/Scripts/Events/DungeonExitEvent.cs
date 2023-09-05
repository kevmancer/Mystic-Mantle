using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExitEvent : GameEvent
{
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        // make everyone stop
        // shake camera
        // play rumbling sound
        // start fading screen to black/white/whatever
        GameManager.instance.LoadNextScene();
    }
}
