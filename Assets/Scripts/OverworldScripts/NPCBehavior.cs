using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public GameObject player;
    PlayerController playerController;
    public GameObject[] dialogueObjects;
    private Dialogue dialogue;
    bool textPlaying = false;
    public Animator playerAnimator;
    public int dialogueCount;
    int index = 0;
    
    void Start()
    {
        //for (int i = 0; i < dialogueCount; i++)
       // {
            dialogue = dialogueObjects[0].GetComponent<Dialogue>();
       // }
    }

    // Update is called once per frame
    void Update()
    {
        playerController = player.GetComponent<PlayerController>();

        if ((Vector3.Distance(player.transform.position, this.transform.position) <= 3f) && Input.GetKeyUp(KeyCode.Z) && !textPlaying)
        {
            playerAnimator.SetFloat("moving", 0);
            playerController.speed = 0f;
            player.GetComponent<PlayerController>().enabled = false;
            textPlaying = true;
            dialogueObjects[0].SetActive(true);
            dialogue.DialogueTriggered();
        }
        if ((dialogue.dialogueEnded == true) && (textPlaying == true))
        {
            Debug.Log("Dialogue ended.");
            playerController.speed = 2f;
            //index = 2;
            player.GetComponent<PlayerController>().enabled = true;
            textPlaying = false;
            //Reset();
        }
        
    }

    //IEnumerator Reset()
    //{
    //    yield return new WaitForSeconds(2f);
    //    player.GetComponent<PlayerController>().enabled = true;
    //    textPlaying = false;
    //}
}
