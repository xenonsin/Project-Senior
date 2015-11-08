using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    //-------------------READ ME-------------------
    //gameobject players must be tagged "Player"
    //---------------------------------------------

    private float lerpspeed = 5f;
    private float cameraFloor = 4f;
    private float cameraCeiling = 7f;
    private Vector3 cameraPos = Vector3.zero;
    private Quaternion cameraRotation = Quaternion.Euler(new Vector3(50, 0, 0));
    public GameObject[] player;
    private Vector3[] playerPos;
    private int numberOfPlayer = 0;
    private float centerOfAllPlayerXPosition, centerOfAllPlayerZPosition,
                    meanX, meanZ,
                    avgChange, maxX, minX, maxZ, minZ;

    void Awake()
    {
        transform.localScale = new Vector3(1, 1, 1);
        transform.localRotation = cameraRotation;
    }

    void FixedUpdate()
    {
        //finding how many players
        player = GameObject.FindGameObjectsWithTag("Player");
        numberOfPlayer = player.Length;

        //player at the right most position
        maxX = 0;
        for (int i = 0; i < numberOfPlayer; i++)
            if (player[i].transform.position.x > maxX) maxX = player[i].transform.position.x;

        //player at the left most position
        minX = maxX;
        for (int i = 0; i < numberOfPlayer; i++)
            if (player[i].transform.position.x < minX) minX = player[i].transform.position.x;

        //player at the upper most position
        maxZ = 0;
        for (int i = 0; i < numberOfPlayer; i++)
            if (player[i].transform.position.z > maxZ) maxZ = player[i].transform.position.z;

        //player at the bottom most position
        minZ = maxZ;
        for (int i = 0; i < numberOfPlayer; i++)
            if (player[i].transform.position.z < minZ) minZ = player[i].transform.position.z;

        //center of the players in world space
        centerOfAllPlayerXPosition = (maxX + minX) / 2;
        centerOfAllPlayerZPosition = (maxZ + minZ) / 2;

        //mean of x and z
        meanX = maxX - minX;
        meanZ = maxZ - minZ;

        //average of both means
        avgChange = (meanX + meanZ) / 2;

        //min and max distance of the camera
        if (avgChange < cameraFloor)
            avgChange = cameraFloor;
        if (avgChange > cameraCeiling)
            avgChange = cameraCeiling;

        //setting camera position
        cameraPos.x = centerOfAllPlayerXPosition;
        cameraPos.y = avgChange;
        cameraPos.z = centerOfAllPlayerZPosition - avgChange;

        //lerp old camera position to new player position
        transform.position = Vector3.Lerp(transform.position, cameraPos, lerpspeed * Time.deltaTime);
    }
}
