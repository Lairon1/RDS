using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using IRDS.ChampionshipSystem;

public class TeamSlotManager : IRDSSlotManager {

    public bool disableMeAtStart = false;
    GarageCarSelectionManager carManager;
    // Use this for initialization
    void Start()
    {
        carManager = FindObjectOfType<GarageCarSelectionManager>();
        if (ChampionShipSystem.Instance != null)
        {
            InitSlots(ChampionShipSystem.Instance.championshipTeams.Count);
            PopulateSlots();
        }else
        {
            gameObject.SetActive(false);
        }
        if (disableMeAtStart) gameObject.SetActive(false);
    }

    public override void PopulateSlots()
    {
        for (int i = 0; i < ChampionShipSystem.Instance.championshipTeams.Count; i++)
        {
            int local_i = i;
            IRDSSlotDetail.IRDSSlotComponentDetail nameComponent = slots[i].GetSlotComponent("teamname");
            if (nameComponent != null)
            {
                nameComponent.SetText(ChampionShipSystem.Instance.championshipTeams[i].teamName);
            }

            IRDSSlotDetail.IRDSSlotComponentDetail imageComponent = slots[i].GetSlotComponent("logo");
            if (imageComponent != null)
            {
                imageComponent.SetImage(ChampionShipSystem.Instance.championshipTeams[i].icon);
            }

            Button myButton = slots[i].GetComponent<Button>();
            if (myButton != null)
            {
                myButton.onClick.RemoveAllListeners();
                myButton.onClick.AddListener(() => {
                    SetTeam(local_i);
                });
            }
        }
    }

    void SetTeam(int team)
    {
        MenuManager.Instance.SetPlayerTeam(team);
        if (carManager != null)
        {
            carManager.UpdateCarListForSelectedTeam();
            carManager.SetSelectedCar();
        }
            
    }
}
