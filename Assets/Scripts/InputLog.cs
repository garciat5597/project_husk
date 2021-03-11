using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLog
{
    /*
     * Container class for all input states for use in shadow and player code
     * Holds raw axis data, jump, dash, and wallrun button data
     */
    public float directionalInput;
    public bool jumpPressed;
    public bool dashPressed;
    public bool wallrunPressed;

    public InputLog(float nDirectional, bool jumping, bool dashing, bool wallrunning)
    {
        directionalInput = nDirectional;
        jumpPressed = jumping;
        dashPressed = dashing;
        wallrunPressed = wallrunning;
    }


}
