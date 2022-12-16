using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class CarDamagePercentage : MonoBehaviour {
    [System.Serializable]
    public class DamageDetail
    {
        public string name;
        public float damage;
        public float health;
        public Text textComponent;
    }

    public DamageDetail[] damageDetail;

    public float maxDamage = 1000f;

    public float vehicleSurvivalProbability = 0;
    public Text vehicleSurvivalProbabilityText;
    float totalDamage;

    public float TotalDamage
    {
        get
        {
            return totalDamage;
        }
    }

    Text myText;

    bool hasText = false;


    // Use this for initialization
    void Start()
    {
        myText = GetComponent<Text>();
        if (myText != null)
        {
            hasText = true;
            myText.text = "0";
        }
        vehicleSurvivalProbability = 100;
    }

    float lastTotalDamage = 0;
    // Update is called once per frame
    void Update()
    {
        if (IRDSStatistics.GetCurrentCar() != null)
        {
            totalDamage = ((IRDSStatistics.GetCurrentCar().GetCarInputs().CarDamage.totalDamage /maxDamage)*100f);
            if (totalDamage > 100) totalDamage = 100;
        }
        if (hasText && totalDamage != lastTotalDamage)
        {
            CalculatePartsDamage();
            lastTotalDamage = totalDamage;
            myText.text = totalDamage.ToString();
            
        }
    }

    void CalculatePartsDamage()
    {
        float currentDamage = totalDamage - lastTotalDamage;
        if (damageDetail == null || damageDetail.Length == 0) return;
        List<int> shuffledIndex = new List<int>();
        for (int i = 0; i < damageDetail.Length; i++)
        {
            shuffledIndex.Add(i);
        }
        shuffledIndex.Shuffle();
        for (int i = 0; i < damageDetail.Length;i++)
        {
            int index = shuffledIndex[i];
            if (currentDamage <= 0) currentDamage = 0;
            float damage = Random.Range(0, currentDamage);
            if (i == damageDetail.Length-1)
                damage = currentDamage;
            damageDetail[index].damage += damage;
            currentDamage -= damage;
            damageDetail[index].health = 100f - damageDetail[index].damage;
            if (damageDetail[index].health < 0)
                damageDetail[index].health = 0;
            if (damageDetail[index].textComponent != null)
                damageDetail[index].textComponent.text = damageDetail[index].health.ToString();
        }
        vehicleSurvivalProbability = totalDamage / damageDetail.Length;
        if (vehicleSurvivalProbabilityText != null)
            vehicleSurvivalProbabilityText.text = vehicleSurvivalProbability.ToString();
    }
}



static class MyExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (var i = 0; i < list.Count; i++)
            list.Swap(i, Random.Range(0, list.Count));
    }

    public static void Swap<T>(this IList<T> list, int i, int j)
    {
        var temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }
}