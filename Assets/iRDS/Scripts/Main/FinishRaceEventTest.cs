using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FinishRaceEventTest : MonoBehaviour {
	
	
	public IRDSCarControllerAI car;
	
	
	
	// Use this for initialization
	void Start () {
		
		car = GetComponent<IRDSCarControllerAI>();
        car.NavigateTWaypoints.OnRaceEnded += OnRaceEnd;
	}

	void OnDestroy(){
        car.NavigateTWaypoints.OnRaceEnded -= OnRaceEnd;
	}

	void OnRaceEnd()
	{
		Debug.Log("FINISHED!");
	}
}
