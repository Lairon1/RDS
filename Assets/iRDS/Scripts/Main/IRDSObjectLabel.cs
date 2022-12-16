using UnityEngine;
using System.Collections;

 

[RequireComponent (typeof (TextMesh))]
[RequireComponent (typeof (MeshRenderer))]
public class IRDSObjectLabel : MonoBehaviour
{
public bool useMainCamera = true;   // Use the camera tagged MainCamera
public Camera cameraToUse ;   // Only use this if useMainCamera is false
public bool addCarPos = false;
public bool enableForPlayerCar = false;
	
Camera cam ;
Transform thisTransform;
Transform camTransform;
	TextMesh text;
	IRDSCarControllerAI AI;
	int lastRacePosition = 0;
 
	void Start () 
    {

		text = GetComponent<TextMesh>();
		AI = transform.root.GetComponent<IRDSCarControllerAI>();
	    thisTransform = transform;
		if (useMainCamera)
        	cam = Camera.main;
    	else
        	cam = cameraToUse;
    	camTransform = cam.transform;
		if (AI != null)
			text.text = AI.GetDriverName();
		if (!enableForPlayerCar && IRDSStatistics.GetCurrentCar() && IRDSStatistics.GetCurrentCar() == AI){
			GetComponent<Renderer>().enabled = false;
		}
	}
 
 
    void Update()
    {
		if (AI != null && !enableForPlayerCar && this.enabled == true && IRDSStatistics.GetCurrentCar() && IRDSStatistics.GetCurrentCar() == AI)
		{
			GetComponent<Renderer>().enabled = false;
		}else if (GetComponent<Renderer>().enabled == false) GetComponent<Renderer>().enabled = true;
 
		if (addCarPos){
			if (lastRacePosition != AI.racePosition)
				text.text = AI.racePosition + " " + AI.GetDriverName();
			lastRacePosition = AI.racePosition;
		}
        thisTransform.rotation = camTransform.rotation;
    }
}