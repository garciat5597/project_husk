using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practice : MonoBehaviour
{
    private static bool practiceMode;
   
    public void setPracticeMode(bool practice)
    {
        practiceMode = practice;
    }

  
    public bool getPracticeMode()
    {
        return practiceMode;
    }
}
