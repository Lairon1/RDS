using UnityEngine;
using System.Collections;
using UnityEditor;


[CanEditMultipleObjects]
[UnityEditor.CustomEditor(typeof(IRDSSpeedLimits))]
public class IRDSSpeedLimitsEditor : Editor {
	
	
	public IRDSManager manager;
	public IRDSManagerEditor managerEditor;

	
	void OnEnable()
	{
		manager = ((IRDSSpeedLimits)target).transform.GetComponentInParent<IRDSManager>();
	}
	
	public override void OnInspectorGUI()
	{
		GUI.changed = false;
		
		SerializedProperty hillradius = serializedObject.FindProperty("hillRadius");
		
		EditorGUILayout.PropertyField(hillradius,new GUIContent("Hill Radius","This is the value of the radius of the Hill, for make the AI Drivers to slow down!"));
		
		if (GUILayout.Button("Reset",EditorStyles.miniButton,GUILayout.Width(50))){
			foreach(GameObject limit in Selection.gameObjects)
			{
				limit.GetComponent<IRDSSpeedLimits>().hillRadius = float.MaxValue;
			}
			EditorUtility.SetDirty((IRDSSpeedLimits)target);
			
		}
		
		if (GUI.changed){
			serializedObject.ApplyModifiedProperties();
			serializedObject.Update();
			manager.waypointsManager.dataNotProcessed = true;
			
			
			
		}
		if (manager.waypointsManager.dataNotProcessed){
			GUI.skin.textField.fontSize = 20;
			GUI.skin.textField.normal.textColor = Color.red;
			GUILayout.TextField("Process All Track Data!!");
			GUI.skin.textField.fontSize = 11;
			GUILayout.TextField("go to IRDSManager object and click on\n the Process All track Data button \n after finishing editing");
			
			GUI.skin.textField.normal.textColor = Color.black;
		}
	}
}
