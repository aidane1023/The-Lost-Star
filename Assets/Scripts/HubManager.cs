using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class HubManager : MonoBehaviour
{
    public GameObject topObject;
    public GameObject middleObject;
    public GameObject bottomObject;
    public GameObject rocket;
    public PlayerController playerController;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera rocketCamera;

    public Transform player;
    public Transform topPos;
    public Transform middlePos;
    public Transform bottomPos;

    public static bool topJumped;
    public static bool middleJumped;
    public static bool bottomJumped;

    public static bool fromShop = false;
    public static bool noWalk = false;
    public static bool allCleared = false;

    public AudioSource playerSound;
    public AudioSource source;
    public AudioClip rocketpieceFound;
    public AudioClip buildRocket;


    void Start()
    {
        player.position = new Vector3(-10.5,0,-9.01000023);
        UpdateAppearance();
        rocket.SetActive(false);
    }

    void UpdateAppearance()
    {
        topObject.SetActive(GameManager.Instance.HasTop);
        middleObject.SetActive(GameManager.Instance.HasMiddle);
        bottomObject.SetActive(GameManager.Instance.HasBottom);

        if (GameManager.Instance.HasTop)
        {
            if (!topJumped)
            {
                JumpToObject(topObject, topPos);
                topJumped = true;
            }
            else
            {
                topObject.transform.position = topPos.position;
            }
        }

        if (GameManager.Instance.HasMiddle)
        {
            if (!middleJumped)
            {
                JumpToObject(middleObject, middlePos);
                middleJumped = true;
            }
            else
            {
                middleObject.transform.position = middlePos.position;
            }
        }

        if (GameManager.Instance.HasBottom)
        {
            if (!bottomJumped)
            {
                JumpToObject(bottomObject, bottomPos);
                bottomJumped = true;
            }
            else
            {
                bottomObject.transform.position = bottomPos.position;
            }
        }

        if ((GameManager.Instance.HasTop || GameManager.Instance.HasMiddle || GameManager.Instance.HasBottom) && !fromShop)
        {
            StartCoroutine(CameraSwitch());
        }
    }



    void JumpToObject(GameObject obj, Transform target)
    {
        noWalk = true;
        playerSound.enabled = false;
        playerController.enabled = false;
        obj.transform.DOJump(target.position, 2f, 1, 3.0f, false)
         .OnComplete(() =>
         {
             obj.GetComponent<SphereCollider>().enabled = true;
         });
    }


    IEnumerator CameraSwitch()
    {
        noWalk = true;
        yield return new WaitForSeconds(0.2f);
        playerController.enabled = false;
        yield return new WaitForSeconds(0.1f);
        primaryCamera.Priority = 0;
        rocketCamera.Priority = 20;
        yield return new WaitForSeconds(1.5f);
        source.PlayOneShot(rocketpieceFound);
        yield return new WaitForSeconds(3);
        if (GameManager.Instance.HasTop && GameManager.Instance.HasMiddle && GameManager.Instance.HasBottom)
        {
            StartCoroutine(DoRocket());
        }
        else
        {
            rocketCamera.Priority = 0;
            primaryCamera.Priority = 20;
            yield return new WaitForSeconds(1);
            playerController.enabled = true;
            noWalk = false;
            playerSound.enabled = true;
        }
      
    }

    IEnumerator DoRocket()
    {
        yield return new WaitForSeconds(0.5f);
        JumpToObject(topObject, middlePos);
        JumpToObject(middleObject, middlePos);
        JumpToObject(bottomObject, middlePos);
        source.PlayOneShot(buildRocket);
        yield return new WaitForSeconds(2.75f);
        rocket.SetActive(true);
        topObject.SetActive(false);
        middleObject.SetActive(false);
        bottomObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        rocketCamera.Priority = 0;
        primaryCamera.Priority = 20;
        allCleared = true;
        playerController.enabled = true;
        noWalk = false;
    }

   
}
