using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float leftBound = -100;
    [SerializeField] private float rightBound = 1000;

    // Update is called once per frame
    void Update()
    {
        //New camera coordinates
        float xPosition;
        float yPosition;

        //Camera follows the player if he is inside bounds
        if(playerTransform.position.x > leftBound && playerTransform.position.x < rightBound)
        {
            xPosition = playerTransform.position.x + 2;
            yPosition = playerTransform.position.y + 2;
            transform.position = new Vector3(xPosition, yPosition, transform.position.z);
        }

        //Camera stops at borders if the player goes beyond them
        else
        {
            yPosition = playerTransform.position.y + 2;
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        }
    }
}
