using UnityEngine;

public class EndRoundSceneCanvasController : MonoBehaviour
{
    private WaveManager waveManager;

    private void Awake()
    {
        waveManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<WaveManager>();
    }

    public void ContinueNextRound()
    {
        waveManager.ContinueGame();
    }
}
