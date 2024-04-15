using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class ExitWall : MonoBehaviour
{
    public Light wallLight;
    public Light tiltLight;
    public Light dropLight;

    private WallPuzzle wallP;
    public GameObject wallPuzzle;
    private TiltPuzzle tiltP;
    public GameObject tiltPuzzle;
    private DropPuzzle dropP;
    public GameObject dropPuzzle;
    private PlayerController playerController;
    public GameObject player;
    private float savedSpeed;

    private bool wallWinRun;
    private bool tiltWinRun;
    private bool dropWinRun;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera lightCamera;
    public CinemachineVirtualCamera wallCamera;


    void Start()
    {
        wallLight.enabled = false;
        tiltLight.enabled = false;
        dropLight.enabled = false;

        wallWinRun = false;
        tiltWinRun = false;
        dropWinRun = false;

        wallP = wallPuzzle.GetComponent<WallPuzzle>();
        tiltP = tiltPuzzle.GetComponent<TiltPuzzle>();
        dropP = dropPuzzle.GetComponent<DropPuzzle>();
        playerController = player.GetComponent<PlayerController>();
        savedSpeed = playerController.speed;

    }

  
    void Update()
    {
        if (wallP.win == true && wallWinRun != true)
        {
            StartCoroutine(SwitchToLight(wallLight));
            wallWinRun=true;

        }

        if (tiltP.win == true && tiltLight != true)
        {
            StartCoroutine(SwitchToLight(tiltLight));
            tiltWinRun=true;


        }

        if (dropP.win == true && dropWinRun != true)
        {
            StartCoroutine(SwitchToLight(dropLight));
            dropWinRun=true;


        }
    }

    IEnumerator SwitchToLight(Light name)
    {
        primaryCamera.Priority = 0;
        lightCamera.Priority = 20;
        playerController.speed = 0;
        yield return new WaitForSeconds(2);
        name.enabled = true;
        yield return new WaitForSeconds(1);
        StartCoroutine(BackToPlayer());
        
    }

    IEnumerator BackToPlayer()
    {
        primaryCamera.Priority = 20;
        lightCamera.Priority = 0;
        playerController.speed = savedSpeed;
        yield return new WaitForSeconds(2);
    }
}
