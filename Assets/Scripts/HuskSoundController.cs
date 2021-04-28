using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuskSoundController : MonoBehaviour
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
    void PlayHuskFootstep()
    {
        // Create instance
        footstepEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Enemies/Husk/Footsteps");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(footstepEvent, gameObject.transform, gameObject.GetComponent<Rigidbody2D>());
        footstepEvent.setParameterByName("MaterialType", materialType);
        footstepEvent.start();
        footstepEvent.release();
    }

    void PlayHuskWallCling()
    {
        // Put here when we get ittt
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Enemies/Husk/Wall_Cling", gameObject);
    }
}
