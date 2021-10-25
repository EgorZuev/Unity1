using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _rateChangeSound;

    private bool _isAlarm = false;
    private const string Alarm = "Alarm";

    private void Start()
    {
        _audioSource.volume = 0;
    }

    private void Update()
    {
        if (_isAlarm)
        {
            if (_audioSource.volume < 1)
                _audioSource.volume += Time.deltaTime / _rateChangeSound;
        }
        else if (_audioSource.volume > 0)
        {
            _audioSource.volume -= Time.deltaTime / _rateChangeSound;
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefController>(out ThiefController ganef))
        {
            _audioSource.Play();
            _animator.SetBool(Alarm, true);
            _isAlarm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefController>(out ThiefController ganef))
        {
            _animator.SetBool(Alarm, false);
            _isAlarm = false;
        }
    }
}