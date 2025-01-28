using TMPro;
using UnityEngine;

public class SelectionCardController : MonoBehaviour
{
    public ArmaMejoras gunAttributes;

    public bool isHeal, isBaseGun;
    public string gunName;

    public int money;
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText.text = money.ToString();
    }
}
