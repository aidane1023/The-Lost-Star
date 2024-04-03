using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float groundDist;

    Animator animator;
    float moving;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>(); 
        if(BattleManager.overworldSpawn != new Vector3 (0,0,0)) RelocateAfterBattle();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1f;

        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

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

        moving = Mathf.Abs(x + y);
        animator.SetFloat("moving", moving);
    }

    void RelocateAfterBattle()
    {
        transform.position = BattleManager.overworldSpawn;
        BattleManager.overworldSpawn = new Vector3(0,0,0);   
    }
}
