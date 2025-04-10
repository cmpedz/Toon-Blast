using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    

    [SerializeField] private TextMeshProUGUI _levelButtonDisplay;

    [SerializeField] private TextMeshProUGUI _levelDisplay;

    [SerializeField] private LoadingScenePanel _loadingScenePanel;

    [SerializeField] private GameObject _lackLifeNotification;

    void Start()
    {
        _levelDisplay.text = "Level " + PlayerPrefs.GetInt(LevelSO.KEY_LEVEL, 1);

        _levelButtonDisplay.text = "Level " + PlayerPrefs.GetInt(LevelSO.KEY_LEVEL, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        if (LifePanel.IsRunningOutOfLife())
        {
            _lackLifeNotification.SetActive(true);
        }
        else
        {
            _loadingScenePanel.ChangeScene(1);
        }
    }
    

}
