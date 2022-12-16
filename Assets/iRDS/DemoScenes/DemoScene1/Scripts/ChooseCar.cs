using UnityEngine;
using System.Collections;
using IRDS.ChampionshipSystem;
using UnityEngine.SceneManagement;


public class ChooseCar : MonoBehaviour
{
    /// <summary>
    /// My GUI skin.  This is used to make customization of the GUI for this car choose script.
    /// </summary>
    public GUISkin myGuiSkin;

    /// <summary>
    /// The button right.
    /// </summary>
    public Texture buttonRight;

    /// <summary>
    /// The button right position.
    /// </summary>
    public Vector2 buttonRightPos;

    /// <summary>
    /// The size of the button right.
    /// </summary>
    public Vector2 buttonRightSize;

    /// <summary>
    /// The button left.
    /// </summary>
    public Texture buttonLeft;

    /// <summary>
    /// The button left position.
    /// </summary>
    public Vector2 buttonLeftPos;

    /// <summary>
    /// The size of the button left.
    /// </summary>
    public Vector2 buttonLeftSize;

    /// <summary>
    /// The track names for UI.
    /// </summary>
    public string[] trackNamesForUI;

    /// <summary>
    /// The track names.
    /// </summary>
    public string[] trackNames;

    /// <summary>
    /// The track images.
    /// </summary>
    public Texture2D[] trackImages;

    /// <summary>
    /// The height of the instantiate.
    /// </summary>
    public float instantiateHeight = 0.1f;

    /// <summary>
    /// The index of the track.
    /// </summary>
    private int trackIndex = 0;

    /// <summary>
    /// The index of the championship.
    /// </summary>
    private int championshipIndex = 0;

    /// <summary>
    /// The car choosen.
    /// </summary>
    private bool carChoosen = false;

    /// <summary>
    /// The current car inputs.
    /// </summary>
    private IRDSCarControllInput currentCarInputs;

    /// <summary>
    /// The champ system.
    /// </summary>
    private ChampionShipSystem champSystem;

    /// <summary>
    /// The have champion ship.
    /// </summary>
    private bool haveChampionShip = false;

    /// <summary>
    /// The temp cars.
    /// </summary>
    Object[] tempCars;

    /// <summary>
    /// The temp object.
    /// </summary>
    GameObject tempObj;

    /// <summary>
    /// The car selected.
    /// </summary>
    int carSelected = 0;

    /// <summary>
    /// The loaded stettings.
    /// </summary>
    IRDSLevelLoadVariables loadedStettings;

    /// <summary>
    /// The texture that would be use for displaying the colors to choose for the car paint selection.
    /// </summary>
    Texture2D texture;

    /// <summary>
    /// The type of race selected.  
    /// 0 = none selected
    /// 1 = single race
    /// 2 = championship race
    /// </summary>
    int typeOfRaceSelected = 0;

    /// <summary>
    /// The menu selection.
    /// </summary>
    int menuSelection = 0;


    /// <summary>
    /// The team selected by player.
    /// </summary>
    int teamSelectedByPlayer = 0;

    /// <summary>
    /// The choose color window rect.
    /// </summary>
    private Rect chooseColorWindowRect;


    /// <summary>
    /// The race mode window rect.
    /// </summary>
    private Rect raceModeWindowRect;

    /// <summary>
    /// The race settings window rect.
    /// </summary>
    private Rect raceSettingsWindowRect;

    /// <summary>
    /// The graphics quality window rect.
    /// </summary>
    private Rect graphicsQualityWindowRect;

    /// <summary>
    /// The track choose window rect.
    /// </summary>
    private Rect trackChooseWindowRect;

    /// <summary>
    /// The team choose window rect.
    /// </summary>
    private Rect teamChooseWindowRect;

