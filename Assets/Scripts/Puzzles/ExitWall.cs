using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class ExitWall : MonoBehaviour
{
    public Light wallLight;
    public Light tiltLight;

    private WallPuzzle wallP;
    //public GameObject wallPuzzle;
    private TiltPuzzle tiltP;
    //public GameObject tiltPuzzle;
    private PlayerController playerController;
    //public GameObject player;
    private float savedSpeed;

    private bool wallWinRun;
    private bool tiltWinRun;
    private bool wallUpRun;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera lightCamera;
    public CinemachineVirtualCamera wallCamera;
    public GameObject door;
    private Rigidbody doorRB;

    private int speed = 5;


    void Start()
    {
        wallLight.enabled = false;
        tiltLight.enabled = false;

        wallWinRun = false;
        tiltWinRun = false;
        wallUpRun = false;

        wallP = FindObjectOfType<WallPuzzle>();
        tiltP = FindObjectOfType<TiltPuzzle>();

        playerController = FindObjectOfType<PlayerController>();
        doorRB = door.GetComponent<Rigidbody>();
        savedSpeed = playerController.speed;
        

    }

  
    void Update()
    {
        if (wallP.win == true && wallWinRun != true)
        {
            StartCoroutine(SwitchToLight(wallLight));
            wallWinRun=true;

        }

        if (tiltP.win == true && tiltWinRun != true)
        {
            StartCoroutine(SwitchToLight(tiltLight));
            tiltWinRun=true;


        }

       

       
    }

    IEnumerator SwitchToLight(Light name)
    {
        playerController.speed = 0;
        primaryCamera.Priority = 0;
        lightCamera.Priority = 20;
        yield return new WaitForSeconds(2);
        name.enabled = true;
        yield return new WaitForSeconds(1);

        if (wallWinRun && tiltWinRun && wallUpRun != true)
        {

            StartCoroutine(SwitchToWall());
            wallUpRun = true;
        }
        else StartCoroutine(BackToPlayer());
        
    }

    IEnumerator BackToPlayer()
    {
        primaryCamera.Priority = 20;
        lightCamera.Priority = 0;
        yield return new WaitForSeconds(2);
        playerController.speed = savedSpeed;
    }

     IEnumerator SwitchToWall()
    {
        playerController.speed = 0;
        primaryCamera.Priority = 0;
        wallCamera.Priority = 30;
        yield return new WaitForSeconds(2);
        Vector3 moveDir = new Vector3(0, 1, 0);
        doorRB.velocity = moveDir * speed;
        yield return new WaitForSeconds(1);
        StartCoroutine(BackToPlayer());
        wallCamera.Priority = 0;

    }
}
