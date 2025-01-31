using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionCardController : MonoBehaviour
{
    public ArmaMejoras gunAttributes;

    public bool isHeal, isBaseGun, canAppearMultipleTimes;
    public string gunName;

    public int money;
    public TextMeshProUGUI moneyText;
    public GameObject moneySectionGameObject;
    public GameObject itemBoughtGameObject;

    public int healQuantity = 50;

    private EndRoundSceneCanvasController endRounCanvas;
    private Button buttonComponent;

    private void Start()
    {
        buttonComponent = GetComponent<Button>();
        endRounCanvas = GameObject.FindGameObjectWithTag("EndRoundCanvas").GetComponent<EndRoundSceneCanvasController>();
        moneyText.text = money.ToString();
    }

    public void ItemSelected()
    {
        if (GameManager.Coins - money >= 0)
        {
            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
            buttonComponent.interactable = false;
            itemBoughtGameObject.SetActive(true);
            moneySectionGameObject.SetActive(false);

            if (selectedObject == gameObject)
            {
                EventSystem.current.SetSelectedGameObject(null);
                SelectAnotherButton();
            }

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
            else
            {
                endRounCanvas.ImproveGun(gunAttributes, gunName, money);
            }
        }
    }

    private void SelectAnotherButton()
    {
        Selectable nextSelectable = buttonComponent.FindSelectable(Vector3.left);

        if (nextSelectable == null)
        {
            nextSelectable = buttonComponent.FindSelectable(Vector3.right);
        }

        if (nextSelectable != null)
        {
            EventSystem.current.SetSelectedGameObject(nextSelectable.gameObject);
        }
    }
}
