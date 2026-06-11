using System;
using System.Collections.Generic;

[Serializable]
public class KnifePool
{
    private readonly Knife _knifePrefab;

    private Queue<Knife> _pool = new();

    public KnifePool(Knife knifePrefab)
    {
        _knifePrefab = knifePrefab;
    }

    public void Initialize(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
            InstantiateNewKnife();
    }

    private void InstantiateNewKnife()
    {
        Knife newKnife = UnityEngine.Object.Instantiate(_knifePrefab);
        newKnife.gameObject.SetActive(false);

        _pool.Enqueue(newKnife);
    }

    public Knife GetKnife()
    {
        if (_pool.Count <= 0)
            InstantiateNewKnife();

        Knife knife = _pool.Dequeue();
        knife.gameObject.SetActive(true);

        return knife;
    }

    public void ReturnKnife(Knife knife)
    {
        knife.gameObject.SetActive(false);
        _pool.Enqueue(knife);
    }
}