using UnityEngine;
using System.Collections;



public class InitialRace : MonoBehaviour {
	
	IRDSCarCamera carCamera;
	bool addedComp = false;
	bool activeRoadCamera = false;
	// Use this for initialization
	void Start () {
		carCamera = GetComponent<IRDSCarCamera>();
	}
	
	// Update is called once per frame
	void OnGUI () {
		if (GUI.Button(new Rect(25, 50, 50, 45), "Cam")){
			if (activeRoadCamera){carCamera.DeactivateRoadCamera();activeRoadCamera = false;}
			else {carCamera.ActivateRoadCamera();activeRoadCamera = true;}
		}
		if (GUI.Button(new Rect(25, 100, 50, 45), "View")){
			carCamera.changeView();
		}
		if (GUI.Button(new Rect(25, 150, 50, 45), "Car")){
			carCamera.changeTarget();
		}
				
		if (IRDSCarCamera.InitialView== false)
		{
			if (!addedComp && IRDSStatistics.GetCurrentCar() )
			{
				addedComp = true;
				IRDSStatistics.GetCurrentCar().gameObject.AddComponent<AndroidPlayerControl>(); 
			}
		}

	}
}
