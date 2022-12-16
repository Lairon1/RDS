using UnityEngine;
using System.Collections;
using UnityEngine.UI;


using IRDS.ChampionshipSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public bool dontDestroyOnLoad = false;
    public int defaultLaps = 10;
    public int teamSelectedByPlayer = 0;
    public string carChooseSceneName = "";
    public string mainMenuSceneName = "";
    public Slider fxVolSlider;
    public Slider musicVolSlider;

    private static MenuManager _instance;
    private float _FXVolume = 1f;
    private float _MusicVolume = 1f;
    /// <summary>
    /// The team selected by player.
    /// </summary>

    public delegate void OnAudioVolumeChanges(float vol);
    public static event OnAudioVolumeChanges onAudioVolumeChanges;
    public delegate void OnMusicVolumeChanges(float vol);
    public static event OnAudioVolumeChanges onMusicVolumeChanges;

    #region Properties Section

    public float FXVolume
    {
        get
        {
            return _FXVolume;
        }
        set
        {
            _FXVolume = value;
            if (onAudioVolumeChanges != null)
                onAudioVolumeChanges(_FXVolume);
        }
    }

    public float MusicVolume
    {
        get
        {
            return _MusicVolume;
        }
        set
        {
            _MusicVolume = value;
            if (onMusicVolumeChanges != null)
                onMusicVolumeChanges(_MusicVolume);
        }
    }

    public static MenuManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<MenuManager>();
            return _instance;
        }
    }

    #endregion

    // Use this for initialization
    void Awake()
    {
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        LoadSettings();
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void UpdateSlidersValues()
    {
        if (fxVolSlider != null)
        {
            fxVolSlider.value = _FXVolume;
        }

        if (musicVolSlider != null)
        {
            musicVolSlider.value = _MusicVolume;
        }
    }

    void Start()
    {

        if (onAudioVolumeChanges != null)
        {
            onAudioVolumeChanges(_FXVolume);
        }

        if (onMusicVolumeChanges != null)
        {
            onMusicVolumeChanges(_MusicVolume);
        }

        UpdateSlidersValues();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadSettings();
        if (onAudioVolumeChanges != null)
            onAudioVolumeChanges(_FXVolume);
        if (onMusicVolumeChanges != null)
            onMusicVolumeChanges(_MusicVolume);
        UpdateSlidersValues();
        ApplyVolumeToCars();
    }

    void ApplyVolumeToCars()
    {
        if (IRDSStatistics.Instance != null)
        {
            foreach (IRDSCarControllerAI car in IRDSStatistics.Instance.GetAllDriversList())
            {
                car.GetComponent<IRDSSoundController>().masterVolume = MenuManager.Instance.FXVolume;
            }
        }
    }


    public void SetTrack(string trackname)
    {
        ChampionShipSystem.currentChampionShip = null;
        IRDSLevelLoadVariables.Instance.trackToRace = trackname;
    }

    public void GoToCarSelectionScene()
    {
        SceneManager.LoadScene(carChooseSceneName);
    }

    public void GoToMainMenuScene()
    {

        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void LoadSelectedTrack()
    {
        SceneManager.LoadScene(IRDSLevelLoadVariables.Instance.trackToRace);
    }

    public void SetPlayerName(string playerName)
    {
        IRDSLevelLoadVariables.Instance.firstPlayerSettings.playerName = playerName;
    }
    public string GetCurrentPlayerName()
    {
        return IRDSLevelLoadVariables.Instance.firstPlayerSettings.playerName;
    }



    public void SetSelectedCar(string carName)
    {
        IRDSLevelLoadVariables.Instance.firstPlayerSettings.selectedCar = carName;
    }



    public void SaveSettings()
    {
        PlayerPrefs.SetInt("Control", IRDSLevelLoadVariables.Instance.firstPlayerSettings.controlAsigments.mobileControlSelected);
        PlayerPrefs.SetString("PlayerName", IRDSLevelLoadVariables.Instance.firstPlayerSettings.playerName);
        PlayerPrefs.SetString("DefaultCar", IRDSLevelLoadVariables.Instance.firstPlayerSettings.selectedCar);
        PlayerPrefs.SetFloat("FXVolume", FXVolume);
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume);

    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("Control"))
            IRDSLevelLoadVariables.Instance.firstPlayerSettings.controlAsigments.mobileControlSelected = PlayerPrefs.GetInt("Control");
        if (PlayerPrefs.HasKey("PlayerName"))
            IRDSLevelLoadVariables.Instance.firstPlayerSettings.playerName = PlayerPrefs.GetString("PlayerName");
        if (PlayerPrefs.HasKey("DefaultCar"))
            IRDSLevelLoadVariables.Instance.firstPlayerSettings.selectedCar = PlayerPrefs.GetString("DefaultCar");
        if (PlayerPrefs.HasKey("FXVolume"))
            FXVolume = PlayerPrefs.GetFloat("FXVolume");
        if (PlayerPrefs.HasKey("MusicVolume"))
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void SaveChampionshipPoints(string ChamionShipName)
    {

    }

    public void LoadChampionshipPoints(string championshipName)
    {

    }


    public void SetChampionShipToRace(int championship)
    {
        ChampionShipSystem.Instance.ApplyChampionShipToLevelLoad(IRDSLevelLoadVariables.Instance, ChampionShipSystem.Instance.championShipData[championship]);
    }



    public void SetPlayerTeam(int team)
    {

        ChampionShipSystem.Instance.playerSelectedTeam = teamSelectedByPlayer = team;
    }



}

