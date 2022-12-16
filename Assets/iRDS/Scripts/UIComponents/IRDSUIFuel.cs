using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace IRDS.UI{

	public class IRDSUIFuel : MonoBehaviour {

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
		public float maxFuel = 75f;

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
			if (IRDSStatistics.Instance != null)
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
		
		
		int lastFuel = 0;
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
			gaugeImage.fillAmount = targetCar.fuelTankBody.mass/maxFuel;
		}
		
		void GaugeUpdate()
		{
			IRDSUIUtility.UpdateRotation(gaugeTransform,targetCar.fuelTankBody.mass,maxFuel,angleMin,angleMax);
		}

		int currentFuel = 0;
		void TextUpdate()
		{
			currentFuel = Mathf.RoundToInt(targetCar.fuelTankBody.mass);
			if (lastFuel != currentFuel)
			{
				lastFuel = currentFuel;
				gaugeText.text = lastFuel.ToString();
			}
		}



	}
}