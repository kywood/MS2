using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ISubJect;

public abstract class AbSubJect : ISubJect
{
    

    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver _observer)
    {
        this.observers.Add(_observer);
    }

    public void RemoveObserver(IObserver _observer)
    {
        this.observers.Remove(_observer);
    }

    public void NotifyObservers(E_UPDAET_TYPE updateType = E_UPDAET_TYPE.NONE)
    {
        foreach (IObserver o in observers)
        {
            o.ObserverUpdate(updateType);
        }
    }

}
