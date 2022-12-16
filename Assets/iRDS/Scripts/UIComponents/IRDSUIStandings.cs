using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using IRDS.ChampionshipSystem;

namespace IRDS.UI
{
    [RequireComponent(typeof(IRDSUIStandingsLayoutGroup))]
    public class IRDSUIStandings : MonoBehaviour
    {

        //		class TextItem{
        //			public Text text;
        ////			public StandingsDetails standingDetail;
        //		}
        class StandingsItems
        {

            /// <summary>
            /// The standings items rects.
            /// </summary>
            public RectTransform standingsItemsRects;

            /// <summary>
            /// My car.  Reference to the IRDSCarControllerAI class, that would be used to get the position from
            /// the car asigned to this slot.
            /// </summary>
            public IRDSCarControllerAI myCar;

            /// <summary>
            /// The details.
            /// </summary>
            public Text[] details;

            /// <summary>
            /// The parent standings.
            /// </summary>
            public IRDSUIStandings parentStandings;

            public void UpdateItemDetailsAll()
            {
                if (myCar == IRDSStatistics.CurrentCar)
                {
                    if (parentStandings.isEndRaceStanding)
                    {
                        if (parentStandings.showOnlyIfRaceEnded)
                        {
                            parentStandings.HideShowStandings(myCar.GetEndRace());
                        }
                    }
                    else if (parentStandings.hideOnRaceEnd)
                    {
                        parentStandings.HideShowStandings(!myCar.GetEndRace());
                    }
                }
                if (details[0] != null)
                    details[0].text = myCar.GetCarName();
                if (details[1] != null)
                    details[1].text = myCar.driverName;
                if (details[2] != null)
                    details[2].text = myCar.NavigateTWaypoints.Lap.ToString();

                GetTimes();

                if (details[6] != null)
                    details[6].text = myCar.racePosition.ToString();

                if (ChampionShipSystem.Instance != null
                    && ChampionShipSystem.currentChampionShip != null
                    )
                {
                    string TeamName = ChampionShipSystem.Instance.GetTeamFromDriverName(myCar.driverName).teamName;
                    if (myCar.GetEndRace())
                    {
                        ChampionShipSystem.Instance.CheckIfResultsProcessed();
                        if (details[7] != null)
                            details[7].text = ChampionShipSystem.currentChampionshipScore.rScores[ChampionShipSystem.currentChampionShip.currentTrack].GetDriverByName(myCar.driverName).points.ToString();
                        if (details[8] != null)
                            details[8].text = ChampionShipSystem.currentChampionshipScore.GetTotalDriverScore(myCar.driverName).ToString();
                        if (details[10] != null)
                            details[10].text = ChampionShipSystem.currentChampionshipScore.rScores[ChampionShipSystem.currentChampionShip.currentTrack].GetTeamScore(TeamName).ToString();
                        if (details[11] != null)
                            details[11].text = ChampionShipSystem.currentChampionshipScore.GetTotalTeamScore(TeamName).ToString();
                    }
                    else
                    {
                        if (details[7] != null)
                            details[7].text = parentStandings.stillRunningText;
                        if (details[8] != null)
                            details[8].text = parentStandings.stillRunningText;
                        if (details[10] != null)
                            details[10].text = parentStandings.stillRunningText;
                        if (details[11] != null)
                            details[11].text = parentStandings.stillRunningText;
                    }
                    if (details[9] != null)
                        details[9].text = TeamName;

                }
            }

            void OnCarPositionChanged()
            {
                if (details[6] != null)
                    details[6].text = myCar.racePosition.ToString();

            }

            void OnLapCompleted(int lap)
            {
                if (details[2] != null)
                    details[2].text = lap.ToString();

                GetTimes();
            }

            void GetTimes()
            {
                if (parentStandings.isEndRaceStanding &&
                    parentStandings.showTimesOnlyEndedCars &&
                    !myCar.GetEndRace())
                {
                    if (details[3] != null)
                        details[3].text = parentStandings.stillRunningText;
                    if (details[4] != null)
                        details[4].text = parentStandings.stillRunningText;
                    if (details[5] != null)
                        details[5].text = parentStandings.stillRunningText;
                }
                else
                {
                    if (details[3] != null)
                        details[3].text = myCar.GetEllapsedTimeString();
                    if (details[4] != null)
                        details[4].text = myCar.GetFastestLapTimeString();
                    if (details[5] != null)
                        details[5].text = myCar.GetTotalTimeString();
                }
            }

