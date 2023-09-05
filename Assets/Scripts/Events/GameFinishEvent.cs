using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinishEvent : GameEvent
{
    public override void TriggerEvent()
    {
        base.TriggerEvent();
        //stop everyones movement
        //camera shake
        //boss explodes
        //camera fades to black/white/whatever
        //change to credits scene
    }
}
