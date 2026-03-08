using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    [Header("Board Settings")]
    public int rows = 4;
    public int cols = 4;

    [Header("References")]
    public GameObject cardPrefab;
    public Transform boardRoot;

    private List<CardController> cards = new List<CardController>();

    public List<CardController> Cards => cards;

    void Start()
    {
        BuildBoard();
    }

    public void BuildBoard()
    {
        ClearBoard();

        int total = rows * cols;

        if (total % 2 != 0)
        {
            Debug.LogError("Card count must be even!");
            return;
        }

        List<int> deck = GenerateDeck(total);

        for (int i = 0; i < total; i++)
        {
            GameObject obj = Instantiate(cardPrefab, boardRoot);
            CardController card = obj.GetComponent<CardController>();

            card.SetCard(deck[i]);

            cards.Add(card);
        }
    }

    List<int> GenerateDeck(int total)
    {
        List<int> deck = new List<int>();

        int pairs = total / 2;

        for (int i = 0; i < pairs; i++)
        {
            deck.Add(i);
            deck.Add(i);
        }

        Shuffle(deck);

        return deck;
    }

    void Shuffle(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    void ClearBoard()
    {
        foreach (Transform child in boardRoot)
        {
            Destroy(child.gameObject);
        }

        cards.Clear();
    }
}