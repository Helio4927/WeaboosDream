using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISimpleQTE
{
    void Init(Action<bool> action, Vector3 qtePos);
    string Name { get; }
}
