using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubJect
{
    public enum E_UPDAET_TYPE
    {
        NONE,
        PLAYER_UPDATE,
        ROOM_INFO_UPSERT,
        GAME_START
    }

    void RegisterObserver(IObserver _observer);
    void RemoveObserver(IObserver _observer);
    void NotifyObservers(E_UPDAET_TYPE updateType);
}
