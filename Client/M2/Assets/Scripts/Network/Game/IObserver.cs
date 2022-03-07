using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISubJect;

public interface IObserver 
{
    void ObserverUpdate(E_UPDAET_TYPE updateType);
}
