using UnityEngine;
using System.Collections;
using System.Collections.Generic;




namespace IRDS.ReplaySystem
{
    public class iRDSReplaySystem : MonoBehaviour
    {

        public enum ReplayMode
        {
            StandBy,
            Play,
            Record
        }
        [System.Serializable]
        public struct iRDSKeyFrame
        {
            public int frame;
            public Vector3 position;
            public Vector3 velocity;
            public Vector3 angularVelocity;
            public Quaternion rotation;
            public float inputSteer;
            public float inputThrottle;
            public float inputBrake;
            public float inputHandBrake;
            public float rpm;

            //These 2 are needed for flashback mode
            public int currentWaypoint;
            public float[] wheelsAngularVelo;
        }


        [System.Serializable]
        public class iRDSCarDetail
        {
            public List<iRDSKeyFrame> keyFrames = new List<iRDSKeyFrame>();
            public string carPrefabName;
            public int carGridPosition;
        }

        public List<IRDSCarControllerAI> cars;
        public List<iRDSCarDetail> carsKeyFrames = new List<iRDSCarDetail>();
        public ReplayMode replayMode = ReplayMode.StandBy;

        public IRDSCarCamera carCamera;

        private int currentFramePlay = 0;

        private int currentFrameRecord = 0;

        private int totalFrames;

        private bool paused;

        private bool isRecording;

        private bool playingBackwards = false;
        /// <summary>
        /// This is the current frame of the actual app on the fixed update loop
        /// </summary>
        private int currentSystemFrame = 0;

        /// <summary>
        /// Every how many frames from the system the replay system should play or record a frame
        /// </summary>
        public int frameRate = 5;

        public GameObject mainButtonPanels;

        public bool disableButtonsWhileRaceNotFinished = true;

        public bool autoStartRecording = true;

        public bool Paused
        {
            get
            {
                return paused;
            }
        }

        public bool IsRecording
        {
            get
            {
                return isRecording;
            }
        }

        void Start()
        {
            StartRecording();
        }

        void Awake()
        {
            if (carCamera == null)
                carCamera = FindObjectOfType<IRDSCarCamera>();
        }

        public void StartRecording()
        {

            if (IRDSStatistics.Instance != null)
            {
                cars = new List<IRDSCarControllerAI>(IRDSStatistics.Instance.GetAllDriversList());
                if (cars == null || cars.Count == 0)
                {
                    isRecording = false;
                    return;
                }
                carsKeyFrames.Clear();
                for (int i = 0; i < cars.Count; i++)
                {
                    iRDSCarDetail newCar = new iRDSCarDetail();
                    newCar.carGridPosition = cars[i].myGridPosition;
                    newCar.carPrefabName = cars[i].GetCarName();
                    newCar.keyFrames = new List<iRDSKeyFrame>();
                    carsKeyFrames.Add(newCar);
                }
                currentFrameRecord = 0;
                currentSystemFrame = 0;
                isRecording = true;
                replayMode = ReplayMode.Record;
                playingBackwards = false;
            }
        }

        public void Stop()
        {
            if (isRecording) isRecording = false;
            else
            {
                paused = true;
                Time.timeScale = 0;
                playingBackwards = false;
                replayMode = ReplayMode.StandBy;
            }
        }

