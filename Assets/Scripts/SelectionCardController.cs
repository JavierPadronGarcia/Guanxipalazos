using TMPro;
using UnityEngine;

public class SelectionCardController : MonoBehaviour
{
    public ArmaMejoras gunAttributes;

    public bool isHeal, isBaseGun, canAppearMultipleTimes;
    public string gunName;

    public int money;
    public TextMeshProUGUI moneyText;

    public int healQuantity = 50;

    private EndRoundSceneCanvasController endRounCanvas;

    private void Start()
    {
        endRounCanvas = GameObject.FindGameObjectWithTag("EndRoundCanvas").GetComponent<EndRoundSceneCanvasController>();
        moneyText.text = money.ToString();
    }

    public void ItemSelected()
    {
        if (GameManager.Coins - money >= 0)
        {
            if (isHeal)
            {
                endRounCanvas.HealPlayer(healQuantity, money);
                return;
            }

            if (isBaseGun)
            {
                endRounCanvas.BuyGun(gunName, money);
                return;
            }

            endRounCanvas.ImproveGun(gunAttributes, gunName, money);
        }
    }
}
