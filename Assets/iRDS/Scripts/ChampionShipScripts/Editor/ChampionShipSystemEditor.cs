using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using System.Reflection;
using IRDS.ChampionshipSystem;



[CustomEditor(typeof(ChampionShipSystem))]
public class ChampionShipSystemEditor : Editor {

	
	private int globlalSelections;
	private int selections;
	private Object[] logos;
	private ChampionShipSystem ChampSystem;
	
	public class IRDSDriversGrids{
		public string driverName;
		public bool isAI;
		public int playerNumber;
		public int myCurrentPosition;
	}
	
	public int selectedValue=0;
	public int layaoutSelected = 0;
	public int selections1 = 0;
	public GUIContent[] layoutContent;
	public int[] values;
	public int selectedAISubOption = 0;
	public string[] selectedAISubOptionDesc;
	public int selectedTeamDriversOption = 0;
	public string[] selectedTeamDriversOptionDesc;
//	SerializedObject serializedLevelload;
	string[] championShipsAvailable;
	int championShipSelected = 0;
	
	public void OnEnable()
	{
		selectedAISubOptionDesc = new string[2];
		selectedAISubOptionDesc[0] = "Teams\nSettings";
		selectedAISubOptionDesc[1] = "Drivers\nSettings";
		selectedTeamDriversOptionDesc = new string[2];
		selectedTeamDriversOptionDesc[0] = "Cars";
		selectedTeamDriversOptionDesc[1] = "Drivers";
		ChampSystem = (ChampionShipSystem)target;
		ResetChampionShipsNames();
		logos = Resources.LoadAll("Pictures",typeof (Texture2D));
		UpdateTeamSelections();
		UpdateTeamDrivers();
		throwWarning = !CheckScene(ChampSystem.mainMenuScene);

		Assembly myAsembly = Assembly.GetAssembly(typeof(IRDSWPManagerEditor));
		string[] rNames = myAsembly.GetManifestResourceNames();
		if (rNames.Length >0){
			logos = new Object[rNames.Length];
			for (int i = 0;i < rNames.Length;i++){
				Stream _imageStrean = myAsembly.GetManifestResourceStream(rNames[i]);
				if (_imageStrean != null){
					Texture2D newTexture = new Texture2D(1,1);
					newTexture.hideFlags = HideFlags.HideAndDontSave;
					byte[] imageDate = new byte[_imageStrean.Length];
					_imageStrean.Read(imageDate,0,(int)_imageStrean.Length);
					newTexture.LoadImage(imageDate);
					logos[i] = newTexture;
				}
			}
		}else logos = Resources.LoadAll("Pictures",typeof (Texture2D));
	}
	
	/// <summary>
	/// Resets the champion ships names.
	/// </summary>
	void ResetChampionShipsNames()
	{
		championShipsAvailable = new string[ChampSystem.championShipData.Count];
		for (int i = 0; i < championShipsAvailable.Length;i++)
		{
			championShipsAvailable[i] = ChampSystem.championShipData[i].ChampionShipName;
		}
	}

	bool throwWarning = true;

	bool CheckScene(string sceneName)
	{
		for (int i = 0; i < EditorBuildSettings.scenes.Length;i++){
			string[] result = EditorBuildSettings.scenes[i].path.Split('/');
			result = result[result.Length-1].Split('.');
			if (result[0].Equals(sceneName)){
				return true;
			}
		}
		return false;
	}
	
	
	public override void OnInspectorGUI(){
		if (championShipSelected > ChampSystem.championShipData.Count || championShipSelected <0)championShipSelected = 0;
		EditorGUILayout.BeginVertical(EditorStyles.textField, GUILayout.Width(Screen.width-45));
		GUI.changed = false;
		GUILayout.Space(25);
		
		GUILayoutOption[] options = new GUILayoutOption[2];
		options[0] =GUILayout.Width(250);
		options[1] = GUILayout.Height(150);
		
		GUILayout.BeginHorizontal();
		GUIStyle style = new GUIStyle(GUI.skin.label);
		style.alignment = TextAnchor.UpperCenter;
		style.imagePosition = ImagePosition.ImageAbove;
		
		Rect logoRect = GUILayoutUtility.GetRect(Screen.width-50,200);
		EditorGUI.LabelField(logoRect,new GUIContent( (Texture2D)logos[12]),style);
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		GUILayout.Space(Screen.width/2 - 75);
		GUILayout.TextArea("ChampionShips Settings", GUILayout.Width(150));
		GUILayout.EndHorizontal();
		
		GUILayout.Space(15);
		GUILayout.Label((Texture2D)logos[8],GUILayout.Width(Screen.width-45));
		GUILayout.Space(25);




		if (throwWarning)
			EditorGUILayout.HelpBox("This scene was not found on the build settings, please either add this scene to the build settings or change the Main menu scene name on the filed bellow",MessageType.Warning);

		ChampSystem.mainMenuScene = EditorGUILayout.TextField("Main Scene",ChampSystem.mainMenuScene);
		if (GUI.changed)
		{
			throwWarning = !CheckScene(ChampSystem.mainMenuScene);
		}

		GUILayout.Space(15);

		GUIContent[] content = new GUIContent[3];
		content[0] = new GUIContent("Global Points","Setup the ChampionShips Global points");
		content[1] = new GUIContent("Teams/Drivers","Setup the teams and drivers");
		content[2] = new GUIContent("ChampionShips","Setup the ChampionShips");
		GUILayout.BeginHorizontal();
		GUILayout.Space(Screen.width/2 - 170);
		options[0] = GUILayout.Width(310);
		options[1]= GUILayout.Height(50);
		globlalSelections = GUILayout.Toolbar(globlalSelections,content,options);
		GUILayout.EndHorizontal();
		switch(globlalSelections)
		{
		case 0:
			Settings();
			break;
		case 1:
			TeamsAndDriversOptions(true);
			break;
		case 2:
			ChampionShipsSettings();
			break;
		}



		GUILayout.Space(25);


		if (GUI.changed){
			EditorUtility.SetDirty(ChampSystem);
//			if (serializedLevelload != null)
//				serializedLevelload.ApplyModifiedProperties();
		}
		EditorGUILayout.EndVertical();
	}
	
