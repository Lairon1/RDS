using UnityEngine;
using System.Collections;


public class AutoThrottle : MonoBehaviour {

	//Attach this script you the car prefab to make the player car have auto throttle that is always accelerating
	//And just stops accelerating if the brakes are hitted

	public IRDSCarControllInput carInput;
	bool once = false;

	// Use this for initialization
	void Start () {
		carInput = GetComponent<IRDSCarControllInput>();
		if (!carInput.GetCarPilot())
			this.enabled = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (IRDSStatistics.GetCanRace()){
			if (carInput.GetThrottleInput() == 0 && carInput.GetBrakeInput() == 0 && carInput.Drivetrain.GetGear() == 0)
			{
				if (carInput.GetCarSpeed() <1)
					carInput.Drivetrain.gearWanted = 2;
				else carInput.setHandBrakeInput(1);
			}
			carInput.setThrottleInput(1-carInput.GetBrakeInput());
			if (!once){
				once = true;
				carInput.shiftUp();
			}
		}
		else carInput.setThrottleInput(1);
	}
}