            public void ChangeColor(Color color)
            {
                for (int i = 0; i < details.Length; i++)
                {
                    if (details[i] != null)
                        details[i].color = color;
                }
            }

            public void RegisterDelegates()
            {

                myCar.NavigateTWaypoints.OnRaceEnded += UpdateItemDetailsAll;
                myCar.NavigateTWaypoints.OnLapCompletedValue += OnLapCompleted;
                myCar.OnPositionChanged += OnCarPositionChanged;
            }

            public void UnRegisterDelegates()
            {
                if (myCar != null)
                {
                    myCar.NavigateTWaypoints.OnRaceEnded -= UpdateItemDetailsAll;
                    myCar.NavigateTWaypoints.OnLapCompletedValue -= OnLapCompleted;
                    myCar.OnPositionChanged -= OnCarPositionChanged;
                }
            }

        }

        static string[] standingDetails = new string[] {
            "CarName",
            "DriverName",
            "Laps",
            "LapTime",
            "BestTime",
            "TotalTime",
            "Position",
            "DriverPoints",
            "DriverTotalPoints",
            "TeamName",
            "TeamPoints",
            "TeamTotalPoints"
		};

        /// <summary>
        /// Gets the standing details.  This is just for internal use.
        /// </summary>
        /// <value>The standing details.</value>
        public string[] StandingDetails
        {
            get
            {
                return standingDetails;
            }
        }

        /// <summary>
        /// The spacing between items.  If used with the IRDSStandings Layout Group this value would be override with the spacing
        /// value of the layout group.
        /// </summary>
        public float spacing;


        /// <summary>
        /// The normal standings swith speed.
        /// </summary>
        public float standingsSwitchSpeed = 15f;

        /// <summary>
        /// The color of the standings when is in highlight state.
        /// </summary>
        public Color standingsHighlightColor = Color.yellow;

        /// <summary>
        /// The color of the standings when its in normal state.
        /// </summary>
        public Color standingsNormalColor = Color.white;

        /// <summary>
        /// The is end race standing.
        /// </summary>
        public bool isEndRaceStanding = false;

        public bool dontShowIfChampionShipPresent = false;

        /// <summary>
        /// The is championship standings.  Is this championship standings to show drivers/teams points?
        /// </summary>
        [HideInInspector]
        public bool isChampionshipStandings = false;

        /// <summary>
        /// The show team results only.  If this is enabled and is a championship standings, onle the teams points would be shown.
        /// </summary>
        [SerializeField]
        [HideInInspector]
        private bool _showTeamResultsOnly = false;

        public bool ShowTeamResultsOnly
        {
            get
            {
                return _showTeamResultsOnly;
            }
            set
            {
                _showTeamResultsOnly = value;
            }
        }

        /// <summary>
        /// The show times only ended cars.
        /// </summary>
        [HideInInspector]
        public bool showTimesOnlyEndedCars = false;

        /// <summary>
        /// The still running text.
        /// </summary>
        [HideInInspector]
        public string stillRunningText = ".....";

        /// <summary>
        /// The show only if race ended.
        /// </summary>
        [HideInInspector]
        public bool showOnlyIfRaceEnded = true;

        /// <summary>
        /// The user interface animator.
        /// </summary>
        [HideInInspector]
        public Animator UIAnimator;

        /// <summary>
        /// The show animation.
        /// </summary>
        [HideInInspector]
        public string showAnimation = "showFinalStanding";

        /// <summary>
        /// The hide animation.
        /// </summary>
        [HideInInspector]
        public string hideAnimation = "hideFinalStanding";

        /// <summary>
        /// The hide on race end.
        /// </summary>
        [HideInInspector]
        public bool hideOnRaceEnd = false;

        /// <summary>
        /// The standing items.  These are the actual slots and car references for the in-race standings.
        /// </summary>
        StandingsItems[] standingItems;

        /// <summary>
        /// The standings initial position.
        /// </summary>
        Vector2 standingsInitialPos = Vector2.zero;

