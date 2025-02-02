// ---------------------------------------------------------------------------------
// SCRIPT PARA LA GESTI�N DE AUDIO (vinculado a un GameObject vac�o "AudioManager")
// ---------------------------------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    // Instancia �nica del AudioManager (porque es una clase Singleton) STATIC
    public static AudioManager instance;

    // Se crean dos AudioSources diferentes para que se puedan
    // reproducir los efectos y la m�sica de fondo al mismo tiempo
    public AudioSource sfxSource; // Componente AudioSource para efectos de sonido
    public AudioSource musicSource; // Componente AudioSource para la m�sica de fondo
    private string currentSFX = ""; // Guarda el SFX actual


    // En vez de usar un vector de AudioClips (que podr�a ser) vamos a utilizar un Diccionario
    // en el que cargaremos directamente los recursos desde la jerarqu�a del proyecto
    // Cada entrada del diccionario tiene una string como clave y un AudioClip como valor
    public Dictionary<string, AudioClip> sfxClips = new Dictionary<string, AudioClip>();
    public Dictionary<string, AudioClip> musicClips = new Dictionary<string, AudioClip>();

    // M�todo Awake que se llama al inicio antes de que se active el objeto. �til para inicializar
    // variables u objetos que ser�n llamados por otros scripts (game managers, clases singleton, etc).
    private void Awake()
    {

        // ----------------------------------------------------------------
        // AQU� ES DONDE SE DEFINE EL COMPORTAMIENTO DE LA CLASE SINGLETON
        // Garantizamos que solo exista una instancia del AudioManager
        // Si no hay instancias previas se asigna la actual
        // Si hay instancias se destruye la nueva
        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }
        // ----------------------------------------------------------------

        // No destruimos el AudioManager aunque se cambie de escena
        DontDestroyOnLoad(gameObject);

        // Subscribe to sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Cargamos los AudioClips en los diccionarios
        LoadSFXClips();
        LoadMusicClips();

    }
    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event to avoid errors
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // M�todo privado para cargar los efectos de sonido directamente desde las carpetas
    private void LoadSFXClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI�N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/SFX
        sfxClips["Run"] = Resources.Load<AudioClip>("SFX/Running/Cartoon_Stomp");
        sfxClips["Shoot"] = Resources.Load<AudioClip>("SFX/Shooting/Cartoon_Shot");
        sfxClips["GrabItem"] = Resources.Load<AudioClip>("SFX/ItemGrab/Cartoon_Uncork");
        sfxClips["Drink"] = Resources.Load<AudioClip>("SFX/Drinks/Open_Can");
        sfxClips["Knife"] = Resources.Load<AudioClip>("SFX/Stab/Knife_Stab");
        sfxClips["EnemyStab"] = Resources.Load<AudioClip>("SFX/Stab/Axe_Hit");
    }

    // M�todo privado para cargar la m�sica de fondo directamente desde las carpetas
    private void LoadMusicClips()
    {
        // Los recursos (ASSETS) que se cargan en TIEMPO DE EJECUCI�N DEBEN ESTAR DENTRO de una carpeta denominada /Assets/Resources/Music
        musicClips["MainGame"] = Resources.Load<AudioClip>("Music/Musica_Gameplay");
        musicClips["MainMenu"] = Resources.Load<AudioClip>("Music/Musica_Menu");
        musicClips["Library"] = Resources.Load<AudioClip>("Music/Musica_Menu");
        musicClips["Vuforia"] = Resources.Load<AudioClip>("Music/Musica_Menu");
        musicClips["Credits"] = Resources.Load<AudioClip>("Music/Musica_Menu");
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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

    // M�todo de la clase singleton para reproducir efectos de sonido
    public void PlaySFX(string clipName)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            sfxSource.clip = sfxClips[clipName];
            sfxSource.Play();
            sfxSource.loop = false;  // Enable looping if needed

        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontr� en el diccionario de sfxClips.");
    }
    // M�todo de la clase singleton para reproducir efectos de sonido CON LOOP
    public void PlaySFXLoop(string clipName, bool loop = false)
    {
        if (sfxClips.ContainsKey(clipName))
        {
            sfxSource.clip = sfxClips[clipName];
            sfxSource.Play();
            sfxSource.loop = loop;  // Enable looping if needed
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontr� en el diccionario de sfxClips.");
    }
    // M�todo para detener todos los SFX
    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
            currentSFX = "";
        }
    }
    // M�todo para detener SFX espec�ficos
    public void StopSFX(string clipName)
    {
        if (currentSFX == clipName && sfxSource.isPlaying)
        {
            sfxSource.Stop();
            currentSFX = "";
        }
    }
    // M�todo de la clase singleton para reproducir m�sica de fondo
    public void PlayMusic(string clipName)
    {
        if (musicClips.ContainsKey(clipName))
        {
            musicSource.clip = musicClips[clipName];
            musicSource.Play();
        }
        else Debug.LogWarning("El AudioClip " + clipName + " no se encontr� en el diccionario de musicClips.");
    }

}