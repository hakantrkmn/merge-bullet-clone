using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class MissileLauncher : MonoBehaviour
{
    public GameStates state;
    public GameObject missilePrefab;
    private float _timer;
    private float _spawnTime;

    private void OnEnable()
    {
        EventManager.ChangeGameState += state => this.state = state;
    }

    private void OnDisable()
    {
        EventManager.ChangeGameState -= state => this.state = state;

    }

    private void Start()
    {
        _spawnTime = Random.Range(3f, 6f);
    }

    private void Update()
    {
        if (state==GameStates.Shooter)
        {
            _timer += Time.deltaTime;
            if (_timer>_spawnTime)
            {
                SpawnMissile();
                _timer = 0;
                _spawnTime = Random.Range(3f, 6f);

            }
        }
       
    }

    void SpawnMissile()
    {
        var missile = Instantiate(missilePrefab, transform);
        missile.transform.position = transform.position;
    }
}
