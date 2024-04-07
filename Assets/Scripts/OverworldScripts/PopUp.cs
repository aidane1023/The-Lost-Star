using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUp : MonoBehaviour
{
    public Animator anim;
    public Transform player;
    
    void Update()
    {
        if(TrainingDummy.cleared)
        {
            if ((Vector3.Distance(player.position, this.transform.position) <= 3.3f))
            {
                anim.SetBool("InRange", true);
            }
            else
            {
                anim.SetBool("InRange", false);
            }
        }
        
    }
}
