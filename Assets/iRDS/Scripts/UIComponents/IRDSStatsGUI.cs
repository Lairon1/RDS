using UnityEngine;

[DisallowMultipleComponent]
public class IRDSStatsGUI : MonoBehaviour {

	/// <summary>
	/// The standings user interface animator.  This is the Animator component that contains all the animation for the UI
	/// of the standings, like the Show/Hide animation, scaling the lap/pos when it changes, or changing its color, etc.
	/// </summary>
	public Animator UIAnimator;

	/// <summary>
	/// The main user interface canvas.
	/// </summary>
	public Canvas mainUICanvas;

	/// <summary>
	/// The show user interface animation.
	/// </summary>
	public string showUIAnimation = "Shown";

	/// <summary>
	/// The hide user interface animation.
	/// </summary>
	public string hideUIAnimation = "Hiden";

	/// <summary>
	/// The race ended animation.  This could be used to trigger your end race animation, like i.e. to show the final standinfs
	/// or show a trophy and then the final standings or stuff like that.
	/// </summary>
	public string raceEndedAnimation = "endRace";

	/// <summary>
	/// The race not ended animation.  This is with the idea of triggering an animation that puts the HUD back if the user changes
	/// the actual car that is followed by the camera and in case that car was already ended the race and this new car has not
	/// ended the race, this animation would be played to get the HUD of normal gameplay.
	/// </summary>
	public string raceNotEndedAnimation = "notEndRace";
	 

	void Start()
	{
		InitializeUI();
	}

	void OnEnable()
	{
		GetDelegatesReferences();
	}

	void OnDisable()
	{
		RemoveDelegatesReferences();
	}

	void GetDelegatesReferences()
	{
        if (IRDSStatistics.Instance != null)
        {
            IRDSStatistics.Instance.onStartingInitialCount += OnRaceInitialCounting;
            IRDSStatistics.onRaceRestarted += InitializeUI;
        }
	}
	void RemoveDelegatesReferences()
	{
        if (IRDSStatistics.Instance == null) { return; }
		IRDSStatistics.Instance.onStartingInitialCount -= OnRaceInitialCounting;
        IRDSStatistics.onRaceRestarted -= InitializeUI;
    }

	/// <summary>
	/// Initializes the UI.
	/// </summary>
	void InitializeUI()
	{
        if (mainUICanvas != null)
        {
            mainUICanvas.enabled = false;
        }
        

	}

	/// <summary>
	/// Raises the race initial counting event.
	/// </summary>
	void OnRaceInitialCounting()
    {
		mainUICanvas.enabled = true;
        if (UIAnimator != null)
        {
            UIAnimator.Play(showUIAnimation);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
	}

}
