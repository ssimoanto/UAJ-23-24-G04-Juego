# Cambios en el juego

En este documento se verán las clases del juego en las que se ha añadido código de instrumentalización

## GameManager.cs

```c#
//...
using G04Telemetry;
//...
int stage = 1;
    [SerializeField]
    string _fileName;
//...
    void Start()
    {
    //...
          G04Telemetry.Tracker.Init("SteamMazehemGame", 5.0f, G04Telemetry.SerializeType.JSON, G04Telemetry.PersistanceType.File, _fileName);
    //...
    }
    void Update(){
    G04Telemetry.Tracker.Instance().update(Time.deltaTime);
    //..
    }
    //..
    public void QuitGame()
    {
        Tracker.Instance().closeTracker();
        //...

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
//...
    }
//... 
     public void ChangeScene(string sceneName)
    {
        //...
        if (Enum.TryParse<G04Telemetry.LevelEnum>(sceneName, out G04Telemetry.LevelEnum sceneEnumValue))
        {
            G04Telemetry.Tracker.Instance().startLevel(sceneEnumValue);
        }
         stage++;
        //...
    }
    //...
     public void NextLevel()
    {
        //...
        if (stage >= scenesInOrder.Length)
        {
            //fin nivel 2
            G04Telemetry.Tracker.Instance().endLevel(LevelEnum.Level2, LevelEnd.Win);
            //...
        }
        else
        {
            //fin nivel 1
            G04Telemetry.Tracker.Instance().endLevel(LevelEnum.Level1, LevelEnd.Win);
            ChangeScene(scenesInOrder[stage]);
        }
        //...
        goMenuAfterLevel();
        //...
    }
    //...
    public void startGame()
    {
        G04Telemetry.Tracker.Instance().startGame();
        ChangeScene("Level1");
    }
    //...
       public void GameOver()
    {
        //...
        if (Enum.TryParse<G04Telemetry.LevelEnum>(scenesInOrder[stage-1], out G04Telemetry.LevelEnum sceneEnumValue))
        {
            G04Telemetry.Tracker.Instance().endLevel(sceneEnumValue, LevelEnd.Loose);
        }
         G04Telemetry.Tracker.Instance().endGame();
        //...
                goMenuAfterLevel();
        //...
    }
    //... Hemos creado este método ya que queríamos hacer distinción de ir al menú al perder/ganar o al salir, ya que nos estaba dando problemas
        void goMenuAfterLevel()
    {
        stage = 1;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        G04Telemetry.Tracker.Instance().endGame();
        SceneManager.LoadScene("Menu");
    }
    //...
     void ReturnMenu()
    {
        stage = 1;
        //...
        if (Enum.TryParse<G04Telemetry.LevelEnum>(scenesInOrder[stage - 1], out G04Telemetry.LevelEnum sceneEnumValue))
        {
            G04Telemetry.Tracker.Instance().endLevel(sceneEnumValue, LevelEnd.Other);
        }
         G04Telemetry.Tracker.Instance().endGame();
        //...
    }
    //...
```

## Puzzle.cs

```c#
//...
using G04Telemetry;
//...
void Update(){
    //...
     G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.RoomMoveEvent());
    //...
}
```
## Enemy.cs

```c#
//...
using G04Telemetry;
//...
    public void TakeDamage(int damage)
    {
        G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.EnemyReceiveDamageEvent());
        //..
    }
```

## Attack.cs

```c#
//...
using G04Telemetry;
//...
    void Attacking()
    {
        print(G04Telemetry.Tracker.Instance());
        G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.AttackEvent());
        //...
    }
//...
```

## EnemyAtkREMIX.cs

```c#
    public IEnumerator AttackDurator()
    {
        //...
        G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.PlayerReceiveDamageEvent(G04Telemetry.EnemyType.Robot));
        //...   
    }
```

## PlayerController.cs

```c#
//...
  private void OnTriggerEnter2D(Collider2D oneHitEnemy)
    {
        //...
        if (oneHitEnemy.tag == "Sierra")
        {
            G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.PlayerReceiveDamageEvent(G04Telemetry.EnemyType.Saw));
        //...
        }
        else if (oneHitEnemy.tag == "Enemy")
        {
            G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.PlayerReceiveDamageEvent(G04Telemetry.EnemyType.Spider));
            //...
        }
    //...
    }
 private void OnTriggerStay2D(Collider2D gas)
    {
        if (gas.tag == "Gas" && !tiempogas)
        {
            G04Telemetry.Tracker.Instance().addEvent(new G04Telemetry.SteamMazehemEvents.PlayerReceiveDamageEvent(G04Telemetry.EnemyType.Sewer));
            //...
        }
    }
//...
```
