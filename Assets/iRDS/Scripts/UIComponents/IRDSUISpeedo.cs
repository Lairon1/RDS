using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{

	public class IRDSUISpeedo : MonoBehaviour {

		public enum UnitsType{
			KPH,
			MPH
		}
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
		/// The type of the unit.
		/// </summary>
		public UnitsType unitType;

		/// <summary>
		/// The max speed of the UI.  This is useful to determine the top speed of the gauge.
		/// </summary>
		public float maxSpeed = 220f;

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
		public IRDSCarControllInput targetCar;

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
				targetCar = FindObjectOfType<IRDSCarControllInput>();
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
			targetCar = IRDSStatistics.CurrentCar.GetComponent<IRDSCarControllInput>();
		}
		
		
		float lastSpeed = 0;
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
			switch(unitType)
			{
			case UnitsType.KPH:
				gaugeImage.fillAmount = targetCar.CarSpeed * 3.6f/maxSpeed;
				break;
			case UnitsType.MPH:
				gaugeImage.fillAmount = targetCar.CarSpeed * 2.2369f/maxSpeed;
				break;
			}
		}
		
		void GaugeUpdate()
		{
			switch(unitType)
			{
			case UnitsType.KPH:
				IRDSUIUtility.UpdateRotation(gaugeTransform,targetCar.CarSpeed * 3.6f,maxSpeed,angleMin,angleMax);
				break;
			case UnitsType.MPH:
				IRDSUIUtility.UpdateRotation(gaugeTransform,targetCar.CarSpeed * 2.2369f,maxSpeed,angleMin,angleMax);
				break;
			}
		}

		int currentSpeed =0;
		void TextUpdate()
		{
			switch(unitType)
			{
			case UnitsType.KPH:
				currentSpeed = Mathf.RoundToInt(targetCar.CarSpeed * 3.6f);
				break;
			case UnitsType.MPH:
				currentSpeed = Mathf.RoundToInt(targetCar.CarSpeed * 2.2369f);
				break;
			}

			if (lastSpeed != currentSpeed)
			{
				lastSpeed = currentSpeed;
				gaugeText.text = currentSpeed.ToString();
			}
		}



	}
}