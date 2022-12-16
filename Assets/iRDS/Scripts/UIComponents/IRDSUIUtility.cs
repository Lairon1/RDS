using UnityEngine;
using System.Collections;

namespace IRDS.UI{
	public class IRDSUIUtility {

		public enum Axis{
			yAxis,
			xAxis,
			zAxis
		}

		void UpdateRotation(Transform objectoToRotate, float mainCurrent, float mainTotal, float stopangle, float topAngle, Axis axis)
		{
			if (objectoToRotate == null)return;
			float speedFraction1 =  mainCurrent/mainTotal;
			float rotatedAngle = Mathf.Lerp(stopangle, topAngle, speedFraction1);
			switch(axis)
			{
			case Axis.xAxis:
				objectoToRotate.transform.localEulerAngles = new Vector3(rotatedAngle,objectoToRotate.transform.localEulerAngles.y,objectoToRotate.transform.localEulerAngles.z);
				break;
			case Axis.yAxis:
				objectoToRotate.transform.localEulerAngles = new Vector3(objectoToRotate.transform.localEulerAngles.x,rotatedAngle,objectoToRotate.transform.localEulerAngles.z);
				break;
			case Axis.zAxis:
				objectoToRotate.transform.localEulerAngles = new Vector3(objectoToRotate.transform.localEulerAngles.x,objectoToRotate.transform.localEulerAngles.y,rotatedAngle);
				break;
			}
			
		}


		public static void UpdateRotation(Transform objectoToRotate, float mainCurrent, float mainTotal, float stopangle, float topAngle)
		{
			if (objectoToRotate == null)return;
			float speedFraction1 =  mainCurrent/mainTotal;
			float rotatedAngle = Mathf.Lerp(stopangle, topAngle, speedFraction1);
			objectoToRotate.transform.localEulerAngles = new Vector3(objectoToRotate.transform.localEulerAngles.x,objectoToRotate.transform.localEulerAngles.y,rotatedAngle);
		}
	}
}