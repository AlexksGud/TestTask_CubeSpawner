using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube cubePrefab;

    [Header("Settings")]
    [SerializeField] private int maxTilesAmount;

    private ObjectPooler<Cube> _cubePool;


    private void OnEnable()
    {
        _cubePool = new ObjectPooler<Cube>(cubePrefab, this.transform);
        _cubePool.CreatePool(maxTilesAmount);
        _cubePool.AutoExpand = true;
    }

    public Cube GetFreeCube()
    {
        var cube = _cubePool.GetFreeObject();
        return cube;

    }
    public List<Cube> GetFreeCubes(int count)
    {
        PoolAmountCheck();

        List<Cube> list = new List<Cube>(count);
        for (int i = 0; i < count; i++)
        {
            list.Add(_cubePool.GetFreeObject());
        }

        return list;


        void PoolAmountCheck()
        {
            if (count > maxTilesAmount)
                _cubePool.CreatePool(count);
        }
    }


}

