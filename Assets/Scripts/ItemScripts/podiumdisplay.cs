using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

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
    public float arcHeight = 1f;
    public float startTime = 0f;
    public float duration = 100.0f;
    private float cumulativeRotation = 0f;
    private bool interact = false;
    private bool activeShop = false;
    private float savedSpeed;


    private PlayerController playerController;


    private Vector3 startingPosition;
    private bool holding = false;

    private void Start()
    {
        startingPosition = transform.position;
        RandomDisplay();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

        savedSpeed = playerController.speed;
    }


    private void Update()
    {
        if (interact && !holding && Input.GetKeyDown(KeyCode.Space))
        {
            PickUpObject();
        }


        if (holding)
        {
           
            cumulativeRotation += rotationSpeed * Time.deltaTime;
            cumulativeRotation = cumulativeRotation % 360;

            Quaternion desiredRotation = Quaternion.Euler(0f, cumulativeRotation, 0f);
            Vector3 rotatedPosition = desiredRotation * offset;
            transform.position = objectToFollow.position + rotatedPosition;

            if (activeShop && Input.GetKeyDown (KeyCode.Space))
            {
                Shop();
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("shop"))
        {

            activeShop = true;
        }
        else if (other.CompareTag("Player"))
        {
            interact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("shop"))
        {

            activeShop = false;
        }
        else if (other.CompareTag("Player"))
        {
            interact =  false;
        }
    }



    private void PickUpObject()
    {
        
        playerController.speed = 0f;

        Vector3 jumpTarget = objectToFollow.position + (Vector3.right * distance);
        transform.DOJump(jumpTarget, 1, 1, 1.0f, false).OnComplete(() => {
            offset = transform.position - objectToFollow.position;
            holding = true;
            playerController.speed = savedSpeed;
        });
    }


    private void Shop()
    {

        if (money < value)
        {
            holding = false;
            ReturnToSender();

        }
        else if (money >= value)
        {
           
            playerController.speed = 0f;
            transform.DOJump(objectToFollow.position, 1, 1, 1.0f, false).OnComplete(() => {
                playerController.speed = savedSpeed;
                Destroy(gameObject);
                holding = false;
 

            });

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
        value = randomItem.Value;
    }

    private void ReturnToSender()
    {
        transform.DOJump(startingPosition, 1, 1, 1.0f, false);
    }

}
