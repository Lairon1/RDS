using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class IRDSUIMiscTimes : MonoBehaviour {

    public enum DigitalOdometerType
    {
        KPH,
        MPH
    }

    public bool showKnockoutTime = false;
	public bool showCheckPointTime = false;
	public bool showSpeedForSpeedTrap = false;

	public DigitalOdometerType speedType = DigitalOdometerType.KPH;

	Text myText;

	private string timeCounter;

	// Use this for initialization
	void Start () {
		myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (IRDSStatistics.canRace){
			if (IRDSManager.Instance.raceModes == IRDSManager.RaceModes.LastManStanding && showKnockoutTime)
			{
				timeFormat(IRDSStatistics.CurrentCar.lastManStandingTimeLeft, out timeCounter);
				myText.text = timeCounter;
			}
			if (IRDSManager.Instance.raceModes == IRDSManager.RaceModes.CheckPoints &&showCheckPointTime)
			{
				timeFormat(IRDSStatistics.CurrentCar.checkPointTimeLeft, out timeCounter);
				myText.text = timeCounter;
			}
			if (IRDSManager.Instance.raceModes == IRDSManager.RaceModes.SpeedTrap &&showSpeedForSpeedTrap)
			{
				float SpeedTrapPoints = IRDSStatistics.CurrentCar.speedTrapPoints;
				switch(speedType)
				{
				case DigitalOdometerType.KPH:
					SpeedTrapPoints *=3.6f;
					break;
				case DigitalOdometerType.MPH:
					SpeedTrapPoints *=2.25f;
					break;
				}
				myText.text = string.Format("{0:0}",SpeedTrapPoints);
			}
		}
		else {
			if (!showSpeedForSpeedTrap && IRDSManager.Instance.raceModes != IRDSManager.RaceModes.SpeedTrap)
				myText.text = "00.00.00";
			else myText.text = string.Format("{0:0}","0");
		}


	}

	void timeFormat (float ellapsedTime, out string timeCounter) 
	{	
		float sec_num = ellapsedTime; // don't forget the second parm
		int hour   = Mathf.FloorToInt((sec_num / 3600));
		int min = Mathf.FloorToInt(((sec_num - (hour * 3600)) / 60));
		int sec = Mathf.FloorToInt(sec_num - (hour * 3600) - (min * 60));
		int fraction = Mathf.FloorToInt((sec_num - (hour * 3600) - (min * 60) - sec)*100);
		if (hour == 0) timeCounter = string.Format("{0:00}:{1:00}:{2:00}",min,sec,fraction);
		else timeCounter = string.Format("{0:00}:{1:00}:{2:00}:{3:00}",hour,min,sec,fraction);
	}

}
