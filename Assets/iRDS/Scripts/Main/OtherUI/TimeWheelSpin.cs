using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TimeWheelSpin : MonoBehaviour {

    public float minWheelSpinPercentage;

    float ellapsedTime;

    public float EllapsedTime
    {
        get
        {
            return ellapsedTime;
        }
    }

    Text myText;
    bool hasText = false;

    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        if (myText != null)
            hasText = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IRDSStatistics.GetCurrentCar() != null) {
            IRDSWheel[] wheels = IRDSStatistics.GetCurrentCar().GetCarInputs().GetIRDSWheels();
            float maxSlip = float.MinValue;
            for (int i = 0; i < wheels.Length;i++)
            {
                float tireSlip = wheels[i].UnitAngle + wheels[i].UnitSlip;
                if (tireSlip> maxSlip)
                {
                        maxSlip = tireSlip;
                }
            }
            if (maxSlip < minWheelSpinPercentage)
                {
                    ellapsedTime += Time.deltaTime;
                }
                else
                    ellapsedTime = 0;
        }
        if (hasText )
        {
            myText.text = ellapsedTime.ToString();
        }

    }
}