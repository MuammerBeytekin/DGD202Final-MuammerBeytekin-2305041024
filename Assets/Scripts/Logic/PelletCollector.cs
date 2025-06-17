using System;
using UnityEngine;
using TMPro;

public class PelletCollector : MonoBehaviour
{
    public static PelletCollector Instance;
    private PelletSpawner _pelletSpawner;
    private GameController _gameController;
    private AudioSource _audioSource;
    [SerializeField] private TextMeshProUGUI _counter;

    private int _numberToCollect;
    private int _numberCollected;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // Null kontrolleri ile component'leri al
        _gameController = GetComponent<GameController>();
        if (_gameController == null)
            Debug.LogError("GameController component bulunamad�!");

        _pelletSpawner = GetComponent<PelletSpawner>();
        if (_pelletSpawner == null)
            Debug.LogError("PelletSpawner component bulunamad�!");

        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
            Debug.LogError("AudioSource component bulunamad�!");
    }

    private void Start()
    {
        // Null kontrol� ekledik
        if (_pelletSpawner != null)
        {
            _numberToCollect = _pelletSpawner.NumberToSpawn;
        }
        else
        {
            Debug.LogError("PelletSpawner null, NumberToSpawn al�namad�!");
            _numberToCollect = 0;
        }
    }

    public void ResetCounter()
    {
        _numberCollected = 0;

        // Counter null kontrol�
        if (_counter != null)
        {
            _counter.text = "0";
        }
        else
        {
            Debug.LogError("Counter UI elementi null!");
        }
    }

    public void PelletCollected()
    {
        // AudioSource null kontrol�
        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.PlayOneShot(_audioSource.clip);
        }
        else
        {
            Debug.LogWarning("AudioSource veya AudioClip null!");
        }

        _numberCollected++;

        // Counter null kontrol�
        if (_counter != null)
        {
            _counter.text = _numberCollected.ToString();
        }
        else
        {
            Debug.LogError("Counter UI elementi null!");
        }

        // GameController null kontrol�
        if (_numberCollected >= _numberToCollect)
        {
            if (_gameController != null)
            {
                _gameController.EndGame();
            }
            else
            {
                Debug.LogError("GameController null, EndGame �a�r�lamad�!");
            }
        }
    }
}