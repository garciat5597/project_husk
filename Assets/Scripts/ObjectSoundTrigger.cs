using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundTrigger : MonoBehaviour
{
    public string eventPath;

    // Play loop of given object
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(eventPath, gameObject);
    }
}
