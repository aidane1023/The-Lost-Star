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

    public AudioSource playerSound;


    void Start()
    {
        UpdateAppearance();
        rocket.SetActive(false);

        BridgeBehavior.enemyCount = 4;
        BossTransition.hydraDefeated = false;
       

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

        if (GameManager.Instance.HasTop || GameManager.Instance.HasMiddle || GameManager.Instance.HasBottom)
        {
            StartCoroutine(CameraSwitch());
        }
    }



    void JumpToObject(GameObject obj, Transform target)
    {
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
        playerController.enabled = false;
        yield return new WaitForSeconds(0.1f);
        primaryCamera.Priority = 0;
        rocketCamera.Priority = 20;
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
            playerSound.enabled = true;
        }
      
    }

    IEnumerator DoRocket()
    {
        yield return new WaitForSeconds(0.5f);
        JumpToObject(topObject, middlePos);
        JumpToObject(middleObject, middlePos);
        JumpToObject(bottomObject, middlePos);
        yield return new WaitForSeconds(2.75f);
        rocket.SetActive(true);
        topObject.SetActive(false);
        middleObject.SetActive(false);
        bottomObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        rocketCamera.Priority = 0;
        primaryCamera.Priority = 20;
        playerController.enabled = true;
        playerSound.enabled = true;


    }

   
}
