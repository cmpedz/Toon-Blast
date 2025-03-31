using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoosterManager : MonoBehaviour
{
    private static BoosterManager _instance;

    public static BoosterManager Instance
    {
        get { return _instance;}

        private set { _instance = value; }
    }

    private void Awake()
    {
        if (_instance == null) 
        { 
            _instance = this;
        }
        else
        {
            if(_instance != gameObject)
            {
                Destroy(gameObject);
            }
        }

    }

    public void HandleBoosterInMatrix(FieldDrawController _field)
    {
        //matrix to mark assessed block
        bool[,] flag = new bool[_field.NumRows, _field.NumCols];


    }


}
