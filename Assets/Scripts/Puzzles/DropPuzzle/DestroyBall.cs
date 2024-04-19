using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hazard"))
            {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("win"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("blueRed"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("greenBlue"))
        {
            Destroy(gameObject);
        }
    }
}
