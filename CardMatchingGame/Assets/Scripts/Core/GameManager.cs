using UnityEngine;

/// <summary>
/// Central controller of the game.
/// Handles overall game state and communicates with other systems.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // Singleton pattern ensures only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartGame();
    }

    /// <summary>
    /// Initializes game systems.
    /// </summary>
    public void StartGame()
    {
        Debug.Log("Game Started");
    }
}