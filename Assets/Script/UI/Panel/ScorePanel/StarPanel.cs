using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPanel : MonoBehaviour
{
    [SerializeField] private GameObject _achievedSymbol;

    public void AchieveStart()
    {
        _achievedSymbol.SetActive(true);
    }
}
