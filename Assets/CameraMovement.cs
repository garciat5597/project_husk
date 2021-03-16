using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject husk;

    // vector to bring camera position towards viewer
    //private Vector3 ten = new Vector3(0, 0, -10);
    private float zOffset = -10;

    private Vector3 target = new Vector3(0f, 0f, 0f);
    private Vector3 targetOffset;

    [SerializeField] private float lerpXratio = 0.2f;
    [SerializeField] private float lerpXdistance = 10.0f;
    [SerializeField] private float cameraSpeed = 10.0f;

    [SerializeField] private float vertBoundUpper = 1;
    [SerializeField] private float vertBoundLower = 1.5f;

    [SerializeField] private float clampRadius = 2.5f;

    // Update is called once per frame
    void Update()
    {
        targetOffset = player.transform.position + new Vector3(player.GetComponent<Rigidbody2D>().velocity.x, player.GetComponent<Rigidbody2D>().velocity.y, 0);
        Debug.Log("targetOffset", targetOffset);
        // get magnitudes between player and camera, and player and husk
        Vector2 diffPlayerCamera = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        Vector2 diffPlayerHusk = new Vector2(player.transform.position.x - husk.transform.position.x, player.transform.position.y - husk.transform.position.y);

        CameraLerpX(diffPlayerHusk, player.transform.position.x, husk.transform.position.x, lerpXratio, lerpXdistance);
        CameraMoveY(diffPlayerCamera);
        //CameraBound(diffPlayerCamera);

        transform.position = Vector3.MoveTowards(transform.position, target, cameraSpeed);
    }

    // Lerp between player and husk for horizontal movement
    void CameraLerpX(Vector2 diff, float playerX, float huskX, float ratio, float distance)
    {
        // if husk is close to player, move camera to show husk
        if (diff.magnitude < lerpXdistance)
        {
            float _x = Mathf.Lerp(playerX, huskX, ratio);
            target = new Vector3(_x, target.y, zOffset);
        }
        // otherwise keep camera focused on player
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, cameraSpeed);
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }

    // Move camera vertically based on player position
    void CameraMoveY(Vector2 diff)
    {        
        // if y exceeds upper boundary
        if (diff.y > vertBoundUpper)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y - vertBoundUpper, transform.position.z);
        }

        // if y exceeds lower boundary
        if (diff.y < -vertBoundLower)
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + vertBoundLower, transform.position.z);
        }
    }

    // Move camera based on box around player
    // Want to keep small, like Sonic, because of fast movement
    void CameraBound(Vector2 diff)
    {
        transform.position = player.transform.position + Vector3.ClampMagnitude(diff, clampRadius);
    }
}
