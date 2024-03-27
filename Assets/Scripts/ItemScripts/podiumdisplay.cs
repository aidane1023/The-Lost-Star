using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class podiumdisplay : MonoBehaviour
{

    public Database itemDatabase;
    public SpriteRenderer displaySprite;
    public float itemScale = 4;
    private bool followPlayer = false;
    private bool holding = false;
    public Transform objectToFollow;
    public Vector3 offset;
   
    public void Start()
    {
        randomDisplay();
        holding = false;
    }

    public void Update()
    {
        if (followPlayer == true)
        {
            transform.position = objectToFollow.position + offset;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "shop")
        {
            Debug.Log("follow player disabled");
            followPlayer = false;
            holding = false;
            displaySprite.enabled = false;
            return;
        }

        if (other.tag == "Player" && holding == false)
            {
            Debug.Log("Space pressed");
            followPlayer = true;
            holding = true;
            return;
            }
       

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
