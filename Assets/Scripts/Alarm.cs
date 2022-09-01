using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumeStep;

    private Coroutine _currentCoroutine;
    private float _currentVolume = 0f;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarmSound.Play();

            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
                _currentCoroutine = StartCoroutine(SoundVolumeIncrease());
            }
            else
            {
                _currentCoroutine = StartCoroutine(SoundVolumeIncrease());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            StopCoroutine(_currentCoroutine);
            StartCoroutine(SoundVolumeDecrease());
        }
    }

    private IEnumerator SoundVolumeIncrease()
    {
        var _waitingTime = new WaitForSeconds(1f);

        while(_currentVolume <= _maxVolume)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _maxVolume, _volumeStep);
            _currentVolume = _alarmSound.volume;
            yield return _waitingTime;
        }
    }

    private IEnumerator SoundVolumeDecrease()
    {
        var _waitingTime = new WaitForSeconds(1f);

        while(_currentVolume > _minVolume)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, _minVolume, _volumeStep);
            _currentVolume = _alarmSound.volume;
            yield return _waitingTime;
        }

        StopCoroutine(_currentCoroutine);
    }
}
