using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBallWall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("win"))
        {
            Destroy(gameObject);
        }
       
    }
}