    /// <summary>
    /// The championship choose window rect.
    /// </summary>
    private Rect championshipChooseWindowRect;
    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {



        //We initialize the texture for showing the colors for the car paint to choose from.
        texture = new Texture2D(15, 25);

        //Get the instance of the levelload object to do the modification and gets information from it
        loadedStettings = GameObject.FindObjectOfType(typeof(IRDSLevelLoadVariables)) as IRDSLevelLoadVariables;

        //Get the instance of the championship system if any
        champSystem = GameObject.FindObjectOfType(typeof(ChampionShipSystem)) as ChampionShipSystem;

        //Check if we got an instance of the championship system and set the corresponding bool to true.
        if (champSystem != null)
            haveChampionShip = true;
        else typeOfRaceSelected = 1;

        //Set the player name by defaul to player.
        loadedStettings.firstPlayerSettings.playerName = "Player";
        UpdateCarListSingleRace();

        //Set the windows sizes
        chooseColorWindowRect = new Rect(15, Screen.height / 1.2f - 25, loadedStettings.carAiColors.Length * 38 + 5, 100);
        raceModeWindowRect = new Rect(Screen.width / 2f - 175, Screen.height / 2f - 120, 175, 125);
        raceSettingsWindowRect = new Rect(15, 25, 300, 300);
        graphicsQualityWindowRect = new Rect(15, 25, 200, 125);
        trackChooseWindowRect = new Rect((Screen.width / 2 - 300), (Screen.height / 2 - 300), 600, 500);
        teamChooseWindowRect = new Rect((Screen.width / 2 - 300), (Screen.height / 2 - 300), 600, 500);
        championshipChooseWindowRect = new Rect((Screen.width / 2 - 300), (Screen.height / 2 - 300), 600, 500);
    }

    /// <summary>
    /// Updates the car list.
    /// </summary>
    void UpdateCarListSingleRace()
    {
        Resources.UnloadUnusedAssets();

        //We have to make a copy of the list and arrays, to avoid erasing them if the list and arrays changes.
        if (champSystem != null)
        {
            loadedStettings.preloadedCarsPath = new System.Collections.Generic.List<IRDSLevelLoadVariables.IRDSCarsPaths>(champSystem.defaultCarsPath);
            loadedStettings.carsFolders = champSystem.originalCarFolders.Clone() as string[];
            loadedStettings.CarsForRace = champSystem.originalCars.Clone() as GameObject[];
        }
        //Preload all the cars that by default are on the default resources folder Cars.
        tempCars = Resources.LoadAll("Cars", typeof(GameObject));
        if (loadedStettings.carsFolders.Length != 0)
        {
            foreach (string folder in loadedStettings.carsFolders)
            {
                Object[] tempcars1 = UnityEngine.Resources.LoadAll(folder, typeof(GameObject));
                int tempindex = tempCars.Length;
                System.Array.Resize<Object>(ref tempCars, tempCars.Length + tempcars1.Length);
                System.Array.Copy(tempcars1, 0, tempCars, tempindex, tempcars1.Length);
            }
        }
        for (int i = 0; i < loadedStettings.preloadedCarsPath.Count; i++)
        {
            if (loadedStettings.preloadedCarsPath[i].enabledForPlayers)
            {
                Object[] tempcars1 = UnityEngine.Resources.LoadAll(loadedStettings.preloadedCarsPath[i].folderName, typeof(GameObject));
                int tempindex = tempCars.Length;
                System.Array.Resize<Object>(ref tempCars, tempCars.Length + tempcars1.Length);
                System.Array.Copy(tempcars1, 0, tempCars, tempindex, tempcars1.Length);
            }
        }
        foreach (GameObject car in loadedStettings.CarsForRace)
        {
            System.Array.Resize<Object>(ref tempCars, tempCars.Length + 1);
            tempCars[tempCars.Length - 1] = car;
        }
    }


    /// <summary>
    /// Updates the car list for selected team.
    /// </summary>
    void UpdateCarListForSelectedTeam()
    {
        tempCars = new Object[0];
        Resources.UnloadUnusedAssets();
        if (champSystem.championshipTeams[teamSelectedByPlayer].teamCars.carsArray.Length != 0)
        {
            int tempindex = tempCars.Length;
            System.Array.Resize<Object>(ref tempCars, tempCars.Length + champSystem.championshipTeams[teamSelectedByPlayer].teamCars.carsArray.Length);
            System.Array.Copy(champSystem.championshipTeams[teamSelectedByPlayer].teamCars.carsArray, 0, tempCars, tempindex, champSystem.championshipTeams[teamSelectedByPlayer].teamCars.carsArray.Length);
        }
        foreach (IRDSLevelLoadVariables.IRDSCarsPaths car in champSystem.championshipTeams[teamSelectedByPlayer].teamCars.teamCars)
        {

            if (car.enabledForPlayers)
            {
                foreach (string carName in car.preloadedCarsPath)
                {
                    Object tempcars1 = UnityEngine.Resources.Load(carName.Replace("\\", "/"), typeof(GameObject));
                    System.Array.Resize<Object>(ref tempCars, tempCars.Length + 1);
                    tempCars[tempCars.Length - 1] = tempcars1;
                }
            }
        }
    }


