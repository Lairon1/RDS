using UnityEngine;
using System.Collections;
using UnityEditor;
  
[InitializeOnLoad] 
public class IRDSVersionUpdateChecker : Editor{
	
	public static IRDSUpdateData windowData;
	static IRDSVersionUpdateChecker() 
	{
		EditorApplication.update +=Init;
	} 
	
	static void Init()
	{
		EditorApplication.update -=Init;
		windowData =(IRDSUpdateData) AssetDatabase.LoadAssetAtPath("Assets/iRDS/Required/iRDSData.asset", typeof( IRDSUpdateData));
		if(!windowData){
			windowData = ScriptableObject.CreateInstance<IRDSUpdateData>();

			AssetDatabase.CreateAsset(windowData, "Assets/iRDS/Required/iRDSData.asset");
		}
		CheckUpdateData();
		IRDSUpdateWindow.Init(windowData);
	}
	static void CheckUpdateData(){
		windowData.iRDSLogo = CheckAssetReference<Texture2D>(windowData.iRDSLogo,"Assets/iRDS/Required/Pictures/13.png");
		windowData.changeLog = CheckAssetReference<TextAsset>(windowData.changeLog,"Assets/iRDS/VersionChangeLog.txt");
	}

	static T CheckAssetReference<T>(T assetRef, string filePath) where T : Object
	{
		string[] tempString = filePath.Split('/');
		string fileName = tempString[tempString.Length-1];
		if (assetRef == null)
		{
			assetRef = (T)AssetDatabase.LoadAssetAtPath(filePath, typeof(T));
			if (assetRef == null)
			{
				string[] allAssets = AssetDatabase.GetAllAssetPaths();
				foreach(string path in allAssets)
				{
					if (path.Contains(fileName))
					{
						assetRef = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
						return assetRef;
					}
				}
			}
		}
		return assetRef;
	}

}




public class IRDSUpdateWindow : EditorWindow {

	static IRDSUpdateData windowData;

	public static void Init (IRDSUpdateData windowData1) {
		// Get existing open window or if none, make a new one:

		windowData = windowData1;
		if (!windowData.dontShowOnLoad){
			IRDSUpdateWindow window = EditorWindow.GetWindow<IRDSUpdateWindow>(true,"iRDS - Intelligent Race Driver System");
			window.minSize = new Vector2(500,500);

			window.Show ();
		} 

	}

	private Rect logoRect = new Rect(125,10,250,225);
	private Rect textRect = new Rect(25,260,450,270);
	private Vector2 changeLogScrolPos;
	 
	void OnEnable()
	{
		 
	} 
	 
	void OnGUI () {
		GUI.DrawTexture(logoRect,windowData.iRDSLogo);
		GUILayout.BeginArea(textRect);
		GUILayout.BeginHorizontal();
		GUILayout.Space(200); 
		GUILayout.Label (IRDSUpdateData.Version, "BoldLabel");
		GUILayout.EndHorizontal();

		//Version log display
		GUILayout.BeginVertical(GUI.skin.box);
		changeLogScrolPos = GUILayout.BeginScrollView( changeLogScrolPos, false, false, GUILayout.Width( 442 ), GUILayout.Height( 70 ) );
		if (windowData.changeLog != null)
			GUILayout.TextArea( windowData.changeLog.text, EditorStyles.wordWrappedLabel );
		GUILayout.EndScrollView();
		GUILayout.EndVertical();
		GUIStyle newStyle = new GUIStyle(GUI.skin.label);

		newStyle.fontSize = 14; 
		newStyle.wordWrap = true;
		newStyle.fontStyle = FontStyle.Bold;
		GUILayout.Label ("Warning!: Always do a project backup before upgrading!  For upgrading to upcoming version 3.0, you must save your car settings and IRDSManagers " +
			"settings before upgrading, if not you would loose your settings!  " +
			"Please read more about it on the following link!", newStyle);

        if (GUILayout.Button("Link..."))
        {
            System.Diagnostics.Process.Start("http://s13.zetaboards.com/Daga_Games_Forum/single/?p=8047515&t=898475");
        }

        //		GUILayout.Space(5);
        windowData.dontShowOnLoad = GUILayout.Toggle(windowData.dontShowOnLoad,"Dont show this on start up?", GUILayout.Width(300));
		if (GUI.changed)
			EditorUtility.SetDirty(windowData);
		GUILayout.EndArea();

	}
}
