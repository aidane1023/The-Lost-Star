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
}
