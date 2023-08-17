using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class Instancer<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        instance = this as T;
    }

    protected virtual void OnApplicationQuit()
    {
        instance = null;
        Destroy(this.gameObject);
    }
}

public abstract class Singleton<T> : Instancer<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
    }
}
