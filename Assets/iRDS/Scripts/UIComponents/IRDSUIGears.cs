using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI
{
    public class IRDSUIGears : MonoBehaviour
    {
        public bool playAnimation = false;
        public string gearChangeAnimation;
        public Animator UIAnimator;
        public IRDSDrivetrain targetCar;

        Text gearText;

        // Use this for initialization
        void Start()
        {
            gearText = GetComponent<Text>();
            if (UIAnimator == null)
            {
                UIAnimator = GetComponent<Animator>();
                if (UIAnimator == null)
                {
                    UIAnimator = transform.root.GetComponent<Animator>();
                }
            }
            GetFirstCar();
        }

        void GetFirstCar()
        {
            if (targetCar == null)
            {
                targetCar = FindObjectOfType<IRDSDrivetrain>();
            }
        }

        void OnEnable()
        {
            GetDelegatesReferences();
        }

        void OnDisable()
        {
            RemoveDelegatesReferences();
        }

        void GetDelegatesReferences()
        {
            if (IRDSStatistics.Instance != null)
            { IRDSStatistics.Instance.onCurrentCarChanged += OnCurrentCarChanged; }
        }
        void RemoveDelegatesReferences()
        {
            if (IRDSStatistics.Instance == null) return;
            { IRDSStatistics.Instance.onCurrentCarChanged -= OnCurrentCarChanged; }
        }


        /// <summary>
        /// Raises the lap change event.
        /// </summary>
        /// <param name="lap">Lap.</param>
        void OnCurrentCarChanged()
        {
            targetCar = IRDSStatistics.CurrentCar.GetComponent<IRDSDrivetrain>();
        }


        int lastGear = -1;
        void Update()
        {
            if (lastGear != targetCar.gear)
            {
                lastGear = targetCar.gear;
                switch (lastGear)
                {
                    case 0:
                        gearText.text = "R";
                        break;
                    case 1:
                        gearText.text = "N";
                        break;
                    default:
                        gearText.text = (lastGear - 1).ToString();
                        break;
                }


                //Try to play the UI animation if any
                if (playAnimation && UIAnimator != null)
                {
                    UIAnimator.Play(gearChangeAnimation);
                }
            }
        }

    }
}