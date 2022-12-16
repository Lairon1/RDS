using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuUIMAnager : MonoBehaviour {

	public Slider FXVolSlider;
	public Slider MusicVolSlider;

	public float FXVolume
	{
		set{

			MenuManager.Instance.FXVolume = value;
		}
	}
	public float MusicVolume
	{
		set{

			MenuManager.Instance.MusicVolume = value;
		}
	}
	void Awake()
	{
		MenuManager.Instance.fxVolSlider = FXVolSlider;
		MenuManager.Instance.musicVolSlider = MusicVolSlider;
		UpdateSlidersValues();
	}

	public void UpdateSlidersValues()
	{
		MenuManager.Instance.UpdateSlidersValues();
	}

	public void SaveSettings()
	{
		MenuManager.Instance.SaveSettings();
	}

	public void SetTrack(string trackname) 
	{
		MenuManager.Instance.SetTrack(trackname);
	}
	
	public void GoToCarSelectionScene()
	{
		MenuManager.Instance.GoToCarSelectionScene();
	}


	
	public void SetPlayerName(string playerName) 
	{
		MenuManager.Instance.SetPlayerName(playerName);
	}
	public string GetCurrentPlayerName() 
	{
		return MenuManager.Instance.GetCurrentPlayerName();
	}

//	public void SetControlInput(int selected)
//	{
//		MenuManager.Instance.SetControlInput(selected);
//	}

	public void SetChampionShipToRace(int championship)
	{
		MenuManager.Instance.SetChampionShipToRace(championship);
	}

	public void SetPlayerTeam(int team)
	{
		MenuManager.Instance.SetPlayerTeam(team);
	}

    public void SetLaps(int laps)
    {

    }


	public void Quit()
	{
		Application.Quit();
	}

	public void GoToMainMenuScene()
	{
		MenuManager.Instance.GoToMainMenuScene();
	}

}
