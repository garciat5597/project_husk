using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSoundController : MonoBehaviour
{
    // Get all busses
    static private FMOD.Studio.Bus sfxBus;

    // Start is called before the first frame update
    void Start()
    {
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        Debug.Log("aa");
    }

    public static void StopAllSFX()
    {
        sfxBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public static void PauseSFX()
    {
        sfxBus.setPaused(true);
    }

    public static void ResumeSFX()
    {
        sfxBus.setPaused(false);
    }
}
