using UnityEngine;
using System.Collections;


public class StartRaceAutomatically : MonoBehaviour {
	
	public IRDSCarCamera carCamera;
	public float minDistanceToStartRace = 10f;
	private Transform cameraTransform;
	private Transform myTransform;
	// Use this for initialization
	void Start () {
		if (carCamera == null)carCamera = FindObjectOfType(typeof(IRDSCarCamera)) as IRDSCarCamera;
		cameraTransform = carCamera.transform;
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if ((cameraTransform.position - myTransform.position).magnitude < 10f)
		{
			IRDSCarCamera.InitialView = false;
			this.enabled = false;
		}
	}
}
