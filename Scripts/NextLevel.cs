using UnityEngine;

public class NextLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<PlayerController>())
        {
            GameManager.GetInstance().NextLevel();
        }
    }
}
