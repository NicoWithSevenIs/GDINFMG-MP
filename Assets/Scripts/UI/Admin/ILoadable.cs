using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoadable 
{
    public float loadProgress { get; }
    public bool hasLoaded { get; }
}
