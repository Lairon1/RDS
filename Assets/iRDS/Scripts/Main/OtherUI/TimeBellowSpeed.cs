using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TimeBellowSpeed : MonoBehaviour {

    public float minSpd;

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
        if (IRDSStatistics.GetCurrentCar() != null)
        {
            if (IRDSStatistics.GetCurrentCar().GetCarInputs().CarSpeed < minSpd)
            {
                ellapsedTime += Time.deltaTime;
            }
            else
                ellapsedTime = 0;
        }
        if (hasText)
        {
            myText.text = ellapsedTime.ToString();
        }

    }
}
