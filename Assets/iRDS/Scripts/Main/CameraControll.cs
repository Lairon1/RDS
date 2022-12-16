
/* This Script can be modified as you want, it controls the input for changing the camera views
 */
using UnityEngine;
using System.Collections;



public class CameraControll : MonoBehaviour {

	IRDSCarCamera carCamera;
	public bool disableGUI =  true;
	bool roadcamera = false;
	public Texture2D absIcon;
	public Texture2D absIconoff;
	public Texture2D absIcontrig;
	public Texture2D tclIcon;
	public Texture2D tclIconoff;
	public Texture2D tclIcontrig;
	public Texture2D espIcon;
	public Texture2D espIconoff;
	public Texture2D espIcontrig;
	public Texture2D steerHelpIcon;
	public Texture2D steerHelpIconoff;
	public Texture2D steerHelpIcontrig;
	
	
	void Start()
	{
		carCamera = GetComponent<IRDSCarCamera>();
		carCamera.DeactivateRoadCamera();
	}
	
		void Update()
	{
		if (Time.timeScale !=0)
		{
			if (Input.GetKeyDown(KeyCode.V))// || Input.GetButtonDown("Jump")
				carCamera.changeView();
			if (Input.GetKeyDown(KeyCode.C))
			{
				carCamera.changeTarget();
				transform.GetComponent<Camera>().fieldOfView = 50;
			}
			
			if (Input.GetKeyDown(KeyCode.B))
			{
				carCamera.ChangeToPlayerCar();
				transform.GetComponent<Camera>().fieldOfView = 50;
			}
			
			if (Input.GetKeyDown(KeyCode.X))
			{
				if (!roadcamera){
					roadcamera = true;
					carCamera.ActivateRoadCamera();
					transform.GetComponent<Camera>().fieldOfView = 50;
				}else {
					carCamera.DeactivateRoadCamera();
					roadcamera = false;
				}
			}
			if (Input.GetKeyDown(KeyCode.T) && IRDSStatistics.GetCurrentCar())
			{
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().tclEnable)
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().tclEnable = false;
				}else
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().tclEnable = true;
				}
				
			}
			if (Input.GetKeyDown(KeyCode.E) && IRDSStatistics.GetCurrentCar())
			{
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().escEnable)
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().escEnable = false;
				}else
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().escEnable = true;
				}
				
			}
			if (Input.GetKeyDown(KeyCode.H) && IRDSStatistics.GetCurrentCar())
			{
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().steerHelpEnable)
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().steerHelpEnable = false;
				}else
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().steerHelpEnable = true;
				}
				
			}		
			if (Input.GetKeyDown(KeyCode.B) && IRDSStatistics.GetCurrentCar())
			{
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().absEnable)
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().absEnable = false;
				}else
				{
					IRDSStatistics.GetCurrentCar().GetCarInputs().absEnable = true;
				}
				
			}		
		}
	}
	
	
	void OnGUI()
	{
		if (!disableGUI){
			Texture2D tempTCL = tclIcon;
			Texture2D tempABS = absIcon;
			Texture2D tempESP = espIcon;
			Texture2D tempSTH = steerHelpIcon;
			
			if (IRDSStatistics.GetCurrentCar())
			{
				IRDSStatistics.GetCurrentCar().GetCarInputs().SetDigitalControl( GUI.Toggle(new Rect(10, Screen.height - 30,100,30),IRDSStatistics.GetCurrentCar().GetCarInputs().GetDigitalControl() ,"Digital input"));
				if (!IRDSStatistics.GetCurrentCar().GetCarInputs().tclEnable)
				{
					tempTCL = tclIconoff;
				}
				if (!IRDSStatistics.GetCurrentCar().GetCarInputs().escEnable)
				{
					tempESP = espIconoff;
				}
				if (!IRDSStatistics.GetCurrentCar().GetCarInputs().absEnable)
				{
					tempABS = absIconoff;
				}
				if (!IRDSStatistics.GetCurrentCar().GetCarInputs().steerHelpEnable)
				{
					tempSTH = steerHelpIconoff;
				}
				
				// if drive aids are triggered
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().tclTriggered)
				{
					tempTCL = tclIcontrig;
				}
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().escTriggered)
				{
					tempESP = espIcontrig;
				}
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().absTriggered)
				{
					tempABS = absIcontrig;
				}
				if (IRDSStatistics.GetCurrentCar().GetCarInputs().steerHelpTriggered)
				{
					tempSTH = steerHelpIcontrig;
				}
				
				GUI.Label(new Rect(Screen.width*0.28f,Screen.height * 0.88f,60,60),tempABS);
				GUI.Label(new Rect(Screen.width*0.3f,Screen.height * 0.88f,100,100),tempESP);
				GUI.Label(new Rect(Screen.width*0.35f,Screen.height * 0.845f,80,80),tempSTH);
				GUI.Label(new Rect(Screen.width*0.4f,Screen.height * 0.86f,80,80),tempTCL);
				
			}
		}
	}
		
	
}
