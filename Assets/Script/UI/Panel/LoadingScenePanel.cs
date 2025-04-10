using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScenePanel : MonoBehaviour
{
    [SerializeField] private GameObject _loadingUI;

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(OnLoadingScene(sceneIndex));
    }
    public IEnumerator OnLoadingScene(int sceneIndex)
    {
        var loadingProcess = SceneManager.LoadSceneAsync(sceneIndex);

        _loadingUI.SetActive(true);

        yield return new WaitUntil(() => loadingProcess.isDone);

        _loadingUI.SetActive(false);
    }

}
