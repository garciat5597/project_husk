using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeScript : MonoBehaviour
{
    public float effectDuration;
    public int effectStrength;
    [SerializeField]
    private Rigidbody2D player;

    private void Start()
    {
        Debug.Log("pre-play drag: " + player.drag);
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }
    void OnTriggerStay2D(Collider2D other)
    { 
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Status Applied " + player.drag);
            //player.velocity *= slimeEffect;
            player.drag = effectStrength;
        }  
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(slimeHazzardEffect());
        }
    }
    IEnumerator slimeHazzardEffect()
    {
        yield return new WaitForSeconds(effectDuration);
        player.drag = 2;
        Debug.Log("Status Removed " + player.drag);
    }
}
