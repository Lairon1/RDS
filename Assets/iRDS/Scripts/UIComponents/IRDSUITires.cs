using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace IRDS.UI{
	
	public class IRDSUITires : MonoBehaviour {

		class TireImage{
			public IRDSWheel tire;
			public Image image;
			public Color color;
		}

		/// <summary>
		/// The target car.  Only need to assign this if not using the iRDSManager, (i.e. you are using the physics only)
		/// </summary>
		public IRDSCarControllInput targetCar;
		
		/// <summary>
		/// The tire images.  The first image is for the front left tire, the second is for the front right tire, the third is for
		/// rear left tire and the last image is for the rear right tire.
		/// </summary>
		public Image[] tireImage;

		private TireImage[] tires;
		
		// Use this for initialization
		void Start () {
			if (tireImage.Length != 4){
				this.enabled = false;
				return;
			}
			GetFirstCar();
			ResetTireColors();
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
		
		

		void Update(){
			UpdateTireColors();
		}

		void ResetTireColors()
		{
			tires = new TireImage[4];
			for (int i = 0; i < tires.Length;i++){
				for (int y =0; y < targetCar.wheels.Length;y++)
				{
					tires[i] = new TireImage();
					switch(i)
					{
					case 0:
						tires[i].tire =GetTireByPosition("FL");
						break;
					case 1:
						tires[i].tire =GetTireByPosition("FR");
						break;
					case 2:
						tires[i].tire =GetTireByPosition("RL");
						break;
					case 3:
						tires[i].tire =GetTireByPosition("RR");
						break;
					}
				}
				tires[i].image = tireImage[i];
				tires[i].color = tires[i].image.color;
				tires[i].color.b = 1;
				tires[i].color.r = 0;
				tires[i].color.g = 0;
				if (tires[i].tire == null)
				{
					this.enabled = false;
					return;
				}
			}
		}

		IRDSWheel GetTireByPosition(string position)
		{
			for (int i =0; i < targetCar.wheels.Length;i++)
			{
				if (position == targetCar.wheels[i].wheelPosition)
					return targetCar.wheels[i];
			}
			return null;
		}

		void UpdateTireColors()
		{
			for (int i =0; i < tires.Length;i++)
			{
				if (tires[i].tire.tyreTemp <1)
				{
					tires[i].color.b= 1-tires[i].tire.tyreTemp;
					tires[i].color.g= tires[i].tire.tyreTemp;
					tires[i].color.r= 0;
				}
				else 
				{
					tires[i].color.r = 1-(tires[i].tire.b[2]/tires[i].tire.originalB2);
					tires[i].color.g= tires[i].tire.b[2]/tires[i].tire.originalB2;
					tires[i].color.b=0;
				}
				tireImage[i].color = tires[i].color;
			}
		}





		
	}
}