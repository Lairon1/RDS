using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Xml.Serialization;




public class IRDSSaveLoadEditor : Editor {
	
	
	public class IRDSManagerSettings{
		
		public class IRDSCheckPointsS {
			public int myCheckPointNumber;
			public float checkPointExtraTime;
			public string thisCollider;
			public CarSettings.AssetPath checkPointModel;
			public int myCheckPointWaypoint;
		}
		
		public class IRDSWPManagerS {
			public bool AutoClose;
			public bool pitsActive;
			public int numSegments;
			public int pitEntrance1;
			public int pitExit1;
			public int numsegmentsPit1;
			public float pitoffset1;
			public float pitMaxSpeed1;
			public int pitStopSector1;
			public int initalPitNumber_;
			public int finalPitNumber_;
			public float Height;
			public bool copModeActive;
			public int mainTrackInitialWaypoint;
			public int mainTrackEndWaypoint;
			public bool isMainTrack;
			public int myMainTrackInitial;
			public int myMainTrackEnd;
			public IRDSCheckPointsS[] checkPoints;
			public LayerMask dnfLayer;
			public bool dnfCarsOnOtherLayer;
		}
		
		public class IRDSManagerS {
			public class IRDSSubTrackDataS{
				public int numSegments;
				public int mainTrackInitialWaypoint;
				public int mainTrackEndWaypoint;
				public bool isMainTrack;
				public int myMainTrackInitial;
				public int myMainTrackEnd;
				public Color redLineColor;
				public Color trackLimitsColor;
				public bool isOpen;
				public float totalSubTrackDisance;
				public float avarageRadiusForSpeedSubTrack;
				public float avarageRadiusForSpeedMainTrack;
				public int myRealMainTrackInitialWaypoint;
				public int myRealMainTrackEndWaypoint;
				public float myTotalDistance;
				public string myEntrance;
				public int myEntrancePlace;
				public int copEntranceWP;
				public int myEntranceWP;
				public IRDSCheckPointsS[] checkPoints;
			}
			public float roadWidth;
			public string[] pitEntranceExit;
			public IRDSSubTrackDataS[] mainManager;
			public bool addTrackLimitAndRacingLine;
			public bool showSplineControlPoints = true;
			public bool showMoveRotateHandles = true;
			public bool showHandlesForSelected = true;
		}
		
		public IRDSManagerS irdsManager;
		public IRDSWPManagerS wpManager;
	}
	
	
	
	
	public class CarSettings{
		public class AssetPath{
			public string name;
			public string path;
//			public bool isPrefab;
			
			public AssetPath()
			{
				name = null;
				path = null;
			}
			
			public AssetPath(Object obj)
			{
				path = AssetDatabase.GetAssetPath(obj);
				name = obj.name;
//				isPrefab = PrefabUtility.GetPrefabType(obj)==null?false:true;
			}
			
			public T GetAsset<T>() where T : Object
			{
				T newObject = (T) AssetDatabase.LoadAssetAtPath(path, typeof(T)) ;
				if (newObject == null)
				{
					string[] splitPath = path.Split('/');
					string assetName = splitPath[splitPath.Length-1];
					//Not found, lets try to search it on the Asse Database
					string[] allAssetsPaths = AssetDatabase.GetAllAssetPaths();
					for (int i = 0; i < allAssetsPaths.Length; i++)
					{
						string[] assetNameSplit = allAssetsPaths[i].Split('/');
						string candidate = assetNameSplit[assetNameSplit.Length-1];
						if (assetName == candidate){
							newObject = (T) AssetDatabase.LoadAssetAtPath(allAssetsPaths[i], typeof(T));
							break;
						}
					}
					
				}
				if (newObject !=null)
					return newObject;
				else return null;
			}
		}
		
		public class IRDSCameraPositionS {
			public float height;
			public float heightDamping;
			public float rotationDamping;
			public float distance;
			public int fieldOfView;
			public int minFieldOfView;
			public int maxFieldOfView;
			public float fieldOfViewChangeSpeedMultiplier;
			public Vector3 distanceSides;
			public float sidesDamping;
			public bool getRotationFromWaypoints;
		}
		
		
		
		
		public class IRDSWingS 
		{
			public float liftCoefficient;
			public float area;
			public float angle;
		}
		
		
		public class IRDSWheelS {
			public string wheelPosition;
			public float radius;
			public float suspensionTravel;
			private float damping;
			public float dampingRatio;
			public float inertia;
			public float grip;
			public float ToeAngle;
			public float CamberAngle;
			public float brakeFrictionTorque;
			public float handbrakeFrictionTorque;
			public float frictionTorque;
			public float maxSteeringAngle;
			public float massFraction;
			public float[] a;
			public float[] b;
			public float[] cCoefficients;
			public float springForceRatio;
			public float tyreHardness;
			public bool wear;
			public float maxSlip;
			public float maxAngle;
			public float ackermanFactor;
			public float camberSteeringFactor;
			public float staticFrictionCoefficient;
			public bool isFront;
			public bool isLeft;
			public bool oldSuspension;
			public float newSuspensionRebound;
			public float newSuspensionBounce;
			public float newSuspensionSpring;
			public float newSuspensionBumpStopRate;
			public float newSuspensionSpringFactorMin;
			public float newSuspensionSpringFactorMax;
			public float newSuspensionDampFactorMin;
			public float newSuspensionDampFactorMax;
			public float slipRatioClamp;
			public float slipAngleClamp;
			public float multiplierFY;
			public float multiplierFX;
			public Vector3 tireAngles;
			public float maxCamberAngle;
			public IRDSWheel.SuspensionType suspensionType;
			[SerializeField]
			public IRDSWheel.IRDSBasicSuspension basicSuspension;
			[SerializeField]
			public IRDSWheel.IRDSMacPhersonSuspension macPhersonSuspension;
			[SerializeField]
			public IRDSWheel.IRDSWishboneSuspension wishBoneSuspension;
			public float wheelieMultiplier;
			public bool isPoweredWheel;
			public IRDSWheel.ContactPatchType contactPatchType;
			public LayerMask layers;
		}
		
		
		public class IRDSPlayerControlsS {
			public float throttleTime;
			public float throttleTimeTraction;
			public float throttleReleaseTime;
			public float throttleReleaseTimeTraction;
			public bool tractionControl;
			public float steerTime;
			public float veloSteerTime;
			public float steerReleaseTime;
			public float veloSteerReleaseTime;
			public float steerCorrectionFactor;
			public string steeringAxis;
			public string steeringButtonLeft;
			public string steeringButtonRight;
			public string throttleAxis;
			public bool overrideThrottleAxis;
			public string throttleButton;
			public string brakeAxis;
			public bool overrideBrakeAxis;
			public string brakeButton;
			public string shiftUpButton;
			public string shiftDownButton;
			public string handbrakeButton;
			public string clutchAxis;
			public string clutchButton;
			public KeyCode throttleKey;
			public KeyCode altThrottleKey;
			public KeyCode brakesKey;
			public KeyCode leftKey;
			public KeyCode rightKey;
			public KeyCode handbrakeKey;
			public KeyCode shiftUpKey;
			public KeyCode shiftDownKey;
			public KeyCode clutchKey;
			public float clutchTime;
			public float clutchReleaseTime;
			public KeyCode startEngineKey;
			public string startEngineButton;
			public KeyCode nitroKey;
			public string nitroButton;
			[SerializeField]
			public IRDSPlayerControls.IRDSAnalogueRawInputs steerRawInput;
			[SerializeField]
			public IRDSPlayerControls.IRDSAnalogueRawInputs throttleRawInput;
			[SerializeField]
			public IRDSPlayerControls.IRDSAnalogueRawInputs brakeRawInput;
			[SerializeField]
			public IRDSPlayerControls.IRDSAnalogueRawInputs clutchRawInput; 
			public KeyCode activateSirenKey;
			public string activateSirenButton;
			public List<string> gearShifterButtons;
			public bool useGearShifter;
			public bool overrideNitroInput;
			public float handbrakeTime;
			public float handbrakeReleaseTime;
			public bool combinedAxisPedals;
		}
		
		
		
		public class IRDSSoundControllerS {
			
			public class IRDSAudioClipsS{
				public AssetPath audio;
				public float volume;
				public float volumeMultiplier;
				public float pitch;
				public float minPitch;
				public float maxPitch;
				public float minVol;
				public float maxVol;
				public float volMinSpeed;
				public float volMaxSpeed;
				public float pitchMinSpeed;
				public float pitchMaxSpeed;
				public bool loop;
				public bool play;
				public bool playOnAwake;
				public bool bypassEffects;
				public int priority;
				public float dopplerLevel;
				public float maxDistance;
				public float minDistance;
				public float pan;
				public float panLevel;
				public float spread;
				public AudioRolloffMode rolloffMode;
				public string audioName;
				public bool onOffManualCtrl;
				public string onOffInput;
				public bool isSwitch;
				public int numberOfPlaysPerEvent;
				public int numberOfTimesPlayed;
				public int endRacePlaceNumber;
				public float lastTimePlayed;
				public bool isTimeInterval;
				public bool isPerNumberOfPlays;
				public float timeInterval;
				public AssetPath materialName;
				public bool isTouchingMaterial;
				public bool playIfImCurrentCar;
				public float lastimeButtonPress;
				public List<IRDSSoundController.IRDSAudioEvents> events;
				public IRDSSoundController.IRDSAudioPitchOptions pitchOpt;
				public IRDSSoundController.IRDSAudioVolumeOptions volOpt;
				public bool playOneShot;
				public float rate;
				public bool engineDependent;
				public IRDSSoundController.IRDSButtonEvents buttonEvents;
				public bool useCurves;
				public AnimationCurve rpmPitch;
				public AnimationCurve rpmVol;
				public AnimationCurve throttleVol;
				public AnimationCurve gripSpeedVol;
				public AnimationCurve gripSpeedPitch;
				public AnimationCurve tireForceVol;
				public AnimationCurve tireForcePitch;
			}
			
			
			
			
			
			public AssetPath engine;
			public AssetPath skid;
			public AssetPath turbo;
			public AssetPath blowOffValve;
			public AssetPath gears;
			public AssetPath gravel;
			public AssetPath crashLowSpeedSound;
			public float crashLowVolume;
			public AssetPath crashHighSpeedSound;
			public float crashHighVolume;
			public AssetPath backFire;
			public AssetPath startEngine;
			public float startEngineVolume;
			public AssetPath tireBump;
			public float timeBetweenBumps;
			public float engineVolumeMultiplier;
			public float skidVolumeMultiplier;
			public float turboVolumeMultiplier;
			public float blowOffValveVolumeMultiplier;
			public float gearsVolumeMultiplier;
			public float gravelVolumeMultiplier;
			public float backFireVolumeMultiplier;
			public float tireBumpVolumeMultiplier;
			public float tireBumpSensitivity;
			public AssetPath wind;
			public float windPitchRatio;
			public float windVolume;
			public AssetPath breakSound;
			public float breakSoundVolume;
			public AssetPath grass;
			public float grassVolume;
			public float grassMinPitch;
			public float grassMaxPitch;
			public float grassMinVol;
			public float grassMaxVol;
			public float gravelMinPitch;
			public float gravelMaxPitch;
			public float gravelMinVol;
			public float gravelMaxVol;
			public float skidMinPitch;
			public float skidMaxPitch;
			public float engineMinPitch;
			public float engineMaxPitch;
			public float turboMinPitch;
			public float turboMaxPitch;
			public float turboMinVol;
			public float turboMaxVol;
			public AssetPath scratch;
			public float scratchVolume;
			public float scratchVolumeMultiplier;
			public float scratchMinPitch;
			public float scratchMaxPitch;
			public AssetPath policeSiren;
			public float policeSirenVolume;
			public float masterVolume;
			public IRDSAudioClipsS[] extraAudioClips;
			public float backFireDopplerLevel;
			public float gearsDopplerLevel;
			public float tireBumpsDopplerLevel;
			public float blowOffDopplerLevel;
			public float skidEffectsThreshold;
			public float skidEffectsSensitivity;
		}
		
		
		public class IRDSSimplePhysicsS {
			public IRDSSimplePhysics.DrivetrainType drivetrainType;
			public float steerSpeed;
			public float lateralGripHighSpeed;
			public float lateralGripLowSpeed;
			public float antiSpinMultiplier;
			public float minSpeedToSteer;
			public float brakeTorque;
			public float longitudinalMaxForce;
		}
		
		
		public class IRDSDrivetrainS {
			public bool fuelConsume;
			public string poweredIRDSWheels;
			public float[] gearRatios;
			public float finalDriveRatio;
			public float minRPM;
			public float maxRPM;
			public float maxTorque;
			public float torqueRPM;
			public float PSI;
			public float turboRPM;
			public float maxPower;
			public float powerRPM;
			public float engineInertia;
			public float engineBaseFriction;
			public float engineRPMFriction;
			public Vector3 engineOrientation;
			public float differentialLockCoefficient;
			public bool automatic;
			public float shiftUpRpmPercent;
			public float shiftDownRpmPercent;
			public bool automaticClucth;
			public float ShiftSpeed;
			public bool startEngine;
			public float engineStartingTime;
			public float engineCrankTime;
			public float fuelConsumptionMultiplier;
			public float nitroFuel;
			public float nitroBoostDurability;
			public bool useNitro;
			public float engineVacuumFriction;
			public float engineVacuumRPMFriction;
			public float engineShakeAmount;
			public float engineMaxShake;
			public float baseShake;
			public float idleRPMRoughness;
			public float engineRevUpToqrueMultiplier;
			public bool autoHeel_ToeBrakeTech;
			public float revLimiterRPM;
			public bool autoThrottleForShifting;
			public float heel_toeRPMPercent;
			public float heel_toeMinThrottle;
			public float heel_toeMaxThrottle;
			public float autoThrottleForShiftingSpeed;
			public float maxThrottleToShiftDown;
			public float minThrottleToShiftup;
			public float minRPMPercentageToShiftDown;
			public float maxRPMPercentageToShiftUp;
		}
		
		
		
