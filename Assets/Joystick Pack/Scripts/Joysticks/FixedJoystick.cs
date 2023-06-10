using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedJoystick : Joystick
{
    public void FixedUpdate()
    {
        GlobalEventControl.HorizontalVerticalMove(Horizontal, Vertical);
    }
}