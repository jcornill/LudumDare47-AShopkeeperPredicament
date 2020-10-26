using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SoundData;

/// <summary>
/// Singleton. Provides game effect, interface and transition sounds
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // AUDIOMIXER - GROUPS
    public AudioMixerGroup GameSFXMixerGroup;
    public AudioMixerGroup InterfaceMixerGroup;

    // AUDIO SOURCES
    private AudioSource GameSFXSource;
    private AudioSource InterfaceSource;

    // SOUND LISTS
    private static List<SoundData> SoundList;

    // GAMEOBJECT SETUP
    private void Awake()
    {
        if (Instance != null)
            return;

        Instance = this;

        //Load up Sounds
        LoadAllSounds();

        // Setup Sources
        GameSFXSource = this.gameObject.AddComponent<AudioSource>();
        GameSFXSource.loop = false;
        GameSFXSource.outputAudioMixerGroup = GameSFXMixerGroup;
        GameSFXSource.playOnAwake = false;
        GameSFXSource.spatialBlend = 1;
        GameSFXSource.rolloffMode = AudioRolloffMode.Linear;
        GameSFXSource.dopplerLevel = 0;

        InterfaceSource = this.gameObject.AddComponent<AudioSource>();
        InterfaceSource.loop = false;
        InterfaceSource.outputAudioMixerGroup = InterfaceMixerGroup;
        InterfaceSource.playOnAwake = false;
    }

    private void Start()
    {
        InitListeners();
    }

    // Add interface listeners
    private void InitListeners()
    {
        EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
        hoverEntry.eventID = EventTriggerType.PointerEnter;
        hoverEntry.callback.AddListener((data) => { PlayInterfaceSound("MenuCursor"); });

        foreach (GameObject gameObject in FindObjectsOfType<Canvas>().Select(x => x.gameObject))
        {
            foreach (Button button in gameObject.GetComponentsInChildren<Button>(true))
            {
                string soundName = "Select";
                if (button.tag == "ButtonStart")
                    soundName = "Start";
                else if (button.tag == "ButtonBack")
                    soundName = "Back";

                button.onClick.AddListener(() => { PlayInterfaceSound(soundName); });
                EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
                trigger.triggers.Add(hoverEntry);
            }

            foreach (Toggle toggle in gameObject.GetComponentsInChildren<Toggle>(true))
            {
                toggle.onValueChanged.AddListener(x =>
                {
                    if (toggle.isOn)
                        PlayInterfaceSound("ToggleOn");
                    else
                        PlayInterfaceSound("ToggleOff");
                });

                EventTrigger trigger = toggle.gameObject.AddComponent<EventTrigger>();
                trigger.triggers.Add(hoverEntry);
            }
        }

        /*foreach (Slider slider in gameObject.GetComponentsInChildren<Slider>(true))
        {
            slider.gameObject.GetComponent<SliderEndEditEvent>().OnEndEdit += () =>
            {
                PlayInterfaceSound("Click");
            };
        }*/

        /*foreach (Dropdown dropdown in gameObject.GetComponentsInChildren<Dropdown>(true))
        {
            dropdown.onValueChanged.AddListener(x =>
            {
                PlayInterfaceSound("Click");
            });
            dropdown.gameObject.GetOrAddComponent<HoverEvent>().OnHover += () =>
            {
                PlayInterfaceSound("Hover");
            };
        }

        foreach (TMP_Dropdown dropdown in gameObject.GetComponentsInChildren<TMP_Dropdown>(true))
        {
            dropdown.onValueChanged.AddListener(x =>
            {
                PlayInterfaceSound("Click");
            });
            dropdown.gameObject.GetOrAddComponent<HoverEvent>().OnHover += () =>
            {
                PlayInterfaceSound("Hover");
            };
        }*/
    }

    // QUERIES
    public static AudioClip GetClipByName(string name)
    {
        return GetSoundByName(name).clip;
    }

    public static SoundData GetSoundByName(string name)
    {
        SoundData sound = null;
        foreach (SoundData s in SoundList)
        {
            if (s.name == name)
            {
                sound = s;
                break;
            }
        }

        if (sound == null)
            Debug.LogError("SoundManager - Sound not found! " + "(" + name + ")");

        return sound;
    }

    // PLAYING
    public static void PlayInterfaceSound(string soundName, float pitch = -1f)
    {
        SoundData sound = GetSoundByName(soundName);
        if (sound == null)
            return;
        SoundData.SetupAudioSource(Instance.InterfaceSource, sound);
        if (pitch > 0f)
            Instance.InterfaceSource.pitch = pitch;
        Instance.InterfaceSource.PlayOneShot(sound.clip);
    }

    // LOADING
    public static void LoadAllSounds()
    {
        List<SoundData> MainMusicSounds = new List<SoundData>();
        List<SoundData> ScenarioMusicSounds = new List<SoundData>();
        List<SoundData> GameSounds = new List<SoundData>();

        GameObject.Find("GameData").GetComponent<GameDataHolder>().Awake();
        foreach (SoundData data in GameDataHolder.data.sounds)
        {
            if (data.SoundCategory == SoundType.GAME_SFX || data.SoundCategory == SoundType.INTERFACE)
                GameSounds.Add(data);
            else if (data.SoundCategory == SoundType.MUSIC_MAIN) 
                MainMusicSounds.Add(data);
            else if (data.SoundCategory == SoundType.MUSIC_SCENARIO) 
                ScenarioMusicSounds.Add(data);
            //else if (data.SoundCategory == SoundData.SoundType.AMBIENCE) 
                //ScenarioMusicSounds.Add(data); TODO
        }

        SoundList = GameSounds;
        MusicPlayer.SetMainMusicList(MainMusicSounds);
        MusicPlayer.SetScenarioMusicList(ScenarioMusicSounds);
    }

}
