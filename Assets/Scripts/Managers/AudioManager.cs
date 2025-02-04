using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;

    public AudioSource[] sfxSources;
    private int maxSfxSources = 3;

    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        sfxSources = new AudioSource[maxSfxSources];
        for (int i = 0; i < sfxSources.Length; i++)
        {
            sfxSources[i] = gameObject.AddComponent<AudioSource>();
            sfxSources[i].volume = 0.151f;
        }

        LoadSFXClips();
        LoadMusicClips();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void LoadSFXClips()
    {
        sfxClips["Run"] = Resources.Load<AudioClip>("SFX/Running/Cartoon_Stomp");
        sfxClips["Shoot"] = Resources.Load<AudioClip>("SFX/Shooting/Cartoon_Shot");
        sfxClips["GrabItem"] = Resources.Load<AudioClip>("SFX/ItemGrab/Cartoon_Uncork");
        sfxClips["Drink"] = Resources.Load<AudioClip>("SFX/Drinks/soda-can-open-183214");
        sfxClips["Knife"] = Resources.Load<AudioClip>("SFX/Stab/Knife_Stab");
        sfxClips["EnemyStab"] = Resources.Load<AudioClip>("SFX/Stab/Axe_Hit");
        sfxClips["LanceThrow"] = Resources.Load<AudioClip>("SFX/Throw/Throw_Lance");
        sfxClips["RockThrow"] = Resources.Load<AudioClip>("SFX/Throw/Rock_Throw");
    }

    private void LoadMusicClips()
    {
        musicClips["MainGame"] = Resources.Load<AudioClip>("Music/Musica_Gameplay");
        musicClips["MainMenu"] = Resources.Load<AudioClip>("Music/Musica_Menu");
        musicClips["Library"] = Resources.Load<AudioClip>("Music/Musica_Menu");
        musicClips["Vuforia"] = Resources.Load<AudioClip>("Music/Musica_Menu");
        musicClips["Credits"] = Resources.Load<AudioClip>("Music/Musica_Menu");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllSFX();
        PlayMusicForScene(scene.name);
    }

    public void PlayMusicForScene(string sceneName)
    {
        if (musicClips.ContainsKey(sceneName) && musicSource.clip != musicClips[sceneName])
        {
            musicSource.clip = musicClips[sceneName];
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(string clipName)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            AudioSource availableSource = GetAvailableSfxSource();
            if (availableSource != null)
            {
                availableSource.clip = sfxClips[clipName];
                availableSource.Play();
            }
            else
            {
                Debug.LogWarning("No hay fuentes de SFX disponibles para " + clipName);
            }
        }
    }

    private AudioSource GetAvailableSfxSource()
    {
        foreach (AudioSource source in sfxSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    public void StopSFX(string clipName)
    {
        foreach (AudioSource source in sfxSources)
        {
            if (source.isPlaying && source.clip != null && source.clip.name == clipName)
            {
                source.Stop();
                break;
            }
        }
    }

    public void StopAllSFX()
    {
        foreach (AudioSource source in sfxSources)
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }
}
