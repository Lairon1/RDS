using UnityEngine;
using IRDS.ChampionshipSystem;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour {

	IRDSCarCamera carCam;
    public string mainMenuScene;


	public void Start()
	{
		carCam = GameObject.FindObjectOfType<IRDSCarCamera>();
	}


	public void RestartRace()
	{
		if (IRDSPlaceCars.Instance != null)
			IRDSPlaceCars.Instance.RestartRaceAutoPositionCarsOnGrid ();

	}

	public void ReturnToMainMenu(){
        if (ChampionShipSystem.Instance != null)
        {
            ChampionShipSystem.Instance.LoadMainMenuScene();
        }else
        {
            SceneManager.LoadScene(mainMenuScene);
        }
        
	}

	public void NextRace()
	{
		ChampionShipSystem.Instance.LoadNextTrack();
	}

	public void CycleCameraCarView()
	{
		carCam.changeView ();
	}

	public void OnRaceStart()
	{

	}

}
