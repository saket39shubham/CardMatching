using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class CardController : MonoBehaviour
{
    public int cardID;

    public bool isFlipped = false;
    public bool isMatched = false;

    public GameObject front;
    public GameObject back;

    [SerializeField] private TMP_Text numberText;
    [SerializeField] private CanvasGroup canvasGroup;
    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCardClicked);
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetCard(int id)
    {
        cardID = id;
        if (numberText != null)
            numberText.text = (id + 1).ToString();
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
        AudioManager.Instance.PlayFlip();
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

        // Disable button instead of hiding
        if (button != null)
            button.interactable = false;

        front.SetActive(true);
        back.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(MatchAnimation());
    }
    private IEnumerator MatchAnimation()
    {
        float duration = 0.5f; // half a second animation
        float elapsed = 0f;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * 1.2f; // small pop

        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 0.5f; // reduce opacity to 50%

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            yield return null;
        }

        // Reset scale to normal after pop
        transform.localScale = startScale;
        canvasGroup.alpha = targetAlpha;
    }
    public void ShowBack()
    {
        isFlipped = false;

        front.SetActive(false);
        back.SetActive(true);
    }
    public void ResetCard()
    {
        isFlipped = false;
        isMatched = false;

        if (button != null)
            button.interactable = true;

        front.SetActive(false);
        back.SetActive(true);

        if (canvasGroup != null)
            canvasGroup.alpha = 1f;

        transform.localScale = Vector3.one;
    }
}