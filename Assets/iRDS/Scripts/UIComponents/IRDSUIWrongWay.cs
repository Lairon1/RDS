using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IRDSUIWrongWay : MonoBehaviour {

	public float fadeTime = 0f;

	Image wrongWayImage;

	Text wrongWayText;

	bool wrongWay;
	// Use this for initialization
	void Start () {

		wrongWayImage = GetComponent<Image>();
		wrongWayText = GetComponent<Text>();

		if (IRDSStatistics.Instance != null)
		{
			IRDSStatistics.Instance.onCurrentCarWrongWay +=ActivateWrongWay;
		}
		FadeOut(0);
	}



	void FadeOut(float value)
	{
		if (wrongWayImage != null)
		{
			wrongWayImage.CrossFadeAlpha(0,value,false);
		}
		if (wrongWayText != null)
		{
			wrongWayText.CrossFadeAlpha(0,value,false);
		}
	}

	void FadeIn(float value)
	{
		if (wrongWayImage != null)
		{
			wrongWayImage.CrossFadeAlpha(1,value,false);
		}
		if (wrongWayText != null)
		{
			wrongWayText.CrossFadeAlpha(1,value,false);
		}
	}

	void ActivateWrongWay(bool wrongWay)
	{
		if (wrongWay)
		{
			FadeIn(fadeTime);
		}else{
			FadeOut(fadeTime);
		}
	}

}
