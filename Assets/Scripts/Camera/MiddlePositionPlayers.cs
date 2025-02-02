using UnityEngine;

public class MiddlePositionPlayers : MonoBehaviour
{
    private Transform[] players;

    void Awake()
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        players = new Transform[playerObjects.Length];

        for (int i = 0; i < playerObjects.Length; i++)
        {
            players[i] = playerObjects[i].transform;
        }
    }

    void Update()
    {
        if (players.Length == 0) return;

        Vector3 middlePoint = Vector3.zero;

        foreach (Transform player in players)
        {
            middlePoint += player.position;
        }

        middlePoint /= players.Length;

        transform.position = middlePoint;
    }
}
