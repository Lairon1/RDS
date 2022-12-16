using UnityEngine;
using System.Collections;

public class AutoStartEngine : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<IRDSDrivetrain>().startEngine = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
