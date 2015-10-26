using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private float Lerpspeed = 5f;
    private Vector3 player_pos1 = Vector3.zero;
    private Vector3 player_pos2 = Vector3.zero;
    private Vector3 camera_pos = Vector3.zero;
    private float avgxc, avgzc, avgxp, avgzp;

	void FixedUpdate()
    {
        //getting Player's position and offsetting for the camera
        player_pos1 = GameObject.Find("Player1").GetComponent<Transform>().position;
        player_pos2 = GameObject.Find("Player2").GetComponent<Transform>().position;
        avgxc = (player_pos1.x + player_pos2.x) / 2;
        avgzc = (player_pos1.z + player_pos2.z) / 2;
        avgxp = Mathf.Abs(player_pos1.x - player_pos2.x);
        avgzp = Mathf.Abs(player_pos1.z - player_pos2.z);
        camera_pos.x = avgxc;
        camera_pos.y = (avgxp+avgzp)/2;
        camera_pos.z = avgzc - (avgxp + avgzp) / 2;
        if (camera_pos.y < 4f)
            camera_pos.y = 4f;

        //Lerp old camera position to new player position
        transform.position = Vector3.Lerp(transform.position, camera_pos, Lerpspeed * Time.deltaTime);
    }
}
