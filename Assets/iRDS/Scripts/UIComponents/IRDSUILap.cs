using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{
	public class IRDSUILap : MonoBehaviour {

		public bool playAnimation = false;
		public string lapChangeAnimation;
		public bool useTotalLap = true;
		public string totalLapSeparator = "/";
		public Animator UIAnimator;

		Text playerLap;

		// Use this for initialization
		void Start () {
			playerLap = GetComponent<Text>();
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
			if (useTotalLap)
				playerLap.text = lap.ToString() + totalLapSeparator + IRDSStatistics.totalLaps;
			else
				playerLap.text = lap.ToString();

			//Try to play the UI animation if any
			if (playAnimation && UIAnimator != null){
				UIAnimator.Play(lapChangeAnimation);
			}
		}
	}
}