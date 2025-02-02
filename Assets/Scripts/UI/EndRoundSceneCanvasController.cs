using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndRoundSceneCanvasController : MonoBehaviour
{
    private WaveManager waveManager;
    private GameObject[] players;
    public List<GameObject> possiblePlayerSelections;
    public Transform parentSelectionGroup;
    public GameObject continueButton;

    private int maxPossibleSelections = 3;
    private List<GameObject> selectionsToRemove = new List<GameObject>();

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(continueButton);
        waveManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveManager>();
        players = GameObject.FindGameObjectsWithTag("Player");

        GameObject player = players[GameManager.isPlayer1Turn ? 0 : 1];
        PlayerGunItemsController playerGunItemsController = player.GetComponent<PlayerGunItemsController>();

        var dictionary = playerGunItemsController.activeGuns;

        foreach (var possibleSelection in possiblePlayerSelections)
        {
            SelectionCardController selectionCardController = possibleSelection.GetComponent<SelectionCardController>();
            if (selectionCardController.isBaseGun && dictionary.ContainsKey(selectionCardController.gunName))
            {
                selectionsToRemove.Add(possibleSelection);
            }
        }

        foreach (var selectionToRemove in selectionsToRemove)
        {
            possiblePlayerSelections.Remove(selectionToRemove);
        }

        selectionsToRemove.Clear();

        foreach (var possibleSelection in possiblePlayerSelections)
        {
            SelectionCardController selectionCardController = possibleSelection.GetComponent<SelectionCardController>();
        }
    }

    private void Start()
    {
        InstanceRandomPossiblePlayerSelectionPrefabs();
    }

    private void InstanceRandomPossiblePlayerSelectionPrefabs()
    {
        for (int i = 0; i < maxPossibleSelections; i++)
        {
            if (possiblePlayerSelections.Count > 1)
            {
                int randomValue = Random.Range(0, possiblePlayerSelections.Count);
                GameObject selectedPrefab = possiblePlayerSelections[randomValue];
                SelectionCardController selectionCardController = selectedPrefab.GetComponent<SelectionCardController>();
                GameObject playerSelection = Instantiate(selectedPrefab);
                playerSelection.transform.SetParent(parentSelectionGroup, false);

                if (!selectionCardController.canAppearMultipleTimes)
                {
                    possiblePlayerSelections.RemoveAt(randomValue);
                }
            }
            else
            {
                GameObject playerSelection = Instantiate(possiblePlayerSelections[0]);
                playerSelection.transform.SetParent(parentSelectionGroup, false);
                possiblePlayerSelections.Remove(playerSelection);
            }
        }
    }

    public void ContinueNextRound()
    {
        waveManager.ContinueGame();
    }

    public void HealPlayer(int healQuantity, int money)
    {
        if (GameManager.isPlayer1Turn)
        {
            players[0].GetComponent<PlayerHealth>().RestoreHealth(healQuantity);
        }
        else
        {
            players[1].GetComponent<PlayerHealth>().RestoreHealth(healQuantity);
        }
        GameManager.Coins -= money;
    }

    public void BuyGun(string gunName, int money)
    {
        if (GameManager.isPlayer1Turn)
        {
            players[0].GetComponent<PlayerGunItemsController>().ActivateGun(gunName);
        }
        else
        {
            players[1].GetComponent<PlayerGunItemsController>().ActivateGun(gunName);
        }
        GameManager.Coins -= money;
    }

    public void ImproveGun(ArmaMejoras gunImproves, string gunName, int money)
    {
        if (GameManager.isPlayer1Turn)
        {
            players[0].GetComponent<PlayerGunItemsController>().UpgradeGun(gunName, gunImproves);
        }
        else
        {
            players[1].GetComponent<PlayerGunItemsController>().UpgradeGun(gunName, gunImproves);
        }
        GameManager.Coins -= money;
    }
}
