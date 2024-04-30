using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back2Hub : MonoBehaviour
{

    public ShopManager shopManager;

    void OnTriggerEnter ()
    {
        shopManager.ResetTotalValue();
        SceneManager.LoadScene("HUBBuild");
        HubManager.fromShop = true;
    }
}
