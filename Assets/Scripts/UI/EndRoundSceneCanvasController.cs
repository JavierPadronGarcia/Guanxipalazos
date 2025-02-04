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

        if (players.Length < 2)
        {
            UpdatePlayersAfterDeath();
        }

        if (players.Length == 0) return;

        foreach (var onePlayer in players)
        {
            PlayerMovement movementScript = onePlayer.GetComponent<PlayerMovement>();
            movementScript.StopRunning();
        }

        GameObject player = null;
        if (GameManager.isPlayer1Turn && players.Length > 0)
        {
            player = players[0];
        }
        else if (!GameManager.isPlayer1Turn && players.Length > 1)
        {
            player = players[1];
        }

        if (player != null)
        {
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
        if (players.Length > 0)
        {
            GameObject currentPlayer = GameManager.isPlayer1Turn ? players[0] : players[1];
            currentPlayer.GetComponent<PlayerHealth>().RestoreHealth(healQuantity);
        }
        GameManager.Coins -= money;
    }

    public void BuyGun(string gunName, int money)
    {

        if (players.Length > 0)
        {
            GameObject currentPlayer = GameManager.isPlayer1Turn ? players[0] : players[1];
            currentPlayer.GetComponent<PlayerGunItemsController>().ActivateGun(gunName);
        }
        GameManager.Coins -= money;
    }

    public void ImproveGun(ArmaMejoras gunImproves, string gunName, int money)
    {

        if (players.Length > 0)
        {
            GameObject currentPlayer = GameManager.isPlayer1Turn ? players[0] : players[1];
            currentPlayer.GetComponent<PlayerGunItemsController>().UpgradeGun(gunName, gunImproves);
        }
        GameManager.Coins -= money;
    }

    private void UpdatePlayersAfterDeath()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length == 1)
        {
            GameManager.isPlayer1Turn = true;
        }
    }
}
