using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class PodiumDisplay : MonoBehaviour
{
    public Database itemDatabase;
    public SpriteRenderer displaySprite;
    public float itemScale = 4;
    public Transform objectToFollow;
    public Vector3 offset;
    public static int money = 100;
    public int value;
    public float distance = 1f;
    public float rotationSpeed = 200f;
    public float arcHeight = 1f;
    public float startTime = 0f;
    public float duration = 100.0f;
    private float cumulativeRotation = 0f;
    private bool interact = false;
    public bool activeShop = false;
    private float savedSpeed;
    public GameObject trail;
    private GameObject playerObject;



    private PlayerController playerController;
    public ShopManager shopManager;


    private Vector3 startingPosition;
    public bool holding = false;

    private void Start()
    {
        startingPosition = transform.position;
        RandomDisplay();

        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }


        savedSpeed = playerController.speed;


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
            cumulativeRotation = cumulativeRotation % 360;

            Quaternion desiredRotation = Quaternion.Euler(0f, cumulativeRotation, 0f);
            Vector3 rotatedPosition = desiredRotation * offset;
            transform.position = objectToFollow.position + rotatedPosition;



            if (activeShop && Input.GetKeyDown(KeyCode.Z))
            {
                Shop();
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

        if (money < shopManager.TotalValue)
        {
            holding = false;
            ReturnToSender();

        }
        else if (money >= shopManager.TotalValue)
        {
            StartCoroutine(TrailDecay());
            playerController.speed = 0f;
            transform.DOJump(objectToFollow.position, 1, 1, 1.0f, false).OnComplete(() => {

                playerController.speed = savedSpeed;
                trail.transform.parent = playerObject.transform;
             
                Destroy(gameObject);
                holding = false;

                BearAnimation.danceSelect = Random.Range(0, 7);
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
