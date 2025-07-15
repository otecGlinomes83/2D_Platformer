using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinCollector _playerCoinCollector;

    [SerializeField] private float _spawnRate = 5f;

    private Coroutine _spawnCoroutine;
    private WaitForSecondsRealtime _spawnDelay;

    private CoinPusher _coinPusher = new CoinPusher();

    private Coin _coin;

    private void Awake()
    {
        _spawnDelay = new WaitForSecondsRealtime(_spawnRate);
    }

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(RespawnDelayed());
    }

    private void OnEnable()
    {
        _playerCoinCollector.CoinCollected += DeactivateCoin;
    }

    private void OnDisable()
    {
        _playerCoinCollector.CoinCollected -= DeactivateCoin;
        StopSpawning();
    }

    private void StartRespawnDelayed()
    {
        StopSpawning();
        _spawnCoroutine = StartCoroutine(RespawnDelayed());
    }

    private void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    private IEnumerator RespawnDelayed()
    {
        yield return _spawnDelay;

        ActivateCoin();

        yield break;
    }

    private void ActivateCoin()
    {
        if (_coin == null)
        {
            _coin = Instantiate(_coinPrefab, transform);
        }

        _coin.gameObject.SetActive(true);
        _coin.transform.position = transform.position;
        _coinPusher.Push(_coin);
    }

    private void DeactivateCoin(Coin coin)
    {
        if (_coin == coin)
        {
            _coin.gameObject.SetActive(false);
            StartRespawnDelayed();
        }
    }
}
