using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {
	public enum EDestroyType
	{
		Destroy = 0,
		Inactive
	}

    public float m_lifeTime = 0.0f;
	public EDestroyType m_destroyType = EDestroyType.Destroy;
    float m_tempTime;
    bool m_isAlive = false;
    
	void OnEnable()
	{
		m_isAlive = true;
		m_tempTime = Time.time;
	}

	void DestroyParticle()
	{
		switch(m_destroyType)
		{
		case EDestroyType.Destroy:
			Destroy(gameObject);
			break;
		case EDestroyType.Inactive:
			gameObject.SetActive(false);
			break;
		}
	}

    void Update()
    {
        if (m_isAlive)
        {
            if (m_lifeTime > 0.0f)
            {
                if (Time.time > m_tempTime + m_lifeTime)
                {
                    m_isAlive = false;
					DestroyParticle();
                }
            }
            else
            {
                ParticleSystem[] systems = GetComponentsInChildren<ParticleSystem>();
                bool isPlaying = false;
                foreach (ParticleSystem system in systems)
                {
                    if (system.isPlaying)
                    {
                        isPlaying = true;
                        break;
                    }
                }

                if (!isPlaying)
                {
                    m_isAlive = false;
					DestroyParticle();
                }
            }
        }
    }
}
