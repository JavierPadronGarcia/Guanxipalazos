using TMPro;
using UnityEngine;

public class MainGameCanvasController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;


    void Update()
    {
        coinsText.text = GameManager.Coins.ToString();
    }
}