		public class IRDSCarVisualsS {
			public string steeringIRDSWheel;
			public float steeringIRDSWheelMaxAngle;
			public string[] brakeLightsTransform;
			public float frontTiresSkidmarksWidth;
			public float rearTiresSkidmarksWidth;
			public float frontTiresSkidmarkOffset;
			public float rearTiresSkidmarkOffset;
			public float skidSmokeThreshold;
			public float skidMarkThreshold;
			public float skidEffectsThreshold;
			public bool useBackFireLight;
			public string[] backFireLights;
			public string backfireSystem;
			public float particleMultiplier;
			public float lightMultiplier;
			public float lightRange;
			public Color backFireLightColor;
			public float brakeIntensityRate;
			public bool useBackFireForNitro;
			public string nitroParticleSystem;
			public bool useNitroLight;
			public string[] nitroLights;
			public float nitroParticleMultiplier;
			public float nitroLightMultiplier;
			public float nitroLightRange;
			public Color nitroLightColor;
			public bool activateNitroEffect;
			public bool activateBackFireEffect;
			public string propertyName;
			public bool onOffBrakeLightsRenderers;
			public string[] brakeLightRenderers;
		}
		
		public class IRDSCarDamageS 
		{
			public float maxMoveDelta;
			public float maxNoiseDeform;
			public float YforceDamp; 
			public float demolutionRange;
			public float maxRotationDelta;
			public float minForce;
			public float repairDelta;
			public string optionalMeshList;
			public string[] optionalColliderList;
			public string[] optionalTransformList;
			public string sparksEmitter;
			public float maxParticleSizeFactor;
			public float maxParticleEnergyFactor;
			public float maxMaxEnergy;
			public float maxMaxSize;
			public float sparksTime;
			public bool disableDamage;
			public float sparksShurikenSpeedFactor;
			public int minSparkShurikenParticles;
			public int maxSparkShurikenParticles;
			public string sparksShuriken;
			public LayerMask ignoreLayer ;
		}
		
		
		public class IRDSCarControllInputS {
			public float ABSSlip;
			public float TCLSlip;
			public float TCLminSPD;
			public float escFactor;
			public float steerHelp;
			public float shiftSpeed;
			public string centerOfMass;
			public float inertiaFactor = 1.0f;
			public bool absEnable = true;
			public bool escEnable = true;
			public bool tclEnable = true;
			public bool steerHelpEnable = true;
			public bool startEngine = false;
			public bool isTopSpeedLimited = false;
			public float topSpeedLimit = 100f;
		}
		
		public class IRDSAntiRollBarS {
			public string wheel1;
			public string wheel2;
			public float coefficient;
		}
		
		public class IRDSAerodynamicResistanceS {
			public Vector3 coefficients;
		}
		
		public IRDSCameraPositionS[] cameraPositions;
		public IRDSPlayerControlsS playerControls;
		public IRDSAerodynamicResistanceS airResistance;
		public IRDSAntiRollBarS[] antiRollBars;
		public IRDSCarControllInputS controlInput;
		public IRDSCarDamageS carDamage;
		public IRDSCarVisualsS carVisual;
		public IRDSDrivetrainS drivetrain;
		public IRDSSoundControllerS sounds;
		public IRDSSimplePhysicsS simplePhysics;
		public IRDSWheelS[] wheels;
		public IRDSWingS[] wings;
	}
	
	static GameObject theTarget;
	
	static IRDSAerodynamicResistance airResistance;
	static IRDSAntiRollBar[] antirollBars;
	static IRDSCarControllInput carInput;
	static IRDSCarDamage carDamage;
	static IRDSCarVisuals carVisual;
	static IRDSDrivetrain drivetrain;
	static IRDSPlayerControls playerControls;
	static IRDSSoundController soundController;
	static IRDSWheel[] wheels;
	static IRDSWing[] wings;
	static IRDSCameraPosition[] cameraPositions;
	
	
	public static T GetComponentInChildrenRecursive<T>(GameObject obj, string childName) where T : Component
	{
		Transform candidate = obj.transform.Find(childName);
		if (candidate !=null)
		{
			T result = candidate.GetComponent<T>();
			return result;
		}else {
			foreach(Transform child in obj.transform)
			{
				T result = GetComponentInChildrenRecursive<T>(child.gameObject,childName);
				if (result != null)
					return result;
			}
		}
		return null;
	}
	
	

	
	
	static void LoadIRDSManagerSettings()
	{
		theTarget = Selection.activeGameObject;
		IRDSManager manager = theTarget.GetComponent<IRDSManager>();
		IRDSWPManager wpManager = theTarget.GetComponentInChildren<IRDSWPManager>();
		
		string fileName1 = EditorUtility.OpenFilePanel("Load Manager Settings","Assets/","XML");
		if (fileName1 == "")return;
		
		IRDSManagerSettings managerSettings = Load<IRDSManagerSettings>(fileName1);
		
		if (managerSettings == null)return;
		
		Debug.Log ("Loaded settings from: " + fileName1);
		
		//IRDSManager Settings
		manager.addTrackLimitAndRacingLine = managerSettings.irdsManager.addTrackLimitAndRacingLine;
		manager.roadWidth = managerSettings.irdsManager.roadWidth;
		manager.showSplineControlPoints = managerSettings.irdsManager.showSplineControlPoints;
		manager.showMoveRotateHandles = managerSettings.irdsManager.showMoveRotateHandles;
		manager.showHandlesForSelected = managerSettings.irdsManager.showHandlesForSelected;
		
		
		manager.pitEntranceExit =  new BoxCollider[managerSettings.irdsManager.pitEntranceExit.Length];
		for (int i = 0; i < managerSettings.irdsManager.pitEntranceExit.Length;i++)
		{
			manager.pitEntranceExit[i] = GetComponentInChildrenRecursive<BoxCollider>(manager.gameObject ,managerSettings.irdsManager.pitEntranceExit[i]);
		}
		
		//Getting sub track data
		
		int index = Mathf.Min(manager.mainManager.Count, managerSettings.irdsManager.mainManager ==null?0:managerSettings.irdsManager.mainManager.Length);
		for (int i = 0; i < index;i++){
			manager.mainManager[i].numSegments = managerSettings.irdsManager.mainManager[i].numSegments;
			manager.mainManager[i].mainTrackInitialWaypoint = managerSettings.irdsManager.mainManager[i].mainTrackInitialWaypoint;
			manager.mainManager[i].mainTrackEndWaypoint = managerSettings.irdsManager.mainManager[i].mainTrackEndWaypoint;
			
			manager.mainManager[i].isMainTrack = managerSettings.irdsManager.mainManager[i].isMainTrack;
			manager.mainManager[i].myMainTrackInitial = managerSettings.irdsManager.mainManager[i].myMainTrackInitial;
			manager.mainManager[i].myMainTrackEnd = managerSettings.irdsManager.mainManager[i].myMainTrackEnd;
			manager.mainManager[i].redLineColor = managerSettings.irdsManager.mainManager[i].redLineColor;
			manager.mainManager[i].trackLimitsColor = managerSettings.irdsManager.mainManager[i].trackLimitsColor;
			manager.mainManager[i].isOpen = managerSettings.irdsManager.mainManager[i].isOpen;
			manager.mainManager[i].totalSubTrackDisance = managerSettings.irdsManager.mainManager[i].totalSubTrackDisance;
			manager.mainManager[i].avarageRadiusForSpeedSubTrack = managerSettings.irdsManager.mainManager[i].avarageRadiusForSpeedSubTrack;
			manager.mainManager[i].avarageRadiusForSpeedMainTrack = managerSettings.irdsManager.mainManager[i].avarageRadiusForSpeedMainTrack;
			manager.mainManager[i].myRealMainTrackInitialWaypoint = managerSettings.irdsManager.mainManager[i].myRealMainTrackInitialWaypoint;
			manager.mainManager[i].myRealMainTrackEndWaypoint = managerSettings.irdsManager.mainManager[i].myRealMainTrackEndWaypoint;
			manager.mainManager[i].myTotalDistance = managerSettings.irdsManager.mainManager[i].myTotalDistance;
			manager.mainManager[i].myEntrance = GetComponentInChildrenRecursive<BoxCollider>(manager.mainManager[i].subTrackContainer, managerSettings.irdsManager.mainManager[i].myEntrance);
			manager.mainManager[i].myEntrancePlace = managerSettings.irdsManager.mainManager[i].myEntrancePlace;
			manager.mainManager[i].copEntranceWP = managerSettings.irdsManager.mainManager[i].copEntranceWP;
			manager.mainManager[i].myEntranceWP = managerSettings.irdsManager.mainManager[i].myEntranceWP;
		
			
			GetCheckpoints(ref manager.mainManager[i].checkPoints,managerSettings.irdsManager.mainManager[i].checkPoints,manager.mainManager[i].checkPointsContainer);
		}
		
		wpManager.AutoClose = managerSettings.wpManager.AutoClose;
		wpManager.pitsActive = managerSettings.wpManager.pitsActive;
		wpManager.numSegments = managerSettings.wpManager.numSegments;
		wpManager.pitEntrance1 = managerSettings.wpManager.pitEntrance1;
		wpManager.pitExit1 = managerSettings.wpManager.pitExit1;
		wpManager.numsegmentsPit1 = managerSettings.wpManager.numsegmentsPit1;
		wpManager.pitoffset1 = managerSettings.wpManager.pitoffset1;
		wpManager.pitMaxSpeed1 = managerSettings.wpManager.pitMaxSpeed1;
		wpManager.pitStopSector1 = managerSettings.wpManager.pitStopSector1;
		wpManager.initalPitNumber_ = managerSettings.wpManager.initalPitNumber_;
		wpManager.finalPitNumber_ = managerSettings.wpManager.finalPitNumber_;
		wpManager.Height = managerSettings.wpManager.Height;
		wpManager.copModeActive = managerSettings.wpManager.copModeActive;
		wpManager.mainTrackInitialWaypoint = managerSettings.wpManager.mainTrackInitialWaypoint;
		wpManager.mainTrackEndWaypoint = managerSettings.wpManager.mainTrackEndWaypoint;
		wpManager.isMainTrack = managerSettings.wpManager.isMainTrack;
		wpManager.myMainTrackInitial = managerSettings.wpManager.myMainTrackInitial;
		wpManager.myMainTrackEnd = managerSettings.wpManager.myMainTrackEnd;
		
		
		
		GetCheckpoints(ref wpManager.checkPoints,managerSettings.wpManager.checkPoints,wpManager.checkPointsContainer);
		
		
		wpManager.dnfLayer = managerSettings.wpManager.dnfLayer;
		wpManager.dnfCarsOnOtherLayer = managerSettings.wpManager.dnfCarsOnOtherLayer;
	}
	
	
	
