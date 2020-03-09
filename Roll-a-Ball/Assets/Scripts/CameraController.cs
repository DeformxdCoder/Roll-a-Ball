using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Start ()
    {
        offset = transform.position - player.transform.position;            //Initialize Camera to above player
    }

    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;            //Update camera position as player moves
    }
}
