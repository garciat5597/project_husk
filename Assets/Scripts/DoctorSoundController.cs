using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorSoundController : MonoBehaviour
{
    // Get FMOD instances
    FMOD.Studio.EventInstance footstepEvent;
    static int materialType; // 0 = stone 1 = wood 2 = slime

    // Start is called before the first frame update
    void Start()
    {
        materialType = 0;
    }

    public static void SetFootstepType(int type)
    {
        materialType = type;
    }

    // Play footstep
    void PlayDoctorFootstep()
    {
        // Create instance
        footstepEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Doctor/Footsteps");
        footstepEvent.setParameterByName("MaterialType", materialType);
        footstepEvent.start();
        footstepEvent.release();
    }

    void PlayDoctorDash()
    { 
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Doctor/Dash");
    }

    void PlayDoctorWallCling()
    {
        // Put here when we get ittt
    }

}
