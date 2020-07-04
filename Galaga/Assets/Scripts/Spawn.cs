using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPref;
    [SerializeField]
    private GameObject _spawnEnemy;
    private bool _stopSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnEnemy());   
    }

    IEnumerator SpawnEnemy()
    {
        while (_stopSpawn)
        {
            Instantiate(_enemyPref, _spawnEnemy.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }

    public void StopSpawn()
    {
        _stopSpawn = false;
    }
}