	/// <summary>
	/// Championships settings.
	/// </summary>
	void ChampionShipsSettings()
	{
		GUILayoutOption[] options = new GUILayoutOption[2];
		GUILayout.BeginHorizontal();
		GUILayout.Label("ChampionShip");
		championShipSelected = EditorGUILayout.Popup(championShipSelected,championShipsAvailable);
		if (GUILayout.Button("New",EditorStyles.miniButton,GUILayout.Width(50))){
			ChampSystem.championShipData.Add(new ChampionShipData());
			ChampSystem.championShipData[ChampSystem.championShipData.Count-1].ChampionShipName = "New Championship " + ChampSystem.championShipData.Count;
			ResetChampionShipsNames();
		}
		GUILayout.EndHorizontal();
		//		levelLoad.destroy = EditorGUILayout.Toggle(new GUIContent("Destroy on Level Load?","Enable this if you want this object with it's level settings destroyed when the level is loaded"),levelLoad.destroy,GUILayout.Width(Screen.width-45));
		
		
		GUIContent[] content = new GUIContent[3];
		content[0] = new GUIContent("Settings","Setup the ChampionShips General Settings");
		content[1] = new GUIContent("Tracks","Setup AI parameters");
		content[2] = new GUIContent("Teams","Setup Player parameters");

		options[0] = GUILayout.Width(300);
		options[1]= GUILayout.Height(50);
		
		if (ChampSystem.championShipData.Count == 0){ 
			return;}

		GUILayout.Space(25);
		GUILayout.BeginVertical(EditorStyles.textField);

		GUILayout.BeginHorizontal();
		GUILayout.Label("Selected: " +ChampSystem.championShipData[championShipSelected].ChampionShipName);
		if (ChampSystem.championShipData.Count >0){
			if (GUILayout.Button("Delete",EditorStyles.miniButton,GUILayout.Width(80)))
			{
				ChampSystem.championShipData.Remove(ChampSystem.championShipData[championShipSelected]);
				if (championShipSelected >ChampSystem.championShipData.Count-1) 
					championShipSelected = ChampSystem.championShipData.Count-1;
				ResetChampionShipsNames();
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal();
			GUILayout.Space(Screen.width/2 - 170);
			selections = GUILayout.Toolbar(selections,content,options);
			GUILayout.EndHorizontal();
			
			
			switch(selections)
			{
			case 0:
				ChampionShipOwnSettings();
				break;
			case 1:
				TracksSettings();
				break;
			case 2:
				TeamsAndDriversOptions(false);
				break;
			}
		}else GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}
	
	/// <summary>
	/// Settings for the Championsship.
	/// </summary>
	void ChampionShipOwnSettings()
	{
		ChampionShipData championShip = ChampSystem.championShipData[championShipSelected];
		championShip.ChampionShipName = EditorGUILayout.TextField("ChampionShip Name:",championShip.ChampionShipName);
		championShip.icon = EditorGUILayout.ObjectField(new GUIContent("Icon","This would be used to display to the player as the championship icon to select it from"),championShip.icon,typeof(Texture2D), false) as Texture2D;
		championShip.useGlobalPoints = EditorGUILayout.Toggle("Use global points?",championShip.useGlobalPoints);
		if (!championShip.useGlobalPoints){
			GUILayout.Label("Points setup");
			if (GUILayout.Button("New point entry")){
				championShip.championShipPoints.points.Add(0f);
			}
			string label= "";
			for (int i = 0; i < championShip.championShipPoints.points.Count; i++)
			{
				switch(i)
				{
				case 0:
					label = "1st";
					break;
				case 1:
					label = "2nd";
					break;
				case 2:
					label = "3rd";
					break;
				default:
					label = (i+1)+"th";
					break;
				}
				GUILayout.BeginHorizontal();
				if (GUILayout.Button("-",GUILayout.Width(15)))
				{
					championShip.championShipPoints.points.Remove(championShip.championShipPoints.points[i]);
					continue;
				}
				championShip.championShipPoints.points[i] = EditorGUILayout.FloatField(label,championShip.championShipPoints.points[i]);
				GUILayout.EndHorizontal();
			}
		}
	}

	/// <summary>
	/// Settings for the tracks.
	/// </summary>
	void TracksSettings(){
		ChampionShipData championShip = ChampSystem.championShipData[championShipSelected];

		GUILayout.Label("Tracks setup");
		if (GUILayout.Button("New track")){
			championShip.championshipTracks.Add(new ChampionShipData.ChampionShipTrack());
		}

		for (int i = 0; i < championShip.championshipTracks.Count; i++)
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical(EditorStyles.textField);
			GUILayout.Space(15);
			if (GUILayout.Button("/\\",EditorStyles.miniButton,GUILayout.Width(20))){
				Move<ChampionShipData.ChampionShipTrack>(championShip.championshipTracks,i,i-1>0?i-1:0);
			}
			if (GUILayout.Button("\\/",EditorStyles.miniButton,GUILayout.Width(20))){
				Move<ChampionShipData.ChampionShipTrack>(championShip.championshipTracks,i,i+1<championShip.championshipTracks.Count-1?i+1:championShip.championshipTracks.Count-1);
			}
			GUILayout.Space(10);
			GUILayout.EndVertical();

			GUILayout.BeginVertical(EditorStyles.textField);
			GUILayout.BeginHorizontal();


			if (GUILayout.Button("-",GUILayout.Width(15)))
			{
				championShip.championshipTracks.Remove(championShip.championshipTracks[i]);
				continue;
			}
			GUILayout.Label("Name");
			championShip.championshipTracks[i].name = EditorGUILayout.TextField(championShip.championshipTracks[i].name);
			GUILayout.EndHorizontal();
			championShip.championshipTracks[i].icon = EditorGUILayout.ObjectField(new GUIContent("Icon","This would be shown to the player on the champion ship selection menu"),championShip.championshipTracks[i].icon,typeof(Texture2D),false) as Texture2D;
			championShip.championshipTracks[i].sceneName = EditorGUILayout.TextField("Scene name",championShip.championshipTracks[i].sceneName);
			championShip.championshipTracks[i].laps = EditorGUILayout.IntField("Laps",championShip.championshipTracks[i].laps);
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
	}

	/// <summary>
	/// Settings this instance.
	/// </summary>
	void Settings()
	{
		GUILayout.BeginVertical(EditorStyles.textField);
		GUILayout.Label("Global Points Settings");
		GUILayout.Label("You can add points by selecting the new button bellow");
		if (GUILayout.Button("New")){
			ChampSystem.championShipPoints.points.Add(0f);
		}
		string label= "";
		for (int i = 0; i < ChampSystem.championShipPoints.points.Count; i++)
		{
			switch(i)
			{
			case 0:
				label = "1st";
				break;
			case 1:
				label = "2nd";
				break;
			case 2:
				label = "3rd";
				break;
			default:
				label = (i+1)+"th";
				break;
			}
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("-",GUILayout.Width(15)))
			{
				ChampSystem.championShipPoints.points.Remove(ChampSystem.championShipPoints.points[i]);
				continue;
			}
			ChampSystem.championShipPoints.points[i] = EditorGUILayout.FloatField(label,ChampSystem.championShipPoints.points[i]);
			GUILayout.EndHorizontal();
		}
		GUILayout.Space(15);
		GUILayout.EndVertical();
		
	}

