using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBattler : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float xpWorth; //how much xp it is worth
    public float defense = 0;
    public float coinDropRate; //how many coins it drops
    public string[] attacks; //the list of attacks this enemy can use
    [Header("Transforms")]
    public Transform inFront;
    public Transform head;

    public GameObject damageStar; //the icon that shows damage dealt
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack1()
    {
            //
    }

    public void RecieveDamage(float damage)
    {
        damage -= defense;
        if(damage < 0) damage = 0;
        health -= damage;
        //show the damage star

        RectTransform textTransform = Instantiate(damageStar).GetComponent<RectTransform>();
        textTransform.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        Canvas canvas = GameObject.Find("2D UI Canvas").GetComponent<Canvas>();
        textTransform.SetParent(canvas.transform);
    }


}
