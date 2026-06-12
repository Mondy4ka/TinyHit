using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _objectPrefab;
    private readonly Queue<T> _pool = new();

    public ObjectPool(T objectPrefab) => _objectPrefab = objectPrefab;

    public void Initialize(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
            InstantiateNewItem();
    }

    private void InstantiateNewItem()
    {
        T newItem = UnityEngine.Object.Instantiate(_objectPrefab);
        newItem.gameObject.SetActive(false);

        _pool.Enqueue(newItem);
    }

    public T GetItem()
    {
        if (_pool.Count <= 0)
            InstantiateNewItem();

        T item = _pool.Dequeue();
        item.gameObject.SetActive(true);

        return item;
    }

    public void ReturnItem(T item)
    {
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);
    }
}