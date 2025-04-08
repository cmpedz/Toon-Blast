using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ScreenPos : MonoBehaviour
{
    private static ScreenPos _instance;

    public static ScreenPos Instance
    {
        get { return _instance; }
    }

    public Transform topLeft;

    public Transform bottomRight;

    void Start()
    {
        if( _instance == null)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
