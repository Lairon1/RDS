using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class IRDSSaveLoadWindow : EditorWindow {
		
	public static void Init () {
		IRDSSaveLoadWindow window = EditorWindow.GetWindow<IRDSSaveLoadWindow>(true,"Load Options");
		window.minSize = window.maxSize = new Vector2(500,500);
		window.Show ();
	}
		
	private Rect textRect = new Rect(25,25,450,470);
	private Vector2 changeLogScrolPos;

	void OnGUI () {
		GUILayout.BeginArea(textRect);

		GUILayout.Label("Choose the setting to import");
		GUILayout.BeginVertical(GUI.skin.box);
		GUILayout.Label("Note:  If you are importing the car settings " +
			"after an update from Version 2.1.0.5, " +
		                "you must import the object references too!", EditorStyles.wordWrappedLabel);
		GUILayout.EndVertical();

		GUILayout.Space(25);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("All",EditorStyles.miniButton,GUILayout.Width(50)))
		{
			for (int i = 0 ; i < IRDSSaveLoadEditor.options.Length;i++)
				if (i >0)
					IRDSSaveLoadEditor.options[i].flag = true;
		}
		if (GUILayout.Button("None",EditorStyles.miniButton,GUILayout.Width(50)))
		{
			for (int i = 0 ; i < IRDSSaveLoadEditor.options.Length;i++)
				if (i >0)
					IRDSSaveLoadEditor.options[i].flag = false;
		}
		GUILayout.Space(25);
		if (GUILayout.Button("Load",EditorStyles.miniButton,GUILayout.Width(50)))
		{
			IRDSSaveLoadEditor.LoadCarSettings();
		}


		GUILayout.EndHorizontal();

		GUILayout.Space(15);
		for (int i =0; i < IRDSSaveLoadEditor.options.Length;i++)
		{
			IRDSSaveLoadEditor.options[i].flag = GUILayout.Toggle(IRDSSaveLoadEditor.options[i].flag,IRDSSaveLoadEditor.options[i].name);
			GUILayout.Space(5);
		}

		GUILayout.EndArea();
		
	}
}
