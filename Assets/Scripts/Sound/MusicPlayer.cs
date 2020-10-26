using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static SoundData;

public class MusicPlayer : MonoBehaviour
{
    public AudioMixer GameMixer;
    public static MusicPlayer Instance;

    // AUDIOMIXER - GROUPS
    public AudioMixerGroup MainMusicMixerGroup;
    public AudioMixerGroup ScenarioMusicMixerGroup;
    public AudioMixerGroup TransitionMixerGroup;

    // AUDIOMIXER - SNAPSHOTS
    public AudioMixerSnapshot MainMusicSnapshot;
    public AudioMixerSnapshot FadeReadyMainMusicSnapshot; //workaround to annoying unity audiomixer stuff
    public AudioMixerSnapshot ScenarioMusicSnapshot;
    public AudioMixerSnapshot FadeReadyScenarioMusicSnapshot; //workaround to annoying unity audiomixer stuff
    public AudioMixerSnapshot MutedMusicSnapshot;

    // AUDIO SOURCES
    // Added and configured by script
    private AudioSource MainMusicSource;
    private AudioSource ScenarioMusicSource;
    private AudioSource TransitionSource;

    // MUSIC LISTS
    private static List<SoundData> MainMusicList;
    private static List<SoundData> ScenarioMusicList;
    private static List<SoundData> TransitionSoundList;

    public float MasterVolume { get; set; } //TODO: MOVE ELSEWHERE

    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;

        // Setup Sources
        MainMusicSource = this.gameObject.AddComponent<AudioSource>();
        MainMusicSource.loop = true;
        MainMusicSource.outputAudioMixerGroup = MainMusicMixerGroup;
        MainMusicSource.playOnAwake = false;

        ScenarioMusicSource = this.gameObject.AddComponent<AudioSource>();
        ScenarioMusicSource.loop = true;
        ScenarioMusicSource.outputAudioMixerGroup = ScenarioMusicMixerGroup;
        ScenarioMusicSource.playOnAwake = false;

        TransitionSource = this.gameObject.AddComponent<AudioSource>();
        TransitionSource.loop = false;
        TransitionSource.outputAudioMixerGroup = TransitionMixerGroup;
        TransitionSource.playOnAwake = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic("MainMenu");
        FadeReadyMainMusicSnapshot.TransitionTo(0f);
        MainMusicSnapshot.TransitionTo(Random.Range(2f, 4f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayMusic(string musicName, float transitionTime = -1f) => PlayMusic(GetMusicByName(musicName));
    public static void PlayMusic(SoundData music, float transitionTime = -1f)
    {
        Instance.doPlayMusic(music, transitionTime);
    }

    /// <summary>
    /// Plays a music based on the music file name. If any music is playing at the moment, it will transition (cross-fade) to the chosen song.
    /// Optional: Cross-fade transition time (in seconds)
    /// </summary>
    private void doPlayMusic(string musicName, float transitionTime = -1f) => PlayMusic(GetMusicByName(musicName));
    private void doPlayMusic(SoundData music, float transitionTime = -1f)
    {
        // TODO: ADD TRANSITION SOUNDS BETWEEN MUSIC
        // TODO: DECIDE IF FADED-OUT MUSIC WILL PAUSE, STOP OR CONTINUE PLAYING WHILE MUTED (RADIO EFFECT)

        AudioSource musicSource;
        if (transitionTime == -1f)
            transitionTime = Random.Range(2f, 4f);

        // In case of delayed play
        bool delayedPlay = false;
        AudioMixerSnapshot fadeReadySnapshot = null;
        AudioMixerSnapshot actualSnapshot = null;

        if (music.SoundCategory == SoundType.MUSIC_MAIN)
            musicSource = MainMusicSource;
        else
            musicSource = ScenarioMusicSource;

        if (MainMusicSource.isPlaying)
        {
            if (music.SoundCategory == SoundType.MUSIC_SCENARIO)
            {
                ScenarioMusicSnapshot.TransitionTo(transitionTime);
            }
            else
            {
                delayedPlay = true; // wait for a fade if the same audioSource is already playing
                fadeReadySnapshot = FadeReadyMainMusicSnapshot;
                actualSnapshot = MainMusicSnapshot;
            }
        }
        else if (ScenarioMusicSource.isPlaying)
        {
            if (music.SoundCategory == SoundType.MUSIC_MAIN)
            {
                MainMusicSnapshot.TransitionTo(transitionTime);
            }
            else
            {
                fadeReadySnapshot = FadeReadyScenarioMusicSnapshot;
                actualSnapshot = ScenarioMusicSnapshot;
                delayedPlay = true;
            }
        }

        if (delayedPlay)
        {
            if (musicSource.clip.name == music.name)
            {
                Debug.Log("Requested delayed play of the same music. Isn't that weird?");
                return;
            }

            SetupAudioSource(musicSource, music);
            float fadeOutTime = Random.Range(0.3f, 1.2f);
            fadeReadySnapshot.TransitionTo(fadeOutTime);
            StartCoroutine(PlayMusicDelayed(musicSource, fadeOutTime, actualSnapshot, transitionTime));
        }
        else
        {
            SetupAudioSource(musicSource, music);
            musicSource.Play();
        }
    }

    private IEnumerator PlayMusicDelayed(AudioSource source, float delay, AudioMixerSnapshot snapshot, float transitionTime)
    {
        yield return new WaitForSeconds(delay);
        snapshot.TransitionTo(transitionTime);
        source.Play();
    }

    /// <summary>
    /// Switches music back to the main village music
    /// </summary>
    public static void TransitionToMainMusic(float transitionTime = -1f)
    {
        if (transitionTime == -1f)
        {
            transitionTime = Random.Range(0.5f, 1.5f);
        }
        Instance.MainMusicSnapshot.TransitionTo(transitionTime);
    }

    /// <summary>
    /// Switches music to scenario music
    /// </summary>
    public static void TransitionToScenarioMusic(float transitionTime = -1f)
    {
        if (transitionTime == -1f)
        {
            transitionTime = Random.Range(0.5f, 1.5f);
        }
        Instance.ScenarioMusicSnapshot.TransitionTo(transitionTime);
    }

    // QUERIES
    public static SoundData GetMusicByName(string name)
    {
        SoundData music = null;
        foreach (SoundData data in MainMusicList)
        {
            if (data.name == name)
            {
                music = data;
                break;
            }
        }

        foreach (SoundData data in ScenarioMusicList)
        {
            if (data.name == name)
            {
                music = data;
                break;
            }
        }

        if (music == null)
            Debug.LogError("MusicPlayer - Music not found! " + "(" + name + ")");

        return music;
    }

    //TODO: MOVE ELSEWHERE
    public void VolumeChange()
    {
        GameMixer.SetFloat("MasterVolume",Mathf.Log10(MasterVolume) * 20);
    }

    // LOADING
    public static void SetMainMusicList(List<SoundData> list)
    {
        MainMusicList = list;
    }

    public static void SetScenarioMusicList(List<SoundData> list)
    {
        ScenarioMusicList = list;
    }
}
