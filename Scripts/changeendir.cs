using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeendir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public int dirx = 1;
    public int diry = 0;
    public Rigidbody2D rbody;
    public int rotZ = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MovEnemy1 dir = collision.gameObject.GetComponent<MovEnemy1>();
        if (dir)
        {
            StartCoroutine(TiempoGirar());
            print("Ha entrado");
            dir.speedx = dirx * dir.speedaux;
            dir.speedy = diry * dir.speedaux;
            rbody.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + rotZ);
        }
    }
    public IEnumerator TiempoGirar()
    {
        yield return new WaitForSeconds(1);
        print("ha pasado 1 seg");

    }
}
