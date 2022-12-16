using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IRDS.ChampionshipSystem;


public class TrackSlotManager : IRDSSlotManager {

    public bool disableMeAtStart = false;

    [System.Serializable]
    public class TrackDetail
    {
        public Sprite logo;
        public string name;
        public string sceneName;
    }

    public TrackDetail[] tracks;
   

	// Use this for initialization
	void Start () {
        
        InitSlots(tracks.Length);
        PopulateSlots();
        if (disableMeAtStart || ChampionShipSystem.Instance != null) gameObject.SetActive(false);
	}

    public override void PopulateSlots()
    {
        for (int i =0; i < tracks.Length;i++)
        {
            int local_i = i;
            IRDSSlotDetail.IRDSSlotComponentDetail nameComponent = slots[i].GetSlotComponent("trackname");
            if (nameComponent != null)
            {
                nameComponent.SetText(tracks[i].name);
            }

            IRDSSlotDetail.IRDSSlotComponentDetail imageComponent = slots[i].GetSlotComponent("logo");
            if (imageComponent != null)
            {
                imageComponent.SetImage(tracks[i].logo);
            }

            Button myButton = slots[i].GetComponent<Button>();
            if (myButton !=null)
            {
                myButton.onClick.RemoveAllListeners();
                myButton.onClick.AddListener(()=>{
                    SetTrackToRace(tracks[local_i].sceneName);
                });
            }
        }
    }

    void SetTrackToRace(string sceneName)
    {
        IRDSLevelLoadVariables.Instance.trackToRace =sceneName;
    }
}
