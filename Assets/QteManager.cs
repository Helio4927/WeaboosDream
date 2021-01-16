using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QteManager : MonoBehaviour
{
    private SimpleQTE[] _qteList;

    void Awake()
    {
        _qteList = GetComponentsInChildren<SimpleQTE>(true);
    }

    public void CallQTE(string nameQte, Action<bool> action)
    {
        foreach(var qte in _qteList)
        {
            if(qte.name.Equals(nameQte))
            {
                qte.Init(action);
                break;
            }
        }
    }
}
