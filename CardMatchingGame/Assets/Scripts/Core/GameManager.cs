using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private CardController firstCard;
    private CardController secondCard;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        Debug.Log("Game Started");
    }

    public void CardSelected(CardController card)
    {
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            CheckMatch();
        }
    }

    void CheckMatch()
    {
        if (firstCard.cardID == secondCard.cardID)
        {
            firstCard.SetMatched();
            secondCard.SetMatched();
        }
        else
        {
            firstCard.SetMismatch();
            secondCard.SetMismatch();
        }

        firstCard = null;
        secondCard = null;
    }
}