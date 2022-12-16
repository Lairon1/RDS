using UnityEngine;
using System.Collections;

public class IRDSWheelVisualForces : MonoBehaviour {

	public LineRenderer fx ;
	public LineRenderer fy ;
	public LineRenderer normalForce;
	public Material material;
	private GameObject fxObject;
	private GameObject fyObject;
	private GameObject normalForceObject;

	private IRDSWheel wheel;
	//private float Fx;
	//private float Fy;
	//private float NormalForce;
	// Use this for initialization
	void Start () {
		wheel = GetComponent<IRDSWheel>();
		fxObject = new GameObject();
		fyObject = new GameObject();
		normalForceObject = new GameObject();
		fxObject.transform.parent = this.transform;
		fyObject.transform.parent = this.transform;
		normalForceObject.transform.parent = this.transform;
		fxObject.transform.localPosition = Vector3.zero;
		fyObject.transform.localPosition = Vector3.zero;
		normalForceObject.transform.localPosition = Vector3.zero;
		fxObject.transform.localRotation = Quaternion.identity;
		fyObject.transform.localRotation = Quaternion.identity;
		normalForceObject.transform.localRotation = Quaternion.identity;

		fx = fxObject.AddComponent<LineRenderer>();
		fy = fyObject.AddComponent<LineRenderer>();
		normalForce = normalForceObject.AddComponent<LineRenderer>();
		fx.positionCount = 2;
		fy.positionCount = 2;
		normalForce.positionCount = 2;
		fx.useWorldSpace = false;
		fy.useWorldSpace = false;
		normalForce.useWorldSpace = false;
		fx.startWidth = 0.05f;
		fx.endWidth = 0.1f;
		fy.startWidth = 0.05f;
		fy.endWidth = 0.1f;
		normalForce.startWidth = 0.1f;
		normalForce.endWidth = 0.15f;
		material.renderQueue = 4000;
		fx.material = material;
		fy.material = material;
		normalForce.material = material;

		fx.SetPosition(0, new Vector3(0,-wheel.GetRadius(),0));
		fy.SetPosition(0, new Vector3(0,-wheel.GetRadius(),0));
		normalForce.SetPosition(0, new Vector3(0,wheel.GetRadius(),0));
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Fx += (wheel.Fx - Fx) * Time.deltaTime * 5f;
		//Fy += (wheel.Fy - Fy) * Time.deltaTime * 5f;
		//NormalForce +=  (wheel.GetNormalForce() - NormalForce) * Time.deltaTime * 5f;
		fx.SetPosition(1, new Vector3(0,-wheel.GetRadius(),wheel.Fx/1000));
		fy.SetPosition(1, new Vector3(wheel.Fy/1000,-wheel.GetRadius(),0));
		normalForce.SetPosition(1,wheel.GroundNormal *(wheel.GetNormalForce()/1000));

	}
}
