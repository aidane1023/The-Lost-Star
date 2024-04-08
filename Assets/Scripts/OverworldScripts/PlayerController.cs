using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Animator animator;
    private float moving;
    public Rigidbody rb;
    public SpriteRenderer sr;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if(BattleManager.overworldSpawn != new Vector3 (0,0,0)) RelocateAfterBattle();
        //if (HubSceneManager.leftHub != new Vector3 (0,0,0) && DemoSceneManager.home) RelocateAfterStage();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        rb.velocity = moveDir * speed;

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }

        moving = (x*x + y*y);
        animator.SetFloat("moving", moving);
    }

    void RelocateAfterBattle()
    {
        transform.position = BattleManager.overworldSpawn;
        BattleManager.overworldSpawn = new Vector3(0,0,0);   
    }

    //void RelocateAfterStage()
    //{
        //transform.position = HubSceneManager.leftHub;
        //HubSceneManager.leftHub = new Vector3(0,0,0);
        //DemoSceneManager.home = false;
    //}
}
