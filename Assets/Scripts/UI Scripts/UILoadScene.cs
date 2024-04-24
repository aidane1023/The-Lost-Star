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
    
    public void LoadScene(string sceneID)
    {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(string sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        operation.allowSceneActivation = false;
        
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/ 0.9f);

            loadBar.fillAmount = progressValue;
            loadProgressText.text = $"{progressValue * 100}%";
            Debug.Log(progressValue * 100);

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
