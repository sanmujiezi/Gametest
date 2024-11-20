using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager<T> where T: Component
{
    private ObjectPool<T> pool;

    public PoolManager(T prefab, Transform parent = null)
    {
        pool = new ObjectPool<T>(prefab, parent);
    }

    public T Spawn()
    {
        var obj = pool.GetFromPool();
        obj.transform.position = Vector3.zero;
        return obj;
    }

    public void Despawn(T obj)
    {
        pool.ReturnToPool(obj);
    }

    public void Clear()
    {
        pool.ClearPool();
    }
}