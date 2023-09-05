using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RooftopEntranceEvent : GameEvent
{
    protected override void Start()
    {
        base.Start();
        TriggerEvent();
    }

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        //make everyone stop
        //fade white/black screen back to normal
        // boss rise
        // camera shake
        //boss scream??
        //continue everything
    }
}