	string[] stringValues = new string[0] ;
	string[]  stringDriverValues= new string[0] ;
	int currentTeamIndex = 0;
	
	/// <summary>
	/// Updates the team selections.
	/// </summary>
	void UpdateTeamSelections()
	{
		stringValues = new string[ChampSystem.championshipTeams.Count];
		
		for (int c = 0; c < stringValues.Length;c++)
		{
			
			stringValues[c] = ChampSystem.championshipTeams[c].teamName;
		}
	}
	
	/// <summary>
	/// Updates the team drivers.
	/// </summary>
	void UpdateTeamDrivers()
	{
		stringDriverValues = new string[ChampSystem.championShipDrivers.Count];
		for (int c = 0; c < ChampSystem.championShipDrivers.Count;c++)
		{
			stringDriverValues[c] = ChampSystem.championShipDrivers[c].AIcarsDrivers.driverName;
		}
	}


	/// <summary>
	/// Options for the teams and drivers.
	/// </summary>
	/// <param name='includeDrivers'>
	/// Include drivers.
	/// </param>
	void TeamsAndDriversOptions(bool includeDrivers)
	{
		GUI.changed = false;
		GUILayout.BeginVertical(EditorStyles.textField);

		if (includeDrivers){
			GUILayout.Space(25);
			selectedAISubOption = GUILayout.Toolbar(selectedAISubOption,selectedAISubOptionDesc);
			GUILayout.Space(25);

			switch(selectedAISubOption)
			{
			case 0:
				TeamOptions();
				break;
			case 1:
				DriversOptions();
				break;
			}
		}
		else {
			if (championShipSelected > -1)
				TeamSelection(ChampSystem.championShipData[championShipSelected].championshipTeams);
		}
		if (GUI.changed) {
			EditorUtility.SetDirty(ChampSystem);
		}
		GUILayout.EndVertical ();
	}


