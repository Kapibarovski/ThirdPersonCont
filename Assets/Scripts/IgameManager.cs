using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IgameManager
{
    ManagerStatus status { get; }

    void Startup();   
}
