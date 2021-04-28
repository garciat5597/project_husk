using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEnemySoundController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void PlaySlimeIdle()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Enemies/Slime/Slime_Idle", gameObject);
    }

    public static void PlaySlimeGrab()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Enemies/Slime/Slime_Grapple");
        Debug.Log("ttttt");
    }

    public static void PlayBookThrow(GameObject hand)
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Enemies/Hand/Book_Throw", hand);
    }
}
