using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{
	public class IRDSUIInitialCounter : MonoBehaviour {

		public enum InitialCounterMode{
			Normal,
			Reverse
		}
		
		public enum InitialCounterType{
			Text,
			Sprite_Array,
		}
		
		/// <summary>
		/// The initial counter mode.  it could be reverse or normal
		/// </summary>
		public InitialCounterMode initialCounterMode = InitialCounterMode.Normal;
		
		/// <summary>
		/// The type of the initial counter.
		/// </summary>
		public InitialCounterType initialCounterType = InitialCounterType.Text;
		

		
		/// <summary>
		/// The counter final text.  This would be the text shown if the counter ends coutning.
		/// </summary>
		public string counterFinalText ="Race!";

		/// <summary>
		/// The counter time.
		/// </summary>
		public int counterTime = 3;

		/// <summary>
		/// The initial counter sprites.  This would be the sprites that would be used with the
		/// Race initial counter, so you can place here the different sprites to switch from when the counter
		/// is counting.
		/// </summary>
		public Sprite[] initialCounterSprites;
		
		
		/// <summary>
		/// The initial counter image.  This one is to be used in combination with the initialCounterTextures, so the
		/// textures are swaped on each counting.
		/// </summary>
		public Image initialCounterImage;
		

		/// <summary>
		/// The initial counter text.  This would be the UI component that would be updated with the
		/// Race initial counter
		/// </summary>
		Text initialCounterText;

		void Awake()
		{
			IRDSStatistics.Instance.startCounterTime = counterTime;
		}

		void Start()
		{
			initialCounterText = GetComponent<Text>();
			InitializeUI();
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
			IRDSStatistics.Instance.onStartingInitialCount += OnRaceInitialCounting;
			IRDSStatistics.Instance.onInitialCounting += OnInitialCounting;
			IRDSStatistics.Instance.onRaceStart += OnRaceStart;
		}
		void RemoveDelegatesReferences()
		{
			if (IRDSStatistics.Instance ==null)return;
			IRDSStatistics.Instance.onStartingInitialCount -= OnRaceInitialCounting;
			IRDSStatistics.Instance.onInitialCounting -= OnInitialCounting;
			IRDSStatistics.Instance.onRaceStart -= OnRaceStart;
		}
		
		/// <summary>
		/// Initializes the UI.
		/// </summary>
		void InitializeUI()
		{
			switch(initialCounterType)
			{
			case InitialCounterType.Text:
				if (initialCounterText != null)
				initialCounterText.enabled = false;
				break;
			case InitialCounterType.Sprite_Array:
				if (initialCounterImage != null)
					initialCounterImage.enabled = false;
				break;
				
			}

		}

		void OnRaceInitialCounting(){
			InitializeCounterUI();
		}
		
		
		/// <summary>
		/// Raises the race start event.
		/// </summary>
		void OnRaceStart()
		{
			SetCountingFinalText();
		}
		
		/// <summary>
		/// Initializes the counter UI.
		/// </summary>
		void InitializeCounterUI()
		{
			switch(initialCounterType)
			{
			case InitialCounterType.Text:
				if (initialCounterText != null){
					initialCounterText.text = "";
					initialCounterText.enabled = true;
				}
				break;
			case InitialCounterType.Sprite_Array:
				if (initialCounterImage != null){
					if (initialCounterSprites.Length >0)
						initialCounterImage.sprite = initialCounterSprites[0];
					initialCounterImage.enabled = true;
				}
				break;

			}
		}
		
		/// <summary>
		/// Sets the counting final text.
		/// </summary>
		void SetCountingFinalText()
		{
			switch(initialCounterType){
			case InitialCounterType.Text:
				if (initialCounterText != null)
					initialCounterText.text = counterFinalText;
				break;
			case InitialCounterType.Sprite_Array:
				if (initialCounterImage != null && initialCounterSprites.Length >0)
					initialCounterImage.sprite = initialCounterSprites[initialCounterSprites.Length-1];
				break;
			}
		}
		
		/// <summary>
		/// Raises the initial counting event.
		/// </summary>
		/// <param name="count">Count.</param>
		void OnInitialCounting(int count)
		{
			switch(initialCounterType)
			{
			case InitialCounterType.Text:
				TextCounter(count);
				break;

			case InitialCounterType.Sprite_Array:
				SpriteCounter(count);
				break;
			}
		}

		void SpriteCounter(int count)
		{
			if (count !=-1){
				if (count < initialCounterSprites.Length)
					initialCounterImage.sprite = initialCounterSprites[count];
			}else{
				//Disable the counter UI since the count has eneded
				initialCounterImage.enabled = false;
			}
		}


		void TextCounter(int count)
		{
			if (count !=-1){
				
				switch(initialCounterMode)
				{
				case InitialCounterMode.Normal:
					initialCounterText.text = count.ToString();
					break;
				case InitialCounterMode.Reverse:
					initialCounterText.text = (IRDSStatistics.Instance.startCounterTime - (count - 1)).ToString();
					break;
				}
			}else{
				//Disable the counter UI since the count has eneded
				initialCounterText.enabled = false;
			}
		}

	}
}