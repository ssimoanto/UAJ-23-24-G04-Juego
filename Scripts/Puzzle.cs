using UnityEngine;
public class Puzzle : MonoBehaviour
{
    // gameobject del espacio vacío del puzle
    public Transform emptySpace;
    public Transform empty;
    public int distance, numOfTiles;
    public GameObject cubes;
    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            //aseguramos que sea solo el tile
            if(hit && hit.collider.gameObject.layer == 13)
            {
                if(Vector2.Distance(emptySpace.position, hit.transform.position) < distance)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    Vector2 lastEmpty = empty.position;
                    emptySpace.position = hit.transform.position;
                    
                    for (int i = 0; i < numOfTiles; i++)
                    {
                        if(hit.collider.gameObject.name == "Tile ("+ i +")")
                        {
                            string name = "Cube (" + i + ")";
                            Transform child = GetCube(cubes,name).transform;
                            empty.position = child.position;
                            child.position = lastEmpty;

                        }
                    }
                    hit.transform.position = lastEmptySpacePosition;
                }

            }
        }
    }
    GameObject GetCube(GameObject cubes,string name)
    {
        Transform childTrans = cubes.transform.Find(name);
        return childTrans.gameObject;
    }
}