using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private SpriteRenderer _sp;
    
    [SerializeField] private Vector2 spawnOffset;
    [SerializeField] private float actualSpawnTimestamp = .75f;
    [SerializeField] private float spawnAnimSpeed = 1;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private EnemyData enemyToSpawn;

    private GameObject childEnemy;

    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _sp.material.SetFloat("_Progress", 0);
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnRoutine() {
        float spawnTimer = 0;
        while (spawnTimer < actualSpawnTimestamp) {
            spawnTimer += Time.deltaTime * spawnAnimSpeed;
            _sp.material.SetFloat("_Progress", spawnTimer);
            yield return null;
        }
        if (enemyToSpawn) {
            childEnemy = Instantiate(enemyPrefab, transform.position + (Vector3)spawnOffset, Quaternion.identity);
            childEnemy.GetComponent<Enemy>().Initialize(enemyToSpawn);
        }
        while (spawnTimer < 1) {
            spawnTimer += Time.deltaTime * spawnAnimSpeed;
            _sp.material.SetFloat("_Progress", spawnTimer);
            yield return null;
        }

        _sp.material.SetFloat("_Progress", 0);

    }

    public void Spawn() {
        StartCoroutine(SpawnRoutine());
    }
}
