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

    private float savedSpeed;
  
    public GameObject trail;

    private void Start()
    {


        playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }


        savedSpeed = playerController.speed;


    }
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
        Vector3 newPostion = objectToFollow.position + (Vector3.up * 0.5f); ;
        transform.DOJump(newPostion, 1, 1, 1.0f, false).OnComplete(() => {


            SceneManager.LoadScene("HUBBuild");


        });
    }



}

