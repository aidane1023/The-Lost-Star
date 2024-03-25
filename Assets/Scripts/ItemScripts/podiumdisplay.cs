using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class podiumdisplay : MonoBehaviour
{

    public Database itemDatabase;
    public SpriteRenderer displaySprite;
    public float itemScale = 4;
   
    public void Start()
    {
        randomDisplay();
    }

    public void Update()
    {
       
    }

    void randomDisplay()
    {
        if (itemDatabase == null || itemDatabase.Items.Count == 0) return;

        int randomID = Random.Range(0, itemDatabase.Items.Count);
        InventoryItemData randomItem = itemDatabase.GetItem(randomID);
        displaySprite.sprite = randomItem.Icon;
        displaySprite.size = new Vector2 (itemScale, itemScale);
    }
}
