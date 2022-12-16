using UnityEngine;
using System.Collections;


public class KeyPressFlipCurrentCar : MonoBehaviour {

	public KeyCode key = KeyCode.R;
	
	// Update is called once per frame
	bool fliped = true;
	void Update () {
		if (Input.GetKey(key))
		{
			if (IRDSStatistics.GetCurrentCar()!=null)
			{
				
				fliped = IRDSStatistics.GetCurrentCar().FlipCar();
			}
		}
		if (!fliped)
		{
			if (IRDSStatistics.GetCurrentCar()!=null)
			{
				fliped = IRDSStatistics.GetCurrentCar().FlipCar();
			}
		}
		
	}
}
