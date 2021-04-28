using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeScript : MonoBehaviour
{
    public float effectDuration;
    public int effectStrength;
    [SerializeField]
    private Rigidbody2D player;
    private float removeEffect;

    private void Start()
    {
        Debug.Log("pre-play drag: " + player.drag);
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        removeEffect = player.drag;
    }
    void OnTriggerStay2D(Collider2D other)
    { 
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Status Applied " + player.drag);
            DoctorSoundController.SetFootstepType(2);
            //player.velocity *= slimeEffect;
            player.drag = effectStrength;
        }  
    }
    void OnTriggerExit2D(Collider2D other)
    {
        DoctorSoundController.SetFootstepType(0);
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(slimeHazzardEffect());
        }
    }
    IEnumerator slimeHazzardEffect()
    {
        yield return new WaitForSeconds(effectDuration);
        player.drag = removeEffect;
        Debug.Log("Status Removed " + player.drag);
    }
}
