using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class LongitudinalGForce : MonoBehaviour {

    float totalGForce;

    public float TotalGForce
    {
        get
        {
            return totalGForce;
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

    float lastVelocity;

    // Update is called once per frame
    void Update()
    {
        if (IRDSStatistics.GetCurrentCar() != null)
        {
            totalGForce = Mathf.Lerp(0f, (IRDSStatistics.GetCurrentCar().GetCarInputs().bodyVelocity.z - lastVelocity), Time.fixedDeltaTime * 10);
            totalGForce /= -Physics.gravity.y;
            lastVelocity = IRDSStatistics.GetCurrentCar().GetCarInputs().bodyVelocity.z;
        }
        if (hasText)
        {
            myText.text = totalGForce.ToString();
        }

    }



}
