using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{
	public class IRDSUITotalTime : MonoBehaviour {

		Text raceTimer;
		// Use this for initialization
		void Start () {
			raceTimer = GetComponent<Text>();
		}
		
		/// <summary>
		/// Update this instance.
		/// </summary>
		void Update()
		{
			UpdateRaceTimer();
		}
		
		/// <summary>
		/// Updates the race timer.
		/// </summary>
		void UpdateRaceTimer()
		{
			if (IRDSStatistics.canRace)
			{
				raceTimer.text = IRDSStatistics.CurrentCar.GetCurrentTotalRaceTimeString();
			}
			else {
				raceTimer.text = "00.00.00";
			}
		}
	}
}