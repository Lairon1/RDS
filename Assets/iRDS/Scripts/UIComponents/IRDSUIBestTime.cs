using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{
	public class IRDSUIBestTime : MonoBehaviour {

		public bool playAnimation = false;
		public string bestTimeChangeAnimation;
		public Animator UIAnimator;

		Text playerBestLapTime;

		// Use this for initialization
		void Start () {
			playerBestLapTime = GetComponent<Text>();
			if (UIAnimator == null){
				UIAnimator = GetComponent<Animator>();
				if (UIAnimator == null)
					UIAnimator = transform.root.GetComponent<Animator>();
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
				IRDSStatistics.Instance.onCurrentCarLapCompleted += OnLapChange;
		}
		void RemoveDelegatesReferences()
		{
			if (IRDSStatistics.Instance ==null)return;
			IRDSStatistics.Instance.onCurrentCarLapCompleted -= OnLapChange;
		}

		
		/// <summary>
		/// Raises the lap change event.
		/// </summary>
		/// <param name="lap">Lap.</param>
		void OnLapChange(int lap)
		{
			string oldBest = playerBestLapTime.text;
			playerBestLapTime.text = IRDSStatistics.CurrentCar.GetFastestLapTimeString();
			if (playAnimation && UIAnimator != null){
				if (oldBest != playerBestLapTime.text)
					UIAnimator.Play(bestTimeChangeAnimation);
			}
		}


	}
}