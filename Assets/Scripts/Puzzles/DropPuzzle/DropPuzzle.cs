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
    private int ballsLeft = 11;

    public AudioSource source;
    public AudioClip ballDrop;
    public AudioSource machineSource;
    public AudioClip buttonClick;

    public UIPauseScript uiPauseScript;
    

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
        

        if (inRange && Input.GetKeyDown(KeyCode.Z) && win != true)
        {
           
            StartCoroutine(WaitActive());
           
            

        }

        if (tubeRB.velocity.magnitude == 0) machineSource.volume = 0;
        else machineSource.volume = 1f;

        if (inRange && Input.GetKeyDown(KeyCode.Escape))
            { 
            StartCoroutine(Finished());
            }

        if (active)
        {
            float x = Input.GetAxis("Horizontal");
            Vector3 moveDir = new Vector3(x, 0, 0);
            tubeRB.velocity = moveDir * speed;



            if (Input.GetKeyDown(KeyCode.Z) && ballsLeft > 0)
            {
                
             Instantiate(sphere, spawner.transform.position+Random.onUnitSphere*0.1f, spawner.transform.rotation);
                ballsLeft --;
            StartCoroutine("BallSound");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ballsLeft = 11;
            }

            if (manager.box1Count == 2 && manager.box2Count == 4 && manager.box3Count == 3)
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
            uiPauseScript.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            uiPauseScript.enabled = true;
        }
    }

    IEnumerator WaitActive()
    {
        primaryCamera.Priority = 0;
        secondaryCamera.Priority = 20;
        
        playerController.speed = 0;
        source.PlayOneShot(buttonClick);
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
    IEnumerator BallSound()
    {
        yield return new WaitForSeconds(0.7f);
        source.PlayOneShot(ballDrop);
    }
}
