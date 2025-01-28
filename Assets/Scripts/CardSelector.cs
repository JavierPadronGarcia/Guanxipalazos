using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class CardSelector : MonoBehaviour
{
    public GameObject cardsGroupParent;

    private GameObject[] cards = new GameObject[3];
    private int currentIndex = 0;
    private float inputCooldown = 0.2f;
    private float lastInputTime = 0f;

    [Header("Colores de Selección")]
    public Color selectedColor = Color.green;
    public Color defaultColor = Color.white;

    void Start()
    {
        if (cardsGroupParent != null)
        {
            for (int i = 0; i < cardsGroupParent.transform.childCount; i++)
            {
                Transform child = cardsGroupParent.transform.GetChild(i);
                cards[i] = child.gameObject;
                Debug.Log("child encontrado");
            }
        }


        if (cards.Length > 0)
        {
            HighlightCard(currentIndex);
        }
        else
        {
            Debug.LogWarning("No se han asignado tarjetas en el inspector.");
        }
    }

    void Update()
    {
        if (Time.time - lastInputTime < inputCooldown)
            return;

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0.5f)
        {
            SelectCard(currentIndex + 1);
            lastInputTime = Time.time;
        }
        else if (horizontal < -0.5f)
        {
            SelectCard(currentIndex - 1);
            lastInputTime = Time.time;
        }

        if (Input.GetButtonDown("Submit"))
        {
            UseCard();
        }
    }

    private void SelectCard(int newIndex)
    {
        if (newIndex >= 0 && newIndex < cards.Length)
        {
            currentIndex = newIndex;
            HighlightCard(currentIndex);
        }
        else
        {
            Debug.Log("Índice fuera de rango, no hay más tarjetas.");
        }
    }

    private void HighlightCard(int index)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            var cardImage = cards[i].GetComponent<Image>();
            if (cardImage != null)
            {
                cardImage.color = (i == index) ? selectedColor : defaultColor;
            }
            else
            {
                Debug.LogWarning($"La tarjeta {cards[i].name} no tiene un componente Image.");
            }
        }
    }

    private void UseCard()
    {
        Debug.Log($"Usando la tarjeta: {cards[currentIndex].name}");
    }
}

