using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarPanel : MonoBehaviour
{
    [System.Serializable]
    class StarScore
    {
        public StarPanel star;
        public float scoreNeed;
    }

    [SerializeField] private Slider _scoreBar;

    [SerializeField] private float _scoreEachBlockDestroyed;

    [SerializeField] private List<StarScore> stars = new List<StarScore>();


    // Start is called before the first frame update
    void Start()
    {
        _scoreBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseScoreBar()
    {
        float pointGot = _scoreEachBlockDestroyed;

        _scoreBar.value += pointGot;

        foreach (StarScore star in stars) { 

            if(star.scoreNeed <= _scoreBar.value)
            {
                star.star.AchieveStart();
            }
        }
    }

    public int GetStarsAchieved()
    {
        int starsAchieved = 0;

        foreach (StarScore star in stars)
        {
            if (star.star.isActiveAndEnabled)
            {
                starsAchieved++;
            }
        }


        
        return starsAchieved;

    }

}
