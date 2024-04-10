using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeBehavior : MonoBehaviour
{
    public Animator anim;
    public Transform player;

    bool raised = true;
    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(player.position, this.transform.position) <= 8f) && Input.GetKeyUp(KeyCode.Z))
            {
                if (raised)
                {
                    raised = false;
                    anim.SetBool("Raise", true);
                }
                else
                {
                    raised = true;
                    anim.SetBool("Raise", false);
                }
                
            }
    }
}
