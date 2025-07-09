using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinsCollector _playerCoinCollector;

    [SerializeField] private float _spawnRate = 5f;

    private CoinPusher _coinPusher = new CoinPusher();

    private Coroutine _spawnCoroutine;
    private WaitForSecondsRealtime _spawnDelay;

    private List<Coin> _coins = new List<Coin>();

    private void Awake()
    {
        _spawnDelay = new WaitForSecondsRealtime(_spawnRate);
    }

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(SpawnDelayed());
    }

    private void OnEnable()
    {
        _playerCoinCollector.CoinCollected += DeSpawn;
    }

    private void OnDisable()
    {
        _playerCoinCollector.CoinCollected -= DeSpawn;
        StopSpawning();
    }

    private void StartSpawnDelayed()
    {
        StopSpawning();
        _spawnCoroutine = StartCoroutine(SpawnDelayed());
    }

    private void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnDelayed()
    {
        yield return _spawnDelay;

        Spawn();

        yield break;
    }

    private void Spawn()
    {
        Coin coin = Instantiate(_coinPrefab, transform);

        _coinPusher.Push(coin);

        _coins.Add(coin);
    }

    private void DeSpawn(Coin coin)
    {
        if (_coins.Contains(coin))
        {
            _coins.Remove(coin);
            Destroy(coin.gameObject);

            StartSpawnDelayed();
        }
    }
}
