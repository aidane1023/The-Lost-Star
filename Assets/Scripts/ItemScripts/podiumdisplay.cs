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
    public float distance = 1f; 
    public float rotationSpeed = 200f;
    public float arcHeight = 5f;
    public float startTime = 0f;
    public float duration = 100.0f; 


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
            { Vector3 desiredPosition = objectToFollow.position + (Vector3.forward * distance); 
                Quaternion desiredRotation = Quaternion.Euler(0f, Time.time * rotationSpeed, 0f); 
                Vector3 offset = desiredRotation * new Vector3(0f, 0f, distance); 
                transform.position = objectToFollow.position + offset; } 
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shop"))
        {
            ReturnToOriginalPosition();
            Shop();
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
    private void Shop()
    {
        if (money < value)
        {
            holding = false;
            ReturnToSender();
            
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
        displaySprite.size = new Vector2(itemScale, itemScale);
    }

    private void ReturnToSender()
    {
        // Set startTime when the item is marked for return
        if (!holding && startTime == 0f)
        {
            startTime = Time.time;
        }

        float elapsedTime = Time.time - startTime;
        float t = elapsedTime / duration;

        // Ensure t stays between 0 and 1
        t = Mathf.Clamp01(t);

        // Calculate the point on the quadratic Bezier curve
        Vector3 p0 = transform.position;
        Vector3 p1 = startingPosition + Vector3.up * arcHeight;
        Vector3 p2 = startingPosition;

        Vector3 newPos = QuadraticBezierCurve(p0, p1, p2, t);

        // Move the object to the new position
        transform.position = newPos;

        // If the time exceeds duration, reset the startTime and holding status
        if (elapsedTime >= duration)
        {
            startTime = 0f;
            holding = false;
        }
    }

    // Calculate the point on a quadratic Bezier curve
    private Vector3 QuadraticBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2 * u * t * p1; // 2(1-t)t * P1
        p += tt * p2; // t^2 * P2

        return p;
    }

}
