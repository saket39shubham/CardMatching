using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<CardController> flippedCards = new List<CardController>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
}