	static void SaveIRDSManagerSettings()
	{
		theTarget = Selection.activeGameObject;
		IRDSManager manager = theTarget.GetComponent<IRDSManager>();
		IRDSWPManager wpManager = theTarget.GetComponentInChildren<IRDSWPManager>();
		
		string fileName1 = EditorUtility.SaveFilePanel("Save Manager Settings","Assets/","ManagerSettings","XML");
		if (fileName1 == "")return;
		
		Debug.Log ("Saved settings At: " + fileName1);
		
		//IRDSManager Settings
		IRDSManagerSettings managerSettings = new IRDSManagerSettings();
		managerSettings.irdsManager = new IRDSSaveLoadEditor.IRDSManagerSettings.IRDSManagerS();
		managerSettings.wpManager = new IRDSSaveLoadEditor.IRDSManagerSettings.IRDSWPManagerS();
		
		managerSettings.irdsManager.addTrackLimitAndRacingLine = manager.addTrackLimitAndRacingLine;
		managerSettings.irdsManager.roadWidth = manager.roadWidth;
		managerSettings.irdsManager.showSplineControlPoints = manager.showSplineControlPoints;
		managerSettings.irdsManager.showMoveRotateHandles = manager.showMoveRotateHandles;
		managerSettings.irdsManager.showHandlesForSelected = manager.showHandlesForSelected;
		
		
		managerSettings.irdsManager.pitEntranceExit =  new string[manager.pitEntranceExit.Length];
		for (int i = 0; i < manager.pitEntranceExit.Length;i++)
		{
			managerSettings.irdsManager.pitEntranceExit[i] = manager.pitEntranceExit[i].transform.name;
		}
		
		//Getting sub track data
		managerSettings.irdsManager.mainManager = new IRDSSaveLoadEditor.IRDSManagerSettings.IRDSManagerS.IRDSSubTrackDataS[manager.mainManager.Count];
		for (int i = 0; i < manager.mainManager.Count;i++){
			managerSettings.irdsManager.mainManager[i] = new IRDSSaveLoadEditor.IRDSManagerSettings.IRDSManagerS.IRDSSubTrackDataS();
			managerSettings.irdsManager.mainManager[i].numSegments = manager.mainManager[i].numSegments;
			managerSettings.irdsManager.mainManager[i].mainTrackInitialWaypoint = manager.mainManager[i].mainTrackInitialWaypoint;
			managerSettings.irdsManager.mainManager[i].mainTrackEndWaypoint=manager.mainManager[i].mainTrackEndWaypoint;
			
			managerSettings.irdsManager.mainManager[i].isMainTrack=manager.mainManager[i].isMainTrack;
			managerSettings.irdsManager.mainManager[i].myMainTrackInitial=manager.mainManager[i].myMainTrackInitial;
			managerSettings.irdsManager.mainManager[i].myMainTrackEnd=manager.mainManager[i].myMainTrackEnd;
			managerSettings.irdsManager.mainManager[i].redLineColor=manager.mainManager[i].redLineColor;
			managerSettings.irdsManager.mainManager[i].trackLimitsColor=manager.mainManager[i].trackLimitsColor;
			managerSettings.irdsManager.mainManager[i].isOpen=manager.mainManager[i].isOpen;
			managerSettings.irdsManager.mainManager[i].totalSubTrackDisance=manager.mainManager[i].totalSubTrackDisance;
			managerSettings.irdsManager.mainManager[i].avarageRadiusForSpeedSubTrack=manager.mainManager[i].avarageRadiusForSpeedSubTrack;
			managerSettings.irdsManager.mainManager[i].avarageRadiusForSpeedMainTrack=manager.mainManager[i].avarageRadiusForSpeedMainTrack;
			managerSettings.irdsManager.mainManager[i].myRealMainTrackInitialWaypoint=manager.mainManager[i].myRealMainTrackInitialWaypoint;
			managerSettings.irdsManager.mainManager[i].myRealMainTrackEndWaypoint=manager.mainManager[i].myRealMainTrackEndWaypoint;
			managerSettings.irdsManager.mainManager[i].myTotalDistance=manager.mainManager[i].myTotalDistance;
			managerSettings.irdsManager.mainManager[i].myEntrance=manager.mainManager[i].myEntrance.transform.name;
			managerSettings.irdsManager.mainManager[i].myEntrancePlace=manager.mainManager[i].myEntrancePlace;
			managerSettings.irdsManager.mainManager[i].copEntranceWP=manager.mainManager[i].copEntranceWP;
			managerSettings.irdsManager.mainManager[i].myEntranceWP=manager.mainManager[i].myEntranceWP;
		
			
			SetCheckpoints(ref managerSettings.irdsManager.mainManager[i].checkPoints,manager.mainManager[i].checkPoints);
		}
		
		managerSettings.wpManager.AutoClose=wpManager.AutoClose;
		managerSettings.wpManager.pitsActive=wpManager.pitsActive;
		managerSettings.wpManager.numSegments=wpManager.numSegments;
		managerSettings.wpManager.pitEntrance1=wpManager.pitEntrance1;
		managerSettings.wpManager.pitExit1=wpManager.pitExit1;
		managerSettings.wpManager.numsegmentsPit1=wpManager.numsegmentsPit1;
		managerSettings.wpManager.pitoffset1=wpManager.pitoffset1;
		managerSettings.wpManager.pitMaxSpeed1=wpManager.pitMaxSpeed1;
		managerSettings.wpManager.pitStopSector1=wpManager.pitStopSector1;
		managerSettings.wpManager.initalPitNumber_=wpManager.initalPitNumber_;
		managerSettings.wpManager.finalPitNumber_=wpManager.finalPitNumber_;
		managerSettings.wpManager.Height=wpManager.Height;
		managerSettings.wpManager.copModeActive=wpManager.copModeActive;
		managerSettings.wpManager.mainTrackInitialWaypoint=wpManager.mainTrackInitialWaypoint;
		managerSettings.wpManager.mainTrackEndWaypoint=wpManager.mainTrackEndWaypoint;
		managerSettings.wpManager.isMainTrack=wpManager.isMainTrack;
		managerSettings.wpManager.myMainTrackInitial=wpManager.myMainTrackInitial;
		managerSettings.wpManager.myMainTrackEnd=wpManager.myMainTrackEnd;
		
		
		
		SetCheckpoints(ref managerSettings.wpManager.checkPoints,wpManager.checkPoints);
		
		
		managerSettings.wpManager.dnfLayer=wpManager.dnfLayer;
		managerSettings.wpManager.dnfCarsOnOtherLayer=wpManager.dnfCarsOnOtherLayer;
		
		Save<IRDSSaveLoadEditor.IRDSManagerSettings>(fileName1,managerSettings);
	}
	
	
	
	static void GetCheckpoints(ref List<IRDSCheckPoints> checkPoints, IRDSManagerSettings.IRDSCheckPointsS[] checkPData, GameObject subTrackContainer)
	{
		int indexC = Mathf.Min(checkPoints.Count, checkPData.Length);
		for (int y =0; y < indexC;y++)
		{
			checkPoints[y].myCheckPointNumber = checkPData[y].myCheckPointNumber;
			checkPoints[y].checkPointExtraTime = checkPData[y].checkPointExtraTime;
			checkPoints[y].thisCollider = GetComponentInChildrenRecursive<BoxCollider>(subTrackContainer ,checkPData[y].thisCollider);
			checkPoints[y].checkPointModel = checkPData[y].checkPointModel.GetAsset<GameObject>();
			checkPoints[y].myCheckPointWaypoint = checkPData[y].myCheckPointWaypoint;
		}
	}
	
	
	static void SetCheckpoints(ref IRDSManagerSettings.IRDSCheckPointsS[] checkPoints,List<IRDSCheckPoints>  checkPData)
	{
		checkPoints = new IRDSSaveLoadEditor.IRDSManagerSettings.IRDSCheckPointsS[checkPData.Count];
		
		for (int y =0; y < checkPData.Count;y++)
		{
			checkPoints[y] = new IRDSSaveLoadEditor.IRDSManagerSettings.IRDSCheckPointsS();
			checkPoints[y].myCheckPointNumber = checkPData[y].myCheckPointNumber;
			checkPoints[y].checkPointExtraTime = checkPData[y].checkPointExtraTime;
			checkPoints[y].thisCollider = checkPData[y].thisCollider.transform.name;
			checkPoints[y].checkPointModel = new IRDSSaveLoadEditor.CarSettings.AssetPath(checkPData[y].checkPointModel);
			checkPoints[y].myCheckPointWaypoint = checkPData[y].myCheckPointWaypoint;
		}
	}
	
	
	[MenuItem("GameObject/iRDS/Load Settings",true)]
	public static bool ValidateLoadCarSettings()
	{
		if (Selection.activeGameObject !=null)
			return Selection.activeGameObject.GetComponent<IRDSDrivetrain>() != null || Selection.activeGameObject.GetComponent<IRDSManager>() != null;
		else return false;
	}
	
	[MenuItem("GameObject/iRDS/Load Settings",false,0)]
	public static void LoadSettings()
	{
		IRDSManager component = Selection.activeGameObject.GetComponent<IRDSManager>();
		
		if (component!=null){
			LoadIRDSManagerSettings();
			return;
		}
		
		IRDSSaveLoadWindow.Init();
			
		
	}
	public static LoadOptions[] options = new LoadOptions[]
	{
		new LoadOptions("Import object references"),
		new LoadOptions("Air Resistance"),
		new LoadOptions("Antiroll Bars"),
		new LoadOptions("IRDSCarControllInput"),
		new LoadOptions("Car Damage"),
		new LoadOptions("Car Visual"),
		new LoadOptions("Drivetrain & Gears"),
		new LoadOptions("Player Controls"),
		new LoadOptions("Sounds"),
		new LoadOptions("Wheels"),
		new LoadOptions("Wings"),
		new LoadOptions("Camera Positions"),
	};

	public class LoadOptions{
		public bool flag;
		public string name;
		public LoadOptions(string n)
		{
			name = n;
			flag = true;
		}
	}

