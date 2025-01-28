using UnityEngine;
using UnityEngine.UI;

public class CardSelector : MonoBehaviour
{
    public GameObject cardsGroupParent;

    private GameObject[] cards = new GameObject[3];
    private bool[] isCardDisabled;
    private int currentIndex = 0;
    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    [Header("Colores de Selección")]
    public Color selectedColor = Color.green;
    public Color defaultColor = Color.white;
    public Color disabledColor = Color.gray;

    void Start()
    {
        if (cardsGroupParent != null)
        {
            int childCount = cardsGroupParent.transform.childCount;
            cards = new GameObject[childCount];
            isCardDisabled = new bool[childCount];

            for (int i = 0; i < childCount; i++)
            {
                Transform child = cardsGroupParent.transform.GetChild(i);
                cards[i] = child.gameObject;
                isCardDisabled[i] = false;
            }
        }

        if (cards.Length > 0)
        {
            HighlightCard(currentIndex);
        }
    }

    void Update()
    {
        if (Time.time - lastInputTime < inputCooldown)
            return;

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0.5f)
        {
            NavigateToCard(1);
            lastInputTime = Time.time;
        }
        else if (horizontal < -0.5f)
        {
            NavigateToCard(-1);
            lastInputTime = Time.time;
        }

        if (Input.GetButtonDown("Submit"))
        {
            UseCard();
        }
    }

    private void NavigateToCard(int direction)
    {
        int newIndex = currentIndex;

        do
        {
            newIndex += direction;

            if (newIndex < 0)
            {
                newIndex = cards.Length - 1;
            }
            else if (newIndex >= cards.Length)
            {
                newIndex = 0;
            }

            if (!isCardDisabled[newIndex])
            {
                currentIndex = newIndex;
                HighlightCard(currentIndex);
                return;
            }

        } while (newIndex != currentIndex);
    }

    private void HighlightCard(int index)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            var cardImage = cards[i].GetComponent<Image>();
            if (cardImage != null)
            {
                if (isCardDisabled[i])
                {
                    cardImage.color = disabledColor;
                }
                else
                {
                    cardImage.color = (i == index) ? selectedColor : defaultColor;
                }
            }
        }
    }

    private void UseCard()
    {
        if (isCardDisabled[currentIndex])
        {
            return;
        }
        DisableCard(currentIndex);
    }

    private void DisableCard(int index)
    {
        isCardDisabled[index] = true;

        var cardImage = cards[index].GetComponent<Image>();
        if (cardImage != null)
        {
            cardImage.color = disabledColor;
        }
    }
}
