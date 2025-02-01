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
        if (players.Count == 0) return;

        if (players.Count == 1)
        {
            Vector3 targetPosition = players[0].transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, positionBetweenPlayers.transform.position + offset, Time.deltaTime * followSpeed);

            float distance = Vector3.Distance(players[0].position, players[1].position);
            float targetSize = Mathf.Clamp(distance + zoomPadding, minSize, maxSize);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
        }
    }

    public void AddPlayer()
    {
        if (GameManager.playerCount == 1)
        {
            Transform player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();
            players.Add(player);
        }
        else if (GameManager.playerCount == 2)
        {
            Transform player = GameObject.FindGameObjectsWithTag("Player")[1].GetComponent<Transform>();
            players.Add(player);
            positionBetweenPlayers.SetActive(true);
        }
    }
}
