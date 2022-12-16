using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using IRDS.ChampionshipSystem;

public class ChampionShipSlotManager : IRDSSlotManager {

    public bool disableMeAtStart = false;

    // Use this for initialization
    void Start()
    {
        if (ChampionShipSystem.Instance != null)
        {
            
            InitSlots(ChampionShipSystem.Instance.championShipData.Count);
            PopulateSlots();
        }else
        {
            gameObject.SetActive(false);
        }
        if (disableMeAtStart) gameObject.SetActive(false);
    }

    public override void PopulateSlots()
    {
        for (int i = 0; i < ChampionShipSystem.Instance.championShipData.Count; i++)
        {
            int local_i = i;
            IRDSSlotDetail.IRDSSlotComponentDetail nameComponent = slots[i].GetSlotComponent("champname");
            if (nameComponent != null)
            {
                nameComponent.SetText(ChampionShipSystem.Instance.championShipData[i].ChampionShipName);
            }

            IRDSSlotDetail.IRDSSlotComponentDetail imageComponent = slots[i].GetSlotComponent("logo");
            if (imageComponent != null)
            {
                imageComponent.SetImage(ChampionShipSystem.Instance.championShipData[i].icon);
            }

            Button myButton = slots[i].GetComponent<Button>();
            if (myButton != null)
            {
                myButton.onClick.RemoveAllListeners();
                myButton.onClick.AddListener(() => {
                    SetChampionship(local_i);
                });
            }
        }
    }

    void SetChampionship(int champ)
    {
        MenuManager.Instance.SetChampionShipToRace(champ);
    }
}
