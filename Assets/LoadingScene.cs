using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public TextMeshProUGUI TxtCarga; 

    public void LoadScene(int SceneId)
    {
        StartCoroutine(LoadSceneSync(SceneId));
    }
    IEnumerator LoadSceneSync(int SceneId)
    {        
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneId);
        LoadingScreen.SetActive(true);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);            

            if (operation.progress >= 0.5f)
            {                
                yield return new WaitForSeconds(0.5f);
            } 

            if (operation.progress >= 0.9f)
            {
                LoadingBarFill.fillAmount = progressValue;
                TxtCarga.text = $"Done...";
                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;                
            }            
            yield return null;
        }
    }


}
