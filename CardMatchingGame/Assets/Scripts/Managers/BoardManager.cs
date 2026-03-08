using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(GridLayoutGroup))]
public class BoardManager : MonoBehaviour
{
    [Header("Layout")]
    [SerializeField, Min(1)] private int rows = 4;
    [SerializeField, Min(1)] private int cols = 4;
    [SerializeField] private Vector2 padding = new Vector2(16, 16);
    [SerializeField] private Vector2 spacing = new Vector2(8, 8);

    [Header("Prefabs/Parents")]
    [SerializeField] private RectTransform boardRoot; // parent where GridLayoutGroup lives
    [SerializeField] private CardController cardPrefab;

    public int Rows => rows;
    public int Cols => cols;
    public int CardCount => rows * cols;
    public IReadOnlyList<CardController> Cards => _cards;

    public event Action<CardController> OnCardFaceUp;

    [SerializeField] private GridLayoutGroup _grid;
    readonly List<CardController> _cards = new List<CardController>();

    void Awake()
    {
        if (boardRoot == null) boardRoot = (RectTransform)transform;
        ApplyGridSpacing();
        RecalcCellSize();
    }

    void OnRectTransformDimensionsChange() => RecalcCellSize();

    void ApplyGridSpacing()
    {
        _grid.spacing = spacing;
        _grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        _grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _grid.constraintCount = cols;
        _grid.childAlignment = TextAnchor.MiddleCenter;
    }

    void RecalcCellSize()
    {
        var rt = boardRoot.rect;
        float availW = rt.width - padding.x * 2f - (cols - 1) * spacing.x;
        float availH = rt.height - padding.y * 2f - (rows - 1) * spacing.y;
        float cellW = availW / cols;
        float cellH = availH / rows;
        float cell = Mathf.Floor(Mathf.Min(cellW, cellH));
        _grid.cellSize = new Vector2(cell, cell);
        _grid.padding = new RectOffset((int)padding.x, (int)padding.x, (int)padding.y, (int)padding.y);
    }

    public void BuildNew(int r, int c, int seed)
    {
        rows = r; cols = c;
        ResetAllCards();
        Clear();
        ApplyGridSpacing();
        RecalcCellSize();

        int total = rows * cols;
        if (total % 2 != 0) throw new Exception("Card count must be even.");

        // deck: pair ids duplicated twice then shuffled
        var deck = new List<int>(total);
        int pairCount = total / 2;
        for (int i = 0; i < pairCount; i++)
        {
            deck.Add(i);
            deck.Add(i);
        }
        Shuffle(deck, seed);
        BuildFromDeck(deck.ToArray(), matched: null);
    }

    public void BuildFromDeck(int[] deck, bool[] matched)
    {
        Clear();
        ApplyGridSpacing();
        RecalcCellSize();

        for (int i = 0; i < deck.Length; i++)
        {
            var card = Instantiate(cardPrefab, boardRoot);
            string label = deck[i].ToString();
            card.SetCard(deck[i]);

            // Restore match state
            if (matched != null && i < matched.Length && matched[i])
            {
                card.SetMatched(); // disables button
            }
            else
            {
                // Reactivate unmatched cards
                var button = card.GetComponent<Button>();
                if (button != null)
                    button.interactable = true;
            }

            // Subscribe event
            _cards.Add(card);
        }
    }



    public bool AllMatched()
    {
        for (int i = 0; i < _cards.Count; i++)
            if (!_cards[i].isMatched) return false;
        return true;
    }

    public void Clear()
    {
        for (int i = boardRoot.childCount - 1; i >= 0; i--)
            Destroy(boardRoot.GetChild(i).gameObject);
        _cards.Clear();
    }

    public static void Shuffle<T>(IList<T> list, int seed)
    {
        var rng = new System.Random(seed);
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    public void SetBoardInteractable(bool state)
    {
        foreach (var card in _cards)
        {
            var btn = card.GetComponent<Button>();
            if (btn != null)
                btn.interactable = state;
        }
    }
    public void ResetAllCards()
    {
        foreach (var card in _cards)
        {
            if (card != null)
                card.ResetCard();
        }
    }

}
