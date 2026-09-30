using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{

    public static SceneLoader Instance;

    [SerializeField] private GameObject _loadingCanvas;
    [SerializeField] private Image _loadingBar;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }    
        else 
        {
            Instance = this;
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        yield return null;
        _loadingCanvas.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; //bloqueamos el canvio de escena

        float fakeLoadPercentage = 0 ;

        while(!asyncLoad.isDone)
        {
            //Metodo real para comprovar la carga del juego y actualizar la barra segun el valor de la carga
           // _loadingBar.fillAmount = asyncLoad.progress;

           fakeLoadPercentage += 0.01f; 
           Mathf.Clamp01(fakeLoadPercentage);

           _loadingBar.fillAmount = fakeLoadPercentage;

           if(asyncLoad.progress >= 0.9f && fakeLoadPercentage >= 0.99f)
           {
            asyncLoad.allowSceneActivation = true;
           }

           yield return new WaitForSecondsRealtime (0.1f);
           
        }

        Time.timeScale = 1;
        _loadingCanvas.SetActive(false);
    }

}
