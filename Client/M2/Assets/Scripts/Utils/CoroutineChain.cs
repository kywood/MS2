using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using static CoroutineChain;

class ChainRoutine
{
    int _id;
    IEnumerator _routine;
    MonoBehaviour _monoBehaviour;
    eRoutineState _routineState;

    public IEnumerator Routine { get { return _routine; } }

    public eRoutineState RoutineState 
    { 
        get { return _routineState; }
        set { _routineState = value; }
    }
    public ChainRoutine(int id , IEnumerator routine , MonoBehaviour monoBehaviour)
    {
        _monoBehaviour = monoBehaviour;
        _routine = routine;
        _id = id;
    }
}

public class CoroutineChain
{
    public enum eRoutineState
    {
        None,
        Run,
        End
    }


    MonoBehaviour _monoBehaviour;
    Queue<ChainRoutine> _coroutines = new Queue<ChainRoutine>();
    int _id;

    int Id { get { return _id++; } }


    public CoroutineChain( MonoBehaviour monoBehaviour )
    {
        _monoBehaviour = monoBehaviour;
    }

    public void Add(IEnumerator routine)
    {
        _coroutines.Enqueue(new ChainRoutine( Id , routine, _monoBehaviour));
    }

    public void Run()
    {
        ChainRoutine currentChainRoutine = null;

        while (_coroutines.Count != 0)
        {
            if(currentChainRoutine == null)
                currentChainRoutine = _coroutines.Dequeue();

            _monoBehaviour.StartCoroutine(currentChainRoutine.Routine);
        }
    }


    

}
