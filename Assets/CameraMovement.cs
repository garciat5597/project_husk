using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject husk;

    private float zOffset = -10;

    private Vector3 target = Vector3.zero;
    private Vector3 targetOffset = new Vector3(0f, 1f, -10f);
    private Vector3 playerVelocity = Vector3.zero;

    [SerializeField] private float lerpXratio = 0.2f;
    [SerializeField] private float lerpXdistance = 12.0f;
    [SerializeField] private float cameraSpeed = 0.2f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float vertBoundUpper = 4.5f;
    [SerializeField] private float vertBoundLower = 5.5f;

    [SerializeField] private float clampRadius = 2.5f;

    // Update is called once per frame
    void Update()
    {
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        //target = Vector3.zero;
        //Debug.Log("targetOffset" + target);

        // get magnitudes between player and camera, and player and husk
        Vector2 diffPlayerCamera = new Vector2(player.transform.position.x - target.x, player.transform.position.y - target.y);
        Vector2 diffPlayerHusk = new Vector2(player.transform.position.x - husk.transform.position.x, player.transform.position.y - husk.transform.position.y);

        CameraMoveX();
        CameraLerpX(diffPlayerHusk, lerpXratio, lerpXdistance);
        CameraMoveY(diffPlayerCamera);
        //CameraBound(diffPlayerCamera);

        target += targetOffset;
        Debug.Log("target: " + target);

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, cameraSpeed);
    }

    void CameraMoveX()
    {
        target = new Vector3(player.transform.position.x + playerVelocity.x / 8, 0, 0);

    }

    // Lerp between player and husk for horizontal movement
    void CameraLerpX(Vector2 diff, float ratio, float distance)
    {
        // if husk is close to player, move camera to show husk
        if (diff.magnitude < distance)
        {
            float lerp = Mathf.Lerp(target.x, husk.transform.position.x, ratio);
            target = new Vector3(lerp, target.y, zOffset);
        }
    }

    // Move camera vertically based on player position
    void CameraMoveY(Vector2 diff)
    {
        Debug.Log("ydiff: " + diff.y);
        // if player is far above camera
        if (player.transform.position.y > target.y + vertBoundUpper)
        {
            Debug.Log("player above camera by " + vertBoundUpper);
            target = new Vector3(target.x, player.transform.position.y - vertBoundUpper, zOffset);
        }

        // if player is far below
        if (player.transform.position.y < target.y - vertBoundLower)
        {
            target = new Vector3(target.x, player.transform.position.y + vertBoundLower, zOffset);
        }

        //target.y = player.transform.position.y - vertBoundUpper;
    }

    // Move camera based on box around player
    // Want to keep small, like Sonic, because of fast movement
    void CameraBound(Vector2 diff)
    {
        transform.position = player.transform.position + Vector3.ClampMagnitude(diff, clampRadius);
    }
}
