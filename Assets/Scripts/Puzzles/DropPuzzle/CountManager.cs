using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : MonoBehaviour
{
    public int box1Count = 0;
    public int box2Count = 0;
    public int box3Count = 0;

    public void AddBox1() { box1Count++; }

    public void AddBox2() { box2Count++; }

    public void AddBox3() { box3Count++;}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            box1Count = 0;
            box2Count = 0;
            box3Count = 0;
        }
    }
}
