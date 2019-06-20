using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement
{
    public Movement() {}

    public abstract void Dispatch();
    public abstract void OnFinished();
}
