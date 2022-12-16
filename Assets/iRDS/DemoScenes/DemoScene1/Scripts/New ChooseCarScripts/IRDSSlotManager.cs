using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class IRDSSlotManager : MonoBehaviour {

    public Transform slotsParent;
    public GameObject slotPrefab;
    [HideInInspector]
    public List<IRDSSlotDetail> slots = new List<IRDSSlotDetail>();
	
    void Start()
    {
        if (slotsParent != null)
        {
            GridLayoutGroup grid = slotsParent.GetComponent<GridLayoutGroup>();
            if (grid == null)
            {
                slotsParent.gameObject.AddComponent<GridLayoutGroup>();
            }
        }
    }

    public void InitSlots(int amount)
    {
        //Early exit if there is no parent to put the slots on, or if there is no prefab for the slots
        if (slotsParent == null || slotPrefab == null) return;
        if (slots.Count < amount)
        {
            int slotsCount = slots.Count;
            for (int i = slotsCount; i < amount;i++)
            {
                GameObject newSlot = Instantiate(slotPrefab) as GameObject;
                newSlot.SetActive(true);
                newSlot.transform.SetParent(slotsParent);
                newSlot.transform.localScale = Vector3.one;
                newSlot.transform.localPosition = Vector3.zero;
                newSlot.transform.localRotation = Quaternion.identity;
                IRDSSlotDetail slot = newSlot.GetComponent<IRDSSlotDetail>();
                slot.InitComponents();
                slots.Add(slot);
            }
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < amount; i++)
            {
                slots[i].gameObject.SetActive(true);
            }
            for (int i = amount; i < slots.Count; i++)
            {
                slots[i].gameObject.SetActive(false);
            }
        }
        
    }

    public virtual void PopulateSlots()
    {

    }
}
