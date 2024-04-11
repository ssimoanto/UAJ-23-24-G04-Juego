using UnityEngine;

//Código implementado por:
//SIMONA ANTONOVA MIHAYLOVA Y EDUARDO GALLARDO

public class Puzzle : MonoBehaviour
{
    // gameobject del espacio vacío del puzle
    public Transform emptySpace;
    public Transform emptyCube;
    public int distance, numOfTiles;
    public GameObject[] tile, cube;

    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // detecta el input del ratón
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // aseguramos que solo detecte al tile
            if(hit && hit.collider.gameObject.layer == 13)
            {
                // if para que solo se puedan mover tiles contiguo al hueco
                if(Vector2.Distance(emptySpace.position, hit.transform.position) < distance)
                {
                    // intercambiar posiciones del tile y el hueco
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    Vector2 lastEmpty = emptyCube.position;
                    emptySpace.position = hit.transform.position;
                    GameManager.GetInstance().Sounds(3);

                    // intercambiar posiciones de la sala y el hueco
                    for (int i = 0; i < numOfTiles; i++)
                    {
                        if(hit.collider.gameObject == tile[i])
                        {
                            emptyCube.position = cube[i].transform.position;
                            cube[i].transform.position = new Vector3(lastEmpty.x, lastEmpty.y, 0f);
                        }
                    }
                    hit.transform.position = lastEmptySpacePosition;
                }
            }
        }
    }
}