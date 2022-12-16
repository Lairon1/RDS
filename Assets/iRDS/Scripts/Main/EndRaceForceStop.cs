/* Atthach this script to any game object on the scene if you want all the cars to get
 * full stop if the race end
*/
using UnityEngine;
using System.Collections;



public class EndRaceForceStop : MonoBehaviour {

	public float lapCountWidth = 20f;
	private IRDSStatistics statistics;
	private IRDSWPManager manager;

	private IRDSManager mainManager;
	// Use this for initialization
	void Start () {
		mainManager = GameObject.FindObjectOfType (typeof(IRDSManager)) as IRDSManager;
		mainManager.roadWidth = lapCountWidth;
		
		statistics = GameObject.FindObjectOfType(typeof(IRDSStatistics)) as IRDSStatistics;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach(IRDSCarControllerAI car in statistics.GetAllDrivers())
		{
			
		   if (car.GetEndRace() && car.enabled)
			{
				IRDSCarControllInput carInput= car.GetCarInputs();
				car.enabled = false;
				carInput.setThrottleInput(0);
				carInput.setBrakeInput(1);carInput.setHandBrakeInput(1);
			}
		}
	}
}
