using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Animator animator;
    private float movingX;
    private float moving;
    private bool flip;
    public Rigidbody rb;
    public SpriteRenderer sr;

    public static bool frozen = false;
    
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

        if (x != 0 && x < 0 && speed > 0)
        {
            flip = true;
            sr.flipX = true;
        }
        else if (x != 0 && x > 0 && speed > 0)
        {
            flip = false;
            sr.flipX = false;
        }

        moving = (x*x + y*y)*speed;
        animator.SetFloat("moving", moving);
        animator.SetBool("flip", flip);
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
