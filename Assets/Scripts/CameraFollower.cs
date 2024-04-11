using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//ALEJANDRO MARTINEZ Y SIMONA ANTONOVA MIHAYLOVA

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public Camera main;
    public GameObject vignette;
    public int duration = 1; 
    public GameObject tiles;
    public GameObject healthbar;

    private bool zoom = false;
    
    void Start()
    {
        StartCoroutine(Scenecamera());
        StartCoroutine(Scenegame());
    }

    void Update () 
    {
        //mover la camara con el jugador
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, -10);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            MakeZoom();
        }
    }
    void MakeZoom()
    {
        if (!zoom)
        {
            main.orthographicSize *= 2;
            main.transform.localScale *= 2;
            zoom = true;
            StartCoroutine(timer());
        }

    }
    IEnumerator timer() {
        yield return new WaitForSeconds(duration);
        main.orthographicSize /= 2;
        main.transform.localScale /= 2;
        zoom = false;
    }
    IEnumerator Scenecamera()
    {
        vignette.SetActive(false);
        tiles.SetActive(false);
        healthbar.SetActive(false);
        
        yield return new WaitForSeconds(15);
    }
    IEnumerator Scenegame()
    {
        yield return new WaitForSeconds(16);
        tiles.SetActive(true);
        healthbar.SetActive(true);
        vignette.SetActive(true);
    }
}