        public void Play()
        {
            playingBackwards = false;
            if (paused) paused = false;
            if (isRecording) isRecording = false;
            replayMode = ReplayMode.Play;
            currentSystemFrame = 0;
            Time.timeScale = 1;
            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].GetCarInputs().carPilot = true;
                cars[i].GetComponent<IRDSPlayerControls>().enabled = false;

            }
        }

        public void Rewind()
        {
            currentFramePlay = 0;
            currentSystemFrame = 0;
        }


        /// <summary>
        /// Experimental code for future implementation of Flash backs
        /// </summary>
        public void GiveControlBack(bool continueRecording)
        {
            if (replayMode == ReplayMode.Record) return;
            for (int i = 0; i < cars.Count; i++)
            {
                if (!cars[i].IsPlayer)
                    cars[i].GetCarInputs().carPilot = false;
                cars[i].GetComponent<IRDSPlayerControls>().enabled = true;
                cars[i].GetCarInputs().setBrakeInput(carsKeyFrames[i].keyFrames[currentFramePlay].inputBrake);
                cars[i].GetCarInputs().setHandBrakeInput(carsKeyFrames[i].keyFrames[currentFramePlay].inputHandBrake);
                cars[i].GetCarInputs().setSteerInput(carsKeyFrames[i].keyFrames[currentFramePlay].inputSteer);
                cars[i].GetCarInputs().setThrottleInput(carsKeyFrames[i].keyFrames[currentFramePlay].inputThrottle);
                cars[i].GetCarInputs().body.position = carsKeyFrames[i].keyFrames[currentFramePlay].position;
                cars[i].GetCarInputs().body.rotation = carsKeyFrames[i].keyFrames[currentFramePlay].rotation;
                cars[i].GetCarInputs().body.velocity = carsKeyFrames[i].keyFrames[currentFramePlay].velocity;
                cars[i].GetCarInputs().body.angularVelocity = carsKeyFrames[i].keyFrames[currentFramePlay].angularVelocity;
                cars[i].GetCarInputs().Drivetrain.rpm = carsKeyFrames[i].keyFrames[currentFramePlay].rpm;
                cars[i].NavigateTWaypoints.currentWaypoint = carsKeyFrames[i].keyFrames[currentFramePlay].currentWaypoint;
                for (int r = 0; r < cars[i].GetCarInputs().wheels.Length; r++)
                {
                    cars[i].GetCarInputs().wheels[r].angularVelocity = carsKeyFrames[i].keyFrames[currentFramePlay].wheelsAngularVelo[r];
                }
            }
            Time.timeScale = 1;
            paused = true;
            replayMode = ReplayMode.StandBy;
            if (continueRecording)
            {
                currentFrameRecord = currentFramePlay;
                if (IRDSStatistics.Instance != null)
                {
                    if (cars == null)
                    {
                        cars = new List<IRDSCarControllerAI>(IRDSStatistics.Instance.GetAllDriversList());
                        if (cars == null || cars.Count == 0)
                        {
                            isRecording = false;
                            return;
                        }
                        for (int i = 0; i < cars.Count; i++)
                        {
                            iRDSCarDetail newCar = new iRDSCarDetail();
                            newCar.carGridPosition = cars[i].myGridPosition;
                            newCar.carPrefabName = cars[i].GetCarName();
                            newCar.keyFrames = new List<iRDSKeyFrame>();
                            carsKeyFrames.Add(newCar);
                        }
                    }
                    if (cars == null || cars.Count == 0)
                    {
                        isRecording = false;
                        return;
                    }
                    for (int i = 0; i < carsKeyFrames.Count; i++)
                    {
                        carsKeyFrames[i].keyFrames.RemoveRange(currentFrameRecord, carsKeyFrames[i].keyFrames.Count - currentFrameRecord);
                    }
                    currentSystemFrame = 0;
                    isRecording = true;
                    replayMode = ReplayMode.Record;
                    playingBackwards = false;
                }
            }
        }

        public void PlayBackwards()
        {
            if (paused) paused = false;
            if (isRecording) isRecording = false;
            replayMode = ReplayMode.Play;
            currentSystemFrame = 0;
            Time.timeScale = 0;
            for (int i = 0; i < cars.Count; i++)
            {
                //cars[i].GetCarInputs().carPilot = true;
                //    cars[i].GetComponent<IRDSPlayerControls>().enabled = false;
                cars[i].NavigateTWaypoints.debugMode = true;
            }
            playingBackwards = true;
            currentFramePlay = GetKeyFramesLength();
            if (carCamera != null)
                carCamera.ignoreTimeScale = true;
            else
            {
                carCamera = FindObjectOfType<IRDSCarCamera>();
                if (carCamera != null)
                    carCamera.ignoreTimeScale = true;
            }
        }

        int GetKeyFramesLength()
        {
            int length = 0;
            for (int i = 0; i < carsKeyFrames.Count; i++)
            {
                if (carsKeyFrames[i].keyFrames.Count > length)
                    length = carsKeyFrames[i].keyFrames.Count - 1;
            }
            return length;
        }

        public void SeekFrame(int frame)
        {
            if (replayMode != ReplayMode.Play || (frame < 0 || frame > totalFrames)) return;
            currentFramePlay = frame;
        }

        /// <summary>
        /// This method would go ahead with the frame recording or playback
        /// </summary>
        void Forward()
        {
            if (isRecording && replayMode == ReplayMode.Record)
            {
                currentSystemFrame++;
                currentFrameRecord++;
                totalFrames = currentFrameRecord;
                if (currentSystemFrame >= frameRate)
                {
                    currentSystemFrame = 0;
                    for (int i = 0; i < cars.Count; i++)
                    {
                        iRDSKeyFrame newKeyFrame = new iRDSKeyFrame();
                        newKeyFrame.frame = currentFrameRecord;
                        newKeyFrame.inputBrake = cars[i].GetCarInputs().GetBrakeInput();
                        newKeyFrame.inputHandBrake = cars[i].GetCarInputs().GetHandBrakeInput();
                        newKeyFrame.inputSteer = cars[i].GetCarInputs().GetSteerInput();
                        newKeyFrame.inputThrottle = cars[i].GetCarInputs().GetThrottleInput();
                        newKeyFrame.position = cars[i].transform.position;
                        newKeyFrame.rotation = cars[i].transform.rotation;
                        newKeyFrame.velocity = cars[i].GetCarInputs().body.velocity;
                        newKeyFrame.angularVelocity = cars[i].GetCarInputs().body.angularVelocity;
                        newKeyFrame.rpm = cars[i].GetCarInputs().Drivetrain.Rpm;
                        newKeyFrame.currentWaypoint = cars[i].NavigateTWaypoints.currentWaypoint;
                        newKeyFrame.wheelsAngularVelo = new float[cars[i].GetCarInputs().wheels.Length];

                        for (int r = 0; r < cars[i].GetCarInputs().wheels.Length; r++)
                        {
                            newKeyFrame.wheelsAngularVelo[r] = cars[i].GetCarInputs().wheels[r].angularVelocity;
                        }

                        carsKeyFrames[i].keyFrames.Add(newKeyFrame);
                    }
                }

            }
            else if (!paused && replayMode == ReplayMode.Play)
            {

                currentSystemFrame++;

                if (currentSystemFrame >= frameRate)
                {
                    if (playingBackwards)
                        currentFramePlay--;
                    else
                        currentFramePlay++;
                    currentSystemFrame = 0;
                }
                int F1 = currentFramePlay;
                int F2 = currentFramePlay + 1;
                float timeLine = (float)currentSystemFrame / (float)(frameRate - 1);
                for (int i = 0; i < cars.Count; i++)
                {
                    if (F2 > carsKeyFrames[i].keyFrames.Count - 1)
                    {
                        F1 = F2 = carsKeyFrames[i].keyFrames.Count - 1;
                        Stop();
                    }

                    if (F2 < 0)
                    {
                        F1 = F2 = 0;
                        Stop();
                    }

                    cars[i].GetCarInputs().setBrakeInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputBrake, carsKeyFrames[i].keyFrames[F2].inputBrake, timeLine));
                    cars[i].GetCarInputs().setHandBrakeInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputHandBrake, carsKeyFrames[i].keyFrames[F2].inputHandBrake, timeLine));
                    cars[i].GetCarInputs().setSteerInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputSteer, carsKeyFrames[i].keyFrames[F2].inputSteer, timeLine));
                    cars[i].GetCarInputs().setThrottleInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputThrottle, carsKeyFrames[i].keyFrames[F2].inputThrottle, timeLine));
                    if (currentFramePlay == 0)
                    {
                        for (int w = 0; w < cars[i].GetCarInputs().wheels.Length; w++)
                        {
                            cars[i].GetCarInputs().wheels[w].angularVelocity = 0;
                        }
                        cars[i].transform.position = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].position, carsKeyFrames[i].keyFrames[F2].position, timeLine);
                        cars[i].transform.rotation = Quaternion.Lerp(carsKeyFrames[i].keyFrames[F1].rotation, carsKeyFrames[i].keyFrames[F2].rotation, timeLine);
                    }
                    else
                    {
                        cars[i].GetCarInputs().body.position = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].position, carsKeyFrames[i].keyFrames[F2].position, timeLine);
                        cars[i].GetCarInputs().body.rotation = Quaternion.Lerp(carsKeyFrames[i].keyFrames[F1].rotation, carsKeyFrames[i].keyFrames[F2].rotation, timeLine);

                    }
                    for (int r = 0; r < cars[i].GetCarInputs().wheels.Length; r++)
                    {
                        cars[i].GetCarInputs().wheels[r].angularVelocity = carsKeyFrames[i].keyFrames[currentFramePlay].wheelsAngularVelo[r];
                    }



                    cars[i].GetCarInputs().body.velocity = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].velocity, carsKeyFrames[i].keyFrames[F2].velocity, timeLine);
                    cars[i].GetCarInputs().body.angularVelocity = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].angularVelocity, carsKeyFrames[i].keyFrames[F2].angularVelocity, timeLine);
                    cars[i].GetCarInputs().Drivetrain.rpm = (Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].rpm, carsKeyFrames[i].keyFrames[F2].rpm, timeLine));
                    cars[i].NavigateTWaypoints.currentWaypoint = carsKeyFrames[i].keyFrames[F1].currentWaypoint;
                }
            }
        }


        void Backwards()
        {
            if (playingBackwards && !paused && replayMode == ReplayMode.Play)
            {
                currentSystemFrame++;
                if (currentSystemFrame >= frameRate)
                {
                    if (playingBackwards)
                        currentFramePlay--;
                    else
                        currentFramePlay++;
                    currentSystemFrame = 0;
                }
                int F1 = currentFramePlay;
                int F2 = currentFramePlay - 1;
                float timeLine = (float)currentSystemFrame / (float)(frameRate - 1);
                for (int i = 0; i < cars.Count; i++)
                {
                    if (F2 < 0)
                    {
                        F1 = F2 = 0;
                        Stop();
                    }

                    cars[i].GetCarInputs().setBrakeInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputBrake, carsKeyFrames[i].keyFrames[F2].inputBrake, timeLine));
                    cars[i].GetCarInputs().setHandBrakeInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputHandBrake, carsKeyFrames[i].keyFrames[F2].inputHandBrake, timeLine));
                    cars[i].GetCarInputs().setSteerInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputSteer, carsKeyFrames[i].keyFrames[F2].inputSteer, timeLine));
                    cars[i].GetCarInputs().setThrottleInput(Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].inputThrottle, carsKeyFrames[i].keyFrames[F2].inputThrottle, timeLine));
                    if (currentFramePlay == 0 || Time.timeScale == 0)
                    {
                        for (int w = 0; w < cars[i].GetCarInputs().wheels.Length; w++)
                        {
                            cars[i].GetCarInputs().wheels[w].angularVelocity = 0;
                        }
                        cars[i].transform.position = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].position, carsKeyFrames[i].keyFrames[F2].position, timeLine);
                        cars[i].transform.rotation = Quaternion.Lerp(carsKeyFrames[i].keyFrames[F1].rotation, carsKeyFrames[i].keyFrames[F2].rotation, timeLine);
                    }
                    else
                    {
                        cars[i].GetCarInputs().body.position = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].position, carsKeyFrames[i].keyFrames[F2].position, timeLine);
                        cars[i].GetCarInputs().body.rotation = Quaternion.Lerp(carsKeyFrames[i].keyFrames[F1].rotation, carsKeyFrames[i].keyFrames[F2].rotation, timeLine);
                    }

                    for (int r = 0; r < cars[i].GetCarInputs().wheels.Length; r++)
                    {
                        cars[i].GetCarInputs().wheels[r].angularVelocity = carsKeyFrames[i].keyFrames[currentFramePlay].wheelsAngularVelo[r];
                    }


                    cars[i].GetCarInputs().body.velocity = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].velocity, carsKeyFrames[i].keyFrames[F2].velocity, timeLine);
                    cars[i].GetCarInputs().body.angularVelocity = Vector3.Lerp(carsKeyFrames[i].keyFrames[F1].angularVelocity, carsKeyFrames[i].keyFrames[F2].angularVelocity, timeLine);
                    cars[i].GetCarInputs().Drivetrain.rpm = (Mathf.Lerp(carsKeyFrames[i].keyFrames[F1].rpm, carsKeyFrames[i].keyFrames[F2].rpm, timeLine));
                    cars[i].NavigateTWaypoints.currentWaypoint = carsKeyFrames[i].keyFrames[F1].currentWaypoint;
                }
            }
        }


        void Update()
        {
            if (disableButtonsWhileRaceNotFinished)
            {
                if (IRDSStatistics.GetCurrentCar() != null && IRDSStatistics.GetCurrentCar().GetEndRace())
                {
                    if (!mainButtonPanels.activeSelf)
                        mainButtonPanels.SetActive(true);
                }
                else
                {
                    if (mainButtonPanels.activeSelf)
                        mainButtonPanels.SetActive(false);
                }
            }

            if (Input.GetKeyUp(KeyCode.R))
                StartRecording();

            if (Input.GetKeyUp(KeyCode.T))
            {

                Play();
            }

            if (Input.GetKeyUp(KeyCode.U))
            {
                Rewind();
            }

            if (Input.GetKeyUp(KeyCode.Y))
            {
                PlayBackwards();
            }

            if (Input.GetKeyUp(KeyCode.I))
            {
                Stop();
            }
            if (Input.GetKeyUp(KeyCode.G))
            {
                GiveControlBack(true);
            }

            Backwards();
        }

        /// <summary>
        /// The fixed update for playing or recording the replay
        /// </summary>
        void FixedUpdate()
        {
            Forward();
        }

    }
}