using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class leavingRocket : MonoBehaviour
{
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera rocketCamera;

    public Transform destination;

    public AudioSource source;
    public AudioClip blastOff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            {
            other.gameObject.SetActive(false);
            StartCoroutine(DoRocket());


        }
    }
    IEnumerator DoRocket()
    {
        rocketCamera.Priority = 20;
        primaryCamera.Priority = 0;
        yield return new WaitForSeconds(2f);
        gameObject.transform.DOJump(destination.position, 1f, 1, 20.0f, false);
        source.PlayOneShot(blastOff);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ThanksForPlayingScene");
    }
}
