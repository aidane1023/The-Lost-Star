using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class BridgeBehavior : MonoBehaviour
{
    public Animator anim;
    public Collider box;
    
    public PlayerController playerController;
    private float savedSpeed = 5f;
    
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera bridgeCamera;

    public static int enemyCount = 5;
    private bool played = false;

    public AudioSource source;
    public AudioClip puzzleSolved;
    public AudioClip bridgeCreak;

    void Update()
    {
        if (enemyCount == 0 && played == false)
        {
            StartCoroutine(RaiseBridge());
        }
    }

    IEnumerator RaiseBridge()
    {
        played = true;
        playerController.speed = 0;
        primaryCamera.Priority = 0;
        bridgeCamera.Priority = 20;
        yield return new WaitForSeconds(2.2f);
        anim.SetBool("Raise", false);
        box.isTrigger = true;
        source.PlayOneShot(bridgeCreak);
        source.PlayOneShot(puzzleSolved);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(BackToPlayer());
    }

     IEnumerator BackToPlayer()
    {
        primaryCamera.Priority = 20;
        bridgeCamera.Priority = 0;
        yield return new WaitForSeconds(2);
        playerController.speed = savedSpeed;
    }
}
