using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    private LayerMask terrainLayer;
    private float groundDist;

    void Start()
    {
        terrainLayer =  LayerMask.GetMask("Terrain");
        groundDist = 0.1f;
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1f;
        castPos.z += .35f;

        Debug.DrawRay(castPos, -(new Vector3(0,1,0)), Color.green);

        if (Physics.Raycast(castPos, -(new Vector3(0, 1, 0)), out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }
    }
}
