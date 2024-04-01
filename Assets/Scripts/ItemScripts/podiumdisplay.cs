using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class podiumdisplay : MonoBehaviour
{
    public Database itemDatabase;
    public SpriteRenderer displaySprite;
    public float itemScale = 4;
    public Transform objectToFollow;
    public Vector3 offset;
    public int money = 10;
    public int value;

    private Vector3 startingPosition;
    private bool holding = false;

    private void Start()
    {
        startingPosition = transform.position;
        RandomDisplay();
    }

    private void Update()
    {
        if (holding)
        {

            transform.position = objectToFollow.position + offset;
            transform.RotateAround(objectToFollow.position, Vector3.up, 100 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shop"))
        {
            ReturnToOriginalPosition();
        }
        else if (other.CompareTag("Player") && !holding)
        {
            PickUpObject();
        }
    }

    private void PickUpObject()
    {
        holding = true;
    }

    private void ReturnToOriginalPosition()
    {
        if (money < value)
        {
            holding = false;
            transform.position = startingPosition;
        }
        else if (money >= value) 
        {
            displaySprite.size = new Vector2(itemScale * 2, itemScale * 2);
            money = money - value;
        }
    }

    private void RandomDisplay()
    {
        if (itemDatabase == null || itemDatabase.Items.Count == 0)
            return;

        int randomID = Random.Range(0, itemDatabase.Items.Count);
        InventoryItemData randomItem = itemDatabase.GetItem(randomID);
        displaySprite.sprite = randomItem.Icon;
        value = randomItem.Value;
        displaySprite.size = new Vector2(itemScale, itemScale);
    }
}
