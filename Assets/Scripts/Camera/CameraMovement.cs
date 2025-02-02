using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private List<Transform> players = new List<Transform>();
    public Vector3 offset = new Vector3(0, 0, -10);
    public float followSpeed = 2f;
    public float zoomSpeed = 2;
    public float minSize = 8f;
    public float maxSize = 16f;
    public float zoomPadding = 2;
    public GameObject positionBetweenPlayers;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (GameManager.gamePaused) return;

        bool playersUpdated = false;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] == null)
            {
                playersUpdated = true;
                break;
            }
        }

        if (playersUpdated)
        {
            players.Clear();
            GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject player in playerObjects)
            {
                if (player != null) players.Add(player.transform);
            }
        }

        if (players.Count == 0) return;

        if (players.Count == 1)
        {
            if (players[0] != null)
            {
                Vector3 targetPosition = players[0].position + offset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 10f, Time.deltaTime * zoomSpeed);
            }
            positionBetweenPlayers.SetActive(false);
        }
        else if (players.Count == 2)
        {
            if (players[0] != null && players[1] != null)
            {
                transform.position = Vector3.Lerp(transform.position, positionBetweenPlayers.transform.position + offset, Time.deltaTime * followSpeed);

                float distance = Vector3.Distance(players[0].position, players[1].position);
                float targetSize = Mathf.Clamp(distance + zoomPadding, minSize, maxSize);
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            }
        }
    }

    public void AddPlayer()
    {
        players.Clear();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playerObjects)
        {
            if (player != null) players.Add(player.transform);
        }

        positionBetweenPlayers.SetActive(players.Count == 2);
    }
}
