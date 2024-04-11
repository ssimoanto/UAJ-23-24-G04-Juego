using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    void Update () 
    {
        //mover la camara con el jugador
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
    }
}
