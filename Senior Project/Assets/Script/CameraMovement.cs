using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private float Lerpspeed = 5f;
    private Vector3 player_pos = Vector3.zero;

	void FixedUpdate()
    {
        //getting Player's position and offsetting for the camera
        player_pos = GameObject.Find("Player").GetComponent<Transform>().position;
        player_pos.y += 4f;
        player_pos.z -= 4f;

        //Lerp old camera position to new player position
        transform.position = Vector3.Lerp(transform.position, player_pos, Lerpspeed * Time.deltaTime);
    }
}
