using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Código implementado por:
//ALEJANDRO MARTÍNEZ

public class changeendir : MonoBehaviour
{
    public int dirx = 1;
    public int diry = 0;
    public Rigidbody2D rbody;
    public int rotZ;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovEnemy1 dir = collision.gameObject.GetComponent<MovEnemy1>();
        if (dir)
        {
            StartCoroutine(TiempoGirar());
            dir.speedx = dirx * dir.speedaux;
            dir.speedy = diry * dir.speedaux;
            rbody.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + rotZ);
        }
    }
    public IEnumerator TiempoGirar()
    {
        yield return new WaitForSeconds(1);
    }
}