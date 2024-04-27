using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public GameObject player, characterDialogue;
    PlayerController playerController;
    public GameObject[] dialogueObjects, interactObjects;
    private Dialogue dialogue;
    bool textPlaying = false;
    public Animator playerAnimator;
    public int dialogueCount;
    int index = 0;
    float timer = 0f;

    public AudioSource source;
    public AudioClip dialoguePress;
    
    void Start()
    {
        //for (int i = 0; i < dialogueCount; i++)
        //{
        dialogue = dialogueObjects[0].GetComponent<Dialogue>();      
        //}
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(player.transform.position, this.transform.position) <= 3f) && Input.GetKeyUp(KeyCode.Z) && !textPlaying)
        {
            characterDialogue.SetActive(true);
            playerAnimator.SetFloat("moving", 0);
            playerController.speed = 0f;
            player.GetComponent<AudioSource>().enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
            textPlaying = true;
            dialogueObjects[0].SetActive(true);
            interactObjects[0].SetActive(false);
            dialogue.DialogueTriggered();

            source.PlayOneShot(dialoguePress);
        }
        if ((dialogue.dialogueEnded == true) && (textPlaying == true))
        {
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<AudioSource>().enabled = true;
            playerController.speed = 4f;
            characterDialogue.SetActive(false);
            if (timer < 2.0f)
            {
                timer += Time.deltaTime;
            }
            else
            {
                Debug.Log("Dialogue ended.");
                //index = 2;
                
                timer = 0f;
                interactObjects[0].SetActive(true);
                textPlaying = false;
                //Reset();
            }

        }
        
    }

    //IEnumerator Reset()
    //{
    //    yield return new WaitForSeconds(2f);
    //    player.GetComponent<PlayerController>().enabled = true;
    //    textPlaying = false;
    //}
}
