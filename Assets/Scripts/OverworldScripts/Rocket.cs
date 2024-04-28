using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.CinemachineOrbitalTransposer;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private PlayerController playerController;
    private GameObject playerObject;
    public Transform objectToFollow;
    public int pickUpType; // 1 for top, 2 for middle, 3 for bottom


    public GameObject trail;

    public AudioSource source;
    public AudioClip collectRocket;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PickUpObject();
            source.PlayOneShot(collectRocket);

        }
    }

    private void PickUpObject()
    {
        playerController.speed = 0f;
        Vector3 newPosition = objectToFollow.position + (Vector3.up * 0.5f); ;
        transform.DOJump(newPosition, 1, 1, 1.0f, false).OnComplete(() =>
        {
            // Access GameManager.Instance to set the appropriate property based on pickUpType
            GameManager.Instance.SetPickupStatus(pickUpType);
            SceneManager.LoadScene("HUBBuild");
        });
    }
}
