using UnityEngine;
using System.Collections;



public class FinishRaceEventGlobal : MonoBehaviour {

	public IRDSCarControllerAI car;
		
	// Use this for initialization
	void Start () {
		
		//WE search all cars to check which one is the player and get it's reference
		foreach(IRDSCarControllerAI c in IRDSManager.Instance.OpponentsVar)
		{
			if (c.IsPlayer)
			{
				car = c;
			}
		}
		
		//Assign the delegate to the method OnRaceEnd (local method)
		car.NavigateTWaypoints.OnRaceEnded += OnRaceEnd;
	}

	void OnDestroy()
	{
        car.NavigateTWaypoints.OnRaceEnded -= OnRaceEnd;
	}
	
	//Method that would get called when the race ends
	void OnRaceEnd()
	{
		Debug.Log("FINISHED!");
		//Put your code here, it would be run when the race finishes
		
	}
}
