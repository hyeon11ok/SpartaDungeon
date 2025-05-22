using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();
                if(instance == null)
                {
                    GameObject obj = new GameObject();

                    instance = obj.AddComponent<T>();

                    // get으로 생성 됐을 때 호출
                    if(instance is Singleton<T> singleton)
                    {
                        singleton.Initialize();
                    }
                }
            }
            return instance;
        }
    }

    public static bool IsAwakeDone { get; private set; } = false;

    protected void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }

        IsAwakeDone = true;
    }

    private void OnDisable()
    {
        IsAwakeDone = false;
    }
    
    protected abstract void Initialize();
}
