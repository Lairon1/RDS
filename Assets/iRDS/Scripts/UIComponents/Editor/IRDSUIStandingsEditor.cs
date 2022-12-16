using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;


namespace IRDS.UI{
	[CustomEditor(typeof(IRDSUIStandings))]
	public class IRDSUIStandingsEditor : Editor {

		IRDSUIStandings standings;
		GameObject[] detailsTexts;
		List<string> details = new List<string>();

		void OnEnable()
		{
			details = new List<string>();
			standings = (IRDSUIStandings)target;
			if (!Application.isPlaying)
				CreateDefaultUI();
		} 

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
			EditorGUI.BeginChangeCheck();
			if (standings.isEndRaceStanding)
			{
				standings.isChampionshipStandings = EditorGUILayout.Toggle("Is Championship Standings",standings.isChampionshipStandings);
				standings.ShowTeamResultsOnly = EditorGUILayout.Toggle("Show Team Results Only",standings.ShowTeamResultsOnly);
				standings.showOnlyIfRaceEnded = EditorGUILayout.Toggle("Show Only If Race Ended",standings.showOnlyIfRaceEnded);
				standings.UIAnimator = EditorGUILayout.ObjectField("UI Animator",standings.UIAnimator,typeof(Animator),true) as Animator;
				standings.showAnimation = EditorGUILayout.TextField("Show Animation",standings.showAnimation);
				standings.hideAnimation = EditorGUILayout.TextField("Hide Animation",standings.hideAnimation);
				standings.showTimesOnlyEndedCars = EditorGUILayout.Toggle("Show Times Only Ended Cars",standings.showTimesOnlyEndedCars);
				standings.stillRunningText = EditorGUILayout.TextField("Still Running Text",standings.stillRunningText);
			}
			else
			{
				standings.hideOnRaceEnd = EditorGUILayout.Toggle("Hide On Race End",standings.hideOnRaceEnd);
				if (standings.hideOnRaceEnd){ 
					standings.UIAnimator = EditorGUILayout.ObjectField("UI Animator",standings.UIAnimator,typeof(Animator),true) as Animator;
					standings.showAnimation = EditorGUILayout.TextField("Show Animation",standings.showAnimation);
					standings.hideAnimation = EditorGUILayout.TextField("Hide Animation",standings.hideAnimation);
				}
			}
			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(standings);
			}
		}

		void CreateDefaultUI()
		{
			for(int i = 0; i < standings.transform.childCount;i++)
			{
				if (i!=0)
					DestroyImmediate(standings.transform.GetChild(i).gameObject);
				else
				{
					CheckStandingsChildren(i);
				}
				
			}
			//If nothing build, we build the basic
			if (standings.transform.childCount ==0)
			{
				GameObject newChild = new GameObject("Item");
				newChild.transform.SetParent(standings.transform);
				newChild.transform.localPosition = Vector3.zero;
				newChild.transform.localScale = Vector3.one;
				newChild.transform.localRotation = Quaternion.identity;
				newChild.AddComponent<Image>();
				newChild.AddComponent<LayoutElement>();
				newChild.GetComponent<LayoutElement>().minWidth = 250;
				newChild.AddComponent<HorizontalLayoutGroup>();
				CheckStandingsChildren(0);
			}
		}

		void CheckStandingsChildren(int i)
		{
			//First we check the amount of text labels we currently have
			for (int y = 0; y <standings.transform.GetChild(i).childCount;y++)
			{
				Text childText = standings.transform.GetChild(i).GetChild(y).GetComponent<Text>();
				if (childText!=null)
					CheckLabelName(childText.name);
			}
			//Then we add the text labels missing
			AddMissingLabels(standings.transform.GetChild(i));
		}

		void CheckLabelName(string name)
		{
			int totalItems = standings.StandingDetails.Length;//Since we have 8 standing details item define on the StandingDetails enum
			for (int i = 0; i < totalItems;i++)
			{
				if (name.ToLower().Contains(standings.StandingDetails[i].ToLower()))
				{
					if (!details.Contains(standings.StandingDetails[i]))
					{
						details.Add(standings.StandingDetails[i]);
					}
					return;
				}
			}
		}

		void AddMissingLabels(Transform parent)
		{

			int totalItems = standings.StandingDetails.Length;//Since we have 8 standing details item define on the StandingDetails enum

			for (int i = 0; i < totalItems;i++)
			{
				bool found = false;
				for (int y = 0; y < details.Count;y++)
				{
					if (details[y]== standings.StandingDetails[i])
						found = true;
						
				}
				if (!found)
				{
					GameObject newText = new GameObject(standings.StandingDetails[i]);
					newText.transform.SetParent(parent);
					newText.transform.localPosition = Vector3.zero;
					newText.transform.localRotation = Quaternion.identity;
					newText.transform.localScale = Vector3.one;
					newText.AddComponent<Text>();
					newText.AddComponent<LayoutElement>();
					Text textComp = newText.GetComponent<Text>();
					if (standings.StandingDetails[i].Contains("Time"))
					{
						textComp.text = "00:00:00";
					}else{
						if (standings.StandingDetails[i].Contains("Position")||standings.StandingDetails[i].Contains("Laps")||standings.StandingDetails[i].Contains("Points"))
						{
							textComp.text = "1";
						}else
						{
							textComp.text = standings.StandingDetails[i];
						}
					}
				}
			}
		}

	}
}