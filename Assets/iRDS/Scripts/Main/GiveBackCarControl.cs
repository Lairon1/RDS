using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


[RequireComponent(typeof(IRDSCarControllerAI))]
public class GiveBackCarControl : MonoBehaviour
{

	IRDSCarControllerAI AI;
	bool isAutomatic = false;
    void Awake()
    {
		AI = GetComponent<IRDSCarControllerAI>();
		AI.NavigateTWaypoints.OnRaceEnded += OnRaceEnd;
    }

	void Start()
	{
		if (AI.GetCarInputs().Drivetrain.GetAutomatic())
			isAutomatic = true;
	}

    void OnDestroy()
    {
        AI.NavigateTWaypoints.OnRaceEnded -= OnRaceEnd;
    }

    void OnRaceEnd()
    {
		if (AI.IsPlayer){
			AI.GetCarInputs().SetCarPilot(true);
			if (isAutomatic)
				AI.GetCarInputs().Drivetrain.SetAutomatic(true);
			Debug.Log("OnRaceEnd, giving control back to player");
		}
    }
}
