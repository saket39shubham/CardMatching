using UnityEngine;

public class CardController : MonoBehaviour
{
    public int cardID;
    public bool isFlipped = false;

    public GameObject front;
    public GameObject back;

    void Start()
    {
        ShowBack();
    }

    public void FlipCard()
    {
        if (isFlipped) return;

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
}