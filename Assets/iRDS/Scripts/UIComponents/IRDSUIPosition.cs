using UnityEngine;
using UnityEngine.UI;

namespace IRDS.UI{

	public class IRDSUIPosition : MonoBehaviour {

		public bool playAnimation;
		public string posChangeAnimation;
		public bool useTotalDrivers = true;
		public string totalDriversSeparator = "/";
		public Animator UIAnimator;
		Text playerPosition;


		void Start () {
			playerPosition = GetComponent<Text>();

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
				IRDSStatistics.Instance.onCurrentCarPositionChange += OnPlayerPositionChange;
		}
		void RemoveDelegatesReferences()
		{
			if (IRDSStatistics.Instance ==null)return;
			IRDSStatistics.Instance.onCurrentCarPositionChange -= OnPlayerPositionChange;
		}

		/// <summary>
		/// Raises the player position change event.  This event is only called when the current player position changes
		/// </summary>
		/// <param name="position">Position.</param>
		void OnPlayerPositionChange(int position)
		{
			if (useTotalDrivers)
				playerPosition.text = position.ToString() + totalDriversSeparator + IRDSStatistics.Instance.GetAllDriversList().Count.ToString();
			else
				playerPosition.text = position.ToString();
			if (playAnimation && UIAnimator != null)
			{
				UIAnimator.Play(posChangeAnimation);
			}
		}
	}
}