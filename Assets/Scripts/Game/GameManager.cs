using UnityEngine;

public enum GameState
{
    MainMenu,
    Lv1,
    Lv2,
    Lv3,
    Lv4,
    Lv5,
    Paused,
}

public class GameManager : MonoBehaviour
{
    public GameState currentState;
    private GameState lastState;
    public static GameManager Instance { get; private set; }

    private LevelManager sceneManager;
    private int levelIndex;
    
    public static Vector3 lastSavedPosition;

    void Awake()
    {
        // Singleton initialization
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persist across levels
        }

        sceneManager = new LevelManager();

    }

    void Start()
    {
        currentState = GameState.MainMenu;
        sceneManager = LevelManager.Instance;

    }

    void Update()
    {
        if (currentState != lastState)
        {
            PerformActionOnStateChange();
        }
        lastState = currentState;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; // Clear the instance when destroyed
        }
    }

    private void PerformActionOnStateChange()
    {
        if (currentState == GameState.Paused){
            Debug.Log("PAUCE");
        }
        else{
            // go to level based on index
            // get index of gamestate
            levelIndex = (int)currentState;
            // go to level based on gamestate
            sceneManager.SwitchScene(levelIndex);
        }

    }


    public void UpdateState(GameState state)
    {
        currentState = state;
        PerformActionOnStateChange();
    }

    public void SavePosition(Vector3 pos)
    {
        // TODO: also save rotation
        lastSavedPosition = pos;
    }
}