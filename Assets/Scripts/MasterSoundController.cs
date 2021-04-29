using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSoundController : MonoBehaviour
{
    // Get all busses
    static private FMOD.Studio.Bus sfxBus;
    static private FMOD.Studio.Bus ambBus;

    // Instances
    private static FMOD.Studio.EventInstance ambience;
    private static FMOD.Studio.EventInstance chaseMus;

    // Start is called before the first frame update
    void Start()
    {
        // get busses
        sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
        ambBus = FMODUnity.RuntimeManager.GetBus("bus:/Ambience");

        // Start ambience
        ambience = FMODUnity.RuntimeManager.CreateInstance("event:/Ambiance/Level_Amb");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(ambience, gameObject.transform, gameObject.GetComponent<Rigidbody>());
        UpdatePlayerPosition(-110);
        ambience.start();

        // Start Main Chase
        chaseMus = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Main_Chase");
        chaseMus.start();
    }

    public static void StopAllSFX()
    {
        sfxBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambBus.stopAllEvents(FMOD.Studio.STOP_MODE.IMMEDIATE);
        ambience.release();
    }

    public static void PauseSFX()
    {
        sfxBus.setPaused(true);
        ambBus.setPaused(true);
    }

    public static void ResumeSFX()
    {
        sfxBus.setPaused(false);
        ambBus.setPaused(false);
    }

    public static void MuteSFX()
    {
        sfxBus.setMute(true);
        ambBus.setMute(true);
    }

    public static void UnmuteSFX()
    {
        sfxBus.setMute(false);
        ambBus.setMute(false);
    }

    public static void UpdatePlayerPosition(float pos)
    {
        ambience.setParameterByName("Vertical Position", pos);
    }

    public static void StartMainMusic()
    {
        chaseMus.setParameterByName("Husk Spawn", 1);
    }

    public static void UpdateHuskDistance(float dist)
    {
        chaseMus.setParameterByName("Husk Distance", dist);
    }
}
