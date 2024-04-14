using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TiltPuzzle : MonoBehaviour
{
    private bool inRange;
    private bool active;
    public List<GameObject> planks;
    private List<Rigidbody> planksRB;
    public GameObject player;
    private PlayerController playerController;
    private float speed = 200;
    private float savedSpeed;
    public GameObject sphere;
    public GameObject spawner;
    private bool noActiveBall = true;
    private GameObject activeBall = null;



    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera secondaryCamera;
    public CinemachineVirtualCamera cam3;

    private void Start()
    {
        planksRB = new List<Rigidbody>();

        foreach (GameObject plank in planks)
        {
            planksRB.Add(plank.GetComponent<Rigidbody>());
        }


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

        if (inRange && Input.GetKeyDown(KeyCode.Escape))
        {
            secondaryCamera.Priority = 10;
            primaryCamera.Priority = 20;
            StartCoroutine(Finished());
        }

        if (active)
        {
            float x = Input.GetAxis("Horizontal");
            float rotationAmount = x * Time.deltaTime * speed;
            for (int i = 0; i < planksRB.Count; i++)
            {

                planksRB[i].transform.Rotate(0, 0, -rotationAmount);
              
            }

            if (Input.GetKeyDown(KeyCode.Space) && noActiveBall)
            {
                noActiveBall = false;
                activeBall = Instantiate(sphere, spawner.transform.position + Random.onUnitSphere * 0.1f, spawner.transform.rotation);
                cam3.Follow = activeBall.transform;
                cam3.LookAt = activeBall.transform;
                cam3.Priority = 30;

               
            }

        }

        if (active && activeBall == null && !noActiveBall)
        {
            noActiveBall = true;
           
            cam3.Priority = 10;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }

        if (other.gameObject.CompareTag("ball") && activeBall != null)
        {
            Debug.Log("Win condition met!");
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
    IEnumerator Finished()
    {

        yield return new WaitForSeconds(2);
        playerController.speed = savedSpeed;
        active = false;
    }


}
