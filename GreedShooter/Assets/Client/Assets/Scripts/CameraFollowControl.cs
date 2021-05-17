using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowControl : MonoBehaviour
{
    public Camera camera;

    PlayerControl player;
    public float speed = 5;

    //TODO: Set camera move max range?

    private void Start()
    {
        camera = Camera.main;
        player = FindObjectOfType<PlayerControl>();
    }

    private void FixedUpdate()
    {
        camera.transform.position = new Vector3(
            Mathf.Lerp(camera.transform.position.x, player.transform.position.x, speed * Time.deltaTime),
            Mathf.Lerp(camera.transform.position.y, player.transform.position.y, speed * Time.deltaTime),
            camera.transform.position.z
            );
    }
}
