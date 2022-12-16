using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{

	public class IRDSUILapTime : MonoBehaviour {

		Text lapTimer;
		
		// Use this for initialization
		void Start () {
			lapTimer = GetComponent<Text>();
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
				lapTimer.text = IRDSStatistics.CurrentCar.GetCurrentLapTimeString();
			}
			else {
				lapTimer.text = "00.00.00";
			}
		}
	}
}