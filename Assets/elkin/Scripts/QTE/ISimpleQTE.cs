using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISimpleQTE
{
    void Init(Action<bool, int> action, Vector3 qtePos, Action<int> onUpdate);
    string Name { get; }
}