    // Update is called once per frame

    void LateUpdate()
    {
        //Set the brake lights on.
        if (currentCarInputs != null) currentCarInputs.CarVisuals.brakeLight(1);
    }

    void Update()
    {

        switch (menuSelection)
        {
            case 1:
                //If no car have been choosen yet
                if (!carChoosen)
                {
                    //Get any key press and check if they are left or right arraw and change the current car (cycle cars)
                    if (Input.GetKeyUp(KeyCode.LeftArrow))
                    {
                        if (carSelected > 0)
                            carSelected--;
                        else carSelected = tempCars.Length - 1;

                        carSelection(carSelected, transform.localRotation);

                    }

                    if (Input.GetKeyUp(KeyCode.RightArrow))
                    {

                        if (carSelected < tempCars.Length - 1)
                            carSelected++;
                        else carSelected = 0;

                        carSelection(carSelected, transform.localRotation);
                    }
                    if (tempObj == null)
                    {
                        carSelection(carSelected, transform.localRotation);
                    }
                    //Check if the player hitted return to continue to the next selction menu
                    if (Input.GetKeyUp(KeyCode.Return))
                    {
                        loadedStettings.firstPlayerSettings.selectedCar = transform.GetComponentInChildren<IRDSCarControllerAI>().name.Replace("(Clone)", "");
                        carChoosen = true;
                        menuSelection = 2;
                    }
                    //Return to the previos menu if the player hits escape key
                    if (Input.GetKeyUp(KeyCode.Escape))
                    {
                        carChoosen = false;
                        switch (typeOfRaceSelected)
                        {
                            case 1:
                                menuSelection = 0;
                                typeOfRaceSelected = 0;
                                break;
                            case 2:
                                menuSelection = 3;
                                break;
                        }
                    }
                }
                break;
            case 2:
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    menuSelection = 1;
                    carChoosen = false;
                }
                break;
            case 3:
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    menuSelection = 0;
                    typeOfRaceSelected = 0;
                    carChoosen = false;
                    champSystem.championshipTeams[teamSelectedByPlayer].isPlayerAssignedToTeam = false;
                }
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    menuSelection = 1;
                    UpdateCarListForSelectedTeam();
                    carSelected = 0;
                    champSystem.championshipTeams[teamSelectedByPlayer].isPlayerAssignedToTeam = true;

