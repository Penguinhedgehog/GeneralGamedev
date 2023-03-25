using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{

    
    [Header("Master Volume Settings")]
    [SerializeField] Slider masterVolumeSlider;

    [Header("Sound Settings")]
    [SerializeField] AudioClip gameOver;
    [SerializeField] Slider soundEffectsSlider;
    public float soundEffectsVolume = 1;
    [SerializeField] AudioClip worldButtonPress;
    [SerializeField] AudioClip worldLeverSwitch;
    [SerializeField] AudioClip sewerAmbiance;

    [Header("Music")]
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] AudioClip currentAreaMusic;
    [SerializeField] AudioSource pauseMenuMusic;
    [SerializeField] float transitionDuration = 1f;
    [SerializeField] List<AudioClip> musicTracks = new List<AudioClip>();
    public float musicVolume = 1;


    AudioSource musicSource;

    static AudioPlayer instance;

    //Create a method to set the player sound based on a slider

    private void Awake() {
        ManageSingleton();
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = currentAreaMusic;
        musicSource.Play();
        masterVolumeSlider.onValueChanged.AddListener(delegate {VolumeUpdate();});
        musicVolumeSlider.onValueChanged.AddListener(delegate {VolumeUpdate();});
        soundEffectsSlider.onValueChanged.AddListener(delegate {VolumeUpdate();});

    }

    private void ManageSingleton() {
        if(instance != null) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void VolumeUpdate() {
        musicVolume = musicVolumeSlider.value * masterVolumeSlider.value;
        soundEffectsVolume = soundEffectsSlider.value * masterVolumeSlider.value;
        musicSource.volume = musicVolume;
    }

    //Set this to bring in another audio player when menu pauses?
    public void PauseMusic() {
        StartCoroutine(TransitionMusic(musicSource, pauseMenuMusic));
    }

    public void PlayMusic() {
        StartCoroutine(TransitionMusic(pauseMenuMusic, musicSource));
    }


    private IEnumerator TransitionMusic(AudioSource fadeOut, AudioSource fadeIn) {
        float currentTime = 0;
        float start = fadeOut.volume;
        float startIn = fadeIn.volume;
        fadeIn.Play();

        while(currentTime < transitionDuration) {
            currentTime += Time.unscaledDeltaTime;
            fadeOut.volume = Mathf.Lerp(start, 0, currentTime/ transitionDuration);
            fadeIn.volume = Mathf.Lerp(startIn, 1, currentTime/transitionDuration);
            yield return null;
        }

        fadeOut.Pause();

        yield break;
    }

    public IEnumerator FadeOutMusic() {
        float currentTime = 0;
        float start = musicSource.volume;

        while(currentTime < transitionDuration) {
            currentTime += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(start, 0, currentTime/ .5f);
            yield return null;
        }

        musicSource.Stop();

        yield break;
    }

    public void PlayerDeathSound() {
        //StartCoroutine(FadeOutMusic());
        PlayAudioClip(gameOver, musicVolume);
    }



    public void PlayCustomAudioClip(AudioClip clip, Vector3 objectPosition) {
        if(clip != null) {
            AudioSource.PlayClipAtPoint(clip, objectPosition, soundEffectsVolume);
        }
    }

    public void PlayCustomAudioClip(AudioClip clip, Vector3 objectPosition, float sound) {
        if(clip != null) {
            AudioSource.PlayClipAtPoint(clip, objectPosition, sound * soundEffectsVolume); //Rework this bit later
        }
    }

    public void PlayAudioClip(AudioClip clip) {
        if(clip != null) {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, soundEffectsVolume);
        }
    }


    public void PlayAudioClip(AudioClip clip, float volume) {
        if(clip != null) {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }


    public void PlayMusicTrack(int trackNumber) {
        if(musicSource.clip == musicTracks[trackNumber]) {return;}
        musicSource.clip = musicTracks[trackNumber];
        musicSource.Play();
    }

    public void DisableAudioLoop() {
        musicSource.loop = false;
    }

    public void EnableAudioLoop() {
        musicSource.loop = true;
    }



    #region ButtonSounds

    public void WorldButtonPress() {
        PlayAudioClip(worldButtonPress);
    }

    public void WorldLeverSwitch() {
        PlayAudioClip(worldLeverSwitch);
    }

    public void StartSewerAmbiance() {
        PlayAudioClip(sewerAmbiance);
    }



    #endregion



}
