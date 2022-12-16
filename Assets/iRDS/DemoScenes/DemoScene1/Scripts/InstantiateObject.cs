using UnityEngine;
using System.Collections;


public class InstantiateObject : MonoBehaviour {
	
	public static GameObject instantiateCar(GameObject tempObj , Transform parent, Object tempObject, Quaternion rotation, Vector3 position, Vector3 offSet, RigidbodyConstraints constraint, float intertiaTensorMultiplier)
	{
		tempObj = Instantiate(tempObject,position, rotation ) as GameObject;
		tempObj.GetComponent<IRDSCarControllerAI>().enabled = false;
		tempObj.transform.parent = parent;
		tempObj.transform.localPosition = offSet;
		tempObj.GetComponent<Rigidbody>().constraints = constraint;
		tempObj.GetComponent<Rigidbody>().inertiaTensor *= intertiaTensorMultiplier;
		return tempObj;
	}
	
	public static GameObject instantiateCar(GameObject tempObj , Object tempObject, Quaternion rotation, Vector3 position, RigidbodyConstraints constraint, float intertiaTensorMultiplier)
	{
		tempObj = Instantiate(tempObject,position, rotation ) as GameObject;
		tempObj.GetComponent<IRDSCarControllerAI>().enabled = false;
		tempObj.GetComponent<Rigidbody>().constraints = constraint;
		tempObj.GetComponent<Rigidbody>().inertiaTensor *= intertiaTensorMultiplier;
		return tempObj;
	}
	
	
}
