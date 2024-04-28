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
    public PlayerController playerController;

    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera rocketCamera;

    public Transform player;
    public Transform topPos;
    public Transform middlePos;
    public Transform bottomPos;

    void Start()
    {
        UpdateAppearance();
    }

    void UpdateAppearance()
    {
        topObject.SetActive(GameManager.Instance.HasTop);
        middleObject.SetActive(GameManager.Instance.HasMiddle);
        bottomObject.SetActive(GameManager.Instance.HasBottom);

        // Check if any pickup is present in the current level
        if (GameManager.Instance.HasTop || GameManager.Instance.HasMiddle || GameManager.Instance.HasBottom)
        {
            if (GameManager.Instance.HasTop)
            {
                JumpToObject(topObject, topPos);
            }
            else if (GameManager.Instance.HasMiddle)
            {
                JumpToObject(middleObject, middlePos);
            }
            else if (GameManager.Instance.HasBottom)
            {
                JumpToObject(bottomObject, bottomPos);
            }

            StartCoroutine(CameraSwitch());
        }
    }

    void JumpToObject(GameObject obj, Transform target)
    {
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
        rocketCamera.Priority = 0;
        primaryCamera.Priority = 20;
        yield return new WaitForSeconds(1);
        playerController.enabled = true;
    }
}
