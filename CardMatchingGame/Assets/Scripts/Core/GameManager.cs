using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<CardController> flippedCards = new List<CardController>();
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TMP_Dropdown layoutDropdown;
    private int seed;
    private bool isChecking = false;
    [Header("UI")]
    [SerializeField] private TMP_Text movesText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TMP_Text winScoreText;
    private int moves = 0;
    private float timer = 0f;
    private int score = 0;
    private bool timerRunning = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        seed = Random.Range(int.MinValue, int.MaxValue);

        if (layoutDropdown != null)
            layoutDropdown.onValueChanged.AddListener(OnLayoutChanged);
        StartGame(layoutDropdown != null ? layoutDropdown.value : 0);
    }
    void Update()
    {
        if (!timerRunning) return;

        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
    public void CardSelected(CardController card)
    {
        if (isChecking) return;
        flippedCards.Add(card);

        if (flippedCards.Count == 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;
        boardManager.SetBoardInteractable(false);
        CardController card1 = flippedCards[0];
        CardController card2 = flippedCards[1];
        moves++;
        UpdateMoves();
        if (card1.cardID == card2.cardID)
        {
            AudioManager.Instance.PlayMatch();
            score += card1.cardID + 1;   
            UpdateScore();
            card1.SetMatched();
            card2.SetMatched();
        }
        else
        {
            AudioManager.Instance.PlayMismatch();
            //card1.SetMismatch();
            //card2.SetMismatch();

            yield return new WaitForSeconds(1f);

            card1.FlipBack();
            card2.FlipBack();
        }

        flippedCards.Clear();
        boardManager.SetBoardInteractable(true);
        isChecking = false;
        if (boardManager.AllMatched())
        {
            WinGame();
            Debug.Log("You Win!");
        }
    }
    void UpdateMoves()
    {
        if (movesText != null)
            movesText.text = "Moves: " + moves;
    }
    void UpdateScore()
    {
        if (scoreText != null)
            scoreText.text = "Score : " + score;
    }
    void WinGame()
    {
        timerRunning = false;

        AudioManager.Instance.PlayGameOver();

        if (winPanel != null)
        {
            winPanel.SetActive(true);

            if (winScoreText != null)
            {
                winScoreText.text = "Score: " + score;  // show final score
            }
        }
    }

    public void ResetGame()
    {
        seed = Random.Range(int.MinValue, int.MaxValue);
        StartGame(layoutDropdown != null ? layoutDropdown.value : 0);
    }
    void StartGame(int layoutIndex)
    {
        moves = 0;
        score = 0;
        timer = 0;
        timerRunning = true;

        UpdateMoves();
        UpdateScore();
        BuildLayout(layoutIndex);

        if (winPanel != null)
            winPanel.SetActive(false);
    }
    void BuildLayout(int index)
    {
        switch (index)
        {
            case 0:
                boardManager.BuildNew(2, 2, seed);
                break;

            case 1:
                boardManager.BuildNew(2, 3, seed);
                break;

            case 2:
                boardManager.BuildNew(4, 4, seed);
                break;
        }
    }

    void OnLayoutChanged(int index)
    {
        seed = Random.Range(int.MinValue, int.MaxValue);
        BuildLayout(index);
    }
}