        /// <summary>
        /// The standing objects.  These are the Text objects that would be updated their text when a car changes positions
        /// This is part of the in race standings UI.
        /// </summary>
        Text[] standingObjects;

        /// <summary>
        /// The initialized state.
        /// </summary>
        bool initialized = false;

        /// <summary>
        /// The normal standings group.
        /// </summary>
        GameObject standingsPanel;


        /// <summary>
        /// The local car list.  this is a cache of the IRDSStatistics Car list.
        /// </summary>
        List<IRDSCarControllerAI> localCarList;

        /// <summary>
        /// The is hiden.
        /// </summary>
        bool isHiden = false;

        public bool Initialized
        {
            get
            {
                return initialized;
            }
        }

        /// <summary>
        /// Gets or sets the standings initial position.
        /// </summary>
        /// <value>The standings initial position.</value>
        public Vector2 StandingsInitialPos
        {
            get
            {
                return standingsInitialPos;
            }
            set
            {
                standingsInitialPos = value;
            }
        }

        void Awake()
        {

            if (UIAnimator == null)
            {
                UIAnimator = GetComponent<Animator>();
                if (UIAnimator == null)
                {
                    UIAnimator = transform.root.GetComponent<Animator>();
                }
            }
            GetDelegates();
        }

        void Start()
        {
            standingsPanel = gameObject;
            localCarList = IRDSStatistics.Instance.GetAllDriversList();
            CreateStandings();
            if (showOnlyIfRaceEnded)
            {
                if (UIAnimator != null)
                {
                    isHiden = true;
                    UIAnimator.Play(hideAnimation);
                }
                else
                {
                    isHiden = true;
                    this.transform.parent.gameObject.SetActive(false);
                }
            }
        }

        void OnDestroy()
        {
            DropDelegates();
        }

        void Update()
        {
            if (IRDSStatistics.canRace)
            {
                UpdateStandingItems(standingsSwitchSpeed);
            }
            else
            {
                UpdateStandingItems(1f / Time.deltaTime);
            }
        }


        void GetDelegates()
        {
            IRDSStatistics.Instance.onRacePositionChangedAnyCar += OnAnyPlayerPositionChanges;
            IRDSStatistics.Instance.onCurrentCarChanged += OnAnyPlayerPositionChanges;
            IRDSStatistics.onRaceRestarted += ResetMe;
        }

        void DropDelegates()
        {
            if (IRDSStatistics.Instance != null)
            {
                IRDSStatistics.Instance.onRacePositionChangedAnyCar -= OnAnyPlayerPositionChanges;
                IRDSStatistics.Instance.onCurrentCarChanged -= OnAnyPlayerPositionChanges;
            }

            IRDSStatistics.onRaceRestarted -= ResetMe;
            if (standingItems != null)
            {
                for (int i = 0; i < standingItems.Length; i++)
                    standingItems[i].UnRegisterDelegates();
            }
        }

        /// <summary>
        /// Creates the normal race standings.
        /// </summary>
        void CreateStandings()
        {

            int totalSlots = standingsPanel.transform.childCount;
            int totalCars = IRDSStatistics.Instance.GetAllDriversList().Count;
            int currentSlots = totalSlots;
            GameObject slot = standingsPanel.transform.GetChild(0).gameObject;

            while (currentSlots < totalCars)
            {
                GameObject newSlot = Instantiate(slot) as GameObject;
                newSlot.transform.SetParent(standingsPanel.transform);
                newSlot.transform.localScale = Vector3.one;
                newSlot.transform.localPosition = slot.transform.localPosition;
                newSlot.transform.localRotation = slot.transform.localRotation;
                currentSlots++;
            }

            standingObjects = standingsPanel.GetComponentsInChildren<Text>();
            AssignDriversToStandingsSlots();
            if (standingObjects.Length != totalCars)
            {
                standingObjects = null;
            }
            initialized = true;
        }

        public void ResetMe()
        {
            initialized = false;
            DropDelegates();
            Awake();
            Start();
        }

