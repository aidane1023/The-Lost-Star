using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DamageStarText : MonoBehaviour
{
    public float timeToLive = 0.5f;
    public float floatSpeed = 250;
    public Vector3 floatDirection = new Vector3(0,1,0);
    public TextMeshProUGUI text;
    float timeElapsed = 0f;

    public RectTransform rTransform;
    public Image star;
    Color startingColor, imageColor;


    // Start is called before the first frame update
    void Start()
    {
        startingColor = text.color;
        imageColor = star.color;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        rTransform.position += floatDirection * floatSpeed * Time.deltaTime;

        text.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed / timeToLive));
        star.color = new Color(imageColor.r, imageColor.g, imageColor.b, 1 - (timeElapsed / timeToLive));

        if(timeElapsed > timeToLive)
        {
            Destroy(gameObject);
        }
    }
}
