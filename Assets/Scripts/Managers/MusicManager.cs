using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource musicSource;

    public Dictionary<string, AudioClip> sceneMusic = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        musicSource = GetComponent<AudioSource>();
        LoadMusicTracks();

        SceneManager.sceneLoaded += OnSceneLoaded; // Listen for scene changes
    }

    private void LoadMusicTracks()
    {
        sceneMusic["MainMenu"] = Resources.Load<AudioClip>("Music/MainMenu");
        sceneMusic["Level1"] = Resources.Load<AudioClip>("Music/Level1");
        sceneMusic["Level2"] = Resources.Load<AudioClip>("Music/Level2");
        sceneMusic["DeathScreen"] = Resources.Load<AudioClip>("Music/DeathScreen");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneMusic.ContainsKey(scene.name))
        {
            PlayMusic(scene.name);
        }
    }

    public void PlayMusic(string sceneName)
    {
        if (sceneMusic.ContainsKey(sceneName) && musicSource.clip != sceneMusic[sceneName])
        {
            musicSource.clip = sceneMusic[sceneName];
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
