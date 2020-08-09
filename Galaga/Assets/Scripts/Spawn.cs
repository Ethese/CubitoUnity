using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private float _spawnRate;
    [SerializeField]
    private GameObject _enemyPref;
    [SerializeField]
    private GameObject _spawnEnemy;
    [SerializeField]
    private GameObject[] _powerUp;
    [SerializeField]
    private GameObject _spawnPowerUps;
    private bool _stopSpawn = true;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnEnemy()
    {
        while (_stopSpawn)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f), 6.5f, 0);
            GameObject newEnemy = Instantiate(_enemyPref, spawnPoint, Quaternion.identity);
            newEnemy.transform.parent = _spawnEnemy.transform;
            yield return new WaitForSeconds(_spawnRate);
        }
    }

    IEnumerator SpawnPowerUp()
    {
        while (_stopSpawn)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(-8f, 8f), 6.5f, 0);
            GameObject newPowerup = Instantiate(_powerUp[Random.Range(0,3)], spawnPoint, Quaternion.identity);
            newPowerup.transform.parent = _spawnPowerUps.transform;
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void StopSpawn()
    {
        _stopSpawn = false;
    }
}
