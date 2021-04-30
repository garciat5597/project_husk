using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practice : MonoBehaviour
{
    private static bool practiceMode = false;
   
    public void activateHusk(bool practice)
    {
        practiceMode = practice;
    }

  
    public bool getGameMode()
    {
        return practiceMode;
    }
}
