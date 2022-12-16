using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace IRDS.ChampionshipSystem
{
    public class ChampionShipSystem : MonoBehaviour
    {

        #region public members

        /// <summary>
        /// The champion ship data.
        /// </summary>
        [SerializeField]
        public List<ChampionShipData> championShipData = new List<ChampionShipData>();

        /// <summary>
        /// The championship teams.
        /// </summary>
        [SerializeField]
        public List<ChampionShipData.ChampionShipTeam> championshipTeams = new List<ChampionShipData.ChampionShipTeam>();

        /// <summary>
        /// The champion ship drivers.
        /// </summary>
        [SerializeField]
        public List<ChampionShipData.ChampionShipDrivers> championShipDrivers = new List<ChampionShipData.ChampionShipDrivers>();

        /// <summary>
        /// The champion ship points.
        /// </summary>
        [SerializeField]
        public ChampionShipData.ChampionShipPoints championShipPoints = new ChampionShipData.ChampionShipPoints();

        /// <summary>
        /// The default levelload.
        /// </summary>
        public static IRDSLevelLoadVariables defaultLevelload;

        /// <summary>
        /// The original cars.
        /// </summary>
        public GameObject[] originalCars;

        /// <summary>
        /// The default cars path.
        /// </summary>
        public List<IRDSLevelLoadVariables.IRDSCarsPaths> defaultCarsPath;

        /// <summary>
        /// The original car folders.
        /// </summary>
        public string[] originalCarFolders;

        /// <summary>
        /// My GUI skin.
        /// </summary>
        public GUISkin myGUISkin;

        /// <summary>
        /// The player selected team.
        /// </summary>
        public int playerSelectedTeam = 0;

        /// <summary>
        /// The player total points.
        /// </summary>
        public float playerTotalPoints = 0f;

        /// <summary>
        /// The main menu scene.
        /// </summary>
        public string mainMenuScene;

        #endregion

        #region private members


        /// <summary>
        /// The stats.  Local reference to the IRDSStatistics object of this race.
        /// </summary>
        private IRDSStatistics stats;

        /// <summary>
        /// All drivers.  Local reference of all drivers on the race.
        /// </summary>
        private List<IRDSCarControllerAI> allDrivers;

        /// <summary>
        /// The drivers total points.  This is just for showing at the end of the race, and for calculating it just once.
        /// </summary>
        private string[] driversTotalPoints;

        /// <summary>
        /// The team total points.  This is just for showing at the end of the race, and for calculating it just once.
        /// </summary>
        private string[] teamTotalPoints;

        /// <summary>
        /// The drivers count.
        /// </summary>
        private int driversCount;

        /// <summary>
        /// The end results option.
        /// </summary>
        private int endResultsOption = 0;

        private GUIContent[] endResultsOptions;

        #endregion

        #region static members

        /// <summary>
        /// The current champion ship.
        /// </summary>
        public static ChampionShipData currentChampionShip;

        public static ChampionShipScores currentChampionshipScore;

        static ChampionShipSystem _instance;

        public static ChampionShipSystem Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GameObject.FindObjectOfType<ChampionShipSystem>();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }


        #endregion


        public class ChampionShipScores
        {
            public string name;
            public RaceScores[] rScores;
            public class RaceScores
            {
                public string name;
                public DriverScores[] scores;
                public class DriverScores
                {
                    public string name;
                    public string team;
                    public float points;
                    public string tTime;
                    public string fTime;
                }
                public float GetTeamScore(string teamName)
                {
                    float points = 0;
                    foreach (DriverScores score in scores)
                    {
                        if (score.team == teamName)
                        {
                            points += score.points;
                        }
                    }
                    return points;
                }
                public DriverScores GetDriverByName(string name)
                {
                    foreach (DriverScores score in scores)
                    {
                        if (score.name == name)
                        {
                            return score;
                        }
                    }
                    //Not found returning null
                    return null;
                }
            }

            public float GetTotalTeamScore(string teamName)
            {
                float points = 0;
                foreach (RaceScores rScore in rScores)
                {
                    if (rScore != null)
                    {
                        foreach (RaceScores.DriverScores score in rScore.scores)
                        {
                            if (score.team == teamName)
                            {
                                points += score.points;
                            }
                        }
                    }
                }
                return points;
            }

            public float GetTotalDriverScore(string name)
            {
                float totalPoints = 0;
                foreach (RaceScores rScore in rScores)
                {
                    if (rScore != null)
                    {
                        foreach (RaceScores.DriverScores score in rScore.scores)
                        {
                            if (score.name == name)
                            {
                                totalPoints += score.points;
                            }
                        }
                    }
                }
                return totalPoints;
            }
        }


        #region Main Code

        void Awake()
        {
            _instance = this;
            defaultLevelload = GameObject.FindObjectOfType(typeof(IRDSLevelLoadVariables)) as IRDSLevelLoadVariables;
            originalCars = defaultLevelload.CarsForRace.Clone() as GameObject[];
            defaultCarsPath = new List<IRDSLevelLoadVariables.IRDSCarsPaths>(defaultLevelload.preloadedCarsPath);
            originalCarFolders = defaultLevelload.carsFolders.Clone() as string[];


        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnLevelLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelLoaded;
        }
         
        void OnLevelLoaded(Scene scene, LoadSceneMode mode)
        {
            if (IRDSStatistics.Instance != null)
            {
                IRDSStatistics.Instance.onCurrentCarChanged -= CarHasChanged;
                IRDSStatistics.Instance.onCurrentCarChanged += CarHasChanged;
                CarHasChanged();
            }
            enableGUI = false;
        }

        void Start()
        {
            stats = GameObject.FindObjectOfType(typeof(IRDSStatistics)) as IRDSStatistics;
            AddPlayerToChampionshipDrivers();
            endResultsOptions = new GUIContent[2];
            endResultsOptions[0] = new GUIContent("Drivers results");
            endResultsOptions[1] = new GUIContent("Teams results");
            if (IRDSStatistics.Instance != null)
            {
                IRDSStatistics.Instance.onCurrentCarChanged -= CarHasChanged;
                IRDSStatistics.Instance.onCurrentCarChanged += CarHasChanged;
                CarHasChanged();
            }
        }


        bool enableGUI = false;

        void CarHasChanged()
        {
            if (IRDSStatistics.GetCurrentCar() != null)
            {
                IRDSStatistics.CurrentCar.NavigateTWaypoints.OnRaceEnded -= ProcessEndRace;
                IRDSStatistics.CurrentCar.NavigateTWaypoints.OnRaceEnded += ProcessEndRace;
            }
        }

        public void CheckIfResultsProcessed()
        {
            ProcessEndRace();
        }

        void ProcessEndRace()
        {

            //		if (!IRDSStatistics.GetCanRace())enableGUI = false;
            if (!enableGUI)
            {
                if (stats == null) stats = GameObject.FindObjectOfType(typeof(IRDSStatistics)) as IRDSStatistics;
                if (stats != null)
                    allDrivers = stats.GetAllDriversList();
                driversCount = allDrivers.Count;
                //The current car have ended the race, show the statistics of this race and all the other stuff.
                enableGUI = true;

                //Process all drivers earned points for this race
                if (currentChampionShip == null) return;
                if (currentChampionShip.currentTrack == 0)
                {
                    currentChampionshipScore = new ChampionShipScores();
                    currentChampionshipScore.name = currentChampionShip.ChampionShipName;
                    currentChampionshipScore.rScores = new ChampionShipScores.RaceScores[currentChampionShip.championshipTracks.Count];
                }
                currentChampionshipScore.rScores[currentChampionShip.currentTrack] = GetCurrentRaceScores();
                //			ProcessDriversPoints();
                //DebugCurrenChampScores();
                //StartCoroutine(CallProcessDrivers());
            }
        }

        void AddPlayerToChampionshipDrivers()
        {
            bool check = true;
            for (int i = 0; i < championShipDrivers.Count; i++)
                if (championShipDrivers[i].imPlayer)
                    check = false;
            if (check)
            {
                championShipDrivers.Add(new ChampionShipData.ChampionShipDrivers());
                championShipDrivers[championShipDrivers.Count - 1].imPlayer = true;
                championShipDrivers[championShipDrivers.Count - 1].AIcarsDrivers = new AICarsDrivers();
            }

        }


        IEnumerator CallProcessDrivers()
        {

            yield return new WaitForSeconds(2f);
            ProcessDriversPoints();
        }

        /// <summary>
        /// Processes the drivers points.
        /// </summary>
        void ProcessDriversPoints()
        {
            if (currentChampionShip == null) return;
            driversTotalPoints = new string[driversCount];
            teamTotalPoints = new string[currentChampionShip.championshipTeams.Count];
            //Reseet last team points
            for (int i = 0; i < championshipTeams.Count; i++)
                championshipTeams[i].lastRacePoints = 0;

            for (int i = 0; i < driversCount; i++)
            {
                float points = 0;
                if (currentChampionShip.useGlobalPoints)
                {

                    if (allDrivers[i].racePosition - 1 < championShipPoints.points.Count)
                        points = championShipPoints.points[allDrivers[i].racePosition - 1];
                }
                else
                {
                    if (allDrivers[i].racePosition - 1 < currentChampionShip.championShipPoints.points.Count)
                        points = currentChampionShip.championShipPoints.points[allDrivers[i].racePosition - 1];
                }
                driversTotalPoints[i] = (GetDriverByName(allDrivers[i].GetDriverName()).driverPoints += points).ToString();
                GetTeamFromDriverName(allDrivers[i].GetDriverName()).lastRacePoints += points;

            }

            for (int i = 0; i < currentChampionShip.championshipTeams.Count; i++)
            {
                teamTotalPoints[i] = championshipTeams[currentChampionShip.championshipTeams[i]].TeamPoints(championShipDrivers).ToString();
            }
        }
         
        public void DebugCurrenChampScores()
        {
            Debug.Log("Championship name ->" + currentChampionshipScore.name);
            foreach (ChampionShipScores.RaceScores rscore in currentChampionshipScore.rScores)
            {
                if (rscore != null)
                {
                    Debug.Log("Race name->" + rscore.name);
                    foreach (ChampionShipScores.RaceScores.DriverScores dscore in rscore.scores)
                    {
                        Debug.Log("Driver name-> " + dscore.name + " Total Time->" + dscore.tTime + " Fastest Time->" + dscore.fTime + " Team->" + dscore.team + " Points->" + dscore.points);
                    }
                }
            }
        }

        public ChampionShipScores.RaceScores GetCurrentRaceScores()
        {
            if (currentChampionShip == null) return null;
            driversTotalPoints = new string[driversCount];
            teamTotalPoints = new string[currentChampionShip.championshipTeams.Count];
            //Reseet last team points
            for (int i = 0; i < championshipTeams.Count; i++)
                championshipTeams[i].lastRacePoints = 0;

            ChampionShipScores.RaceScores rScores = new ChampionShipScores.RaceScores();


            rScores.name = currentChampionShip.championshipTracks[currentChampionShip.currentTrack].name;
            rScores.scores = new ChampionShipScores.RaceScores.DriverScores[driversCount];
            for (int i = 0; i < driversCount; i++)
            {

                float points = 0;
                if (currentChampionShip.useGlobalPoints)
                {
                    if (allDrivers[i].racePosition - 1 < championShipPoints.points.Count)
                        points = championShipPoints.points[allDrivers[i].racePosition - 1];
                }
                else
                {
                    if (allDrivers[i].racePosition - 1 < currentChampionShip.championShipPoints.points.Count)
                        points = currentChampionShip.championShipPoints.points[allDrivers[i].racePosition - 1];
                }
                driversTotalPoints[i] = (GetDriverByName(allDrivers[i].GetDriverName()).driverPoints += points).ToString();
                GetTeamFromDriverName(allDrivers[i].GetDriverName()).lastRacePoints += points;
                rScores.scores[i] = new ChampionShipScores.RaceScores.DriverScores();
                rScores.scores[i].name = allDrivers[i].driverName;
                rScores.scores[i].points = points;
                rScores.scores[i].team = GetTeamFromDriverName(allDrivers[i].driverName).teamName;
                rScores.scores[i].tTime = allDrivers[i].GetTotalTimeString();
                rScores.scores[i].fTime = allDrivers[i].GetFastestLapTimeString();

            }
            for (int i = 0; i < currentChampionShip.championshipTeams.Count; i++)
            {
                teamTotalPoints[i] = championshipTeams[currentChampionShip.championshipTeams[i]].TeamPoints(championShipDrivers).ToString();
            }
            return rScores;

        }


        //OLD CODE FOR THE GUI OF THE END RESULTS
        //void OnGUI()
        //{
        //    if (enableGUI)
        //    {
        //        if (myGUISkin != null) GUI.skin = myGUISkin;
        //        //This is for showing the end standings at the race end and some other options too.
        //        endRaceResultWindowRect = GUILayout.Window(10, endRaceResultWindowRect, EndRaceResultsWindow, "End Race Results", GUI.skin.GetStyle("window"));
        //    }
        //}

        public void LoadMainMenuScene()
        {
            if (currentChampionShip != null)
            {
                currentChampionShip.currentTrack = 0;
                currentChampionShip = null;
            }
            SceneManager.LoadScene(mainMenuScene);
        }

        public void LoadNextTrack()
        {
            currentChampionShip.currentTrack++;
            IRDSLevelLoadVariables.Instance.laps = currentChampionShip.championshipTracks[currentChampionShip.currentTrack].laps;
            SceneManager.LoadScene(currentChampionShip.championshipTracks[currentChampionShip.currentTrack].sceneName);
        }


        /// <summary>
        /// End race results window.
        /// </summary>
        /// <param name="windowID">Window I.</param>
        void EndRaceResultsWindow(int windowID)
        {
            if (currentChampionShip != null)
            {
                endResultsOption = GUILayout.Toolbar(endResultsOption, endResultsOptions);
                switch (endResultsOption)
                {
                    case 0:
                        ShowDriversResults();
                        break;
                    case 1:
                        ShowTeamsResults();
                        break;
                    case 2:
                        currentChampionShip.currentTrack++;
                        endResultsOption = 0;
                        IRDSLevelLoadVariables.Instance.laps = currentChampionShip.championshipTracks[currentChampionShip.currentTrack].laps;
                        enableGUI = false;
                        SceneManager.LoadScene(currentChampionShip.championshipTracks[currentChampionShip.currentTrack].sceneName);
                        break;
                    case 3:
                        currentChampionShip.currentTrack = 0;
                        endResultsOption = 0;
                        enableGUI = false;
                        SceneManager.LoadScene(mainMenuScene);
                        break;
                }
                GUILayout.BeginHorizontal();

                //We check if this is not the last track, if it is the last track we just dont show this button
                if (currentChampionShip.currentTrack < currentChampionShip.championshipTracks.Count - 1)
                {
                    if (GUILayout.Button("Next race!"))
                    {
                        endResultsOption = 2;
                    }
                    if (GUILayout.Button("Abandon Championship"))
                    {
                        endResultsOption = 3;
                    }
                }
                else
                {
                    //This is the last track of this championship, we just can give one option, return to main menu
                    if (GUILayout.Button("Main Menu"))
                    {
                        endResultsOption = 3;
                    }
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                if (GUILayout.Button("Main Menu"))
                {
                    endResultsOption = 0;
                    enableGUI = false;
                    SceneManager.LoadScene(mainMenuScene);
                }
            }
        }

        /// <summary>
        /// Shows the drivers results.
        /// </summary>
        void ShowDriversResults()
        {
            //Headers Labels
            GUILayout.BeginHorizontal();
            GUILayout.Label("Driver", GUILayout.Width(150));
            GUILayout.Label("Team", GUILayout.Width(150));
            GUILayout.Label("Total time", GUILayout.Width(120));
            GUILayout.Label("Best time", GUILayout.Width(120));
            GUILayout.Label("Points", GUILayout.Width(50));
            GUILayout.Label("Total points", GUILayout.Width(50));
            GUILayout.EndHorizontal();


            if (allDrivers != null)
            {
                for (int i = 0; i < driversCount; i++)
                {
                    GUILayout.BeginHorizontal();
                    //Driver name
                    GUILayout.Label(allDrivers[i].GetDriverName(), GUILayout.Width(150));

                    //Team name
                    GUILayout.Label(GetTeamFromDriverName(allDrivers[i].GetDriverName()).teamName, GUILayout.Width(150));
                    GUILayout.Label(allDrivers[i].GetTotalTimeString(), GUILayout.Width(120));
                    GUILayout.Label(allDrivers[i].GetFastestLapTimeString(), GUILayout.Width(120));
                    if (currentChampionShip.useGlobalPoints)
                    {
                        float points = 0;
                        if (allDrivers[i].racePosition - 1 < championShipPoints.points.Count)
                            points = championShipPoints.points[allDrivers[i].racePosition - 1];
                        GUILayout.Label(points.ToString(), GUILayout.Width(50));
                    }
                    else
                    {
                        float points = 0;
                        if (allDrivers[i].racePosition - 1 < currentChampionShip.championShipPoints.points.Count)
                            points = currentChampionShip.championShipPoints.points[allDrivers[i].racePosition - 1];
                        GUILayout.Label(points.ToString(), GUILayout.Width(50));
                    }

                    //Get the total points the player have
                    if (driversTotalPoints != null)
                        GUILayout.Label(driversTotalPoints[i], GUILayout.Width(50));
                    GUILayout.EndHorizontal();
                }
            }
        }


        /// <summary>
        /// Shows the teams results.
        /// </summary>
        void ShowTeamsResults()
        {
            //Headers Labels
            GUILayout.BeginHorizontal();
            GUILayout.Label("Team", GUILayout.Width(200));
            GUILayout.Label("Points", GUILayout.Width(50));
            GUILayout.Label("Total points", GUILayout.Width(50));
            GUILayout.EndHorizontal();

            if (allDrivers != null)
            {
                for (int i = 0; i < currentChampionShip.championshipTeams.Count; i++)
                {
                    int teamIndex = currentChampionShip.championshipTeams[i];
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(championshipTeams[teamIndex].teamName, GUILayout.Width(200));
                    //gets the player team name
                    GUILayout.Label(championshipTeams[teamIndex].lastRacePoints.ToString(), GUILayout.Width(50));
                    //Get the total points the player have
                    if (teamTotalPoints != null)
                        GUILayout.Label(teamTotalPoints[i], GUILayout.Width(50));
                    GUILayout.EndHorizontal();
                }
            }
        }




        /// <summary>
        /// Applies the champion ship to level load.  You have to setup the championship player before running this method.
        /// </summary>
        /// <param name="levelLoad">Level load.</param>
        /// <param name="selectedChampionShip">Selected champion ship.</param>
        public void ApplyChampionShipToLevelLoad(IRDSLevelLoadVariables levelLoad,
                                                        ChampionShipData selectedChampionShip)
        {

            //We set to the currentChampionShip singleton the current championship fur future reference when the race ends
            currentChampionShip = selectedChampionShip;

            //We set here the current track to race for this championship
            levelLoad.trackToRace = selectedChampionShip.championshipTracks[selectedChampionShip.currentTrack].sceneName;
            levelLoad.laps = selectedChampionShip.championshipTracks[selectedChampionShip.currentTrack].laps;



            //Set all car folders available to disabled
            for (int i = 0; i < levelLoad.preloadedCarsPath.Count; i++)
            {
                levelLoad.preloadedCarsPath[i].enabled = false;
                levelLoad.preloadedCarsPath[i].enabledForAI = false;
                levelLoad.preloadedCarsPath[i].enabledForPlayers = false;
            }



            //Clean all assigned drivers on the levelload object, since every championship have its own
            //AI drivers
            levelLoad.carsDrivers = new List<AICarsDrivers>();


            levelLoad.assignCarsToAIDrivers = true;
            int playerIndex = 0;
            for (int i = 0; i < championShipDrivers.Count; i++)
            {

                if (championShipDrivers[i].imPlayer)
                {
                    championShipDrivers[i].AIcarsDrivers.driverName = levelLoad.firstPlayerSettings.playerName;
                    playerIndex = i;
                }
            }

            if (!championshipTeams[playerSelectedTeam].teamDrivers.Contains(playerIndex))
                championshipTeams[playerSelectedTeam].teamDrivers.Add(playerIndex);





            //Team loop
            for (int teamsIndex = 0; teamsIndex < selectedChampionShip.championshipTeams.Count; teamsIndex++)
            {
                // Car in this team loop
                int currentTeam = selectedChampionShip.championshipTeams[teamsIndex];
                for (int teamCarsIndex = 0; teamCarsIndex < championshipTeams[currentTeam].teamCars.teamCars.Count; teamCarsIndex++)
                {
                    //Car in levelload loop, to search if they are in there and enable them
                    bool found = false;
                    if (championshipTeams[currentTeam].teamCars.teamCars.Count > 0)
                    {
                        levelLoad.preloadCarsEnabled = true;
                        for (int i = 0; i < levelLoad.preloadedCarsPath.Count; i++)
                        {
                            if (levelLoad.preloadedCarsPath[i].folderName == championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].folderName)
                            {
                                found = true;
                                levelLoad.preloadedCarsPath[i].enabled = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabled;
                                levelLoad.preloadedCarsPath[i].enabledForAI = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabledForAI;
                                levelLoad.preloadedCarsPath[i].enabledForPlayers = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabledForPlayers;
                                if (championshipTeams[currentTeam].isPlayerAssignedToTeam && championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabledForPlayers && levelLoad.firstPlayerSettings.selectedCar == "")
                                {

                                    //We check if the actual car that the player selected is available on the team
                                    bool currentlySelectedCarExists = false;
                                    for (int r = 0; r < championshipTeams[currentTeam].teamCars.teamCars.Count; r++)
                                    {
                                        for (int w = 0; w < championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath.Count; w++)
                                        {
                                            string[] carStrings = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath[w].Split('\\');
                                            if (carStrings[carStrings.Length - 1] == levelLoad.firstPlayerSettings.selectedCar)
                                                currentlySelectedCarExists = true;
                                        }
                                    }
                                    if (!currentlySelectedCarExists)
                                    {
                                        int selectedCar = Random.Range(0, championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath.Count);
                                        string[] carStrings = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath[selectedCar].Split('\\');
                                        levelLoad.firstPlayerSettings.selectedCar = carStrings[carStrings.Length - 1];
                                    }
                                }
                                //							else levelLoad.preloadedCarsPath[i].enabledForPlayers = false;
                            }
                        }
                        //If the path have not been found, we add it
                        if (!found)
                        {
                            IRDSLevelLoadVariables.IRDSCarsPaths carPath = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex];
                            carPath.enabled = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabled;
                            carPath.enabledForAI = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabledForAI;
                            carPath.enabledForPlayers = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabledForPlayers;
                            if (carPath.enabledForPlayers && championshipTeams[currentTeam].isPlayerAssignedToTeam && levelLoad.firstPlayerSettings.selectedCar == "")
                            {

                                //We check if the actual car that the player selected is available on the team
                                bool currentlySelectedCarExists = false;
                                for (int r = 0; r < championshipTeams[currentTeam].teamCars.teamCars.Count; r++)
                                {
                                    for (int w = 0; w < championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath.Count; w++)
                                    {
                                        string[] carStrings = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath[w].Split('\\');
                                        if (carStrings[carStrings.Length - 1] == levelLoad.firstPlayerSettings.selectedCar)
                                            currentlySelectedCarExists = true;
                                    }
                                }
                                if (!currentlySelectedCarExists)
                                {

                                    int selectedCar = Random.Range(0, championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath.Count);
                                    carPath.enabledForAI = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].enabledForAI;
                                    string[] carStrings = championshipTeams[currentTeam].teamCars.teamCars[teamCarsIndex].preloadedCarsPath[selectedCar].Split('\\');
                                    levelLoad.firstPlayerSettings.selectedCar = carStrings[carStrings.Length - 1];
                                }
                            }
                            //						else carPath.enabledForPlayers = false;
                            levelLoad.preloadedCarsPath.Add(carPath);
                        }
                    }

                    if (championshipTeams[currentTeam].teamCars.carsArray.Length > 0)
                    {
                        for (int i = 0; i < championshipTeams[currentTeam].teamCars.carsArray.Length; i++)
                        {
                            System.Array.Resize<GameObject>(ref levelLoad.CarsForRace, levelLoad.CarsForRace.Length + 1);
                            levelLoad.CarsForRace[levelLoad.CarsForRace.Length - 1] = championshipTeams[currentTeam].teamCars.carsArray[i];
                        }

                    }
                }
                //We add now all the AI drivers we have on this ChampionShip to the levelload,
                //Also we activate the assignCarsToAIDrivers option so the System would use
                //The preassigned AI drivers we have on this ChampionShip and that we are going to
                //Pass to the levelload object below

                for (int i = 0; i < championshipTeams[currentTeam].teamDrivers.Count; i++)
                {
                    //We check here if on this team is the player and if it replaces the first AI of the team
                    int driverIndex = championshipTeams[currentTeam].teamDrivers[i];
                    if (!championShipDrivers[driverIndex].imPlayer)
                    {
                        bool test = (
                            championshipTeams[currentTeam].isPlayerAssignedToTeam
                            &&
                            ((!championshipTeams[currentTeam].playerReplacesFirstAI
                            )
                            || (championshipTeams[currentTeam].playerReplacesFirstAI
                            && i > 0)))
                            || !championshipTeams[currentTeam].isPlayerAssignedToTeam;

                        if (test)
                        {
                            levelLoad.carsDrivers.Add(championShipDrivers[championshipTeams[currentTeam].teamDrivers[i]].AIcarsDrivers);
                            levelLoad.carsDrivers[levelLoad.carsDrivers.Count - 1].car = GetRandomCarTeam(championshipTeams[currentTeam], true, false);
                        }
                    }

                }

            }



        }

        /// <summary>
        /// Gets the random car team.
        /// </summary>
        /// <returns>
        /// The random car team.
        /// </returns>
        /// <param name='championshipTeams'>
        /// Championship teams.
        /// </param>
        public string GetRandomCarTeam(ChampionShipData.ChampionShipTeam championshipTeams, bool checkAI, bool checkPlayer)
        {
            List<string> carList = new List<string>();
            if (championshipTeams.teamCars.teamCars.Count > 0)
            {
                for (int i = 0; i < championshipTeams.teamCars.teamCars.Count; i++)
                {
                    if (championshipTeams.teamCars.teamCars[i].enabled &&
                        ((!checkAI || (checkAI && championshipTeams.teamCars.teamCars[i].enabledForAI))
                        && (!checkPlayer || (checkPlayer && championshipTeams.teamCars.teamCars[i].enabledForPlayers))))
                        for (int w = 0; w < championshipTeams.teamCars.teamCars[i].preloadedCarsPath.Count; w++)
                        {

                            string[] carStrings = championshipTeams.teamCars.teamCars[i].preloadedCarsPath[w].Split('\\');
                            carList.Add(carStrings[carStrings.Length - 1]);
                        }
                }
            }
            if (championshipTeams.teamCars.carsArray.Length > 0)
            {
                for (int i = 0; i < championshipTeams.teamCars.carsArray.Length; i++)
                {
                    carList.Add(championshipTeams.teamCars.carsArray[i].name);
                }
            }
            return carList[Random.Range(0, carList.Count)];
        }

        /// <summary>
        /// Gets the driver championship instance by name.
        /// </summary>
        /// <returns>The driver by name.</returns>
        /// <param name="driverName">Driver name.</param>
        public ChampionShipData.ChampionShipDrivers GetDriverByName(string driverName)
        {
            for (int i = 0; i < championShipDrivers.Count; i++)
            {
                if (championShipDrivers[i].AIcarsDrivers.driverName.Equals(driverName, System.StringComparison.Ordinal))
                    return championShipDrivers[i];
            }
            return championShipDrivers[0];
        }


        public ChampionShipData.ChampionShipTeam GetTeamFromDriverName(string driverName)
        {
            for (int i = 0; i < championshipTeams.Count; i++)
            {
                for (int y = 0; y < championshipTeams[i].teamDrivers.Count; y++)

                    if (championShipDrivers[championshipTeams[i].teamDrivers[y]].AIcarsDrivers.driverName.Equals(driverName, System.StringComparison.Ordinal))
                        return championshipTeams[i];
            }
            return championshipTeams[0];
        }

        #endregion




    }
}