	/// <summary>
	/// Options for the drivers.
	/// </summary>
	void DriversOptions(){
		List<ChampionShipData.ChampionShipDrivers> champDrivers = ChampSystem.championShipDrivers;

		if(GUILayout.Button(new GUIContent( "Add","Add new team to assign Drivers to it"),GUILayout.Width(50))){
			champDrivers.Add(new ChampionShipData.ChampionShipDrivers());
			champDrivers[champDrivers.Count-1].AIcarsDrivers = new AICarsDrivers();
		}

		string[] stringValues = new string[champDrivers.Count];
		int[] selecteableOptions = new int[champDrivers.Count];
		for (int c = 0; c < selecteableOptions.Length;c++)
		{
			selecteableOptions[c]=c;
			stringValues[c] = "AI Driver " +(c+1);
		}
		for (int indexer1 = 0; indexer1 < champDrivers.Count; indexer1++)
		{
			if (!champDrivers[indexer1].imPlayer){
				GUILayout.Space(25);
				
				GUILayout.BeginVertical(EditorStyles.textField, GUILayout.Width(Screen.width-45));
				
				GUILayout.BeginHorizontal();
				GUILayout.Label("--- AI Driver " + (indexer1+1) +"---",GUILayout.Width(130));



				if(GUILayout.Button("Remove",GUILayout.Width(60))){
					champDrivers.Remove(champDrivers[indexer1]);
					continue;
				}
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				champDrivers[indexer1].AIcarsDrivers.car= EditorGUILayout.TextField("Car",champDrivers[indexer1].AIcarsDrivers.car,GUILayout.Width(230));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				champDrivers[indexer1].AIcarsDrivers.driverName = EditorGUILayout.TextField("Driver Name",champDrivers[indexer1].AIcarsDrivers.driverName,GUILayout.Width(230));
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				champDrivers[indexer1].AIcarsDrivers.carColor = EditorGUILayout.ColorField("Car Color",champDrivers[indexer1].AIcarsDrivers.carColor,GUILayout.Width(230));
				GUILayout.EndHorizontal();
				
				
				
				GUILayout.BeginHorizontal();
				champDrivers[indexer1].AIcarsDrivers.gridPosNumber = EditorGUILayout.IntField("Grid Pos Number",champDrivers[indexer1].AIcarsDrivers.gridPosNumber,GUILayout.Width(230));
				GUILayout.EndHorizontal();
				GUILayout.BeginHorizontal();
				champDrivers[indexer1].AIcarsDrivers.useOwnSettings = EditorGUILayout.Toggle("Own Settings?",champDrivers[indexer1].AIcarsDrivers.useOwnSettings,GUILayout.Width(230));
				GUILayout.EndHorizontal();
				
				if (champDrivers[indexer1].AIcarsDrivers.useOwnSettings)
				{
					GUILayout.BeginHorizontal();
					champDrivers[indexer1].AIcarsDrivers.showSettings = EditorGUILayout.Toggle("Show Settings?",champDrivers[indexer1].AIcarsDrivers.showSettings,GUILayout.Width(230));
					GUILayout.EndHorizontal();
					
					
					GUILayout.BeginHorizontal();
					GUILayout.Label("Copy From:", GUILayout.Width(80));
					selectedValue = EditorGUILayout.IntPopup(selectedValue,stringValues, selecteableOptions, GUILayout.Width(80));
					if(GUILayout.Button("Copy",GUILayout.Width(60)))
					{
						champDrivers[indexer1].AIcarsDrivers.driverSettings.AggressivenessOnBrakeMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.AggressivenessOnBrakeMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.speedSteeringFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.speedSteeringFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.LOOKAHEAD_FACTORMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.LOOKAHEAD_FACTORMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.CorneringSpeedFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.CorneringSpeedFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxDriftAngleMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.maxDriftAngleMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.driftingThrotleFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.driftingThrotleFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.steeringDriftFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.steeringDriftFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.sideAvoidingFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.sideAvoidingFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.collisionSideFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.collisionSideFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.overtakeFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.humanErrorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.humanErrorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeSpeedDiferenceMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.overtakeSpeedDiferenceMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMinMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMinMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMaxMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMaxMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.backToLineIncrementMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.backToLineIncrementMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.shifFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.shifFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.shiftUpFactorMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.shiftUpFactorMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.tyrechangePorcentageMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.tyrechangePorcentageMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.fuelloadPorcentageMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.fuelloadPorcentageMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.FullAccelMaringMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.FullAccelMaringMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.frontCollDistMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.frontCollDistMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.backCollDistMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.backCollDistMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.sideMarginMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.sideMarginMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.heightMarginMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.heightMarginMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.LENGTH_MARGINMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.LENGTH_MARGINMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.SIDE_MARGINMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.SIDE_MARGINMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.LookAheadConst = champDrivers[selectedValue].AIcarsDrivers.driverSettings.LookAheadConst;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.offTrackThrottleMulpilierMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.offTrackThrottleMulpilierMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleTimeMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.jumpThrottleTimeMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleMulpilierMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.jumpThrottleMulpilierMax;
						
						//Dac implementation
						champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistFromPlayerMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.trgtDistFromPlayerMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistPlayerThresholdMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.trgtDistPlayerThresholdMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxAgressOnBrkDACMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.maxAgressOnBrkDACMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxCornerSpdFctrDACMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.maxCornerSpdFctrDACMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.minAgressOnBrkDACMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.minAgressOnBrkDACMax;
						champDrivers[indexer1].AIcarsDrivers.driverSettings.minCornerSpdFctrDACMax = champDrivers[selectedValue].AIcarsDrivers.driverSettings.minCornerSpdFctrDACMax;
					}
					GUILayout.EndHorizontal();
					
					if(champDrivers[indexer1].AIcarsDrivers.showSettings)
					{
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.subTrackChoosedMode = (IRDSNavigateTWaypoints.SubTrackChooseModes) EditorGUILayout.EnumPopup("Sub Track Choosed Mode",champDrivers[indexer1].AIcarsDrivers.driverSettings.subTrackChoosedMode, GUILayout.Width(230));
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.AggressivenessOnBrakeMax = EditorGUILayout.FloatField("Agressiveness on brake",champDrivers[indexer1].AIcarsDrivers.driverSettings.AggressivenessOnBrakeMax, GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.AggressivenessOnBrakeMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.AggressivenessOnBrakeMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.speedSteeringFactorMax = EditorGUILayout.FloatField("Spd Steering Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.speedSteeringFactorMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.speedSteeringFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.speedSteeringFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.LOOKAHEAD_FACTORMax = EditorGUILayout.FloatField("LOOKAHEAD_FACTOR",champDrivers[indexer1].AIcarsDrivers.driverSettings.LOOKAHEAD_FACTORMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.LOOKAHEAD_FACTORMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.LOOKAHEAD_FACTORMax;

						champDrivers[indexer1].AIcarsDrivers.driverSettings.LookAheadConst = EditorGUILayout.FloatField("Look Ahead Constant",champDrivers[indexer1].AIcarsDrivers.driverSettings.LookAheadConst,GUILayout.Width(230));
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.CorneringSpeedFactorMax = EditorGUILayout.FloatField("Cornering Speed Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.CorneringSpeedFactorMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.CorneringSpeedFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.CorneringSpeedFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxDriftAngleMax = EditorGUILayout.FloatField("Max Drift Angle",champDrivers[indexer1].AIcarsDrivers.driverSettings.maxDriftAngleMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxDriftAngleMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.maxDriftAngleMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.driftingThrotleFactorMax = EditorGUILayout.FloatField("Drifting Throtle Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.driftingThrotleFactorMax,GUILayout.Width(230));
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.driftingThrotleFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.driftingThrotleFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.steeringDriftFactorMax = EditorGUILayout.FloatField("Steering Drift Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.steeringDriftFactorMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.steeringDriftFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.steeringDriftFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.sideAvoidingFactorMax = EditorGUILayout.FloatField("Side Avoiding Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.sideAvoidingFactorMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.sideAvoidingFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.sideAvoidingFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.collisionSideFactorMax = EditorGUILayout.FloatField("Collision Side Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.collisionSideFactorMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.collisionSideFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.collisionSideFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeFactorMax = EditorGUILayout.FloatField("Overtake Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeFactorMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.humanErrorMax = EditorGUILayout.FloatField("Human Error",champDrivers[indexer1].AIcarsDrivers.driverSettings.humanErrorMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.humanErrorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.humanErrorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeSpeedDiferenceMax = EditorGUILayout.FloatField("Overtake Speed Diference",champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeSpeedDiferenceMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeSpeedDiferenceMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeSpeedDiferenceMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMinMax = EditorGUILayout.FloatField("Overtake Offset Increment Min",champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMinMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMinMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMinMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMaxMax = EditorGUILayout.FloatField("Overtake Offset Increment Max",champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMaxMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMaxMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.overtakeOffsetIncrementMaxMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.backToLineIncrementMax = EditorGUILayout.FloatField("Back To Line Increment",champDrivers[indexer1].AIcarsDrivers.driverSettings.backToLineIncrementMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.backToLineIncrementMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.backToLineIncrementMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.shifFactorMax = EditorGUILayout.FloatField("Shift Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.shifFactorMax,GUILayout.Width(230));
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.shifFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.shifFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.shiftUpFactorMax = EditorGUILayout.FloatField("Shift Up Factor",champDrivers[indexer1].AIcarsDrivers.driverSettings.shiftUpFactorMax,GUILayout.Width(230));
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.shiftUpFactorMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.shiftUpFactorMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.tyrechangePorcentageMax = EditorGUILayout.FloatField("Tyre Change Porcentage",champDrivers[indexer1].AIcarsDrivers.driverSettings.tyrechangePorcentageMax,GUILayout.Width(230));
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.tyrechangePorcentageMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.tyrechangePorcentageMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.fuelloadPorcentageMax = EditorGUILayout.FloatField("Fuel Load Porcentage",champDrivers[indexer1].AIcarsDrivers.driverSettings.fuelloadPorcentageMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.fuelloadPorcentageMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.fuelloadPorcentageMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.FullAccelMaringMax = EditorGUILayout.FloatField("Full Accel Maring",champDrivers[indexer1].AIcarsDrivers.driverSettings.FullAccelMaringMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.FullAccelMaringMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.FullAccelMaringMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.frontCollDistMax = EditorGUILayout.FloatField("Front Coll Dist",champDrivers[indexer1].AIcarsDrivers.driverSettings.frontCollDistMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.frontCollDistMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.frontCollDistMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.backCollDistMax = EditorGUILayout.FloatField("Back Coll Dist",champDrivers[indexer1].AIcarsDrivers.driverSettings.backCollDistMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.backCollDistMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.backCollDistMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.sideMarginMax = EditorGUILayout.FloatField("Side Margin",champDrivers[indexer1].AIcarsDrivers.driverSettings.sideMarginMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.sideMarginMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.sideMarginMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.heightMarginMax = EditorGUILayout.FloatField("Height Margin",champDrivers[indexer1].AIcarsDrivers.driverSettings.heightMarginMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.heightMarginMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.heightMarginMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.LENGTH_MARGINMax = EditorGUILayout.FloatField("LENGTH_MARGIN",champDrivers[indexer1].AIcarsDrivers.driverSettings.LENGTH_MARGINMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.LENGTH_MARGINMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.LENGTH_MARGINMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.SIDE_MARGINMax = EditorGUILayout.FloatField("SIDE_MARGIN",champDrivers[indexer1].AIcarsDrivers.driverSettings.SIDE_MARGINMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.SIDE_MARGINMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.SIDE_MARGINMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.offTrackThrottleMulpilierMax = EditorGUILayout.FloatField("Off Track Throttle Mult",champDrivers[indexer1].AIcarsDrivers.driverSettings.offTrackThrottleMulpilierMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.offTrackThrottleMulpilierMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.offTrackThrottleMulpilierMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleMulpilierMax = EditorGUILayout.FloatField("Jump Throttle Mult",champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleMulpilierMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleMulpilierMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleMulpilierMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleTimeMax = EditorGUILayout.FloatField("Jump Throttle Time",champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleTimeMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleTimeMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.jumpThrottleTimeMax;
						
						//DAC Implementation
						GUILayout.Space(10);
						GUILayout.Label("--- DAC Settings ---");
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistFromPlayerMax = EditorGUILayout.FloatField("trgt Dist From Player Max",champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistFromPlayerMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistFromPlayerMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistFromPlayerMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistPlayerThresholdMax = EditorGUILayout.FloatField("trgt Dist Player Threshold Max",champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistPlayerThresholdMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistPlayerThresholdMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.trgtDistPlayerThresholdMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.minAgressOnBrkDACMax = EditorGUILayout.FloatField("min Agress On Brk DAC Max",champDrivers[indexer1].AIcarsDrivers.driverSettings.minAgressOnBrkDACMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.minAgressOnBrkDACMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.minAgressOnBrkDACMax;
						
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxAgressOnBrkDACMax = EditorGUILayout.FloatField("max Agress On Brk DAC Max",champDrivers[indexer1].AIcarsDrivers.driverSettings.maxAgressOnBrkDACMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxAgressOnBrkDACMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.maxAgressOnBrkDACMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.minCornerSpdFctrDACMax = EditorGUILayout.FloatField("min Corner Spd Fctr DAC Max",champDrivers[indexer1].AIcarsDrivers.driverSettings.minCornerSpdFctrDACMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.minCornerSpdFctrDACMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.minCornerSpdFctrDACMax;
						
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxCornerSpdFctrDACMax = EditorGUILayout.FloatField("max Corner Spd Fctr DAC Max",champDrivers[indexer1].AIcarsDrivers.driverSettings.maxCornerSpdFctrDACMax,GUILayout.Width(230));
						champDrivers[indexer1].AIcarsDrivers.driverSettings.maxCornerSpdFctrDACMin = champDrivers[indexer1].AIcarsDrivers.driverSettings.maxCornerSpdFctrDACMax;
					}
				}
				GUILayout.EndVertical();
			}
		}
		if (GUI.changed)
			UpdateTeamDrivers();
	}

	/// <summary>
	/// Options of the teams.
	/// </summary>
	void TeamOptions()
	{
		if(GUILayout.Button(new GUIContent( "Add","Add new team to assign Drivers to it"),GUILayout.Width(50))){
			ChampSystem.championshipTeams.Add(new ChampionShipData.ChampionShipTeam());
			ChampSystem.championshipTeams[ChampSystem.championshipTeams.Count-1].teamName = "Team " + ChampSystem.championshipTeams.Count.ToString();
			UpdateTeamSelections();
		}
		currentTeamIndex = EditorGUILayout.Popup("Selected Team",currentTeamIndex,stringValues);
		GUILayout.BeginVertical(EditorStyles.textField);
		if(ChampSystem.championshipTeams.Count >0 && GUILayout.Button(new GUIContent( "Remove","Removes current selected team"),GUILayout.Width(70))){
			ChampSystem.championshipTeams.Remove(ChampSystem.championshipTeams[currentTeamIndex]);
			UpdateTeamSelections();
			if (currentTeamIndex >= ChampSystem.championshipTeams.Count)currentTeamIndex =ChampSystem.championshipTeams.Count-1;
			return;
		}
		if (ChampSystem.championshipTeams.Count >0){
			ChampionShipData.ChampionShipTeam myTeam = ChampSystem.championshipTeams[currentTeamIndex];
			myTeam.teamName = EditorGUILayout.TextField("Team Name:",myTeam.teamName);
			myTeam.icon = EditorGUILayout.ObjectField("Icon",myTeam.icon,typeof(Texture2D),false) as Texture2D;
			myTeam.isPlayerAssignedToTeam = EditorGUILayout.Toggle("Player is on this team?:",myTeam.isPlayerAssignedToTeam);
			myTeam.playerReplacesFirstAI = EditorGUILayout.Toggle("Player replaces one AI?:",myTeam.playerReplacesFirstAI);
			selectedTeamDriversOption = GUILayout.Toolbar(selectedTeamDriversOption,selectedTeamDriversOptionDesc);
			switch(selectedTeamDriversOption)
			{
			case 0:
				TeamCarOptions(myTeam.teamCars);
				break;
			case 1:
				TeamDrivers(myTeam.teamDrivers);
				break;
			}
		}
		EditorGUILayout.EndVertical();
	}
	
	/// <summary>
	/// Selection of the teams.
	/// </summary>
	/// <param name='teams'>
	/// Teams.
	/// </param>
	void TeamSelection(List<int> teams)
	{
		if(GUILayout.Button(new GUIContent( "Add team","Add new team"),GUILayout.Width(90))){
			if (teams.Count < ChampSystem.championshipTeams.Count)
				teams.Add(0);
		}
		
		for (int i =0; i < teams.Count;i++) 
		{
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent( "-","Remove this car from the cars paths list"),EditorStyles.miniButton,GUILayout.Width(15))){
				teams.Remove(teams[i]);
				continue;
			}
			int teamSelected = ChampSystem.championshipTeams.FindIndex(X => X==ChampSystem.championshipTeams[teams[i]]);
			teamSelected = EditorGUILayout.Popup("Team:", teamSelected, stringValues);
			if (teamSelected >=0 && teamSelected < ChampSystem.championshipTeams.Count)
				teams[i] = teamSelected;// ChampSystem.championShipDrivers.FindIndex(X => X==ChampSystem.championShipDrivers[driverSelected]);
			EditorGUILayout.EndHorizontal();
		}
		
	} 

	/// <summary>
	/// Options for the Teams drivers.
	/// </summary>
	/// <param name='teamDrivers'>
	/// Team drivers.
	/// </param>
	void TeamDrivers(List<int> teamDrivers)
	{
		if(GUILayout.Button(new GUIContent( "Add driver","Add new cars to the cars path list"),GUILayout.Width(90))){
			if (teamDrivers.Count < ChampSystem.championShipDrivers.Count)
				teamDrivers.Add(0);
		}

		for (int i =0; i < teamDrivers.Count;i++) 
		{
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent( "-","Remove this car from the cars paths list"),EditorStyles.miniButton,GUILayout.Width(15))){
				teamDrivers.Remove(teamDrivers[i]);
				continue;
			}
			int driverSelected = ChampSystem.championShipDrivers.FindIndex(X => X==ChampSystem.championShipDrivers[teamDrivers[i]]);
			driverSelected = EditorGUILayout.Popup("Driver:", driverSelected, stringDriverValues);
			if (driverSelected >=0 && driverSelected < ChampSystem.championShipDrivers.Count)
				teamDrivers[i] = driverSelected;// ChampSystem.championShipDrivers.FindIndex(X => X==ChampSystem.championShipDrivers[driverSelected]);
			EditorGUILayout.EndHorizontal();
		}

	} 

	/// <summary>
	/// Options for the Teams cars.
	/// </summary>
	/// <param name='teamCar'>
	/// Team car.
	/// </param>
	void TeamCarOptions(ChampionShipData.TeamCars teamCar){
		
		GUILayout.Space(15);
		
		EditorGUILayout.BeginVertical(EditorStyles.textField);
		EditorGUILayout.LabelField("Cars References");
		if(GUILayout.Button(new GUIContent( "Add Car Path","Add new cars to the cars path list"),GUILayout.Width(90))){
			teamCar.teamCars.Add(new IRDSLevelLoadVariables.IRDSCarsPaths());
			teamCar.teamCars[teamCar.teamCars.Count-1].enabled = true;
			teamCar.teamCars[teamCar.teamCars.Count-1].enabledForAI = true;
			teamCar.teamCars[teamCar.teamCars.Count-1].enabledForPlayers = true;
		}
		
		
		for (int y = 0; y < teamCar.teamCars.Count;y++) 
		{
			EditorGUILayout.BeginVertical(EditorStyles.textField);
			EditorGUILayout.BeginHorizontal();
			if(GUILayout.Button(new GUIContent( "-","Remove this folder from the cars paths list"),GUILayout.Width(20))){
				teamCar.teamCars.Remove(teamCar.teamCars[y]);
				continue;
			}
			teamCar.teamCars[y].folderName = EditorGUILayout.TextField("Folder name:",teamCar.teamCars[y].folderName);
			
			
			
			if(GUILayout.Button(new GUIContent( "Get Cars","Gets all the cars of the specified folder"),GUILayout.Width(70))){
				GetResourcesDirectories(teamCar.teamCars[y]);
			}
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Enabled->", GUILayout.Width(62));
			teamCar.teamCars[y].enabled = EditorGUILayout.Toggle(teamCar.teamCars[y].enabled, GUILayout.Width(10));
			EditorGUILayout.LabelField("Player->", GUILayout.Width(54));
			teamCar.teamCars[y].enabledForPlayers = EditorGUILayout.Toggle(teamCar.teamCars[y].enabledForPlayers, GUILayout.Width(10));
			EditorGUILayout.LabelField("AI->", GUILayout.Width(30));
			teamCar.teamCars[y].enabledForAI = EditorGUILayout.Toggle(teamCar.teamCars[y].enabledForAI, GUILayout.Width(10));
			EditorGUILayout.EndHorizontal();
			
			
			if (teamCar.teamCars[y].preloadedCarsPath.Count>0){
				EditorGUILayout.LabelField("Cars found on folder-->" + teamCar.teamCars[y].folderName,EditorStyles.boldLabel);
				for (int i =0;i< teamCar.teamCars[y].preloadedCarsPath.Count;i++)
				{
					EditorGUILayout.BeginHorizontal();
					if(GUILayout.Button(new GUIContent( "-","Remove this car from the cars paths list"),EditorStyles.miniButton,GUILayout.Width(15))){
						teamCar.teamCars[y].preloadedCarsPath.Remove(teamCar.teamCars[y].preloadedCarsPath[i]);
						continue;
					}
					EditorGUILayout.LabelField(teamCar.teamCars[y].preloadedCarsPath[i]);
					EditorGUILayout.EndHorizontal();
				}
				
			}else
			{
				EditorGUILayout.TextArea("No Cars found on folder-->" + teamCar.teamCars[y].folderName + " \nYou need either to check the folder name or \npress the Get Cars Button!",EditorStyles.boldLabel);
			}
			
			EditorGUILayout.EndVertical();
			
		}
		
		EditorGUILayout.EndVertical();
		
		GUILayout.Space(15);
		
		EditorGUILayout.BeginVertical(EditorStyles.textField);
		EditorGUILayout.LabelField("Cars References");
		if(GUILayout.Button(new GUIContent( "Add Car","Add new cars to the cars array"),GUILayout.Width(80))){
			if (teamCar.carsArray == null)
			{
				teamCar.carsArray = new GameObject[1];
				
			}else{
				System.Array.Resize<GameObject>(ref teamCar.carsArray,teamCar.carsArray.Length+1);
			}
		}
		if (teamCar.carsArray != null)
		{
			for (int y = 0; y < teamCar.carsArray.Length;y++) 
			{
				EditorGUILayout.BeginHorizontal(EditorStyles.textField);
				if(GUILayout.Button(new GUIContent( "-","Remove this car from the cars array"),GUILayout.Width(20))){
					ArrayRemove<GameObject>(ref teamCar.carsArray,y);
					if (teamCar.carsArray.Length ==0)break;
					continue;
				}
				teamCar.carsArray[y] =(GameObject) EditorGUILayout.ObjectField("Car Game Object " + y,teamCar.carsArray[y],typeof(GameObject),true);
				EditorGUILayout.EndHorizontal();
			}
		}
		
		EditorGUILayout.EndVertical();
		
		

	}
	
	
	

	/// <summary>
	/// Move the specified list, oldIndex and newIndex.
	/// </summary>
	/// <param name='list'>
	/// List.
	/// </param>
	/// <param name='oldIndex'>
	/// Old index.
	/// </param>
	/// <param name='newIndex'>
	/// New index.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	void Move<T>(List<T> list, int oldIndex, int newIndex)
    {
        T aux = list[newIndex];
        list[newIndex] = list[oldIndex];
        list[oldIndex] = aux;
    }
	
	int compareOppPosition ( IRDSDriversGrids x, IRDSDriversGrids y)
	{
		return x.myCurrentPosition.CompareTo( y.myCurrentPosition );
	}

	/// <summary>
	/// Get all available cars in the specified Resources directory paths within the current project.
	/// </summary>
	public static void GetResourcesDirectories (IRDSLevelLoadVariables.IRDSCarsPaths carPaths)
	{  
		List<string> result = new List<string>();
		result.Add(carPaths.folderName);
		
		Stack<string> stack = new Stack<string>();
		// Add the root directory to the stack
		stack.Push(Application.dataPath);
		// While we have directories to process...
		carPaths.preloadedCarsPath = new List<string>();
		Debug.Log ("Pre-Seacrhing all cars on the specified directories!");
		while (stack.Count > 0) {
			// Grab a directory off the stack
			string currentDir = stack.Pop();
			try {
				foreach (string dir in Directory.GetDirectories(currentDir)) {
					foreach(string subDir in result){
						if (dir.Contains("Resources\\"+subDir)) {
							List<string> tempCarList = new List<string>();
							foreach(string file in Directory.GetFiles(dir)){
								if (!file.Contains(".meta")){
									Debug.Log ("Normal Car: -->" + file.Remove(0,dir.Length-subDir.Length).Split('.')[0]);
									tempCarList.Add(file.Remove(0,dir.Length-subDir.Length).Split('.')[0]);
								}
							}
							carPaths.preloadedCarsPath = new List<string>(tempCarList);
						}
					}
					// Add directories at the current level into the stack
					stack.Push(dir);
				}
				
			}
			catch {
				Debug.LogError("Directory " + currentDir + " couldn't be read from.");
			}
		}

	}
	

	/// <summary>
	/// Removes an item fron an array.
	/// </summary>
	/// <param name='array'>
	/// Array.
	/// </param>
	/// <param name='index'>
	/// Index.
	/// </param>
	/// <typeparam name='T'>
	/// The 1st type parameter.
	/// </typeparam>
	public static void ArrayRemove<T>(ref T[] array,int index)
	{
		T[] tempArray = new T[array.Length-1];
		int newCounter =0;
		for (int w = 0; w < array.Length; w++)
		{
			if (w != index){
				tempArray[newCounter] = (T)array[w];
				newCounter++;
			}
		}
		array = tempArray;
	}
}