	public static void LoadCarSettings(){
		theTarget = Selection.activeGameObject;
		airResistance = theTarget.GetComponent<IRDSAerodynamicResistance>();
		antirollBars = theTarget.GetComponents<IRDSAntiRollBar>();
		carInput = theTarget.GetComponent<IRDSCarControllInput>();
		carDamage = theTarget.GetComponent<IRDSCarDamage>();
		carVisual = theTarget.GetComponent<IRDSCarVisuals>();
		drivetrain = theTarget.GetComponent<IRDSDrivetrain>();
		playerControls = theTarget.GetComponent<IRDSPlayerControls>();
		soundController = theTarget.GetComponent<IRDSSoundController>();
		wheels = IRDSUtility.GetAllChilds<IRDSWheel>(theTarget.transform);
		wings = IRDSUtility.GetAllChilds<IRDSWing>(theTarget.transform);
		cameraPositions = IRDSUtility.GetAllChilds<IRDSCameraPosition>(theTarget.transform);
		
		
		
		string fileName1 = EditorUtility.OpenFilePanel("Load Car Settings","Assets/","XML");
		if (fileName1 == "")return;
		
		CarSettings carS = Load<CarSettings>(fileName1);
		
		if (carS == null)return;
		
//		string[] allAssets = AssetDatabase.GetAllAssetPaths();
		
		
		
		Debug.Log ("Loaded settings from: " + fileName1);
		
		//AirResistance Settings
		if (options[1].flag){
			airResistance.SetCoefficients(carS.airResistance.coefficients);
			EditorUtility.SetDirty(airResistance);
		}
		
		//Antiroll bars settings
		if (options[2].flag)
		for (int i = 0; i < carS.antiRollBars.Length; i++){
			antirollBars[i].SetCoefficient(carS.antiRollBars[i].coefficient);
			if (options[0].flag){
				antirollBars[i].wheel1 = GetComponentInChildrenRecursive<IRDSWheel>(theTarget.gameObject, carS.antiRollBars[i].wheel1);
				antirollBars[i].wheel2= GetComponentInChildrenRecursive<IRDSWheel>(theTarget.gameObject, carS.antiRollBars[i].wheel2);
			}
			EditorUtility.SetDirty(antirollBars[i]);
		}

		// Tire, brakes and suspension settings
		if (options[9].flag){
			for (int i = 0; i < wheels.Length;i++)
			{
				//Tire settings
				if (wheels[i].model == null)
					wheels[i].model = wheels[i].transform.Find("Tire").gameObject;
				if (wheels[i].modelCalliper ==null)
					wheels[i].modelCalliper = wheels[i].transform.Find("Caliper").gameObject;
				wheels[i].SetRadius(carS.wheels[i].radius);
				wheels[i].SetInertia(carS.wheels[i].inertia);
				wheels[i].SetGrip(carS.wheels[i].grip);
				wheels[i].staticFrictionCoefficient=carS.wheels[i].staticFrictionCoefficient;
				wheels[i].SetFrictionTorque(carS.wheels[i].frictionTorque);
				wheels[i].SetTyreHardness(carS.wheels[i].tyreHardness);
				wheels[i].SetACoefficients(carS.wheels[i].a);
				wheels[i].SetBCoefficients(carS.wheels[i].b);
				wheels[i].SetCCoefficients(carS.wheels[i].cCoefficients);
				
				//Brake settings
				wheels[i].SetBrakeFrictionTorque(carS.wheels[i].brakeFrictionTorque);
				wheels[i].SetHandBrakeFrictionTorque(carS.wheels[i].handbrakeFrictionTorque);
				
				//Suspension settings
				wheels[i].SetMassFraction(carS.wheels[i].massFraction);
				wheels[i].SetMaxSteeringAngle(carS.wheels[i].maxSteeringAngle);
				wheels[i].SetCamberAngle(carS.wheels[i].CamberAngle);
				wheels[i].SetToeAngle(carS.wheels[i].ToeAngle);
				wheels[i].SetSuspensionTravel(carS.wheels[i].suspensionTravel);
				wheels[i].SetSpringForceRatio(carS.wheels[i].springForceRatio);
				wheels[i].wheelPosition = carS.wheels[i].wheelPosition;
				wheels[i].SetDampingRatio(carS.wheels[i].dampingRatio);
				wheels[i].SetWear(carS.wheels[i].wear);
				wheels[i].ackermanFactor=carS.wheels[i].ackermanFactor;
				wheels[i].camberSteeringFactor=carS.wheels[i].camberSteeringFactor;
				wheels[i].isFront=carS.wheels[i].isFront;
				wheels[i].isLeft=carS.wheels[i].isLeft;
				wheels[i].oldSuspension=carS.wheels[i].oldSuspension;
				wheels[i].newSuspensionRebound=carS.wheels[i].newSuspensionRebound;
				wheels[i].newSuspensionBounce=carS.wheels[i].newSuspensionBounce;
				wheels[i].newSuspensionSpring=carS.wheels[i].newSuspensionSpring;
				wheels[i].newSuspensionBumpStopRate=carS.wheels[i].newSuspensionBumpStopRate;
				wheels[i].newSuspensionSpringFactorMin=carS.wheels[i].newSuspensionSpringFactorMin;
				wheels[i].newSuspensionSpringFactorMax=carS.wheels[i].newSuspensionSpringFactorMax;
				wheels[i].newSuspensionDampFactorMin=carS.wheels[i].newSuspensionDampFactorMin;
				wheels[i].newSuspensionDampFactorMax=carS.wheels[i].newSuspensionDampFactorMax;
				wheels[i].slipRatioClamp=carS.wheels[i].slipRatioClamp;
				wheels[i].slipAngleClamp=carS.wheels[i].slipAngleClamp;
				wheels[i].multiplierFX=carS.wheels[i].multiplierFY;
				wheels[i].multiplierFY=carS.wheels[i].multiplierFX;
				wheels[i].tireAngles=carS.wheels[i].tireAngles;
				wheels[i].maxCamberAngle=carS.wheels[i].maxCamberAngle;
				wheels[i].suspensionType=carS.wheels[i].suspensionType;
				wheels[i].basicSuspension=carS.wheels[i].basicSuspension;
				wheels[i].macPhersonSuspension= carS.wheels[i].macPhersonSuspension;
				wheels[i].wishBoneSuspension= carS.wheels[i].wishBoneSuspension;
				wheels[i].wheelieMultiplier=carS.wheels[i].wheelieMultiplier;
				wheels[i].isPoweredWheel=carS.wheels[i].isPoweredWheel;
				wheels[i].contactPatchType=carS.wheels[i].contactPatchType;
				wheels[i].layers=carS.wheels[i].layers;
				EditorUtility.SetDirty(wheels[i]);
			}
		}


		// IRDSCarControllInput settings
		if (options[3].flag){
		carInput.SetABSSlip(carS.controlInput.ABSSlip);
		carInput.SetTCLSlip(carS.controlInput.TCLSlip);
		carInput.SetTCLMinSPD(carS.controlInput.TCLminSPD);
		carInput.SetescFactor(carS.controlInput.escFactor);
		carInput.SetSteerHelpFactor(carS.controlInput.steerHelp);
		carInput.inertiaFactor = carS.controlInput.inertiaFactor;
		
		
		carInput.shiftSpeed = carS.controlInput.shiftSpeed;
		if (options[0].flag)
    	carInput.centerOfMass = GetComponentInChildrenRecursive<Transform>(theTarget.gameObject,carS.controlInput.centerOfMass);
		carInput.absEnable = carS.controlInput.absEnable;
		carInput.escEnable = carS.controlInput.escEnable;
		carInput.tclEnable = carS.controlInput.tclEnable;
		carInput.steerHelpEnable = carS.controlInput.steerHelpEnable;
		carInput.startEngine = carS.controlInput.startEngine;
		carInput.isTopSpeedLimited = carS.controlInput.isTopSpeedLimited ;
		carInput.topSpeedLimit = carS.controlInput.topSpeedLimit;
		}
		EditorUtility.SetDirty(carInput);
		
		//IRDSCarDamage settings
		if (options[4].flag){
    carDamage.disableDamage = carS.carDamage.disableDamage;
		carDamage.SetMaxMoveDelta(carS.carDamage.maxMoveDelta);
		carDamage.SetMaxNoiseDeform(carS.carDamage.maxNoiseDeform);
		carDamage.SetYForceDamp(carS.carDamage.YforceDamp);
		carDamage.SetDemolutionRange(carS.carDamage.demolutionRange);
		carDamage.SetMaxRotationDelta(carS.carDamage.maxRotationDelta);
		carDamage.SetMinForce(carS.carDamage.minForce);
		carDamage.SetRepairDelta(carS.carDamage.repairDelta);
		
		//Scratch Sparks under Carvisuals tab on the Car Setup script
		carDamage.maxParticleSizeFactor = carS.carDamage.maxParticleSizeFactor;
		carDamage.maxParticleEnergyFactor = carS.carDamage.maxParticleEnergyFactor;
		carDamage.maxMaxSize = carS.carDamage.maxMaxSize;
		carDamage.maxMaxEnergy = carS.carDamage.maxMaxEnergy;
		carDamage.sparksTime = carS.carDamage.sparksTime;
		
    if (options[0].flag){
		carDamage.SetOptionalMeshList(GetComponentInChildrenRecursive<Transform>(theTarget.gameObject,carS.carDamage.optionalMeshList));
		
		MeshCollider[] meshes = new MeshCollider[carS.carDamage.optionalColliderList.Length];
		for (int i = 0; i < carS.carDamage.optionalColliderList.Length;i++)
		{
			meshes[i] = GetComponentInChildrenRecursive<MeshCollider>(theTarget.gameObject,carS.carDamage.optionalColliderList[i]);
		}
		carDamage.SetOptionalColliderList(meshes);
		
		Transform[] transforms = new Transform[carS.carDamage.optionalTransformList.Length];
		for (int i = 0; i < carS.carDamage.optionalTransformList.Length;i++)
		{
			transforms[i] = GetComponentInChildrenRecursive<Transform>(theTarget.gameObject,carS.carDamage.optionalTransformList[i]);
		}
			carDamage.SetOptionalTransformList(transforms);
		
		
    carDamage.sparksShuriken = GetComponentInChildrenRecursive<ParticleSystem>(theTarget.gameObject,carS.carDamage.sparksShuriken);
		}
		carDamage.disableDamage = carS.carDamage.disableDamage;
		carDamage.sparksShurikenSpeedFactor = carS.carDamage.sparksShurikenSpeedFactor;
		carDamage.minSparkShurikenParticles = carS.carDamage.minSparkShurikenParticles;
		carDamage.maxSparkShurikenParticles = carS.carDamage.maxSparkShurikenParticles;
		carDamage.ignoreLayer = carS.carDamage.ignoreLayer;
    }
	EditorUtility.SetDirty(carDamage);
		
		
		//IRDSCarVisual Settings
		if (options[5].flag){
    carVisual.steeringIRDSWheelMaxAngle = carS.carVisual.steeringIRDSWheelMaxAngle;
		carVisual.brakeIntensityRate = carS.carVisual.brakeIntensityRate;
		carVisual.frontTiresSkidmarksWidth = carS.carVisual.frontTiresSkidmarksWidth;
		carVisual.rearTiresSkidmarksWidth = carS.carVisual.rearTiresSkidmarksWidth;
		carVisual.frontTiresSkidmarkOffset = carS.carVisual.frontTiresSkidmarkOffset;
		carVisual.rearTiresSkidmarkOffset = carS.carVisual.rearTiresSkidmarkOffset;
		carVisual.skidEffectsThreshold = carS.carVisual.skidEffectsThreshold; 
		carVisual.skidSmokeThreshold = carS.carVisual.skidSmokeThreshold;
		carVisual.skidMarkThreshold = carS.carVisual.skidMarkThreshold;
		carVisual.useBackFireForNitro = carS.carVisual.useBackFireForNitro;
		carVisual.particleMultiplier = carS.carVisual.particleMultiplier;
		carVisual.useBackFireLight = carS.carVisual.useBackFireLight;
		carVisual.lightMultiplier = carS.carVisual.lightMultiplier;
		carVisual.lightRange = carS.carVisual.lightRange;
		carVisual.backFireLightColor = carS.carVisual.backFireLightColor;
		if (options[0].flag){
    Transform tempT = GetComponentInChildrenRecursive<Transform>(theTarget.gameObject,carS.carVisual.steeringIRDSWheel);
		carVisual.steeringIRDSWheel = tempT!=null?tempT.gameObject:null;
		Transform[] tr = new Transform[carS.carVisual.brakeLightsTransform.Length];
		for (int i =0;i< carS.carVisual.brakeLightsTransform.Length;i++)
		{
			tr[i] = GetComponentInChildrenRecursive<Transform>(theTarget.gameObject,carS.carVisual.brakeLightsTransform[i]);
		}
		carVisual.brakeLightsTransform = tr;
		Light[] lights = new Light[carS.carVisual.backFireLights.Length];
		for (int i =0;i< carS.carVisual.backFireLights.Length;i++)
		{
			lights[i] = GetComponentInChildrenRecursive<Light>(theTarget.gameObject,carS.carVisual.backFireLights[i]);
		}
		carVisual.backFireLights = lights;
		carVisual.backfireSystem = GetComponentInChildrenRecursive<ParticleSystem>(theTarget.gameObject,carS.carVisual.backfireSystem);
		carVisual.nitroParticleSystem = GetComponentInChildrenRecursive<ParticleSystem>(theTarget.gameObject,carS.carVisual.nitroParticleSystem);
		Light[] lights1 = new Light[carS.carVisual.nitroLights.Length];
		for (int i =0;i< carS.carVisual.nitroLights.Length;i++)
		{
			lights1[i] = GetComponentInChildrenRecursive<Light>(theTarget.gameObject,carS.carVisual.nitroLights[i]);
		}
		carVisual.nitroLights = lights1;
//carS.carVisual.brakeLightRenderers = new string[carVisual.brakeLightRenderers.Length];
		Renderer[] rends = new Renderer[carS.carVisual.brakeLightRenderers.Length];
		for (int i =0;i< carS.carVisual.brakeLightRenderers.Length;i++)
		{
			rends[i] = GetComponentInChildrenRecursive<Renderer>(theTarget.gameObject,carS.carVisual.brakeLightRenderers[i]);
		}
		carVisual.brakeLightRenderers = rends;
    }
    carVisual.useNitroLight = carS.carVisual.useNitroLight;
		carVisual.nitroParticleMultiplier = carS.carVisual.nitroParticleMultiplier;
		carVisual.nitroLightMultiplier = carS.carVisual.nitroLightMultiplier;
		carVisual.nitroLightRange = carS.carVisual.nitroLightRange;
		carVisual.nitroLightColor = carS.carVisual.nitroLightColor;
		carVisual.activateNitroEffect = carS.carVisual.activateNitroEffect;
		carVisual.activateBackFireEffect = carS.carVisual.activateBackFireEffect;
		carVisual.propertyName = carS.carVisual.propertyName;
		carVisual.onOffBrakeLightsRenderers = carS.carVisual.onOffBrakeLightsRenderers;
	}
		EditorUtility.SetDirty(carVisual);
		
		
		
		
		//IRDSDrivetrain settings
	if(options[6].flag){
  	drivetrain.SetDifferentialLockCoefficient(carS.drivetrain.differentialLockCoefficient) ;
		drivetrain.engineCrankTime = carS.drivetrain.engineCrankTime;
		drivetrain.engineStartingTime = carS.drivetrain.engineStartingTime;
		drivetrain.fuelConsumptionMultiplier = carS.drivetrain.fuelConsumptionMultiplier;
		drivetrain.nitroFuel = carS.drivetrain.nitroFuel;
		drivetrain.nitroBoostDurability = carS.drivetrain.nitroBoostDurability;
		drivetrain.SetMaxPower(carS.drivetrain.maxPower);
		drivetrain.SetPowerRPM(carS.drivetrain.powerRPM);
		drivetrain.SetMinRPM(carS.drivetrain.minRPM);
		drivetrain.SetMaxRPM(carS.drivetrain.maxRPM);
		drivetrain.SetMaxTorque(carS.drivetrain.maxTorque);
		drivetrain.SetTorqueRPM(carS.drivetrain.torqueRPM);
		drivetrain.SetTurboPSI(carS.drivetrain.PSI);
		drivetrain.SetTurboRPM(carS.drivetrain.turboRPM);
		drivetrain.SetEngineInertia(carS.drivetrain.engineInertia);
		drivetrain.SetEngineBaseFriction(carS.drivetrain.engineBaseFriction);
		drivetrain.SetEngineRPMFriction(carS.drivetrain.engineRPMFriction);
		drivetrain.SetEngineOrientation(carS.drivetrain.engineOrientation);
		
		//Drivetrain Gear settings
		
		drivetrain.SetAutomatic(carS.drivetrain.automatic);
		drivetrain.automaticClucth = carS.drivetrain.automaticClucth;
		drivetrain.shiftUpRpmPercent = carS.drivetrain.shiftUpRpmPercent;
		drivetrain.shiftDownRpmPercent = carS.drivetrain.shiftDownRpmPercent;
		drivetrain.SetGearRatios(carS.drivetrain.gearRatios);
		drivetrain.SetFinalDriveRatio(carS.drivetrain.finalDriveRatio);
		drivetrain.ShiftSpeed = carS.drivetrain.ShiftSpeed;
		
		drivetrain.SetFuelConsume(carS.drivetrain.fuelConsume);

	drivetrain.SetDrivetrain((carS.drivetrain.poweredIRDSWheels=="FWD"?0:(carS.drivetrain.poweredIRDSWheels=="RWD"?1:(carS.drivetrain.poweredIRDSWheels=="AWD/4x4"?2:-1))));
		drivetrain.startEngine = carS.drivetrain.startEngine;
		
		
		drivetrain.engineVacuumFriction  = carS.drivetrain.engineVacuumFriction;
		drivetrain.engineVacuumRPMFriction = carS.drivetrain.engineVacuumRPMFriction;
		drivetrain.engineShakeAmount=carS.drivetrain.engineShakeAmount;
		drivetrain.engineMaxShake=carS.drivetrain.engineMaxShake;
		drivetrain.baseShake=carS.drivetrain.baseShake;
		drivetrain.idleRPMRoughness=carS.drivetrain.idleRPMRoughness;
		drivetrain.engineRevUpToqrueMultiplier=carS.drivetrain.engineRevUpToqrueMultiplier;
		drivetrain.autoHeel_ToeBrakeTech=carS.drivetrain.autoHeel_ToeBrakeTech;
		drivetrain.revLimiterRPM=carS.drivetrain.revLimiterRPM;
		drivetrain.autoThrottleForShifting=carS.drivetrain.autoThrottleForShifting;
		drivetrain.heel_toeRPMPercent=carS.drivetrain.heel_toeRPMPercent;
		drivetrain.heel_toeMinThrottle=carS.drivetrain.heel_toeMinThrottle;
		drivetrain.heel_toeMaxThrottle=carS.drivetrain.heel_toeMaxThrottle;
		drivetrain.autoThrottleForShiftingSpeed=carS.drivetrain.autoThrottleForShiftingSpeed;
		drivetrain.maxThrottleToShiftDown=carS.drivetrain.maxThrottleToShiftDown;
		drivetrain.minThrottleToShiftup=carS.drivetrain.minThrottleToShiftup;
		drivetrain.minRPMPercentageToShiftDown=carS.drivetrain.minRPMPercentageToShiftDown;
		drivetrain.maxRPMPercentageToShiftUp=carS.drivetrain.maxRPMPercentageToShiftUp;
    }	
		if (drivetrain.fuelTank == null){
			drivetrain.fuelTank = drivetrain.transform.Find("FuelTank");
			
		}
		drivetrain.fuelTankBody = drivetrain.fuelTank.GetComponent<Rigidbody>();
	EditorUtility.SetDirty(drivetrain);
		
		
		//Player controls settings
	if (options[7].flag){
  	playerControls.SetThrottleTime(carS.playerControls.throttleTime);
		playerControls.SetThrottleTimeTraction(carS.playerControls.throttleTimeTraction);
		playerControls.SetThrottleReleaseTime(carS.playerControls.throttleReleaseTime);
		playerControls.SetThrottleReleaseTimeTraction(carS.playerControls.throttleReleaseTimeTraction);
		playerControls.SetSteerTime(carS.playerControls.steerTime);
		playerControls.SetVeloSteerTime(carS.playerControls.veloSteerTime);
		playerControls.SetSteerReleaseTime(carS.playerControls.steerReleaseTime);
		playerControls.SetVeloSteerReleaseTime(carS.playerControls.veloSteerReleaseTime);
		playerControls.SetSteerCorrectionFactor(carS.playerControls.steerCorrectionFactor);
		
		playerControls.clutchTime = carS.playerControls.clutchTime;
		playerControls.clutchReleaseTime = carS.playerControls.clutchReleaseTime;
		
		playerControls.steeringAxis = carS.playerControls.steeringAxis;
		
		playerControls.throttleAxis = carS.playerControls.throttleAxis;
		playerControls.brakeAxis = carS.playerControls.brakeAxis;
		playerControls.shiftUpButton = carS.playerControls.shiftUpButton;
		playerControls.shiftDownButton = carS.playerControls.shiftDownButton;
		playerControls.handbrakeButton = carS.playerControls.handbrakeButton;
		playerControls.clutchAxis = carS.playerControls.clutchAxis;
		playerControls.startEngineButton = carS.playerControls.startEngineButton;
		playerControls.nitroButton = carS.playerControls.nitroButton;
		
		playerControls.throttleKey = carS.playerControls.throttleKey;
		playerControls.altThrottleKey = carS.playerControls.altThrottleKey;
		playerControls.brakesKey = carS.playerControls.brakesKey;
		playerControls.leftKey = carS.playerControls.leftKey;
		playerControls.rightKey=carS.playerControls.rightKey;
		playerControls.handbrakeKey=carS.playerControls.handbrakeKey;
		playerControls.shiftUpKey=carS.playerControls.shiftUpKey;
		playerControls.shiftDownKey=carS.playerControls.shiftDownKey;
		playerControls.clutchKey=carS.playerControls.clutchKey;
		playerControls.startEngineKey=carS.playerControls.startEngineKey;
		playerControls.nitroKey=carS.playerControls.nitroKey;
		
		
		playerControls.steeringButtonLeft=carS.playerControls.steeringButtonLeft;
		playerControls.steeringButtonRight=carS.playerControls.steeringButtonRight;
		playerControls.overrideThrottleAxis=carS.playerControls.overrideThrottleAxis;
		playerControls.throttleButton=carS.playerControls.throttleButton;
		playerControls.overrideBrakeAxis=carS.playerControls.overrideBrakeAxis;
		playerControls.brakeButton=carS.playerControls.brakeButton;
		playerControls.clutchButton=carS.playerControls.clutchButton;
		playerControls.steerRawInput=carS.playerControls.steerRawInput;
		playerControls.throttleRawInput=carS.playerControls.throttleRawInput;
		playerControls.brakeRawInput=carS.playerControls.brakeRawInput;
		playerControls.clutchRawInput=carS.playerControls.clutchRawInput;
		playerControls.activateSirenKey=carS.playerControls.activateSirenKey;
		playerControls.activateSirenButton=carS.playerControls.activateSirenButton;
		playerControls.gearShifterButtons=carS.playerControls.gearShifterButtons;
		playerControls.useGearShifter=carS.playerControls.useGearShifter;
		playerControls.overrideNitroInput=carS.playerControls.overrideNitroInput;
		playerControls.handbrakeTime=carS.playerControls.handbrakeTime;
		playerControls.handbrakeReleaseTime=carS.playerControls.handbrakeReleaseTime;
		playerControls.combinedAxisPedals=carS.playerControls.combinedAxisPedals;
    }
		
		EditorUtility.SetDirty(playerControls);
		
		
		//Sound controller settings
   if (options[8].flag){
		soundController.startEngineVolume=carS.sounds.startEngineVolume;
		soundController.engineVolumeMultiplier=carS.sounds.engineVolumeMultiplier;
		soundController.engineMinPitch=carS.sounds.engineMinPitch;
		soundController.engineMaxPitch=carS.sounds.engineMaxPitch;
		soundController.skidVolumeMultiplier=carS.sounds.skidVolumeMultiplier;
		soundController.skidMinPitch=carS.sounds.skidMinPitch;
		soundController.skidMaxPitch=carS.sounds.skidMaxPitch;
		soundController.turboVolumeMultiplier=carS.sounds.turboVolumeMultiplier;
		soundController.turboMinPitch=carS.sounds.turboMinPitch;
		soundController.turboMaxPitch=carS.sounds.turboMaxPitch;
		soundController.turboMinVol=carS.sounds.turboMinVol;
		soundController.turboMaxVol=carS.sounds.turboMaxVol;
		soundController.blowOffValveVolumeMultiplier=carS.sounds.blowOffValveVolumeMultiplier;
		soundController.gearsVolumeMultiplier=carS.sounds.gearsVolumeMultiplier;
		soundController.gravelVolumeMultiplier=carS.sounds.gravelVolumeMultiplier;
		soundController.gravelMinPitch=carS.sounds.gravelMinPitch;
		soundController.gravelMaxPitch=carS.sounds.gravelMaxPitch;
		soundController.gravelMaxVol=carS.sounds.gravelMaxVol;
		soundController.gravelMinVol=carS.sounds.gravelMinVol;
		soundController.grassVolume=carS.sounds.grassVolume;
		soundController.grassMinPitch=carS.sounds.grassMinPitch;
		soundController.grassMaxPitch=carS.sounds.grassMaxPitch;
		soundController.grassMaxVol=carS.sounds.grassMaxVol;
		soundController.grassMinVol=carS.sounds.grassMinVol;
		soundController.crashLowVolume=carS.sounds.crashLowVolume;
		soundController.crashHighVolume=carS.sounds.crashHighVolume;
		soundController.backFireVolumeMultiplier=carS.sounds.backFireVolumeMultiplier;
		soundController.timeBetweenBumps=carS.sounds.timeBetweenBumps;
		soundController.tireBumpVolumeMultiplier=carS.sounds.tireBumpVolumeMultiplier;
		soundController.windPitchRatio=carS.sounds.windPitchRatio;
		soundController.windVolume=carS.sounds.windVolume;
		soundController.breakSoundVolume=carS.sounds.breakSoundVolume;
		soundController.scratchVolumeMultiplier=carS.sounds.scratchVolumeMultiplier;
		soundController.scratchMinPitch=carS.sounds.scratchMinPitch;
		soundController.scratchMaxPitch=carS.sounds.scratchMaxPitch;
		
		if(options[0].flag){
		if (carS.sounds.engine !=null)
			soundController.engine=carS.sounds.engine.GetAsset<AudioClip>();
		
		if (carS.sounds.skid !=null)
			soundController.skid=carS.sounds.skid.GetAsset<AudioClip>();
		
		if (carS.sounds.turbo !=null)
			soundController.turbo=carS.sounds.turbo.GetAsset<AudioClip>();
		
		if (carS.sounds.blowOffValve !=null)
			soundController.blowOffValve=carS.sounds.blowOffValve.GetAsset<AudioClip>();
		
		if (carS.sounds.gears !=null)
			soundController.gears=carS.sounds.gears.GetAsset<AudioClip>();
		
		if (carS.sounds.gravel !=null)
			soundController.gravel=carS.sounds.gravel.GetAsset<AudioClip>();
		
		if (carS.sounds.crashLowSpeedSound !=null)
			soundController.crashLowSpeedSound=carS.sounds.crashLowSpeedSound.GetAsset<AudioClip>();
		
		if (carS.sounds.crashHighSpeedSound !=null)
			soundController.crashHighSpeedSound=carS.sounds.crashHighSpeedSound.GetAsset<AudioClip>();
		
		if (carS.sounds.backFire !=null)
			soundController.backFire=carS.sounds.backFire.GetAsset<AudioClip>();
		
		if (carS.sounds.startEngine !=null)
			soundController.startEngine=carS.sounds.startEngine.GetAsset<AudioClip>();
		
		if (carS.sounds.tireBump !=null)
			soundController.tireBump=carS.sounds.tireBump.GetAsset<AudioClip>();
		
		if (carS.sounds.wind !=null)
			soundController.wind=carS.sounds.wind.GetAsset<AudioClip>();
		
		if (carS.sounds.breakSound !=null)
			soundController.breakSound=carS.sounds.breakSound.GetAsset<AudioClip>();
		
		if (carS.sounds.grass !=null)
			soundController.grass=carS.sounds.grass.GetAsset<AudioClip>();
		
		if (carS.sounds.scratch !=null)
			soundController.scratch=carS.sounds.scratch.GetAsset<AudioClip>();
		
		if (carS.sounds.policeSiren !=null)
			soundController.policeSiren = carS.sounds.policeSiren.GetAsset<AudioClip>();
   }
		
		soundController.scratchVolume=carS.sounds.scratchVolume;
		soundController.policeSirenVolume=carS.sounds.policeSirenVolume;
		soundController.masterVolume=carS.sounds.masterVolume;
		soundController.tireBumpSensitivity=carS.sounds.tireBumpSensitivity;
		
		soundController.extraAudioClips = new List<IRDSSoundController.IRDSAudioClips>();
		foreach (CarSettings.IRDSSoundControllerS.IRDSAudioClipsS clip in carS.sounds.extraAudioClips)
		{
			IRDSSoundController.IRDSAudioClips newClip = new IRDSSoundController.IRDSAudioClips();
      if (options[0].flag){
			if (clip.audio !=null)
				newClip.audio = clip.audio.GetAsset<AudioClip>();
        if (clip.materialName != null)
						newClip.materialName = clip.materialName.GetAsset<PhysicMaterial>();
			}
			newClip.volume = clip.volume;
			newClip.volumeMultiplier = clip.volumeMultiplier;
			newClip.pitch = clip.pitch;
			newClip.minPitch = clip.minPitch;
			newClip.maxPitch = clip.maxPitch;
			newClip.minVol = clip.minVol;
			newClip.maxVol = clip.maxVol;
			newClip.volMinSpeed = clip.volMinSpeed;
			newClip.volMaxSpeed = clip.volMaxSpeed;
			newClip.pitchMinSpeed = clip.pitchMinSpeed;
			newClip.pitchMaxSpeed = clip.pitchMaxSpeed;
			newClip.loop = clip.loop;
			newClip.play = clip.play;
			newClip.playOnAwake = clip.playOnAwake;
			newClip.bypassEffects = clip.bypassEffects;
			newClip.priority = clip.priority;
			newClip.dopplerLevel = clip.dopplerLevel;
			newClip.maxDistance = clip.maxDistance;
			newClip.minDistance = clip.minDistance;
			newClip.pan = clip.pan;
			newClip.panLevel = clip.panLevel;
			newClip.spread = clip.spread;
			newClip.rolloffMode = clip.rolloffMode;
			newClip.audioName = clip.audioName;
			newClip.onOffManualCtrl = clip.onOffManualCtrl;
			newClip.onOffInput = clip.onOffInput;
			newClip.isSwitch = clip.isSwitch;
			newClip.numberOfPlaysPerEvent = clip.numberOfPlaysPerEvent;
			newClip.numberOfTimesPlayed = clip.numberOfTimesPlayed;
			newClip.endRacePlaceNumber = clip.endRacePlaceNumber;
			newClip.lastTimePlayed = clip.lastTimePlayed;
			newClip.isTimeInterval = clip.isTimeInterval;
			newClip.isPerNumberOfPlays = clip.isPerNumberOfPlays;
			
			newClip.timeInterval = clip.timeInterval;
			
			
			newClip.isTouchingMaterial = clip.isTouchingMaterial;
			newClip.playIfImCurrentCar = clip.playIfImCurrentCar;
			newClip.lastimeButtonPress = clip.lastimeButtonPress;
			newClip.events = clip.events;
			newClip.pitchOpt = clip.pitchOpt;
			newClip.volOpt = clip.volOpt;
			newClip.playOneShot = clip.playOneShot;
			newClip.rate = clip.rate;
			newClip.engineDependent = clip.engineDependent;
			newClip.buttonEvents = clip.buttonEvents;
			newClip.useCurves = clip.useCurves;
			newClip.rpmPitch = clip.rpmPitch;
			newClip.rpmVol = clip.rpmVol;
			
			newClip.throttleVol = clip.throttleVol;
			newClip.gripSpeedVol = clip.gripSpeedVol;
			newClip.gripSpeedPitch = clip.gripSpeedPitch;
			newClip.tireForceVol = clip.tireForceVol;
			newClip.tireForcePitch = clip.tireForcePitch;
			
			
			soundController.extraAudioClips.Add(newClip);
		}
		
		soundController.backFireDopplerLevel=carS.sounds.backFireDopplerLevel;
		soundController.gearsDopplerLevel=carS.sounds.gearsDopplerLevel;
		soundController.tireBumpsDopplerLevel=carS.sounds.tireBumpsDopplerLevel;
		soundController.blowOffDopplerLevel=carS.sounds.blowOffDopplerLevel;
		soundController.skidEffectsThreshold=carS.sounds.skidEffectsThreshold;
		soundController.skidEffectsSensitivity=carS.sounds.skidEffectsSensitivity;
	}
EditorUtility.SetDirty(soundController);

if (options[10].flag){
		for (int i = 0; i < wings.Length; i++)
		{
			wings[i].SetLiftCoefficient(carS.wings[i].liftCoefficient);
			wings[i].area = carS.wings[i].area;
			wings[i].angle = carS.wings[i].angle;
			if (wings[i].transform.localPosition.z <0)
			{
				if (theTarget.GetComponent<IRDSCarControllerAI>() != null)
					theTarget.GetComponent<IRDSCarControllerAI>().wing = wings[i];
			}
			EditorUtility.SetDirty(wings[i]);
		}
	}
		
	if (options[11].flag){
  	for (int i=0; i < cameraPositions.Length;i++)
		{
			if (i < carS.cameraPositions.Length){
				cameraPositions[i].distance = carS.cameraPositions[i].distance;
				cameraPositions[i].distanceSides = carS.cameraPositions[i].distanceSides;
				cameraPositions[i].fieldOfView = carS.cameraPositions[i].fieldOfView;
				cameraPositions[i].fieldOfViewChangeSpeedMultiplier = carS.cameraPositions[i].fieldOfViewChangeSpeedMultiplier;
				cameraPositions[i].getRotationFromWaypoints = carS.cameraPositions[i].getRotationFromWaypoints;
				cameraPositions[i].height = carS.cameraPositions[i].height;
				cameraPositions[i].heightDamping = carS.cameraPositions[i].heightDamping;
				cameraPositions[i].maxFieldOfView = carS.cameraPositions[i].maxFieldOfView;
				cameraPositions[i].minFieldOfView = carS.cameraPositions[i].minFieldOfView;
				cameraPositions[i].rotationDamping = carS.cameraPositions[i].rotationDamping;
				cameraPositions[i].sidesDamping = carS.cameraPositions[i].sidesDamping;
				EditorUtility.SetDirty(cameraPositions[i]);
			}
		}
	}
}
			
