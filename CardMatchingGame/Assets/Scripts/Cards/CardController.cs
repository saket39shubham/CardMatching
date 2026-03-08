using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int cardID;

    public bool isFlipped = false;
    public bool isMatched = false;

    public GameObject front;
    public GameObject back;

    public Image frontImage;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCardClicked);
    }

    public void SetCard(int id)
    {
        cardID = id;
        ShowBack();
    }

    void OnCardClicked()
    {
        if (isFlipped || isMatched) return;

        FlipCard();

        GameManager.Instance.CardSelected(this);
    }

    public void FlipCard()
    {
        isFlipped = true;

        front.SetActive(true);
        back.SetActive(false);
    }

    public void FlipBack()
    {
        isFlipped = false;

        front.SetActive(false);
        back.SetActive(true);
    }

    public void SetMatched()
    {
        isMatched = true;

        frontImage.color = Color.green;
    }

    public void SetMismatch()
    {
        frontImage.color = Color.red;
    }

    public void ResetColor()
    {
        frontImage.color = Color.white;
    }

    public void ShowBack()
    {
        isFlipped = false;

        front.SetActive(false);
        back.SetActive(true);
    }
}