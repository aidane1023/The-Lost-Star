using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    public bool movingUp = false;
    public AudioSource source;

    public int pieceInArea = 0; //if the rocket piece is obtained from here, disable the popup
    bool interactable = true;

    void Start()
    {
        if(GameManager.Instance.HasTop && pieceInArea == 1) interactable = false;
        if(GameManager.Instance.HasMiddle && pieceInArea == 2) interactable = false;
        if(GameManager.Instance.HasBottom && pieceInArea == 3) interactable = false;
    }
    
    void Update()
    {
        if (TrainingDummy.cleared && interactable)
        {
            if ((Vector3.Distance(player.position, this.transform.position) <= 3.3f))
            {
                anim.SetBool("InRange", true);
                if (!movingUp)
                {
                    movingUp = true;
                    source.Play();

                }
            }
            else
            {
                anim.SetBool("InRange", false);
                if (movingUp) movingUp = false;
            }
        }
        
    }
}
