using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    private CountManager manager;
    public GameObject managerObject;

    private void Start()
    {
        manager = managerObject.GetComponent<CountManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ball"))
        {

            if (gameObject.CompareTag("box1"))
                manager.AddBox1();
            else if (gameObject.CompareTag("box2"))
                manager.AddBox2();
            else if (gameObject.CompareTag("box3"))
                manager.AddBox3();
        }
    }
}
