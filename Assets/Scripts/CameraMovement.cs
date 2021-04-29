using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject husk;

    private float zOffset = -5;

    private Vector3 target = Vector3.zero;
    private Vector3 targetOffset = new Vector3(0f, 0f, -8f);
    private Vector3 playerVelocity = Vector3.zero;

    [SerializeField] private float lerpXratio = 0.15f;          // smaller value = closer to Player
    [SerializeField] private float lerpXdistance = 12.0f;       // distance where CameraLerpX() starts
    [SerializeField] private float cameraSpeed = 0.15f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float vertBoundUpper = 2f;         // boundaries for CameraMoveY()
    [SerializeField] private float vertBoundLower = 4f;

    [SerializeField] private float clampRadius = 2.5f;

    // Update is called once per frame
    void FixedUpdate()
    {
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

        // get magnitudes between player and camera, and player and husk
        Vector2 diffPlayerCamera = new Vector2(player.transform.position.x - target.x, player.transform.position.y - target.y);
        Vector2 diffPlayerHusk = new Vector2(player.transform.position.x - husk.transform.position.x, player.transform.position.y - husk.transform.position.y);

        CameraMoveX();
        CameraLerpX(diffPlayerHusk, lerpXratio, lerpXdistance);
        CameraMoveY(diffPlayerCamera);

        target += targetOffset;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, cameraSpeed);

        MasterSoundController.UpdateHuskDistance(diffPlayerHusk.magnitude);
    }

    // Move horizontally based on Player's velocity
    void CameraMoveX()
    {
        target = new Vector3(player.transform.position.x + playerVelocity.x / 6, target.y, 0);
    }

    // If near to the Husk, lerp between player and husk for horizontal movement
    void CameraLerpX(Vector2 diff, float ratio, float distance)
    {
        if (diff.magnitude < distance)
        {
            float lerp = Mathf.Lerp(target.x, husk.transform.position.x, ratio);
            target = new Vector3(lerp, target.y, 0);
        }
    }

    // Move camera vertically if Player crosses upper or lower boundary
    void CameraMoveY(Vector2 diff)
    {
        // if player is above camera
        if (diff.y > vertBoundUpper)
        {
            target = new Vector3(target.x, player.transform.position.y - vertBoundUpper, 0);
        }

        // if player is below camera
        if (diff.y < -vertBoundLower)
        {
            target = new Vector3(target.x, player.transform.position.y + vertBoundLower, 0);
        }
    }
}
