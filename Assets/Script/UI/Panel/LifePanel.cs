using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class LifePanel : MonoBehaviour
{
    public static readonly string KEY_LIFE = "Life";
    public static readonly string KEY_LAST_TIME_COUNT = "LastTimeCount";
    private const int MAX_LIFE = 5;

    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI _lifeQuantities;

    [SerializeField] private TextMeshProUGUI _timeWattingDisplay;

    [SerializeField] private float _minuteWatting;

    [SerializeField] private string _lastTimeCount;



    void Start()
    {
        _lifeQuantities.text = PlayerPrefs.GetInt(KEY_LIFE, 5) + "";

        _lastTimeCount = PlayerPrefs.GetString(KEY_LAST_TIME_COUNT, DateTime.Now.Hour + "");
    }

    // Update is called once per frame
    void Update()
    {
        int currentLifes =  int.Parse(_lifeQuantities.text);

        if(currentLifes < MAX_LIFE)
        {
            float waitedSeconds = (float)(DateTime.Now - DateTime.Parse(_lastTimeCount)).TotalSeconds;
            

            if (waitedSeconds / 60f > _minuteWatting) {
                currentLifes++;

                _lifeQuantities.text = currentLifes + "";

                _lastTimeCount = DateTime.Now.ToString();

                if (currentLifes == MAX_LIFE)
                {
                    _timeWattingDisplay.text = "Full";
                }

            }
            else
            {
                TimeSpan timeWatting = TimeSpan.FromMinutes(_minuteWatting - waitedSeconds/ 60f);
                _timeWattingDisplay.text = timeWatting.ToString(@"mm\:ss");
            }
        }
        
    }

    public static bool IsRunningOutOfLife()
    {
        int currentLife = PlayerPrefs.GetInt(KEY_LIFE);
        return currentLife == 0;
    }

  

    public static void DecreaseLife()
    {
        int currentLife = PlayerPrefs.GetInt(KEY_LIFE);

        currentLife -= 1;

        if(currentLife < 0)
        {
            currentLife = 0;
        }

        PlayerPrefs.SetInt(KEY_LIFE, currentLife);

        PlayerPrefs.Save();


    }

    public static void IncreaseLife()
    {
        int currentLife = PlayerPrefs.GetInt(KEY_LIFE);
        currentLife += 1;

        if (currentLife > MAX_LIFE)
        {
            currentLife = MAX_LIFE;
        }

        PlayerPrefs.SetInt(KEY_LIFE, currentLife );

        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(KEY_LIFE, int.Parse(_lifeQuantities.text));
        PlayerPrefs.SetString(KEY_LAST_TIME_COUNT, _lastTimeCount);
        PlayerPrefs.Save();
    }
}
