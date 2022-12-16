using UnityEngine;
using System.Collections;


public class IRDSCarDebug : MonoBehaviour {


	IRDSCarControllInput carInput;

	// Use this for initialization
	void Start () {
		if (IRDSStatistics.Instance!= null) 
			IRDSStatistics.Instance.onCurrentCarChanged +=OnCarChanged;
		carInput = GetComponent<IRDSCarControllInput>();
	}

	void OnCarChanged()
	{
		carInput = IRDSStatistics.CurrentCar.GetComponent<IRDSCarControllInput>();
	}
	Rect windowsRect = new Rect(150,150,700,200);
	float calculatedMass =0f;
	void FixedUpdate()
	{
		calculatedMass =0f;
		for (int i =0; i < carInput.wheels.Length;i++)
		{
			calculatedMass +=carInput.wheels[i].GetNormalForce();
		}
	}

	void OnGUI()
	{
		windowsRect = GUILayout.Window(0,windowsRect,x=>{
			float calculatedMass =0f;
			for (int i =0; i < carInput.wheels.Length;i++)
			{
				calculatedMass +=carInput.wheels[i].GetNormalForce();
				GUILayout.BeginHorizontal();
				GUILayout.Label("Pos->" + carInput.wheels[i].wheelPosition, GUILayout.Width(100));
				GUILayout.Space(5);
				GUILayout.Label("SR->" + (carInput.wheels[i].slipRatio).ToString("F2"), GUILayout.Width(100));
//				GUILayout.Label("SR2->" + (carInput.wheels[i].SlipRatio2).ToString("F2"), GUILayout.Width(100));
				GUILayout.Space(5);
				GUILayout.Label("SA->" + (carInput.wheels[i].slipAngle).ToString("F2"), GUILayout.Width(100));
				GUILayout.Space(5);
				GUILayout.Label("FX->" + Mathf.Round(carInput.wheels[i].Fx).ToString(), GUILayout.Width(100));
				GUILayout.Space(5);
				GUILayout.Label("FY->" + Mathf.Round(carInput.wheels[i].Fy).ToString(), GUILayout.Width(100));
                GUILayout.Space(5);
                GUILayout.Label("N->" + Mathf.Round(carInput.wheels[i].GetNormalForce()).ToString(), GUILayout.Width(100));
                GUILayout.Space(5);
                GUILayout.Label("V->" + Mathf.Round(carInput.wheels[i].angularVelocity * carInput.wheels[i].radius).ToString(), GUILayout.Width(100));
                GUILayout.EndHorizontal();
			}
			GUILayout.Label("Rigidbody mass->" + carInput.body.mass.ToString("F2"), GUILayout.Width(200));
			GUILayout.Label("NF mass->" + Mathf.Round(calculatedMass/Mathf.Abs(Physics.gravity.y)).ToString("F2"), GUILayout.Width(200));

		},"");


	}

}
