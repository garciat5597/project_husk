using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoctorSoundController : MonoBehaviour
{
    // Get FMOD instances
    FMOD.Studio.EventInstance footstepEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Create instance
        footstepEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Doctor/Footsteps");

    }

    // Play footstep
    void PlayDoctorFootstep()
    {
        footstepEvent.start();
        footstepEvent.release();
    }


}