        /// <summary>
        /// Assigns the drivers to standings slots.
        /// </summary>
        void AssignDriversToStandingsSlots()
        {
            localCarList = IRDSStatistics.Instance.GetAllDriversList();
            standingItems = new StandingsItems[localCarList.Count];
            for (int i = 0; i < localCarList.Count; i++)
            {
                standingItems[i] = new StandingsItems();
                standingItems[i].standingsItemsRects = standingsPanel.transform.GetChild(i).GetComponent<RectTransform>();
                standingItems[i].myCar = localCarList[i];
                standingItems[i].parentStandings = this;
                standingItems[i].RegisterDelegates();
                standingItems[i].standingsItemsRects.anchoredPosition3D = new Vector2(StandingsInitialPos.x, StandingsInitialPos.y - (i * (standingItems[i].standingsItemsRects.rect.height + spacing)));
                GetStandingDetails(standingItems[i]);
            }
        }

        void GetStandingDetails(StandingsItems item)
        {
            int totalItems = standingDetails.Length;
            item.details = new Text[totalItems];
            for (int i = 0; i < totalItems; i++)
            {
                int childIndex = 0;
                for (int ii = 0; ii < totalItems; ii++)
                {
                    for (; childIndex < item.standingsItemsRects.childCount; childIndex++)
                    {
                        Text tempText = item.standingsItemsRects.GetChild(childIndex).GetComponent<Text>();
                        //We check the order of the Text objects found and assign them to the details array
                        //corresponding to the order of the standing details to standarize the code
                        if (tempText != null && tempText.name.ToLower().Contains(standingDetails[i].Trim().ToLower()))
                        {
                            //							item.details[i] = new TextItem();
                            item.details[i] = tempText;
                        }
                    }
                }

            }
            item.UpdateItemDetailsAll();
        }


        //This shouldn't be used if cars are going to be added and removed while the race is on
        /// <summary>
        /// Updates the standing items.
        /// </summary>
        /// <param name="switchSpeed">Switch speed.</param>
        void UpdateStandingItems(float switchSpeed)
        {
            for (int i = 0; i < localCarList.Count; i++)
            {
                standingItems[i].standingsItemsRects.anchoredPosition3D = new Vector2(standingsInitialPos.x, Mathf.Lerp(standingItems[i].standingsItemsRects.transform.localPosition.y, standingsInitialPos.y - ((standingItems[i].myCar.racePosition - 1) * (standingItems[i].standingsItemsRects.rect.height + spacing)), Time.deltaTime * switchSpeed));
            }
        }




        /// <summary>
        /// Raises the any player position changes event.
        /// </summary>
        void OnAnyPlayerPositionChanges()
        {
            for (int i = 0; i < localCarList.Count; i++)
            {
                if (standingItems[i].myCar == IRDSStatistics.CurrentCar)
                {
                    standingItems[i].ChangeColor(standingsHighlightColor);
                    if (isEndRaceStanding)
                    {
                        if (showOnlyIfRaceEnded)
                        {
                            HideShowStandings(standingItems[i].myCar.GetEndRace());
                        }
                    }
                    else
                    {
                        if (hideOnRaceEnd)
                        {
                            HideShowStandings(!standingItems[i].myCar.GetEndRace());
                        }
                    }
                }
                else standingItems[i].ChangeColor(standingsNormalColor);
            }
        }

        void HideShowStandings(bool endRace)
        {
            if (endRace
                && ((!isChampionshipStandings && !dontShowIfChampionShipPresent) || (!isChampionshipStandings && dontShowIfChampionShipPresent && ChampionShipSystem.Instance == null
                && ChampionShipSystem.currentChampionShip == null)
                || isChampionshipStandings
                && ChampionShipSystem.Instance != null
                && ChampionShipSystem.currentChampionShip != null))
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Show()
        {
            
            if (UIAnimator != null && isHiden)
            {
                isHiden = false;
                this.transform.parent.gameObject.SetActive(true);
                UIAnimator.Play(showAnimation);
            }
            else if (isHiden)
            {
                isHiden = false;
                this.transform.parent.gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            
            if (UIAnimator != null && !isHiden)
            {
                isHiden = true;
                UIAnimator.Play(hideAnimation);
            }
            else if (!isHiden)
            {
                isHiden = true;
                this.transform.parent.gameObject.SetActive(false);
            }

        }

    }
}