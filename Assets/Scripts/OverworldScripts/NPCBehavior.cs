using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCBehavior : MonoBehaviour
{
    public GameObject player, characterDialogue;
    PlayerController playerController;
    public GameObject[] dialogueObjects, interactObjects;
    public Dialogue[] dialogue;
    bool textPlaying = false;
    public bool isOneShot = false, isBossTrigger;
    public Animator playerAnimator;
    public int dialogueCount;
    int index = 0;
    float timer = 0f;
    public bool isMozzy, isBouncer, isLegoGuy;
    public Level3Items itemManager;

    public AudioSource source;
    public AudioClip dialoguePress;
    
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        if (isBossTrigger)
        {
            interactObjects[0].SetActive(false);
        }   
    }

    // Update is called once per frame
    void Update()
    {    
        if ((Vector3.Distance(player.transform.position, this.transform.position) <= 3f) && Input.GetButtonDown("Submit") && !textPlaying)
        {
            if (!isBossTrigger)
            {

                characterDialogue.SetActive(true);
                playerAnimator.SetFloat("moving", 0);
                playerController.speed = 0f;
                player.GetComponent<AudioSource>().enabled = false;
                player.GetComponent<PlayerController>().enabled = false;
                textPlaying = true;

                if (isMozzy && itemManager.watchFound && !itemManager.watchGiven)
                {
                    index = 2;
                    Debug.Log("Watch Found Dialogue");
                }

                if (isLegoGuy && itemManager.lighterFound && !itemManager.lighterGiven)
                {
                    index = 1;
                }

                if (isBouncer && itemManager.ticketFound && !itemManager.ticketGiven)
                {
                    index = 1;
                }
                Debug.Log(index);

                dialogueObjects[index].SetActive(true);
                interactObjects[0].SetActive(false);
                dialogue[index].DialogueTriggered();

                source.PlayOneShot(dialoguePress);
            }
            else
            {
                Debug.Log("Boss Triggered");
                SceneManager.LoadScene("JackTransition");
            }
        }
        if (dialogue[index] != null)
        {
            if ((dialogue[index].dialogueEnded == true) && (textPlaying == true))
            {
                player.GetComponent<PlayerController>().enabled = true;
                player.GetComponent<AudioSource>().enabled = true;
                playerController.speed = 4f;
                characterDialogue.SetActive(false);
                if (isOneShot)
                {
                    ShutUp();
                }
                if (timer < 2.0f)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    Debug.Log("Dialogue ended.");
                    //index = 1;
                    
                    timer = 0f;
                    interactObjects[0].SetActive(true);
                    textPlaying = false;
                    //Reset();
                    Debug.Log(index);
                    if (isMozzy && index == 0)
                    {
                        index = 1;
                    }
                }
            }
        }   
    }
    public void ShutUp()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<AudioSource>().enabled = true;
        playerController.speed = 4f;
        dialogueObjects[0].SetActive(false);
        this.enabled = false;
    }

    public void giveWatch()
    {
        itemManager.watchGiven = true;
        index = 3;
    }

    public void getLighter()
    {
        itemManager.lighterFound = true;
    }

    public void getTicket()
    {
        itemManager.ticketFound = true;
    }

    public void giveLighter()
    {
        itemManager.lighterGiven = true;
        index = 2;
    }

    public void giveTicket()
    {
        itemManager.ticketGiven = true;
        index = 2;
    }
    //IEnumerator Reset()
    //{
    //    yield return new WaitForSeconds(2f);
    //    player.GetComponent<PlayerController>().enabled = true;
    //    textPlaying = false;
    //}
}
