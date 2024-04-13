using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using G04Telemetry;
//Código implementado por:
//EDUARDO GALLARDO

public class GameManager : MonoBehaviour
{
    private static GameManager instance; //Definimos la instancia del game manager. Paso imprescindible para el Singleton
    public string[] scenesInOrder;
    public GameObject menuCanvas, pauseCanvas, levelCanvas, endCanvas;
    public Text levelText, endText;
    public int duration;
    int stage=1;
    bool gamePaused = false, menuoff = false;
    void Awake() //Utilizamos awake en vez de Start para asegurarnos que la UIManager se inicia antes que cualquier otro componente de la escena
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //utilizamos este código para mantener el GameManager en todas los escenas pero teniéndolo únicamente en la primera
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
        StartCoroutine(Timer());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        if (stage >= scenesInOrder.Length)
        {
            endText.text = "You won";
            StartCoroutine(End());
            ReturnMenu();
        }
        else
        {
            ChangeScene(scenesInOrder[stage]);
        }
    }
    void Start()
    {
        G04Telemetry.Tracker.Init("MiPrueba",5.0f,G04Telemetry.SerializeType.JSON,G04Telemetry.PersistanceType.File,"MiFile");
        endCanvas.SetActive(false);
        levelCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
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
            levelText.text = SceneManager.GetActiveScene().name;
        }
        else if(!menuoff)
        {
            menuCanvas.SetActive(true);
        }
    }
    void ReturnMenu()
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
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.1f);
        levelCanvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        levelCanvas.SetActive(false);
    }
    IEnumerator End()
    {
        menuoff = true;
        yield return new WaitForSeconds(0.1f);
        endCanvas.SetActive(true);
        yield return new WaitForSeconds(duration);
        endCanvas.SetActive(false);
        menuoff = false;
    }
    public void GameOver()
    {
        endText.text = "Game Over";
        StartCoroutine(End());
        ReturnMenu();
    }
    public void Sounds(int n)
    {
        var audioManager = GetComponentInChildren<AudioManager>();
        audioManager.PlaySound(n);
    }
}
