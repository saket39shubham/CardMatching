using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public int cardID;
    public bool isFlipped = false;
    public bool isMatched = false;

    public GameObject front;
    public GameObject back;

    public Image frontImage;   // used to change color when matched

    void Start()
    {
        ShowBack();
    }

    public void OnCardClicked()
    {
        if (isFlipped || isMatched) return;

        FlipCard();

        // Inform GameManager that a card was clicked
        GameManager.Instance.CardSelected(this);
    }

    public void FlipCard()
    {
        isFlipped = true;

        front.SetActive(true);
        back.SetActive(false);
    }

    public void ShowBack()
    {
        isFlipped = false;

        front.SetActive(false);
        back.SetActive(true);
    }

    public void SetMatched()
    {
        isMatched = true;

        // Change color to green when matched
        if (frontImage != null)
        {
            frontImage.color = Color.green;
        }
    }

    public void SetMismatch()
    {
        // change color to red briefly
        if (frontImage != null)
        {
            frontImage.color = Color.red;
        }

        Invoke(nameof(ResetCard), 0.8f);
    }

    void ResetCard()
    {
        frontImage.color = Color.white;
        ShowBack();
    }
}