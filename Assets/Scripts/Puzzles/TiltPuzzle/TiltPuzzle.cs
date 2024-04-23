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
    public bool win;
    public AudioSource source;
    public AudioClip ballDrop;
    public AudioSource machineSource;
    public AudioClip buttonClick;
    public AudioClip puzzleSolved;




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


        if (inRange && Input.GetKeyDown(KeyCode.Z) && win != true && noActiveBall)
        {
   
            
            StartCoroutine(WaitActive());
        }

        if (inRange && Input.GetKeyDown(KeyCode.X))
        {
            
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

            if (x == 0) machineSource.volume = 0;
            else machineSource.volume = 1f;

            if (Input.GetKeyDown(KeyCode.Z) && noActiveBall)
            {
                noActiveBall = false;
                activeBall = Instantiate(sphere, spawner.transform.position + Random.onUnitSphere * 0.1f, spawner.transform.rotation);
                StartCoroutine("BallSound");
                cam3.Follow = activeBall.transform;
                cam3.LookAt = activeBall.transform;
                secondaryCamera.Priority = 0;
                cam3.Priority = 20;

               
            }

        }
        else machineSource.volume = 0;

        if (active && activeBall == null && !noActiveBall)
        {
            noActiveBall = true;
           
            cam3.Priority = 0;
            secondaryCamera.Priority = 20;
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
            cam3.Priority = 0;
            win = true;
            active = false;
            source.PlayOneShot(puzzleSolved);
            
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
