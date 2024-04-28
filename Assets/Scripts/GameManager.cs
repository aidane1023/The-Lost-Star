using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool HasTop { get; set; }
    public bool HasMiddle { get; set; }
    public bool HasBottom { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPickupStatus(int pickUpType)
    {
        switch (pickUpType)
        {
            case 1:
                HasTop = true;
                break;
            case 2:
                HasMiddle = true;
                break;
            case 3:
                HasBottom = true;
                break;
            default:
                Debug.LogWarning("Invalid pickUpType");
                break;
        }
    }
}
