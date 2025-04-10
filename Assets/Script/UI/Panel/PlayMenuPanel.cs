using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenuPanel : MonoBehaviour
{

   
    public void OnPlayMenuAppear()
    {
        gameObject.SetActive(true);
        transform.DOShakeScale(1f, 0.1f);
        
    }

    public void OnPlayMenuDisappear()
    {
        gameObject.SetActive(false);
        

    }


}
