using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;



public class WallPuzzle : MonoBehaviour
{
    private bool inRange;
    private bool active;
    public GameObject tube;
    public GameObject player;
    private PlayerController playerController;
    private Rigidbody tubeRB;
    private float speed = 6;
    private float savedSpeed;
    public GameObject spawner;
    public GameObject sphere;
    public bool win;
    public GameObject red;
    public GameObject blue;
    public GameObject green;
    private bool redDown = true;
    private bool blueDown = true;
    private bool greenDown = true;
    private Rigidbody redRB;
    private Rigidbody blueRB;
    private Rigidbody greenRB;
    public GameObject activeBall;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera secondaryCamera;

    private void Start()
    {
        tubeRB = tube.GetComponent<Rigidbody>();
        redRB = red.GetComponent<Rigidbody>();
        blueRB = blue.GetComponent<Rigidbody>();
        greenRB = green.GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
        savedSpeed = playerController.speed;

    }

    private void Update()
    {


        if (inRange && Input.GetKeyDown(KeyCode.F) && win != true)
        {
            StartCoroutine(WaitActive());
        }

        else if (inRange && Input.GetKeyDown(KeyCode.Escape))
        {
          
            StartCoroutine(Finished());
        }

        

        if (active)
        {
            Movement();

        }
        if (activeBall == null)
        {
            win = true;
            StartCoroutine(Finished());
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
        secondaryCamera.Priority = 20;
        primaryCamera.Priority = 0;
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

    public void RedMove()
    {
        Vector3 moveDir = new Vector3(0,1, 0);
        if (redDown)
        {
            redRB.velocity = moveDir * speed;
            redDown = false;
        }
        else
        {
            redRB.velocity = moveDir * speed * -1;
            redDown = true;
        }
    }

    public void GreenMove()
    {
        Vector3 moveDir = new Vector3(0, 1, 0);
        if (greenDown)
        {
            greenRB.velocity = moveDir * speed;
            greenDown = false;
        }
        else
        {
            greenRB.velocity = moveDir * speed * -1;
            greenDown = true;
        }
    }
    public void BlueMove()
    {
        Vector3 moveDir = new Vector3(0, 1, 0);
        if (blueDown)
        {
            blueRB.velocity = moveDir * speed;
            blueDown = false;
        }
        else
        {
            blueRB.velocity = moveDir * speed * -1;
            blueDown = true;
        }
    }
    public void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 moveDir = new Vector3(x, 0, 0);
        tubeRB.velocity = moveDir * speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(sphere, spawner.transform.position + Random.onUnitSphere * 0.1f, spawner.transform.rotation);

        }
    }
}
