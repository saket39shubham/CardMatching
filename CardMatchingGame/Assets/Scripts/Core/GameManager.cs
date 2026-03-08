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
        StartGame();
    }
    public void CardSelected(CardController card)
    {
        flippedCards.Add(card);

        if (flippedCards.Count >= 2)
        {
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        CardController card1 = flippedCards[0];
        CardController card2 = flippedCards[1];

        if (card1.cardID == card2.cardID)
        {
            card1.SetMatched();
            card2.SetMatched();
        }
        else
        {
            card1.SetMismatch();
            card2.SetMismatch();

            yield return new WaitForSeconds(1f);

            card1.FlipBack();
            card2.FlipBack();
        }

        flippedCards.Clear();
    }
    void StartGame()
    {
        BuildLayout(layoutDropdown != null ? layoutDropdown.value : 2);
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