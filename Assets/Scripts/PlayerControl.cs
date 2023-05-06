using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControl: EntityControl
{

    new void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            LightAttack();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            HeavyAttack();
        }
    }
}
