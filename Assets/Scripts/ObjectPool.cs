using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly Queue<T> pool = new Queue<T>();
    private readonly T prefab;
    private readonly Transform parent;
    private readonly int initialSize;
    private readonly bool autoExpand;

    /// <summary>
    /// 创建一个对象池
    /// </summary>
    /// <param name="prefab">池中对象的预制体</param>
    /// <param name="parent">对象池中对象的父节点（可选）</param>
    /// <param name="initialSize">池的初始大小</param>
    /// <param name="autoExpand">池是否可以自动扩展</param>
    public ObjectPool(T prefab, Transform parent = null, int initialSize = 10, bool autoExpand = true)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.initialSize = initialSize;
        this.autoExpand = autoExpand;

        // 初始化对象池
        for (int i = 0; i < initialSize; i++)
        {
            var obj = CreateObject();
            ReturnToPool(obj);
        }
    }

    /// <summary>
    /// 从池中获取一个对象
    /// </summary>
    /// <returns>池中的对象</returns>
    public T GetFromPool()
    {
        if (pool.Count == 0)
        {
            if (autoExpand)
            {
                return CreateObject();
            }
            else
            {
                Debug.LogWarning("对象池已空且无法扩展！");
                return null;
            }
        }

        var obj = pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 将对象归还到池中
    /// </summary>
    /// <param name="obj">要归还的对象</param>
    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }

    /// <summary>
    /// 清理池中所有对象，释放内存
    /// </summary>
    public void ClearPool()
    {
        while (pool.Count > 0)
        {
            var obj = pool.Dequeue();
            Object.Destroy(obj.gameObject);
        }
    }

    /// <summary>
    /// 创建一个对象实例
    /// </summary>
    /// <returns>新创建的对象</returns>
    private T CreateObject()
    {
        var obj = Object.Instantiate(prefab, parent);
        obj.gameObject.SetActive(false);
        return obj;
    }
}
