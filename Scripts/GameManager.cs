using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance; //Definimos la instancia del game manager. Paso imprescindible para el Singleton
    public string[] scenesInOrder;
    public GameObject menuCanvas, pauseCanvas, puzzle3x2Canvas, puzzle3x3Canvas;
    int stage=1;
    bool gamePaused = false;
    void Awake() //Utilizamos awake en vez de Start para asegurarnos que la UIManager se inicia antes que cualquier otro componente      de la escena
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //utilizamos este código para mantener el GameManager en todas los escenas pero                  teniéndolo únicamente en la primera
        }
        else Destroy(this.gameObject); //Eliminamos los prefabs que queden restantes
    }
    public static GameManager GetInstance() //Habilitamos la respuesta de instancia para el GameManager
    {
        return instance;
    }
    public void ChangeScene (string sceneName)
    {
        stage++;
        SceneManager.LoadScene(sceneName);
    }
    public void NextLevel()
    {
        if (stage >= scenesInOrder.Length)
        {
            ReturnMenu();
        }
        else
        {
            ChangeScene(scenesInOrder[stage]);
        }
    }
    void Start()
    {
        pauseCanvas.SetActive(false);
        puzzle3x2Canvas.SetActive(false);
        puzzle3x3Canvas.SetActive(false);
        Cursor.visible = true;
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            menuCanvas.SetActive(false);
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(gamePaused)
                {
                    Return ();
                }
                else
                {
                    Pause ();
                }
            }
        }
        else
        {
            menuCanvas.SetActive(true);
        }
        if (SceneManager.GetActiveScene().name == "Prueba1")
        {
            puzzle3x2Canvas.SetActive(true);
        }
        else
        {
            puzzle3x2Canvas.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "Prueba2")
        {
            puzzle3x3Canvas.SetActive(true);
        }
        else
        {
            puzzle3x3Canvas.SetActive(false);
        }
    }
    public void ReturnMenu()
    {
        stage = 1;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void Return()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }
    void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
}
