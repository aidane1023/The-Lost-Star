using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UILoadScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image loadBar;
    public TextMeshProUGUI loadProgressText;
    // Start is called before the first frame update
    
    public void LoadScene(int sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/ 0.9f);

            loadBar.fillAmount = progressValue;
            loadProgressText.text = $"{Mathf.Round(progressValue/100)}";

            yield return null;
        }
    }
}