                    carSelection(carSelected, transform.localRotation);
                }
                break;

        }







        //Make sure the car wont be able to go forward with the user inputs.
        if (currentCarInputs != null)
        {
            currentCarInputs.setBrakeInput(1);
            currentCarInputs.Drivetrain.changeGearToTarget(1, 0f);
            currentCarInputs.Drivetrain.SetGear(1);
            currentCarInputs.Drivetrain.gearWanted = 1;
        }

    }
    /// <summary>
    /// Update the current car to the new selected one
    /// </summary>
    /// <param name='i'>
    /// I, this is the index of the selected car.
    /// </param>
    /// <param name='rotation'>
    /// Rotation.
    /// </param>
    void carSelection(int i, Quaternion rotation)
    {
        if (tempObj != null) DestroyImmediate(tempObj);
        tempObj = InstantiateObject.instantiateCar(tempObj, transform, tempCars[i], rotation, Vector3.zero, new Vector3(0, instantiateHeight, 0), RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ, 10);
        GameObject.Destroy(tempObj.GetComponent<IRDSNavigateTWaypoints>().CurrentWaypointTransform.gameObject);
        tempObj.GetComponent<IRDSCarControllerAI>().enabled = false;
        tempObj.GetComponent<IRDSNavigateTWaypoints>().enabled = false;
        tempObj.transform.parent = transform;
        tempObj.transform.localPosition = new Vector3(0, instantiateHeight, 0);
        tempObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        tempObj.GetComponent<Rigidbody>().inertiaTensor *= 10;

        ChangeCarColor(loadedStettings.carAiColors[0]);
        loadedStettings.firstPlayerSettings.selectedCarColor[0] = loadedStettings.carAiColors[0];
        currentCarInputs = tempObj.GetComponent<IRDSCarControllInput>();
    }


    void ColorSelectionWindow(int windowID)
    {
        for (int x = 0; x < loadedStettings.carAiColors.Length; x++)
        {
            for (int i = 0; i <= 15; i++)
                for (int y = 0; y <= 25; y++)
                    texture.SetPixel(i, y, loadedStettings.carAiColors[x]);
            texture.Apply();
            //Make Main Menu button
            if (GUI.Button(new Rect(10 + x * 35, 55, 35, 35), texture))
            {
                ChangeCarColor(loadedStettings.carAiColors[x]);
                loadedStettings.firstPlayerSettings.selectedCarColor[0] = loadedStettings.carAiColors[x];
            }
        }
        GUI.DragWindow();
    }

    void RaceModeWindow(int windowID)
    {
        if (GUILayout.Button("Single Race"))
        {//new Rect(12.5f,50,150,25), 

            typeOfRaceSelected = 1;
            menuSelection = 1;
            UpdateCarListSingleRace();
            carSelected = 0;
            carSelection(carSelected, transform.localRotation);

        }
        if (GUILayout.Button("Championships"))
        {//new Rect(12.5f,85,150,25),
            typeOfRaceSelected = 2;
            menuSelection = 3;
        }
        GUI.DragWindow();
    }

    void RaceSettingsWindow(int windowID)
    {
        // Number of opponents
        ShadowAndOutline.DrawShadow(new Rect(15, 80, 130, 35), new GUIContent("No. Opponents"), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
        if (GUI.Button(new Rect(190, 80, 30, 30), "-"))
        {
            loadedStettings.numberOfAiCars--;
        }
        if (GUI.Button(new Rect(230, 80, 30, 30), "+"))
        {
            loadedStettings.numberOfAiCars++;
        }
        loadedStettings.numberOfAiCars = Mathf.Clamp(loadedStettings.numberOfAiCars, 1, 10);
        ShadowAndOutline.DrawShadow(new Rect(140, 80, 25, 35), new GUIContent(loadedStettings.numberOfAiCars.ToString()), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));

        // No. of Laps
        ShadowAndOutline.DrawShadow(new Rect(15, 120, 120, 35), new GUIContent("No. Laps"), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));

        if (GUI.Button(new Rect(190, 120, 30, 30), "-"))
        {
            loadedStettings.laps--;
        }
        if (GUI.Button(new Rect(230, 120, 30, 30), "+"))
        {
            loadedStettings.laps++;
        }
        loadedStettings.laps = Mathf.Clamp(loadedStettings.laps, 2, 100);
        ShadowAndOutline.DrawShadow(new Rect(140, 120, 25, 35), new GUIContent(loadedStettings.laps.ToString()), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
        ShadowAndOutline.DrawShadow(new Rect(15, 175, 100, 35), new GUIContent("Name:"), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));



        loadedStettings.firstPlayerSettings.playerName = GUI.TextField(new Rect(80, 175, 150, 40), loadedStettings.firstPlayerSettings.playerName);

        ShadowAndOutline.DrawShadow(new Rect(15, 200, 100, 35), new GUIContent("Auto-Shifting"), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));

        loadedStettings.firstPlayerSettings.automaticGearShifting = GUI.Toggle(new Rect(100, 210, 100, 35), loadedStettings.firstPlayerSettings.automaticGearShifting, "");
        ShadowAndOutline.DrawShadow(new Rect(15, 225, 250, 100), new GUIContent("Press Enter To Select Current Car \nUse arrows keys to \n  change cars"), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
        GUI.DragWindow();

    }


    void GraphicsQualityWindow(int windowID)
    {
        if (GUI.Button(new Rect(15, 50, 30, 30), "+"))
        {
            QualitySettings.IncreaseLevel(true);
        }
        if (GUI.Button(new Rect(15, 80, 30, 30), "-"))
        {
            QualitySettings.DecreaseLevel(true);
        }
        ShadowAndOutline.DrawShadow(new Rect(60, 50, 150, 250), new GUIContent("Current Setting: \n" + QualitySettings.names[QualitySettings.GetQualityLevel()]), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
        GUI.DragWindow();
    }

    void TrackChooseWindow(int windowID)
    {
        int row = 0;
        int column = 0;
        for (int r = 1; r < trackNames.Length + 1; r++)
        {
            ShadowAndOutline.DrawShadow(new Rect((20 + column * 110), 50 + row * 145, 100, 35), new GUIContent(trackNamesForUI[r - 1]), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
            if (GUI.Button(new Rect((15 + column * 110), 75 + row * 145, 100, 100), trackImages[r - 1]))
            {
                trackIndex = r - 1;
            }
            column++;
            if (r % 5 == 0)
            {
                row++;
                column = 0;
            }
        }
        loadedStettings.trackToRace = trackNames[trackIndex];
        ShadowAndOutline.DrawShadow(new Rect(15, 450, 300, 50), new GUIContent("Current Course: " + trackNamesForUI[trackIndex]), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
        if (GUI.Button(new Rect(225, 450, 150, 35), "Race!"))
        {
            LoadLevel();
        }
        GUI.DragWindow();
    }

    void TeamSelectionWindow(int windowID)
    {
        if (GUI.Button(new Rect(225, 450, 150, 35), "Continue"))
        {
            UpdateCarListForSelectedTeam();
            menuSelection = 1;
            carSelected = 0;
            carSelection(carSelected, transform.localRotation);
            champSystem.championshipTeams[teamSelectedByPlayer].isPlayerAssignedToTeam = true;
        }
        int row = 0;
        int column = 0;
        for (int r = 1; r < champSystem.championshipTeams.Count + 1; r++)
        {
            ShadowAndOutline.DrawShadow(new Rect(20 + column * 110, 50 + row * 145, 100, 35), new GUIContent(champSystem.championshipTeams[r - 1].teamName), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
            if (GUI.Button(new Rect(15 + column * 110, 75 + row * 145, 100, 100), champSystem.championshipTeams[r - 1].icon))
            {
                teamSelectedByPlayer = r - 1;
            }
            column++;
            if (r % 5 == 0)
            {
                row++;
                column = 0;
            }
        }
        ShadowAndOutline.DrawShadow(new Rect(25, 450, 300, 50), new GUIContent("Current Team: " + champSystem.championshipTeams[teamSelectedByPlayer].teamName), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
        GUI.DragWindow();
    }

    void ChampionshipSelectionWindow(int windowID)
    {
        // Track to Race
        int row = 0;
        int column = 0;
        for (int r = 1; r < champSystem.championShipData.Count + 1; r++)
        {
            ShadowAndOutline.DrawShadow(new Rect(20 + column * 110, 50 + row * 145, 100, 35), new GUIContent(champSystem.championShipData[r - 1].ChampionShipName), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
            if (GUI.Button(new Rect(15 + column * 110, 75 + row * 145, 100, 100), champSystem.championShipData[r - 1].icon))
            {
                championshipIndex = r - 1;
            }
            column++;
            if (r % 5 == 0)
            {
                row++;
                column = 0;
            }
        }
        ShadowAndOutline.DrawShadow(new Rect(25, 430, 300, 50), new GUIContent("Current Championship: " + champSystem.championShipData[championshipIndex].ChampionShipName), GUI.skin.label, Color.white, Color.black, new Vector2(2f, 2f));
        if (GUI.Button(new Rect(225, 450, 150, 35), "Race!"))
        {
            champSystem.ApplyChampionShipToLevelLoad(loadedStettings, champSystem.championShipData[championshipIndex]);
            LoadLevel();
        }
        GUI.DragWindow();
    }

    void OnGUI()
    {
        GUI.skin = myGuiSkin;
        //GUI content
        switch (menuSelection)
        {
            case 0:
                //Type of game, single race or championship
                if (haveChampionShip && typeOfRaceSelected == 0)
                {
                    raceModeWindowRect = GUILayout.Window(0, raceModeWindowRect, RaceModeWindow, "Race Mode", GUI.skin.GetStyle("window"));
                }
                else
                {
                    menuSelection = 1;
                }

                break;
            case 1:
                //Car selection menu
                if (!carChoosen)
                {
                    //Use this for mobile
                    if (GUI.Button(new Rect((Screen.width / 2 - 100), (Screen.height / 2 + (Screen.height / 2) / 1.5f), 150, 50), "Continue"))
                    {
                        loadedStettings.firstPlayerSettings.selectedCar = transform.GetComponentInChildren<IRDSCarControllerAI>().name.Replace("(Clone)", "");
                        carChoosen = true;
                        menuSelection = 2;
                    }


                    //Use this for mobile, car selection, also can work on other platforms	
                    if (buttonLeft != null && GUI.Button(new Rect(Mathf.Clamp((Screen.width * buttonLeftPos.x) - buttonLeftSize.x, 0f, float.MaxValue), Mathf.Clamp((Screen.height * buttonLeftPos.y) - buttonLeftSize.y, 0f, float.MaxValue), buttonLeftSize.x, buttonLeftSize.y), buttonLeft))
                    {
                        if (carSelected > 0)
                            carSelected--;
                        else carSelected = tempCars.Length - 1;
                        carSelection(carSelected, transform.localRotation);
                    }

                    if (buttonRight != null && GUI.Button(new Rect(Mathf.Clamp((Screen.width * buttonRightPos.x) - buttonRightSize.x, 0f, float.MaxValue), Mathf.Clamp((Screen.height * buttonRightPos.y) - buttonRightSize.y, 0f, float.MaxValue), buttonRightSize.x, buttonRightSize.y), buttonRight))
                    {
                        if (carSelected < tempCars.Length - 1)
                            carSelected++;
                        else carSelected = 0;
                        carSelection(carSelected, transform.localRotation);
                    }

                    //color chooser window
                    chooseColorWindowRect = GUI.Window(1, chooseColorWindowRect, ColorSelectionWindow, "Choose Color", GUI.skin.GetStyle("window"));

                    //Race settings window
                    raceSettingsWindowRect = GUI.Window(2, raceSettingsWindowRect, RaceSettingsWindow, "Race Settings", GUI.skin.GetStyle("window"));

                }
                break;
            case 2:
                //Selection of the championship or track, depends on what option was selected on step 0.
                switch (typeOfRaceSelected)
                {
                    case 1:
                        //Single track race

                        //Track Choose window
                        trackChooseWindowRect = GUI.Window(4, trackChooseWindowRect, TrackChooseWindow, "Select Course", GUI.skin.GetStyle("window"));

                        //Graphics quality window
                        graphicsQualityWindowRect = GUI.Window(3, graphicsQualityWindowRect, GraphicsQualityWindow, "Graphics Settings", GUI.skin.GetStyle("window"));

                        break;

                    case 2:

                        //Championship race window
                        championshipChooseWindowRect = GUI.Window(6, championshipChooseWindowRect, ChampionshipSelectionWindow, "Select Championship", GUI.skin.GetStyle("window"));

                        //Graphics quality window
                        graphicsQualityWindowRect = GUI.Window(3, graphicsQualityWindowRect, GraphicsQualityWindow, "Graphics Settings", GUI.skin.GetStyle("window"));

                        champSystem.playerSelectedTeam = teamSelectedByPlayer;

                        break;
                }
                break;
            case 3:
                //Team selection menu for the player
                //This is only for Championship races
                //Team selection window
                teamChooseWindowRect = GUI.Window(5, teamChooseWindowRect, TeamSelectionWindow, "Select Team", GUI.skin.GetStyle("window"));
                break;
        }

    }



    void ChangeCarColor(Color32 carColor)
    {
        MeshRenderer[] tempMaterial = tempObj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in tempMaterial)
            foreach (Material material in mesh.materials)
                foreach (Shader shader in loadedStettings.carShader)
                    if ((material.shader.name.ToString() == shader.name.ToString())) material.SetColor("_Color", carColor);
        //			if ((material.shader.name.ToString().Length> 8?material.shader.name.ToString().Substring(0,9): "no") == "Car Paint") material.SetColor("_Color",carColor);
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(loadedStettings.trackToRace); 
    }

}
