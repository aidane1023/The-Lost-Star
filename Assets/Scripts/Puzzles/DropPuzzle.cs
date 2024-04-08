using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;



public class DropPuzzle : MonoBehaviour
{
    private bool inRange;
    private bool active;
    public GameObject tube;
    public GameObject player;
    private PlayerController playerController;
    private Rigidbody tubeRB;
    private float speed = 3;
    private float savedSpeed;
    public GameObject spawner;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera secondaryCamera;

    private void Start()
    {
        tubeRB = tube.GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
        savedSpeed = playerController.speed;
    }
    private void Update()
    {
       

        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            secondaryCamera.Priority = 20;
            primaryCamera.Priority = 10;
            StartCoroutine(WaitActive());
           
            

        }

        if (active)
        {
            float x = Input.GetAxis("Horizontal");
            Vector3 moveDir = new Vector3(x, 0, 0);
            tubeRB.velocity = moveDir * speed;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    IEnumerator WaitActive()
    {
        playerController.speed = 0;
        yield return new WaitForSeconds(2);
        active = true;
    }
}
