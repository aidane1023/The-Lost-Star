using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject sphere;

    private void SpawnSphere()
    {
        Instantiate(sphere);
    }
}
