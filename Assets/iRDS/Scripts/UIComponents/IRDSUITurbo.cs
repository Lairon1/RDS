using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{

	public class IRDSUITurbo : MonoBehaviour {

		public enum UIType{
			Filled,
			Text,
			Gauge
		}

		/// <summary>
		/// The type of the user interface.
		/// </summary>
		public UIType uiType;

		/// <summary>
		/// The max speed of the UI.  This is useful to determine the top speed of the gauge.
		/// </summary>
		public float maxPSI = 25f;

		/// <summary>
		/// The angle minimum.
		/// </summary>
		public float angleMin;

		/// <summary>
		/// The angle maximum.
		/// </summary>
		public float angleMax;


		/// <summary>
		/// The target car.  Only need to assign this if not using the iRDSManager, (i.e. you are using the physics only)
		/// </summary>
		public IRDSDrivetrain targetCar;

		/// <summary>
		/// The gauge image.
		/// </summary>
		Image gaugeImage;

		Transform gaugeTransform;

		/// <summary>
		/// The gauge text.
		/// </summary>
		Text gaugeText;
		
		// Use this for initialization
		void Start () {
			gaugeText = GetComponent<Text>();
			gaugeImage = GetComponent<Image>();
			gaugeTransform = transform;
			GetFirstCar();
		}
		
		void GetFirstCar()
		{
			if (targetCar ==null)
				targetCar = FindObjectOfType<IRDSDrivetrain>();
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
			IRDSStatistics.Instance.onCurrentCarChanged += OnCurrentCarChanged;
		}
		void RemoveDelegatesReferences()
		{
			if (IRDSStatistics.Instance ==null)return;
			IRDSStatistics.Instance.onCurrentCarChanged -= OnCurrentCarChanged;
		}
		
		
		/// <summary>
		/// Raises the lap change event.
		/// </summary>
		/// <param name="lap">Lap.</param>
		void OnCurrentCarChanged()
		{
			targetCar = IRDSStatistics.CurrentCar.GetComponent<IRDSDrivetrain>();
		}
		
		
		int lastPSI = 0;
		void Update(){
			switch(uiType)
			{
			case UIType.Filled:
				FilledUpdate();
				break;
			case UIType.Gauge:
				GaugeUpdate();
				break;
			case UIType.Text:
				TextUpdate();
				break;
			}
		}

		void FilledUpdate()
		{
			gaugeImage.fillAmount = targetCar.turboAirPresure/maxPSI;
		}
		
		void GaugeUpdate()
		{
			IRDSUIUtility.UpdateRotation(gaugeTransform,targetCar.turboAirPresure,maxPSI,angleMin,angleMax);
		}

		int currentPSI = 0;
		void TextUpdate()
		{
			currentPSI = Mathf.RoundToInt(targetCar.turboAirPresure);
			if (lastPSI != currentPSI)
			{
				lastPSI = currentPSI;
				gaugeText.text = currentPSI.ToString();
			}
		}



	}
}