	[MenuItem("GameObject/iRDS/Save Settings",true)]
	public static bool ValidateSaveCarSettings()
	{
		if (Selection.activeGameObject !=null)
			return Selection.activeGameObject.GetComponent<IRDSDrivetrain>() != null || Selection.activeGameObject.GetComponent<IRDSManager>() != null;
		else return false;
	}
	
	
	
	[MenuItem("GameObject/iRDS/Save Settings",false,0)]
	public static void SaveCarSettings()
	{
		IRDSManager component = Selection.activeGameObject.GetComponent<IRDSManager>();
		
		if (component!=null){
			SaveIRDSManagerSettings();
			return;
		}
		
		
		
		
		string fileName1 = EditorUtility.SaveFilePanel("Save Car Settings","Assets/","CarSettings_" + ".xml", "xml");
		CarSettings carS = new CarSettings();
		Debug.Log("Saving Settings at: " + fileName1);
		
		
		theTarget = Selection.activeGameObject;
		airResistance = theTarget.GetComponent<IRDSAerodynamicResistance>();
		antirollBars = theTarget.GetComponents<IRDSAntiRollBar>();
		carInput = theTarget.GetComponent<IRDSCarControllInput>();
		carDamage = theTarget.GetComponent<IRDSCarDamage>();
		carVisual = theTarget.GetComponent<IRDSCarVisuals>();
		drivetrain = theTarget.GetComponent<IRDSDrivetrain>();
		playerControls = theTarget.GetComponent<IRDSPlayerControls>();
		soundController = theTarget.GetComponent<IRDSSoundController>();
		wheels = IRDSUtility.GetAllChilds<IRDSWheel>(theTarget.transform);
		wings = IRDSUtility.GetAllChilds<IRDSWing>(theTarget.transform);
		cameraPositions = IRDSUtility.GetAllChilds<IRDSCameraPosition>(theTarget.transform);
		
		//AirResistance Settings
		carS.airResistance = new IRDSSaveLoadEditor.CarSettings.IRDSAerodynamicResistanceS();
		carS.airResistance.coefficients = airResistance.GetCoefficients();
		
		//Antiroll bars settings
		carS.antiRollBars = new IRDSSaveLoadEditor.CarSettings.IRDSAntiRollBarS[antirollBars.Length];
		for (int i = 0; i < antirollBars.Length; i++){
			carS.antiRollBars[i] = new IRDSSaveLoadEditor.CarSettings.IRDSAntiRollBarS();
			carS.antiRollBars[i].coefficient = antirollBars[i].GetCoefficient();
			carS.antiRollBars[i].wheel1 = antirollBars[i].wheel1.transform.name;
			carS.antiRollBars[i].wheel2 = antirollBars[i].wheel2.transform.name;
		}
		
		// IRDSCarControllInput settings
		carS.controlInput = new IRDSSaveLoadEditor.CarSettings.IRDSCarControllInputS();
		
		carS.controlInput.ABSSlip =carInput.GetABSSlip();
		carS.controlInput.TCLSlip =carInput.GetTCLSlip();
		carS.controlInput.TCLminSPD =carInput.GetTCLMinSPD();
		carS.controlInput.escFactor =carInput.GetescFactor();
		carS.controlInput.steerHelp =carInput.GetSteerHelpFactor();
		carS.controlInput.inertiaFactor =carInput.inertiaFactor;
		
		
		carS.controlInput.shiftSpeed =carInput.shiftSpeed;
		carS.controlInput.centerOfMass =carInput.centerOfMass.name;
		carS.controlInput.absEnable =carInput.absEnable;
		carS.controlInput.escEnable =carInput.escEnable;
		carS.controlInput.tclEnable =carInput.tclEnable;
		carS.controlInput.steerHelpEnable =carInput.steerHelpEnable;
		carS.controlInput.startEngine =carInput.startEngine;
		carS.controlInput.isTopSpeedLimited =carInput.isTopSpeedLimited;
		carS.controlInput.topSpeedLimit =carInput.topSpeedLimit;
		
		
		//IRDSCarDamage settings
		carS.carDamage = new IRDSSaveLoadEditor.CarSettings.IRDSCarDamageS();
		carS.carDamage.disableDamage = carDamage.disableDamage;
		carS.carDamage.maxMoveDelta = carDamage.GetMaxMoveDelta();
		carS.carDamage.maxNoiseDeform = carDamage.GetMaxNoiseDeform();
		carS.carDamage.YforceDamp = carDamage.GetYForceDamp();
		carS.carDamage.demolutionRange = carDamage.GetDemolutionRange();
		carS.carDamage.maxRotationDelta = carDamage.GetMaxRotationDelta();
		carS.carDamage.minForce = carDamage.GetMinForce();
		carS.carDamage.repairDelta = carDamage.GetRepairDelta();
		
		//Scratch Sparks under Carvisuals tab on the Car Setup script
		carS.carDamage.maxParticleSizeFactor = carDamage.maxParticleSizeFactor;
		carS.carDamage.maxParticleEnergyFactor = carDamage.maxParticleEnergyFactor;
		carS.carDamage.maxMaxSize = carDamage.maxMaxSize;
		carS.carDamage.maxMaxEnergy = carDamage.maxMaxEnergy;
		carS.carDamage.sparksTime = carDamage.sparksTime;
		
		if (carDamage.GetOptionalMeshList() == null)
			carS.carDamage.optionalMeshList = "null";
		else
			carS.carDamage.optionalMeshList = carDamage.GetOptionalMeshList().transform.name;
		
		carS.carDamage.optionalColliderList = new string[carDamage.GetOptionalColliderList().Length];
		for (int i = 0; i < carDamage.GetOptionalColliderList().Length;i++)
			carS.carDamage.optionalColliderList[i] = carDamage.GetOptionalColliderList()[i].transform.name;
		
		carS.carDamage.optionalTransformList = new string[carDamage.GetOptionalTransformList().Length];
		for (int i = 0; i < carDamage.GetOptionalTransformList().Length;i++)
			carS.carDamage.optionalTransformList[i] = carDamage.GetOptionalTransformList()[i].name;
		
		carS.carDamage.disableDamage = carDamage.disableDamage;
		carS.carDamage.sparksShurikenSpeedFactor = carDamage.sparksShurikenSpeedFactor;
		carS.carDamage.minSparkShurikenParticles = carDamage.minSparkShurikenParticles;
		carS.carDamage.maxSparkShurikenParticles = carDamage.maxSparkShurikenParticles;
		
		if (carDamage.sparksShuriken == null)
			carS.carDamage.sparksShuriken = "null";
		else
			carS.carDamage.sparksShuriken = carDamage.sparksShuriken.transform.name;
		
		carS.carDamage.ignoreLayer = carDamage.ignoreLayer;
		
		
		//IRDSCarVisual Settings
		carS.carVisual = new IRDSSaveLoadEditor.CarSettings.IRDSCarVisualsS();
		carS.carVisual.steeringIRDSWheelMaxAngle = carVisual.steeringIRDSWheelMaxAngle;
		carS.carVisual.brakeIntensityRate = carVisual.brakeIntensityRate;
		carS.carVisual.frontTiresSkidmarksWidth = carVisual.frontTiresSkidmarksWidth;
		carS.carVisual.rearTiresSkidmarksWidth = carVisual.rearTiresSkidmarksWidth;
		carS.carVisual.frontTiresSkidmarkOffset = carVisual.frontTiresSkidmarkOffset;
		carS.carVisual.rearTiresSkidmarkOffset = carVisual.rearTiresSkidmarkOffset;
		carS.carVisual.skidEffectsThreshold = carVisual.skidEffectsThreshold;
		carS.carVisual.skidSmokeThreshold = carVisual.skidSmokeThreshold;
		carS.carVisual.skidMarkThreshold = carVisual.skidMarkThreshold;
		
		carS.carVisual.useBackFireForNitro = carVisual.useBackFireForNitro;
		carS.carVisual.particleMultiplier = carVisual.particleMultiplier;
		carS.carVisual.useBackFireLight = carVisual.useBackFireLight;
		carS.carVisual.lightMultiplier = carVisual.lightMultiplier;
		carS.carVisual.lightRange = carVisual.lightRange;
		carS.carVisual.backFireLightColor = carVisual.backFireLightColor;
		
		if (carVisual.steeringIRDSWheel == null)
			carS.carVisual.steeringIRDSWheel = "null";
		else
			carS.carVisual.steeringIRDSWheel = carVisual.steeringIRDSWheel.name;
		
		carS.carVisual.brakeLightsTransform = new string[carVisual.brakeLightsTransform.Length];
		for (int i =0;i< carVisual.brakeLightsTransform.Length;i++)
			carS.carVisual.brakeLightsTransform[i] = carVisual.brakeLightsTransform[i].name;
		
		
		
		carS.carVisual.backFireLights = new string[carVisual.backFireLights.Length];
		for (int i =0;i< carVisual.backFireLights.Length;i++)
			carS.carVisual.backFireLights[i] = carVisual.backFireLights[i].transform.name;
		
		if (carVisual.backfireSystem == null)
			carS.carVisual.backfireSystem = "null";
		else
			carS.carVisual.backfireSystem = carVisual.backfireSystem.transform.name;
		
		if (carVisual.nitroParticleSystem == null)
			carS.carVisual.nitroParticleSystem = "null";
		else
			carS.carVisual.nitroParticleSystem = carVisual.nitroParticleSystem.transform.name;
		carS.carVisual.useNitroLight = carVisual.useNitroLight;
		
		
		carS.carVisual.nitroLights = new string[carVisual.nitroLights.Length];
		for (int i =0;i< carVisual.nitroLights.Length;i++)
			carS.carVisual.nitroLights[i] = carVisual.nitroLights[i].transform.name;
		
		
		carS.carVisual.nitroParticleMultiplier = carVisual.nitroParticleMultiplier;
		carS.carVisual.nitroLightMultiplier = carVisual.nitroLightMultiplier;
		carS.carVisual.nitroLightRange = carVisual.nitroLightRange;
		carS.carVisual.nitroLightColor = carVisual.nitroLightColor;
		carS.carVisual.activateNitroEffect = carVisual.activateNitroEffect;
		carS.carVisual.activateBackFireEffect = carVisual.activateBackFireEffect;
		carS.carVisual.propertyName = carVisual.propertyName;
		carS.carVisual.onOffBrakeLightsRenderers = carVisual.onOffBrakeLightsRenderers;
		
		carS.carVisual.brakeLightRenderers = new string[carVisual.brakeLightRenderers.Length];
		for (int i =0;i< carVisual.brakeLightRenderers.Length;i++)
			carS.carVisual.brakeLightRenderers[i] = carVisual.brakeLightRenderers[i].transform.name;
		
		
		
		
		
		//IRDSDrivetrain settings
		carS.drivetrain = new IRDSSaveLoadEditor.CarSettings.IRDSDrivetrainS();
		carS.drivetrain.differentialLockCoefficient = drivetrain.GetDifferentialLockCoefficient();
		carS.drivetrain.engineCrankTime = drivetrain.engineCrankTime;
		carS.drivetrain.engineStartingTime = drivetrain.engineStartingTime;
		carS.drivetrain.fuelConsumptionMultiplier = drivetrain.fuelConsumptionMultiplier;
		carS.drivetrain.nitroFuel = drivetrain.nitroFuel;
		carS.drivetrain.nitroBoostDurability = drivetrain.nitroBoostDurability;
		carS.drivetrain.maxPower = drivetrain.GetMaxPower();
		carS.drivetrain.powerRPM = drivetrain.GetPowerRPM();
		carS.drivetrain.minRPM = drivetrain.GetOriginalMinRPM();
		carS.drivetrain.maxRPM = drivetrain.GetMaxRPM();
		carS.drivetrain.maxTorque = drivetrain.GetMaxTorque();
		carS.drivetrain.torqueRPM = drivetrain.GetTorqueRPM();
		carS.drivetrain.PSI = drivetrain.GetPSI();
		carS.drivetrain.turboRPM = drivetrain.GetTurboRPM();
		carS.drivetrain.engineInertia = drivetrain.GetEngineInertia();
		carS.drivetrain.engineBaseFriction = drivetrain.GetEngineBaseFriction();
		carS.drivetrain.engineRPMFriction = drivetrain.GetEngineRPMFriction();
		carS.drivetrain.engineOrientation = drivetrain.GetEngineOrientation();
		
		//Drivetrain Gear settings
		
		carS.drivetrain.automatic = drivetrain.GetAutomatic();
		carS.drivetrain.automaticClucth = drivetrain.automaticClucth;
		carS.drivetrain.shiftUpRpmPercent = drivetrain.shiftUpRpmPercent;
		carS.drivetrain.shiftDownRpmPercent = drivetrain.shiftDownRpmPercent;
		carS.drivetrain.gearRatios = drivetrain.GetGearRatios();
		carS.drivetrain.finalDriveRatio = drivetrain.GetFinalDriveRatio();
		carS.drivetrain.ShiftSpeed = drivetrain.ShiftSpeed;
		
		carS.drivetrain.fuelConsume = drivetrain.GetFuelConsume();
		
		carS.drivetrain.poweredIRDSWheels = drivetrain.GetDrivetrain();
		carS.drivetrain.startEngine = drivetrain.startEngine;
		
		
		carS.drivetrain.engineVacuumFriction = drivetrain.engineVacuumFriction;
		carS.drivetrain.engineVacuumRPMFriction = drivetrain.engineVacuumRPMFriction;
		carS.drivetrain.engineShakeAmount = drivetrain.engineShakeAmount;
		carS.drivetrain.engineMaxShake = drivetrain.engineMaxShake;
		carS.drivetrain.baseShake = drivetrain.baseShake;
		carS.drivetrain.idleRPMRoughness = drivetrain.idleRPMRoughness;
		carS.drivetrain.engineRevUpToqrueMultiplier = drivetrain.engineRevUpToqrueMultiplier;
		carS.drivetrain.autoHeel_ToeBrakeTech = drivetrain.autoHeel_ToeBrakeTech;
		carS.drivetrain.revLimiterRPM = drivetrain.revLimiterRPM;
		carS.drivetrain.autoThrottleForShifting = drivetrain.autoThrottleForShifting;
		carS.drivetrain.heel_toeRPMPercent = drivetrain.heel_toeRPMPercent;
		carS.drivetrain.heel_toeMinThrottle = drivetrain.heel_toeMinThrottle;
		carS.drivetrain.heel_toeMaxThrottle = drivetrain.heel_toeMaxThrottle;
		carS.drivetrain.autoThrottleForShiftingSpeed = drivetrain.autoThrottleForShiftingSpeed;
		carS.drivetrain.maxThrottleToShiftDown = drivetrain.maxThrottleToShiftDown;
		carS.drivetrain.minThrottleToShiftup = drivetrain.minThrottleToShiftup;
		carS.drivetrain.minRPMPercentageToShiftDown = drivetrain.minRPMPercentageToShiftDown;
		carS.drivetrain.maxRPMPercentageToShiftUp = drivetrain.maxRPMPercentageToShiftUp;
		
		
		
		
		// Tire, brakes and suspension settings
		carS.wheels = new IRDSSaveLoadEditor.CarSettings.IRDSWheelS[wheels.Length];
		for (int i = 0; i < wheels.Length;i++)
		{
			carS.wheels[i] = new IRDSSaveLoadEditor.CarSettings.IRDSWheelS();
			//Tire settings
			carS.wheels[i].radius = wheels[i].GetRadius();
			carS.wheels[i].inertia = wheels[i].GetInertia();
			carS.wheels[i].grip = wheels[i].GetGrip();
			carS.wheels[i].staticFrictionCoefficient = wheels[i].staticFrictionCoefficient;
			carS.wheels[i].frictionTorque = wheels[i].GetFrictionTorque();
			carS.wheels[i].tyreHardness = wheels[i].GetTyreHardness();
			carS.wheels[i].a = wheels[i].GetACoefficients();
			carS.wheels[i].b = wheels[i].GetBCoefficients();
			carS.wheels[i].cCoefficients = wheels[i].GetCCoefficients();
			
			//Brake settings
			carS.wheels[i].brakeFrictionTorque = wheels[i].GetBrakeFrictionTorque();
			carS.wheels[i].handbrakeFrictionTorque = wheels[i].GetHandBrakeFrictionTorque();
			
			//Suspension settings
			carS.wheels[i].massFraction = wheels[i].GetMassFraction();
			carS.wheels[i].maxSteeringAngle = wheels[i].GetMaxSteeringAngle();
			carS.wheels[i].CamberAngle = wheels[i].GetCamberAngle();
			carS.wheels[i].ToeAngle = wheels[i].GetToeAngle();
			carS.wheels[i].suspensionTravel = wheels[i].GetSuspensionTravel();
			carS.wheels[i].springForceRatio = wheels[i].GetSpringForceRatio();
			carS.wheels[i].wheelPosition= wheels[i].GetWheelPosition();
			carS.wheels[i].dampingRatio= wheels[i].GetDampingRatio();
			carS.wheels[i].wear= wheels[i].GetWear();
			carS.wheels[i].ackermanFactor= wheels[i].ackermanFactor;
			carS.wheels[i].camberSteeringFactor= wheels[i].camberSteeringFactor;
			carS.wheels[i].isFront= wheels[i].isFront;
			carS.wheels[i].isLeft= wheels[i].isLeft;
			carS.wheels[i].oldSuspension= wheels[i].oldSuspension;
			carS.wheels[i].newSuspensionRebound= wheels[i].newSuspensionRebound;
			carS.wheels[i].newSuspensionBounce= wheels[i].newSuspensionBounce;
			carS.wheels[i].newSuspensionSpring= wheels[i].newSuspensionSpring;
			carS.wheels[i].newSuspensionBumpStopRate= wheels[i].newSuspensionBumpStopRate;
			carS.wheels[i].newSuspensionSpringFactorMin= wheels[i].newSuspensionSpringFactorMin;
			carS.wheels[i].newSuspensionSpringFactorMax= wheels[i].newSuspensionSpringFactorMax;
			carS.wheels[i].newSuspensionDampFactorMin= wheels[i].newSuspensionDampFactorMin;
			carS.wheels[i].newSuspensionDampFactorMax= wheels[i].newSuspensionDampFactorMax;
			carS.wheels[i].slipRatioClamp= wheels[i].slipRatioClamp;
			carS.wheels[i].slipAngleClamp= wheels[i].slipAngleClamp;
			carS.wheels[i].multiplierFY= wheels[i].multiplierFX;
			carS.wheels[i].multiplierFX= wheels[i].multiplierFY;
			carS.wheels[i].tireAngles= wheels[i].tireAngles;
			carS.wheels[i].maxCamberAngle=wheels[i].maxCamberAngle;
			carS.wheels[i].suspensionType=wheels[i].suspensionType;
			carS.wheels[i].basicSuspension=wheels[i].basicSuspension;
			carS.wheels[i].macPhersonSuspension=wheels[i].macPhersonSuspension;
			carS.wheels[i].wishBoneSuspension=wheels[i].wishBoneSuspension;
			carS.wheels[i].wheelieMultiplier=wheels[i].wheelieMultiplier;
			carS.wheels[i].isPoweredWheel=wheels[i].isPoweredWheel;
			carS.wheels[i].contactPatchType=wheels[i].contactPatchType;
			carS.wheels[i].layers=wheels[i].layers;
		}
		
		
		
		//Player controls settings
		carS.playerControls = new IRDSSaveLoadEditor.CarSettings.IRDSPlayerControlsS();
		carS.playerControls.throttleTime = playerControls.GetThrottleTime();
		carS.playerControls.throttleTimeTraction = playerControls.GetThrottleTimeTraction();
		carS.playerControls.throttleReleaseTime = playerControls.GetThrottleReleaseTime();
		carS.playerControls.throttleReleaseTimeTraction = playerControls.GetThrottleReleaseTimeTraction();
		carS.playerControls.steerTime = playerControls.GetSteerTime();
		carS.playerControls.veloSteerTime = playerControls.GetVeloSteerTime();
		carS.playerControls.steerReleaseTime = playerControls.GetSteerReleaseTime();
		carS.playerControls.veloSteerReleaseTime = playerControls.GetVeloSteerReleaseTime();
		carS.playerControls.steerCorrectionFactor = playerControls.GetSteerCorrectionFactor();
		
		carS.playerControls.clutchTime = playerControls.clutchTime;
		carS.playerControls.clutchReleaseTime = playerControls.clutchReleaseTime;
		
		carS.playerControls.steeringAxis = playerControls.steeringAxis;
		
		carS.playerControls.throttleAxis = playerControls.throttleAxis;
		carS.playerControls.brakeAxis = playerControls.brakeAxis;
		carS.playerControls.shiftUpButton = playerControls.shiftUpButton;
		carS.playerControls.shiftDownButton = playerControls.shiftDownButton;
		carS.playerControls.handbrakeButton = playerControls.handbrakeButton;
		carS.playerControls.clutchAxis = playerControls.clutchAxis;
		carS.playerControls.startEngineButton = playerControls.startEngineButton;
		carS.playerControls.nitroButton = playerControls.nitroButton;
		
		carS.playerControls.throttleKey = playerControls.throttleKey;
		carS.playerControls.altThrottleKey = playerControls.altThrottleKey;
		carS.playerControls.brakesKey = playerControls.brakesKey;
		carS.playerControls.leftKey = playerControls.leftKey;
		carS.playerControls.rightKey = playerControls.rightKey;
		carS.playerControls.handbrakeKey = playerControls.handbrakeKey;
		carS.playerControls.shiftUpKey = playerControls.shiftUpKey;
		carS.playerControls.shiftDownKey = playerControls.shiftDownKey;
		carS.playerControls.clutchKey = playerControls.clutchKey;
		carS.playerControls.startEngineKey = playerControls.startEngineKey;
		carS.playerControls.nitroKey = playerControls.nitroKey;
		
		
		carS.playerControls.steeringButtonLeft = playerControls.steeringButtonLeft;
		carS.playerControls.steeringButtonRight = playerControls.steeringButtonRight;
		carS.playerControls.overrideThrottleAxis = playerControls.overrideThrottleAxis;
		carS.playerControls.throttleButton = playerControls.throttleButton;
		carS.playerControls.overrideBrakeAxis = playerControls.overrideBrakeAxis;
		carS.playerControls.brakeButton = playerControls.brakeButton;
		carS.playerControls.clutchButton = playerControls.clutchButton;
		carS.playerControls.steerRawInput = playerControls.steerRawInput;
		carS.playerControls.throttleRawInput = playerControls.throttleRawInput;
		carS.playerControls.brakeRawInput = playerControls.brakeRawInput;
		carS.playerControls.clutchRawInput = playerControls.clutchRawInput;
		carS.playerControls.activateSirenKey = playerControls.activateSirenKey;
		carS.playerControls.activateSirenButton = playerControls.activateSirenButton;
		carS.playerControls.gearShifterButtons = playerControls.gearShifterButtons;
		carS.playerControls.useGearShifter = playerControls.useGearShifter;
		carS.playerControls.overrideNitroInput = playerControls.overrideNitroInput;
		carS.playerControls.handbrakeTime = playerControls.handbrakeTime;
		carS.playerControls.handbrakeReleaseTime = playerControls.handbrakeReleaseTime;
		carS.playerControls.combinedAxisPedals = playerControls.combinedAxisPedals;
		
		
		
		
		//Sound controller settings
		carS.sounds = new IRDSSaveLoadEditor.CarSettings.IRDSSoundControllerS();
		carS.sounds.startEngineVolume = soundController.startEngineVolume;
		carS.sounds.engineVolumeMultiplier = soundController.engineVolumeMultiplier;
		carS.sounds.engineMinPitch = soundController.engineMinPitch;
		carS.sounds.engineMaxPitch = soundController.engineMaxPitch;
		carS.sounds.skidVolumeMultiplier = soundController.skidVolumeMultiplier;
		carS.sounds.skidMinPitch = soundController.skidMinPitch;
		carS.sounds.skidMaxPitch = soundController.skidMaxPitch;
		carS.sounds.turboVolumeMultiplier = soundController.turboVolumeMultiplier;
		carS.sounds.turboMinPitch = soundController.turboMinPitch;
		carS.sounds.turboMaxPitch = soundController.turboMaxPitch;
		carS.sounds.turboMinVol = soundController.turboMinVol;
		carS.sounds.turboMaxVol = soundController.turboMaxVol;
		carS.sounds.blowOffValveVolumeMultiplier = soundController.blowOffValveVolumeMultiplier;
		carS.sounds.gearsVolumeMultiplier = soundController.gearsVolumeMultiplier;
		carS.sounds.gravelVolumeMultiplier = soundController.gravelVolumeMultiplier;
		carS.sounds.gravelMinPitch = soundController.gravelMinPitch;
		carS.sounds.gravelMaxPitch = soundController.gravelMaxPitch;
		carS.sounds.gravelMaxVol = soundController.gravelMaxVol;
		carS.sounds.gravelMinVol = soundController.gravelMinVol;
		carS.sounds.grassVolume = soundController.grassVolume;
		carS.sounds.grassMinPitch = soundController.grassMinPitch;
		carS.sounds.grassMaxPitch = soundController.grassMaxPitch;
		carS.sounds.grassMaxVol = soundController.grassMaxVol;
		carS.sounds.grassMinVol = soundController.grassMinVol;
		carS.sounds.crashLowVolume = soundController.crashLowVolume;
		carS.sounds.crashHighVolume = soundController.crashHighVolume;
		carS.sounds.backFireVolumeMultiplier = soundController.backFireVolumeMultiplier;
		carS.sounds.timeBetweenBumps = soundController.timeBetweenBumps;
		carS.sounds.tireBumpVolumeMultiplier = soundController.tireBumpVolumeMultiplier;
		carS.sounds.windPitchRatio = soundController.windPitchRatio;
		carS.sounds.windVolume = soundController.windVolume;
		carS.sounds.breakSoundVolume = soundController.breakSoundVolume;
		carS.sounds.scratchVolumeMultiplier = soundController.scratchVolumeMultiplier;
		carS.sounds.scratchMinPitch = soundController.scratchMinPitch;
		carS.sounds.scratchMaxPitch = soundController.scratchMaxPitch;
		
		if (soundController.engine)
			carS.sounds.engine = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.engine);
		if (soundController.skid)
			carS.sounds.skid = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.skid);
		if (soundController.turbo)
			carS.sounds.turbo = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.turbo);
		if (soundController.blowOffValve)
			carS.sounds.blowOffValve = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.blowOffValve);
		if (soundController.gears)
			carS.sounds.gears = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.gears);
		if (soundController.gravel)
			carS.sounds.gravel = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.gravel);
		if (soundController.crashLowSpeedSound)
			carS.sounds.crashLowSpeedSound = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.crashLowSpeedSound);
		if (soundController.crashHighSpeedSound)
			carS.sounds.crashHighSpeedSound = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.crashHighSpeedSound);
		if (soundController.backFire)
			carS.sounds.backFire = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.backFire);
		if (soundController.startEngine)
			carS.sounds.startEngine = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.startEngine);
		if (soundController.tireBump)
			carS.sounds.tireBump = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.tireBump);
		if (soundController.wind)
			carS.sounds.wind = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.wind);
		if (soundController.breakSound)
			carS.sounds.breakSound = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.breakSound);
		if (soundController.grass)
			carS.sounds.grass = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.grass);
		if (soundController.scratch)
			carS.sounds.scratch = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.scratch);
		if (soundController.policeSiren)
			carS.sounds.policeSiren = new IRDSSaveLoadEditor.CarSettings.AssetPath(soundController.policeSiren);
		
		carS.sounds.scratchVolume = soundController.scratchVolume;
		carS.sounds.policeSirenVolume = soundController.policeSirenVolume;
		carS.sounds.masterVolume = soundController.masterVolume;
		carS.sounds.tireBumpSensitivity = soundController.tireBumpSensitivity;
		
		carS.sounds.backFireDopplerLevel = soundController.backFireDopplerLevel;
		carS.sounds.gearsDopplerLevel = soundController.gearsDopplerLevel;
		carS.sounds.tireBumpsDopplerLevel = soundController.tireBumpsDopplerLevel;
		carS.sounds.blowOffDopplerLevel = soundController.blowOffDopplerLevel;
		carS.sounds.skidEffectsThreshold = soundController.skidEffectsThreshold;
		carS.sounds.skidEffectsSensitivity = soundController.skidEffectsSensitivity;
		
		//Extra AudioClips serialization
		carS.sounds.extraAudioClips = new IRDSSaveLoadEditor.CarSettings.IRDSSoundControllerS.IRDSAudioClipsS[soundController.extraAudioClips.Count ];
		int c = 0;
		foreach (IRDSSoundController.IRDSAudioClips clip in soundController.extraAudioClips)
		{
			carS.sounds.extraAudioClips[c] = new IRDSSaveLoadEditor.CarSettings.IRDSSoundControllerS.IRDSAudioClipsS();
			
			if (clip.audio)
				carS.sounds.extraAudioClips[c].audio = new IRDSSaveLoadEditor.CarSettings.AssetPath(clip.audio);
			
			carS.sounds.extraAudioClips[c].volume = clip.volume;
			carS.sounds.extraAudioClips[c].volumeMultiplier = clip.volumeMultiplier;
			carS.sounds.extraAudioClips[c].pitch = clip.pitch;
			carS.sounds.extraAudioClips[c].minPitch = clip.minPitch;
			carS.sounds.extraAudioClips[c].maxPitch = clip.maxPitch;
			carS.sounds.extraAudioClips[c].minVol = clip.minVol;
			carS.sounds.extraAudioClips[c].maxVol = clip.maxVol;
			carS.sounds.extraAudioClips[c].volMinSpeed = clip.volMinSpeed;
			carS.sounds.extraAudioClips[c].volMaxSpeed = clip.volMaxSpeed;
			carS.sounds.extraAudioClips[c].pitchMinSpeed = clip.pitchMinSpeed;
			carS.sounds.extraAudioClips[c].pitchMaxSpeed = clip.pitchMaxSpeed;
			carS.sounds.extraAudioClips[c].loop = clip.loop;
			carS.sounds.extraAudioClips[c].play = clip.play;
			carS.sounds.extraAudioClips[c].playOnAwake = clip.playOnAwake;
			carS.sounds.extraAudioClips[c].bypassEffects = clip.bypassEffects;
			carS.sounds.extraAudioClips[c].priority = clip.priority;
			carS.sounds.extraAudioClips[c].dopplerLevel = clip.dopplerLevel;
			carS.sounds.extraAudioClips[c].maxDistance = clip.maxDistance;
			carS.sounds.extraAudioClips[c].minDistance = clip.minDistance;
			carS.sounds.extraAudioClips[c].pan = clip.pan;
			carS.sounds.extraAudioClips[c].panLevel = clip.panLevel;
			carS.sounds.extraAudioClips[c].spread = clip.spread;
			carS.sounds.extraAudioClips[c].rolloffMode = clip.rolloffMode;
			carS.sounds.extraAudioClips[c].audioName = clip.audioName;
			carS.sounds.extraAudioClips[c].onOffManualCtrl = clip.onOffManualCtrl;
			carS.sounds.extraAudioClips[c].onOffInput = clip.onOffInput;
			carS.sounds.extraAudioClips[c].isSwitch = clip.isSwitch;
			carS.sounds.extraAudioClips[c].numberOfPlaysPerEvent = clip.numberOfPlaysPerEvent;
			carS.sounds.extraAudioClips[c].numberOfTimesPlayed = clip.numberOfTimesPlayed;
			carS.sounds.extraAudioClips[c].endRacePlaceNumber = clip.endRacePlaceNumber;
			carS.sounds.extraAudioClips[c].lastTimePlayed = clip.lastTimePlayed;
			carS.sounds.extraAudioClips[c].isTimeInterval = clip.isTimeInterval;
			carS.sounds.extraAudioClips[c].isPerNumberOfPlays = clip.isPerNumberOfPlays;
			
			carS.sounds.extraAudioClips[c].timeInterval = clip.timeInterval;
			
			if (clip.materialName !=null)
				carS.sounds.extraAudioClips[c].materialName = new IRDSSaveLoadEditor.CarSettings.AssetPath( clip.materialName);
			
			carS.sounds.extraAudioClips[c].isTouchingMaterial = clip.isTouchingMaterial;
			carS.sounds.extraAudioClips[c].playIfImCurrentCar = clip.playIfImCurrentCar;
			carS.sounds.extraAudioClips[c].lastimeButtonPress = clip.lastimeButtonPress;
			carS.sounds.extraAudioClips[c].events = clip.events;
			carS.sounds.extraAudioClips[c].pitchOpt = clip.pitchOpt;
			carS.sounds.extraAudioClips[c].volOpt = clip.volOpt;
			carS.sounds.extraAudioClips[c].playOneShot = clip.playOneShot;
			carS.sounds.extraAudioClips[c].rate = clip.rate;
			carS.sounds.extraAudioClips[c].engineDependent = clip.engineDependent;
			carS.sounds.extraAudioClips[c].buttonEvents = clip.buttonEvents;
			carS.sounds.extraAudioClips[c].useCurves = clip.useCurves;
			carS.sounds.extraAudioClips[c].rpmPitch = clip.rpmPitch;
			carS.sounds.extraAudioClips[c].rpmVol = clip.rpmVol;
			
			carS.sounds.extraAudioClips[c].throttleVol = clip.throttleVol;
			carS.sounds.extraAudioClips[c].gripSpeedVol = clip.gripSpeedVol;
			carS.sounds.extraAudioClips[c].gripSpeedPitch = clip.gripSpeedPitch;
			carS.sounds.extraAudioClips[c].tireForceVol = clip.tireForceVol;
			carS.sounds.extraAudioClips[c].tireForcePitch = clip.tireForcePitch;
			
			
			c++;
		}
		
		
		
		carS.wings = new IRDSSaveLoadEditor.CarSettings.IRDSWingS[wings.Length]; 
		for (int i = 0; i < wings.Length; i++)
		{
			carS.wings[i] = new IRDSSaveLoadEditor.CarSettings.IRDSWingS();
			carS.wings[i].liftCoefficient = wings[i].GetLiftCoefficient();
			carS.wings[i].area = wings[i].area;
			carS.wings[i].angle = wings[i].angle;
		}
		
		
		
		carS.cameraPositions = new IRDSSaveLoadEditor.CarSettings.IRDSCameraPositionS[cameraPositions.Length];
		for (int i=0; i < cameraPositions.Length;i++)
		{
			carS.cameraPositions[i] = new IRDSSaveLoadEditor.CarSettings.IRDSCameraPositionS();
			carS.cameraPositions[i].distance = cameraPositions[i].distance;
			carS.cameraPositions[i].distanceSides = cameraPositions[i].distanceSides;
			carS.cameraPositions[i].fieldOfView = cameraPositions[i].fieldOfView;
			carS.cameraPositions[i].fieldOfViewChangeSpeedMultiplier = cameraPositions[i].fieldOfViewChangeSpeedMultiplier;
			carS.cameraPositions[i].getRotationFromWaypoints = cameraPositions[i].getRotationFromWaypoints;
			carS.cameraPositions[i].height = cameraPositions[i].height;
			carS.cameraPositions[i].heightDamping = cameraPositions[i].heightDamping;
			carS.cameraPositions[i].maxFieldOfView = cameraPositions[i].maxFieldOfView;
			carS.cameraPositions[i].minFieldOfView = cameraPositions[i].minFieldOfView;
			carS.cameraPositions[i].rotationDamping = cameraPositions[i].rotationDamping;
			carS.cameraPositions[i].sidesDamping = cameraPositions[i].sidesDamping;
		}
		
		
		Save<CarSettings>(fileName1, carS);	
	}
	

	public static void Save<T>(string path, T obj)
	{
		var serializer = new XmlSerializer(typeof(T));
		using(StreamWriter stream = new StreamWriter( new FileStream(path, FileMode.Create), System.Text.Encoding.UTF8 ))
		{
			serializer.Serialize(stream, obj);
		}
	}
	
	public static T Load<T>(string path)
	{
		var serializer = new XmlSerializer(typeof(T));
		using(StreamReader stream = new StreamReader( new FileStream(path, FileMode.Open),System.Text.Encoding.UTF8))
		{
			return (T)serializer.Deserialize(stream) ;
		}
	}
	
	
}
