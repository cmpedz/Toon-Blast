using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMenuPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> starsAchieved = new List<GameObject>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStarsAchieved(int quantities)
    {
        for (int i = 0; i < quantities; i++)
        {
            starsAchieved[i].gameObject.SetActive(true);
        }
    }
    
}
