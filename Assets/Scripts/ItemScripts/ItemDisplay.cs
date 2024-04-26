using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplay : MonoBehaviour
{
    public Database itemDatabase;
    public SpriteRenderer displaySprite;
    public float itemScale = 4;
    public Transform objectToFollow;
    public Vector3 offset;
    public int value;  // Represents the cost of the item in coins
    public float distance = 1f;
    public float rotationSpeed = 200f;
    public float arcHeight = 1f;
    private float cumulativeRotation = 0f;
    private bool interact = false;
    public bool activeShop = false;
    private float savedSpeed;
    public GameObject trail;
    private GameObject playerObject;
    private PlayerController playerController;
    public ShopManager shopManager;
    public PlayerInventory playerInventory;
    private InventoryItemData currentItem;
    private Vector3 startingPosition;
    public bool holding = false;
    private static List<ItemDisplay> pickedItems = new List<ItemDisplay>(); // List to track picked up items

    private void Start()
    {
        startingPosition = transform.position;
        RandomDisplay();

        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
            savedSpeed = playerController.speed;
        }
    }

    private void Update()
    {
        if (interact && !holding && Input.GetKeyDown(KeyCode.Z))
        {
            PickUpObject();
        }

        if (holding)
        {
            cumulativeRotation += rotationSpeed * Time.deltaTime;
            cumulativeRotation %= 360;
            Quaternion desiredRotation = Quaternion.Euler(0f, cumulativeRotation, 0f);
            Vector3 rotatedPosition = desiredRotation * offset;
            transform.position = objectToFollow.position + rotatedPosition;

            if (activeShop && Input.GetKeyDown(KeyCode.Z))
            {
                TryCheckout();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact = false;
        }
    }

    private void PickUpObject()
    {
        shopManager.AddItemValue(value);
        pickedItems.Add(this);
        playerController.speed = 0f;

        Vector3 jumpTarget = objectToFollow.position + (Vector3.back * distance);
        transform.DOJump(jumpTarget, arcHeight, 1, 1.0f, false).OnComplete(() => {
            offset = transform.position - objectToFollow.position;
            holding = true;
            playerController.speed = savedSpeed;
        });
    }

    private void TryCheckout()
    {
        int totalCost = shopManager.TotalValue;
        if (PlayerBattler.coins >= totalCost)
        {
            foreach (var item in pickedItems)
            {
                if (!item.AddToInventory())
                {
                    item.ReturnToSender(); 
                    continue;
                }
                playerController.speed = 0f;
                StartCoroutine(TrailDecay());
                transform.DOJump(objectToFollow.position, arcHeight, 1, 1.0f, false).OnComplete(() => {
                    playerController.speed = savedSpeed;
                    trail.transform.parent = playerObject.transform;
                    Destroy(gameObject);
                    pickedItems.Clear();
                    PlayerBattler.coins -= totalCost;
                    shopManager.ResetTotalValue();
                });
            }
            
        }
        else
        {
            foreach (var item in pickedItems)
            {
                item.ReturnToSender();
            }
            pickedItems.Clear();
        }
    }

    private bool AddToInventory()
    {
        if (playerInventory.AddItem(currentItem))
        {
            return true;
        }
        else
        {
            Debug.Log("Inventory is full.");
            return false;
        }
    }

    
    public void ReturnToSender()
    {
        transform.DOJump(startingPosition, arcHeight, 1, 1.0f, false).OnComplete(() => {
            holding = false;
        });
    }

    private void RandomDisplay()
    {
        if (itemDatabase == null || itemDatabase.Items.Count == 0)
            return;

        int randomID = Random.Range(0, itemDatabase.Items.Count);
        currentItem = itemDatabase.GetItem(randomID);
        if (currentItem != null)
        {
            displaySprite.sprite = currentItem.Icon;
            displaySprite.size = new Vector2(itemScale, itemScale);
            value = currentItem.Value;
        }
    }

    private IEnumerator TrailDecay()
    {
        TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();
        trailRenderer.autodestruct = true;

        trailRenderer.time = 3f;
        yield return new WaitForSeconds(0.3f);
        trailRenderer.time = 2.5f;
        yield return new WaitForSeconds(0.3f);
        trailRenderer.time = 2f;
        yield return new WaitForSeconds(0.3f);
        trailRenderer.time = 1.5f;
        yield return new WaitForSeconds(0.3f);
        trailRenderer.time = 1f;
        yield return new WaitForSeconds(0.3f);
        trailRenderer.time = 0.5f;
        yield return new WaitForSeconds(0.3f);
        trailRenderer.time = 0f;
        trailRenderer.emitting = false;


    }

}
