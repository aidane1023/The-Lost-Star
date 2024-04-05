using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttacksHandler : MonoBehaviour
{
    public GameObject chargeUI;
    public Image chargeMeter;

    public float chargeAmount = 0; //max is 100
    public float chargeThreshholdMin = 90; //lowest amount of chargeAmount for attack to be succesful
    public float chargeThreshholdMax = 110; //highest amount of chargeAmount before attack fails
    public float chargeRate = 0;

    public bool isCharging = false;

    // Start is called before the first frame update
    void Start()
    {
        chargeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isCharging == true)
        {
            chargeAmount += chargeRate * Time.deltaTime;
            chargeMeter.fillAmount = chargeAmount/100;
        }
    }
}
