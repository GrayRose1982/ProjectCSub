/**<summary> Singleton class which is not MonoBehavior</summary> */

using UnityEngine;

public class Singleton<T> where T : new()
{
    protected static T instance;

    /**<summary> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </summary> */
    public static bool Initialized { get; private set; }

    /**<summary> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </summary> */
    public static T Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = new T();
            (instance as Singleton<T>).Initialize();
            return instance;
        }
    }

    protected virtual void Initialize()
    {
        if (Initialized) return;
        Initialized = true;
    }

    /**<summary> Call T.Instance.Preload() at the first script startup to pre init. </summary>*/
    public virtual void Preload() { }
}

/** <summary> Base Singleton class which is MonoBehavior </summary> */
public abstract class SingletonMono<T> : MonoBehaviour where T : Component
{
    protected static T instance;

    /**<summary> If you want to check null, use 'if (T.Initialized)' instead of 'if (T.Instance != null)' </summary> */
    public static bool Initialized
    {
        get { return instance != null; }
    }

    protected virtual void Awake()
    {
        if (instance == null) instance = this as T;
        else if ((this as T) != instance)
        {
            Debug.LogWarningFormat("[MonoSingleton] Class {0} is initialized multiple times", typeof(T).FullName);
            DestroyImmediate(gameObject);
            return;
        }

        OnAwake();
    }

    protected abstract void OnAwake();

    /**<summary> Call T.Instance.Preload() at the first script startup to pre init. </summary>*/
    public virtual void Preload() { }

    protected virtual void OnDestroy()
    {
        instance = null;
    }
}