using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QteManager : MonoBehaviour
{
    [SerializeField] private RectTransform _qtePos;
    private SimpleQTE[] _qteList;

    void Awake()
    {
        _qteList = GetComponentsInChildren<SimpleQTE>(true);
    }

    public void CallQTE(string nameQte, Action<bool> action)
    {
        var qteWorldPos = Camera.main.ScreenToWorldPoint(_qtePos.position);
        foreach (var qte in _qteList)
        {
            if(qte.name.Equals(nameQte))
            {
                qte.Init(action, qteWorldPos);
                break;
            }
        }
    }
}
