using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Watch : MonoBehaviour
{
    public PlayerController playerController;
    public Transform objectToFollow;
    public Level3Items level3Items;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpObject();


        }
    }

    private void PickUpObject()
    {
        playerController.speed = 0f;
        Vector3 newPosition = objectToFollow.position + (Vector3.back * 0.5f); ;
        transform.DOJump(newPosition, 1, 1, 1.0f, false).OnComplete(() =>
        {
           Destroy(gameObject);
            playerController.speed = 5f;
            level3Items.watchFound = true;
        });
    }
}
