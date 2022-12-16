using UnityEngine;
using System.Collections;


public class AndroidPlayerControl : MonoBehaviour {
	
	public float Brake;
	public float Throttle;
	public float ThrottleInput;
	public float ThrottleInput1;
	public float BrakeInput;
	public float Steering;
	public float Handbrake;
	// How long the car takes to shift gears
//	public float shiftSpeed = 0.8f;
	

	// These values determine how fast throttle value is changed when the accelerate keys are pressed or released.
	// Getting these right is important to make the car controllable, as keyboard input does not allow analogue input.
	// There are different values for when the wheels have full traction and when there are spinning, to implement 
	// traction control schemes.
	
	// How long it takes to fully engage the throttle
	public float throttleTime = 1.0f;
	// How long it takes to fully engage the throttle 
	// when the wheels are spinning (and traction control is disabled)
	public float throttleTimeTraction = 10.0f;
	// How long it takes to fully release the throttle
	public float throttleReleaseTime = 0.5f;
	// How long it takes to fully release the throttle 
	// when the wheels are spinning.
	public float throttleReleaseTimeTraction = 0.1f;

	// Turn traction control on or off
	public bool tractionControl = true;
	
	
	// These values determine how fast steering value is changed when the steering keys are pressed or released.
	// Getting these right is important to make the car controllable, as keyboard input does not allow analogue input.
	
	// How long it takes to fully turn the steering wheel from center to full lock
	public float steerTime = 1.2f;
	// This is added to steerTime per m/s of velocity, so steering is slower when the car is moving faster.
	public float veloSteerTime = 0.1f;

	// How long it takes to fully turn the steering wheel from full lock to center
	public float steerReleaseTime = 0.6f;
	// This is added to steerReleaseTime per m/s of velocity, so steering is slower when the car is moving faster.
	public float veloSteerReleaseTime = 0f;
	// When detecting a situation where the player tries to counter steer to correct an oversteer situation,
	// steering speed will be multiplied by the difference between optimal and current steering times this 
	// factor, to make the correction easier.
	public float steerCorrectionFactor = 4.0f;
	
	
	
	private IRDSCarControllInput carInputs;
	private IRDSDrivetrain drivetrain;
	
	public Texture2D accelIcon;
    public Texture2D brakeIcon;
	public Texture2D pedalUpIcon;
    public Texture2D pedalDwnIcon;
	
	//IRDSCarCamera camera;
	
	float nextActionTime;
	
//	private float maxSteerLock = 0.0f;
//	private float maxCurrentSteerLock = 0.0f;
	
	// Use this for initialization
	void Start () {
		
		carInputs = GetComponent<IRDSCarControllInput>();
		drivetrain = GetComponent<IRDSDrivetrain>();
		accelIcon = Resources.Load("Textures/AccelPedal")as Texture2D;
		brakeIcon = Resources.Load("Textures/BrakePedal")as Texture2D;
		pedalUpIcon = Resources.Load("Textures/ShiftPaddleUp")as Texture2D;
		pedalDwnIcon = Resources.Load("Textures/ShiftPaddleDwn")as Texture2D;
		Input.multiTouchEnabled = true;
		nextActionTime = Time.time;
	}
	
	
	int cornerSize;
	
	// Update is called once per frame
	void Update () {
		
		 if(Input.touches.Length > 0)
         {
			bool brake = false;
			bool throttle = false;
			bool upB = false;
			bool downB = false;
         int i = 0;
         while (i < Input.touchCount) 
          {  
          Vector2 fingerPos = Input.GetTouch(i).position;
				
				fingerPos.y = Screen.height - fingerPos.y;
				
			
			if(up.Contains(fingerPos))
              {
					upB = true;
              }
          if(down.Contains(fingerPos))
              {
					downB = true;
              } 
				
          if(r.Contains(fingerPos))
              {
					throttle = true;
              }
          if(rforward.Contains(fingerPos))
              {
					brake = true;
              }
				
			
          ++i;
          }
			
		

//			carInputs.setThrottleInput(1);
		if (drivetrain.GetAutomatic() && drivetrain.GetGear() == 0)
		{
			if (throttle)
				carInputs.setBrakeInput( 1);
			else carInputs.setBrakeInput( 0);
			if (brake)
				carInputs.setThrottleInput(1);
			else carInputs.setThrottleInput(0);
		}else
		{
			if (throttle)
				carInputs.setThrottleInput( 1);
			else carInputs.setThrottleInput( 0);
			if (brake)
				carInputs.setBrakeInput(1);
			else carInputs.setBrakeInput(0);
		}
		
		
		if (drivetrain.GetAutomatic() && (drivetrain.GetGear()== 1 || drivetrain.GetGear() == 2) && brake && carInputs.GetCarSpeed() < 1f) 
		{
			drivetrain.gearWanted = 0;
		}
		
		if (drivetrain.GetAutomatic() && drivetrain.GetGear()== 0 && throttle && carInputs.GetCarSpeed() < 1f) 
		{
			drivetrain.gearWanted = 2;
		}
			
			
			if (drivetrain.GetAutomatic()  && !IRDSStatistics.GetCanRace() && IRDSStatistics.raceStartTime == 0)
			{
				drivetrain.gearWanted = 1;
				drivetrain.SetGear(1);
			}else if (drivetrain.GetAutomatic()  && IRDSStatistics.raceStartTime < 1)
			{
				drivetrain.gearWanted = 2;
				drivetrain.SetGear(2);
			}

			if (Time.time - nextActionTime > 0.3){
				if (upB)
					carInputs.shiftUp();
				nextActionTime = Time.time;
				if (downB)
					carInputs.shiftDown();
				
			}
              
         }
		carInputs.setSteerInput(Input.acceleration.x);
		
		if(Input.touches.Length > 0)
		{
			int i = 0;
			while (i < Input.touchCount) 
         	{  
          		Vector2 fingerPos = Input.GetTouch(i).position;
				fingerPos.y = Screen.height - fingerPos.y;
				++i;
			}
		}
		carInputs.setHandBrakeInput(0);
}
	
	private Rect r;
	private Rect rforward;
	private Rect up;
	private Rect down;
	
	
	void OnGUI() {
       
		r = new Rect(Screen.width-120,Screen.height-110,100,100);
       	GUI.DrawTexture(r, accelIcon);
 
       	rforward = new Rect(Screen.width-250,Screen.height-110,100,100);
       	GUI.DrawTexture(rforward, brakeIcon);
		
		up = new Rect(25,Screen.height-220,100,100);
       	GUI.DrawTexture(up, pedalUpIcon);
 
       	down = new Rect(25,Screen.height-100,100,100);
       	GUI.DrawTexture(down, pedalDwnIcon);
		
    }
}