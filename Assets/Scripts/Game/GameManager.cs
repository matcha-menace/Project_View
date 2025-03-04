using UnityEngine;

public enum GameState
{
    MainMenu,
    Lv1,
    Lv2,
    Lv3,
    Lv4,
    Lv5,
    paused,
}

public class GameManager : MonoBehaviour
{
    public GameState currentState;
    public static GameManager Instance { get; private set; }

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
    }

    void Start()
    {
        currentState = GameState.MainMenu;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null; // Clear the instance when destroyed
        }
    }




    public void UpdateState(GameState state)
    {
        currentState = state;
    }
}
