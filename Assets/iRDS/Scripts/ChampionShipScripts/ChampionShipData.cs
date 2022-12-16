using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace IRDS.ChampionshipSystem
{
    #region Classes
    /// <summary>
    /// Champion ship data class.
    /// </summary>
    [System.Serializable]
    public class ChampionShipData
    {
        /// <summary>
        /// The name of the champion ship.
        /// </summary>
        public string ChampionShipName;

        /// <summary>
        /// The championship icon.  This would be use to show this icon on the championship selection menu for the player
        /// on the ChooseCar script.
        /// </summary>
        public Texture2D icon;

        /// <summary>
        /// The championship teams.
        /// </summary>
        public List<int> championshipTeams = new List<int>();

        /// <summary>
        /// The championship tracks.
        /// </summary>
        public List<ChampionShipTrack> championshipTracks = new List<ChampionShipTrack>();

        /// <summary>
        /// The current track.
        /// </summary>
        public int currentTrack = 0;

        /// <summary>
        /// Use global points?
        /// </summary>
        public bool useGlobalPoints = true;

        /// <summary>
        /// The champion ship points.
        /// </summary>
        public ChampionShipPoints championShipPoints = new ChampionShipPoints();

        /// <summary>
        /// Champion ship points.
        /// </summary>
        [System.Serializable]
        public class ChampionShipPoints
        {
            public List<float> points;
        }

        /// <summary>
        /// Champion ship track class, define here all the tracks that would be included
        /// on the Championship.
        /// </summary>
        [System.Serializable]
        public class ChampionShipTrack
        {
            /// <summary>
            /// The name of the track for UI.
            /// </summary>
            public string name;

            /// <summary>
            /// The track icon.  This would be used to show this icon to the player when showin the tracks included
            /// on a championship.
            /// </summary>
            public Texture2D icon;

            /// <summary>
            /// The name of the scene for the track.
            /// </summary>
            public string sceneName;

            /// <summary>
            /// The laps for this track.
            /// </summary>
            public int laps;
        }

        /// <summary>
        /// Champion ship team class.
        /// </summary>
        [System.Serializable]
        public class ChampionShipTeam
        {
            /// <summary>
            /// The name of the team.
            /// </summary>
            public string teamName;

            /// <summary>
            /// The icon for this team.  This would be used for showing the player this icon to choose a team.
            /// </summary>
            public Texture2D icon;

            /// <summary>
            /// The team cars.
            /// </summary>
            [SerializeField]
            public TeamCars teamCars = new TeamCars();

            /// <summary>
            /// The team drivers.
            /// </summary>
            [SerializeField]
            public List<int> teamDrivers = new List<int>();

            /// <summary>
            /// Is player?, enable this if the player is assigned to it.
            /// </summary>
            public bool isPlayerAssignedToTeam = false;


            /// <summary>
            /// The player replaces the first AI?.  If enabled, the player would take
            /// the place of the first AI car.  If disabled the player would be added and all
            /// the AI remains on the team aslo.
            /// </summary>
            public bool playerReplacesFirstAI = false;

            /// <summary>
            /// Gets the team points.
            /// </summary>
            /// <value>
            /// The team points.  This method calculates the team points iterating all the drivers and
            /// adding each driver points, is not intended to be called every frame, instead call it
            /// once and store the result in a local variable.
            /// </value>
            public float TeamPoints(List<ChampionShipDrivers> drivers)
            {
                float points = 0f;
                for (int i = 0; i < teamDrivers.Count; i++)
                {

                    points += drivers[teamDrivers[i]].driverPoints;
                }

                return points;
            }

            public float lastRacePoints;
        }

        /// <summary>
        /// Champion ship drivers class.
        /// </summary>
        [System.Serializable]
        public class ChampionShipDrivers
        {

            /// <summary>
            /// Im player?.
            /// </summary>
            public bool imPlayer = false;

            /// <summary>
            /// AI cars drivers.
            /// </summary>
            public AICarsDrivers AIcarsDrivers;

            /// <summary>
            /// The driver points.
            /// </summary>
            public float driverPoints;
        }

        [System.Serializable]
        public class TeamCars
        {
            /// <summary>
            /// The team cars path.
            /// </summary>
            [SerializeField]
            public List<IRDSLevelLoadVariables.IRDSCarsPaths> teamCars = new List<IRDSLevelLoadVariables.IRDSCarsPaths>();

            /// <summary>
            /// The cars array.  You can assign here the car game objects directly
            /// </summary>
            public GameObject[] carsArray;
        }

    }

    #endregion
}
