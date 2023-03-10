using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{

	public class IRDSUILastLapTime : MonoBehaviour {
		public bool playAnimation = false;
		public string lapTimeChangeAnimation;
		public Animator UIAnimator;

		Text playerLapTime;

		// Use this for initialization
		void Start () {
			playerLapTime = GetComponent<Text>();
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
			string oldLapTime = playerLapTime.text;
			playerLapTime.text = IRDSStatistics.CurrentCar.GetEllapsedTimeString();
			if (playAnimation && UIAnimator != null){
				if (oldLapTime != playerLapTime.text)
					UIAnimator.Play(lapTimeChangeAnimation);
			}
		}



	}
}