using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Menu menu;
    public CharacterManager charManager;
    public UpdatePanel updatePanel;

    public GameObject PauseMenu;
    
    public static Game Instance;

    public void TogglePauseMenu()
    {
        PauseMenu.SetActive(PauseMenu.activeSelf == false);
        Time.timeScale = this.PauseMenu.activeSelf ? 0 : 1;
    }
    
    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (Time.timeScale == 0 || NPCPlayerData.DevianceLevel == 0 || this.PauseMenu.activeSelf)
        {
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = 5;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Time.timeScale = 1;
        }
    }

    private void Start()
    {
        MusicPlayer.PlayMusic("HappyVillageLoop", UnityEngine.Random.Range(1f,2.5f));
    }
}
