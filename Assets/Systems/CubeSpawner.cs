using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _cubePooler;
    [SerializeField] private Transform spawnPosition;

    [Space(2f)]
    [Header("Settings")]

    [SerializeField,Range(5f,50f)] private float distance;
    [SerializeField,Min(1f)] private float _movementSpeed;
    [SerializeField, Range(0.1f, 2f)] private float _cubeSpawnDelay = 0.5f;

    private float duration;//Длительность для Твина
    private float endValue;//Конечная точка для Твина


    private float savedSpeed;
    private float savedDistance;
    private void Start()
    {
        StartCoroutine(InfiniteSpawn());
        StartCoroutine(CheckIfFieldsChanged());
    }
    private IEnumerator InfiniteSpawn()
    {
        CalculateDuration();
        while (true)
        {
            yield return new WaitForSeconds(_cubeSpawnDelay);

            var cube = _cubePooler.GetFreeCube();
            cube.transform
                .DOMoveZ(endValue, duration).SetEase(Ease.Flash)
                .OnComplete(() => OnCubeFinish(cube));
        }

    }

    /// <summary>
    /// Проверяем изменились ли значения скорости или расстояния 
    /// </summary>
    /// <returns></returns>
     private IEnumerator CheckIfFieldsChanged()
     {
       
        if ((savedDistance != distance) ||
           (savedSpeed != _movementSpeed))
        {
           
            CalculateDuration();
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(CheckIfFieldsChanged());

     }

    
    private void CalculateDuration()
    {
        savedSpeed = _movementSpeed;
        savedDistance = distance;

        endValue = spawnPosition.position.z + distance;
        duration = distance / _movementSpeed;
    }
    
    private void OnCubeFinish(Cube cube)
    {
        cube.gameObject.SetActive(false);
        cube.transform.position = spawnPosition.position;
    }

}
