using UnityEngine;
using System.Collections;

public class DontDestroy<T> : MonoBehaviour where T : DontDestroy<T>
{
	static public T Instance { get; private set; }
	
	void Awake()
	{
        if (Instance == null)
		{
            Instance = (T)this;
			DontDestroyOnLoad(gameObject);
			OnAwake();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	
	void Start()
	{
        if (Instance == (T)this)
		{
			OnStart();
		}
	}

    void Update()
    {
		if (Instance == (T)this)
		{
			OnUpdate();
		}
	}

    virtual protected void OnAwake()
	{
	}
	
	virtual protected void OnStart()
	{
	}

	virtual protected void OnUpdate()
	{
	}

}
