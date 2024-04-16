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
    public GameObject sphere;
    public bool delete = false;
    private CountManager manager;
    public GameObject managerObject;
    public bool win;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera secondaryCamera;

    private void Start()
    {
        tubeRB = tube.GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
        savedSpeed = playerController.speed;
        manager = managerObject.GetComponent<CountManager>();
    }

    private void Update()
    {
        

        if (inRange && Input.GetKeyDown(KeyCode.F) && win != true)
        {
           
            StartCoroutine(WaitActive());
           
            

        }

        if (inRange && Input.GetKeyDown(KeyCode.Escape))
            { 
            StartCoroutine(Finished());
            }

        if (active)
        {
            float x = Input.GetAxis("Horizontal");
            Vector3 moveDir = new Vector3(x, 0, 0);
            tubeRB.velocity = moveDir * speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
             Instantiate(sphere, spawner.transform.position+Random.onUnitSphere*0.1f, spawner.transform.rotation);
            }

            if (manager.box1Count == 1 && manager.box2Count == 2 && manager.box3Count == 3)
            {
                active = false;
                
                
                win = true;
            }

           
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
        primaryCamera.Priority = 0;
        secondaryCamera.Priority = 20;
        
        playerController.speed = 0;
        yield return new WaitForSeconds(2);
        active = true;
    }
    IEnumerator Finished()
    {
        active = false;
        secondaryCamera.Priority = 0;
        primaryCamera.Priority = 20;
        yield return new WaitForSeconds(2);
        playerController.speed = savedSpeed;
    }
}
