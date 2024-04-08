using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject sphere;
  

    public void SpawnSphere()
    {
        Instantiate(sphere);
    }
}
